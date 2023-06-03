using FurryFeast.Utility;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FurryFeast.Controllers
{
	public class PayMoneyController : Controller
	{
		PayMoneyInfo PayMoneyInfo = new PayMoneyInfo
		{
			MerchantID = "MS149054042",
			HashKey = "YhMBZHNsbMbVVJtjhbV87gzFLApRblCV",
			HashIV = "C0jt6somi3UVt0eP",
			ReturnURL = "http://yourWebsitUrl/Bank/SpgatewayReturn",
			NotifyURL = "http://yourWebsitUrl/Bank/SpgatewayNotify",
			CustomerURL = "http://yourWebsitUrl/Bank/SpgatewayCustomer",
			AuthUrl = "https://ccore.newebpay.com/MPG/mpg_gateway",
			CloseUrl = "https://core.newebpay.com/API/CreditCard/Close"
		};

		//public IActionResult Index()
		//{
		//	return View();
		//}

		[HttpPost]
		public async Task SpgatewayPayBillAsync(string ordernumber, int amount, string payType)
		{
			string version = "1.5";

			// 目前時間轉換 +08:00, 防止傳入時間或Server時間時區不同造成錯誤
			DateTimeOffset taipeiStandardTimeOffset = DateTimeOffset.Now.ToOffset(new TimeSpan(8, 0, 0));

			PayMoneyTradeInfo tradeInfo = new PayMoneyTradeInfo()
			{
				// * 商店代號
				MerchantID = PayMoneyInfo.MerchantID,
				// * 回傳格式
				RespondType = "String",
				// * TimeStamp
				TimeStamp = taipeiStandardTimeOffset.ToUnixTimeSeconds().ToString(),
				// * 串接程式版本
				Version = version,
				// * 商店訂單編號
				//MerchantOrderNo = $"T{DateTime.Now.ToString("yyyyMMddHHmm")}",
				MerchantOrderNo = ordernumber,
				// * 訂單金額
				Amt = amount,
				// * 商品資訊
				ItemDesc = "商品資訊(自行修改)",
				// 繳費有效期限(適用於非即時交易)
				ExpireDate = null,
				// 支付完成 返回商店網址
				ReturnURL = PayMoneyInfo.ReturnURL,
				// 支付通知網址
				NotifyURL = PayMoneyInfo.NotifyURL,
				// 商店取號網址
				CustomerURL = PayMoneyInfo.CustomerURL,
				// 支付取消 返回商店網址
				ClientBackURL = null,
				// * 付款人電子信箱
				Email = "thm101777@gmail.com",
				// 付款人電子信箱 是否開放修改(1=可修改 0=不可修改)
				EmailModify = 0,
				// 商店備註
				OrderComment = "????",
				// 信用卡 一次付清啟用(1=啟用、0或者未有此參數=不啟用)
				CREDIT = 1,
				// WEBATM啟用(1=啟用、0或者未有此參數，即代表不開啟)
				WEBATM = 1,
				// ATM 轉帳啟用(1=啟用、0或者未有此參數，即代表不開啟)
				VACC = 0,
				// 超商代碼繳費啟用(1=啟用、0或者未有此參數，即代表不開啟)(當該筆訂單金額小於 30 元或超過 2 萬元時，即使此參數設定為啟用，MPG 付款頁面仍不會顯示此支付方式選項。)
				CVS = null,
				// 超商條碼繳費啟用(1=啟用、0或者未有此參數，即代表不開啟)(當該筆訂單金額小於 20 元或超過 4 萬元時，即使此參數設定為啟用，MPG 付款頁面仍不會顯示此支付方式選項。)
				BARCODE = null,

			};

			if (string.Equals(payType, "CREDIT"))
			{
				tradeInfo.CREDIT = 1;
			}
			else if (string.Equals(payType, "WEBATM"))
			{
				tradeInfo.WEBATM = 1;
			}
			else if (string.Equals(payType, "VACC"))
			{
				// 設定繳費截止日期
				tradeInfo.ExpireDate = taipeiStandardTimeOffset.AddDays(1).ToString("yyyyMMdd");
				tradeInfo.VACC = 1;
			}
			else if (string.Equals(payType, "CVS"))
			{
				// 設定繳費截止日期
				tradeInfo.ExpireDate = taipeiStandardTimeOffset.AddDays(1).ToString("yyyyMMdd");
				tradeInfo.CVS = 1;
			}
			else if (string.Equals(payType, "BARCODE"))
			{
				// 設定繳費截止日期
				tradeInfo.ExpireDate = taipeiStandardTimeOffset.AddDays(1).ToString("yyyyMMdd");
				tradeInfo.BARCODE = 1;
			}

			Atom<string> result = new Atom<string>()
			{
				IsSuccess = true
			};

			var inputParameter = new PostParameter
			{
				MerchantID = PayMoneyInfo.MerchantID,
				Version = version
			};

			// 將model 轉換為List<KeyValuePair<string, string>>, null值不轉
			List<KeyValuePair<string, string>> tradeData = TranTypeUtility.ModelToKeyValuePairList<PayMoneyTradeInfo>(tradeInfo);
			// 將List<KeyValuePair<string, string>> 轉換為 key1=Value1&key2=Value2&key3=Value3...
			var tradeQueryParameter = string.Join("&", tradeData.Select(x => $"{x.Key}={x.Value}"));
			// AES 加密
			inputParameter.TradeInfo = CryptUtility.EncryptAESHex(tradeQueryParameter, PayMoneyInfo.HashKey, PayMoneyInfo.HashIV);
			// SHA256 加密
			inputParameter.TradeSha = CryptUtility.EncryptSHA256($"HashKey={PayMoneyInfo.HashKey}&{inputParameter.TradeInfo}&HashIV={PayMoneyInfo.HashIV}");

			// 將model 轉換為List<KeyValuePair<string, string>>, null值不轉
			List<KeyValuePair<string, string>> postData = TranTypeUtility.ModelToKeyValuePairList<PostParameter>(inputParameter);

			Response.Clear();

			StringBuilder s = new StringBuilder();
			s.Append("<html>");
			s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
			s.AppendFormat("<form name='form' action='{0}' method='post'>", PayMoneyInfo.AuthUrl);
			foreach (KeyValuePair<string, string> item in postData)
			{
				s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", item.Key, item.Value);
			}

			s.Append("</form></body></html>");
			byte[] bytes = Encoding.ASCII.GetBytes(s.ToString());
			await HttpContext.Response.Body.WriteAsync(bytes);
		}
	}
}

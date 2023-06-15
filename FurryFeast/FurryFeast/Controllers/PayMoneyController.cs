using FurryFeast.Models;
using FurryFeast.Utility;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace FurryFeast.Controllers
{
	public class PayMoneyController : Controller
	{
        private readonly db_a989fb_furryfeastContext _context;

        public PayMoneyController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        PayMoneyInfo PayMoneyInfo = new PayMoneyInfo
		{
			MerchantID = "MS149054042",
			HashKey = "YhMBZHNsbMbVVJtjhbV87gzFLApRblCV",
			HashIV = "C0jt6somi3UVt0eP",
			//ReturnURL = "https://localhost:7110/PayMoney/FinishPay",//local端
			ReturnURL = "https://furryfeastweb.azurewebsites.net/PayMoney/FinishPay",//線上部屬URL

			NotifyURL = "http://yourWebsitUrl/Bank/SpgatewayNotify",
			CustomerURL = "http://yourWebsitUrl/Bank/SpgatewayCustomer",
			AuthUrl = "https://ccore.newebpay.com/MPG/mpg_gateway",
			CloseUrl = "https://core.newebpay.com/API/CreditCard/Close"
		};

		PayMoneyInfo PetClassPayMoneyInfo = new PayMoneyInfo
		 {
			//ReturnURL = "https://localhost:7110/PayMoney/PetClassFinishPay",//local端
			ReturnURL = "https://furryfeastweb.azurewebsites.net/PayMoney/PetClassFinishPay",
		};

		[HttpPost]
		public async Task SpgatewayPayBillAsync(PayBill payBill)
		{
			string version = "1.5";
			string PayReturnURL;

			
			DateTimeOffset taipeiStandardTimeOffset = DateTimeOffset.Now.ToOffset(new TimeSpan(8, 0, 0));


			if (payBill.area == "petClass")
			{
				PayReturnURL = PetClassPayMoneyInfo.ReturnURL;
			}
			else
			{
				PayReturnURL = PayMoneyInfo.ReturnURL;
			}

			PayMoneyTradeInfo tradeInfo = new PayMoneyTradeInfo()
			{
				
				MerchantID = PayMoneyInfo.MerchantID,
				
				RespondType = "String",
				
				TimeStamp = taipeiStandardTimeOffset.ToUnixTimeSeconds().ToString(),
				
				Version = version,
				
				MerchantOrderNo = payBill.ordernumber,
				
				Amt = payBill.amount,
				
				ItemDesc = "商品資訊(自行修改)",
				
				ExpireDate = null,
				
				ReturnURL = PayReturnURL,

				NotifyURL = PayMoneyInfo.NotifyURL,

				CustomerURL = PayMoneyInfo.CustomerURL,

				ClientBackURL = null,

				Email = payBill.email,

				EmailModify = 1,

				OrderComment = "若有任何問題，請聯繫FurryFeast客服:0800-000-000",

				CREDIT = 1,

				WEBATM = 1,

				VACC = 0,

				CVS = null,
		
				BARCODE = null,

			};

			if (string.Equals(payBill.payType, "CREDIT"))
			{
				tradeInfo.CREDIT = 1;
			}
			else if (string.Equals(payBill.payType, "WEBATM"))
			{
				tradeInfo.WEBATM = 1;
			}
			else if (string.Equals(payBill.payType, "VACC"))
			{

				tradeInfo.ExpireDate = taipeiStandardTimeOffset.AddDays(1).ToString("yyyyMMdd");
				tradeInfo.VACC = 1;
			}
			else if (string.Equals(payBill.payType, "CVS"))
			{
	
				tradeInfo.ExpireDate = taipeiStandardTimeOffset.AddDays(1).ToString("yyyyMMdd");
				tradeInfo.CVS = 1;
			}
			else if (string.Equals(payBill.payType, "BARCODE"))
			{
			
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

		public async Task<IActionResult> FinishPay()
		{
			StringBuilder receive = new StringBuilder();
			foreach (var item in Request.Form)
			{
				receive.AppendLine(item.Key + "=" + item.Value + "<br>");
			}
			ViewData["ReceiveObj"] = receive.ToString();

			string HashKey = PayMoneyInfo.HashKey;
			string HashIV = PayMoneyInfo.HashIV;

			string TradeInfoDecrypt = CryptUtility.DecryptAESHex(Request.Form["TradeInfo"], HashKey, HashIV);
			NameValueCollection decryptTradeCollection = HttpUtility.ParseQueryString(TradeInfoDecrypt);
			receive.Length = 0;
			foreach (String key in decryptTradeCollection.AllKeys)
			{
				receive.AppendLine(key + "=" + decryptTradeCollection[key] + "<br>");
			}
			ViewData["TradeInfo"] = receive.ToString();
			//ViewData["Status"]=SUCCESS.ToString();
			string Status = decryptTradeCollection[decryptTradeCollection.Keys[0]];
			int OrderID= Convert.ToInt32(decryptTradeCollection[decryptTradeCollection.Keys[5]]);
			
			
			await PutProductOrderState(Status, OrderID);
            ViewBag.Status = Status;
			return View();
		}

		public async Task<IActionResult> PetClassFinishPay()
		{
			StringBuilder receive = new StringBuilder();
			foreach (var item in Request.Form)
			{
				receive.AppendLine(item.Key + "=" + item.Value + "<br>");
			}
			ViewData["ReceiveObj"] = receive.ToString();

			string HashKey = PayMoneyInfo.HashKey;
			string HashIV = PayMoneyInfo.HashIV;

			string TradeInfoDecrypt = CryptUtility.DecryptAESHex(Request.Form["TradeInfo"], HashKey, HashIV);
			NameValueCollection decryptTradeCollection = HttpUtility.ParseQueryString(TradeInfoDecrypt);
			receive.Length = 0;
			foreach (String key in decryptTradeCollection.AllKeys)
			{
				receive.AppendLine(key + "=" + decryptTradeCollection[key] + "<br>");
			}
			ViewData["TradeInfo"] = receive.ToString();
			//ViewData["Status"]=SUCCESS.ToString();
			string status = decryptTradeCollection[decryptTradeCollection.Keys[0]];
			int classReserveId = Convert.ToInt32(decryptTradeCollection[decryptTradeCollection.Keys[5]]);


			await PutClassReserveState(status, classReserveId);
			ViewBag.Status = status;
			return View();
		}

		[HttpPut]
        public async Task<string> PutProductOrderState(string state,int orderId)
        {


            var order = await _context.Orders.FindAsync(orderId);

            if (state == "SUCCESS")
            {
                order.OrderStatus=1;
            }
			else
			{
				return "付款失敗";
            }
            

            _context.Orders.Update(order);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (order.OrderId!= orderId)
                {
                    
                        return "付款狀態更新失敗";
                    
                }

                else
                {
                    throw;
                }
            }

            return "付款狀態更新成功";
        }

		[HttpPut]
		public async Task<string> PutClassReserveState(string state, int classReserveId)
		{


			var petClass = await _context.ClassReservetions.FindAsync(classReserveId);

			if (state == "SUCCESS")
			{
				petClass.ClassReservetionState = 1;
			}
			else
			{
				return "付款失敗";
			}


			_context.ClassReservetions.Update(petClass);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (petClass.ClassReservetionId != classReserveId)
				{

					return "付款狀態更新失敗";

				}

				else
				{
					throw;
				}
			}

			return "付款狀態更新成功";
		}
	}
}

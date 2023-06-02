namespace FurryFeast.ViewModels
{
	public class PayMoneyInfo
	{
		//商店代號
		public string MerchantID { get; set; }
		//AES 加密/SHA256 加密 Key
		public string HashKey { get; set; }
		//AES 加密/SHA256 加密 IV
		public string HashIV { get; set; }
		//支付完成後返回商店的網址
		public string ReturnURL { get; set; }
		//支付通知網址
		public string NotifyURL { get; set; }
		//商店取號網址
		public string CustomerURL { get; set; }
		//授權網址
		public string AuthUrl { get; set; }
		//請退款網址(取消)
		public string CloseUrl { get; set; }
	}
}

namespace DecodificaISO.API.Models
{
	public class Message
	{
		public string? MessageId { get; set; }
		public string? Bitmap { get; set; }
		public string? ProcessingCode { get; set; }
		public string? TransactionAmount { get; set; }		
		public string? AuditNumber { get; set; }
		public string? TransactionTime { get; set; }
		public string? SettlementDate { get; set; }
		public string? AcquiringCountry { get; set; }
		public string? PANCountryCode { get; set; }
		public string? PANSequenceNumber { get; set; }
		public string? FunctionCode { get; set; }
		public string? POSCaptureCode { get; set; }
		public string? AcquiringIndetificationCode { get; set; }
		public string? Track2 { get; set; }
	}
}

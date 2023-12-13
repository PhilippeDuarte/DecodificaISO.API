namespace DecodificaISO.API.Models
{
	public class SimpleMessage : Message
	{
		public string? TransmissionDateAndTime { get; set; }
		public string? FeeAmount { get; set; }
	}
}

using DecodificaISO.API.Models;

namespace DecodificaISO.API.Services.Interfaces
{
	public interface IDecoder
	{
		SimpleMessage DecodeSimpleMessage(string code);
		BCDMessage DecodeBCDMessage(string code);
		IEnumerable<string> DecodeByBitmap(string code);
	}
}

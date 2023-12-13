using DecodificaISO.API.Services.Interfaces;
using System.Text;

namespace DecodificaISO.API.Services.Readers
{
	public class Reader : IReader
	{		
		public string ReadFile(string path)
		{
			try
			{
				//Le o arquivo .dat e envia o código em formato string para o controller
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
				string readText = File.ReadAllText(path, Encoding.Latin1);
				string parsedText = ParseChar(readText);
				return parsedText;
			}
			catch(Exception ex)
			{
				throw new InvalidCastException(ex.Message);
			}
		}		
		public string ParseChar(string readText) 
		{
			//traduz os caracteres especiais que estão em Unicode
			string parsedText = readText.Replace("\0", " ");
			parsedText = readText.Replace("\u0080", "€");

			return parsedText;
		}

	}
}

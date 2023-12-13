using DecodificaISO.API.Models;
using DecodificaISO.API.Services.Interfaces;
using System.Text;

namespace DecodificaISO.API.Services.Decoders
{
	public class Decoder : IDecoder
	{
		//dicionário traduzindo caracteres Hexadecimal para Binário
		private static readonly Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
			{ '0', "0000" },
			{ '1', "0001" },
			{ '2', "0010" },
			{ '3', "0011" },
			{ '4', "0100" },
			{ '5', "0101" },
			{ '6', "0110" },
			{ '7', "0111" },
			{ '8', "1000" },
			{ '9', "1001" },
			{ 'a', "1010" },
			{ 'b', "1011" },
			{ 'c', "1100" },
			{ 'd', "1101" },
			{ 'e', "1110" },
			{ 'f', "1111" }
		};

		private readonly List<int> typeRuleString =  new List<int>();
		public Decoder()
		{
			//cria uma lista de inteiros com o tamanho dos campos definidos pela iso 8583-1987. Foi montado até o bitmap 40 devido a falta de conhecimento dos itens superiores. 
			typeRuleString.AddRange(new[]{64, 19, 6, 12, 12, 12, 10, 8, 8, 8, 6, 6, 4, 4, 4, 4, 4, 4,
									3, 3, 3, 3, 3, 3, 2, 2, 1, 8, 8, 8, 8, 1, 1, 28, 37, 104, 12, 6, 2, 
									3, 8, 15, 40 });

		}
		//monta o Json do arquivo message_with_hex_bcd.dat
		public BCDMessage DecodeBCDMessage(string code)
		{
			try
			{
				BCDMessage message = new BCDMessage();

				message.MessageId = code.Substring(0, 4);
				message.Bitmap = HexStringToBinary(code.Substring(4, 16));
				message.ProcessingCode = code.Substring(12, 6);
				message.TransactionAmount = code.Substring(18, 12);
				message.SettlementAmount = code.Substring(30, 12);
				message.AuditNumber = code.Substring(42, 6);
				message.TransactionTime = code.Substring(48, 6);
				message.SettlementDate = code.Substring(54, 4);
				message.AcquiringCountry = code.Substring(58, 3);
				message.PANCountryCode = code.Substring(61, 3);
				message.PANSequenceNumber = code.Substring(64, 2);
				message.FunctionCode = code.Substring(67, 3);
				message.POSCaptureCode = code.Substring(70, 2);
				message.AcquiringIndetificationCode = code.Substring(71, 1) == "0" ? "" : code.Substring(71, 1);
				message.Track2 = int.Parse(code.Substring(72, 4)) == 0 ? "" : code.Substring(72, 4);

				return message;
			}
			catch (Exception ex)
			{
				throw new InvalidCastException(ex.Message);
			}
		}
		//PROTÓTIPO - Monta o Json baseado no bitmap. Faz uso da lista typeRuleString.
		public IEnumerable<string> DecodeByBitmap(string code)
		{
			//try
			//{
				List<string> result = new List<string>();
				string binaryBitmap = HexStringToBinary(code.Substring(4, 16));
				int counter = 0;
				int position = 12;
				result.Add(code.Substring(0, 4));
				result.Add(binaryBitmap);
				foreach (char item in binaryBitmap)
				{
					if (item == '1')
					{
						result.Add(code.Substring(position, typeRuleString[counter]));
					position += typeRuleString[counter];
					}
					counter++;
					if (counter > 40) break;
				}
				return result;
			//}
			//catch (Exception ex)
			//{
			//	throw new InvalidCastException(ex.Message);
			//}
		}
		//monta o Json do arquivo financial_transaction_message.dat
		public SimpleMessage DecodeSimpleMessage(string code)
		{
			try
			{
				SimpleMessage message = new SimpleMessage();

				message.MessageId = code.Substring(0, 4);
				message.Bitmap = HexStringToBinary(code.Substring(4, 16));
				message.ProcessingCode = code.Substring(12, 6);
				message.TransactionAmount = code.Substring(18, 12);
				message.TransmissionDateAndTime = code.Substring(30, 14);
				message.FeeAmount = code.Substring(44, 12);
				message.AuditNumber = code.Substring(56, 6);
				message.TransactionTime = code.Substring(62, 6);
				message.SettlementDate = code.Substring(68, 4);
				message.AcquiringCountry = code.Substring(72, 3);
				message.PANCountryCode = code.Substring(75, 3);
				message.PANSequenceNumber = code.Substring(78, 3);
				message.FunctionCode = code.Substring(81, 3);
				message.POSCaptureCode = code.Substring(83, 2);
				message.AcquiringIndetificationCode = code.Substring(87, 1);
				message.Track2 = code.Substring(91, 35);

				return message;
			}
			catch(Exception ex)
			{
				throw new InvalidCastException(ex.Message);
			}
		}
		//traduz os valores do bitmap de Hexadecimal para binário
		private string HexStringToBinary(string hex)
		{
			StringBuilder result = new StringBuilder();
			foreach (char c in hex)
			{				
				result.Append(hexCharacterToBinary[char.ToLower(c)]);
			}
			return result.ToString();
		}
	}
}

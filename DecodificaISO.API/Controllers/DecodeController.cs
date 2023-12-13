using DecodificaISO.API.Models;
using DecodificaISO.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IO;
using DecodificaISO.API.Services.Readers;

namespace DecodificaISO.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DecodeController : ControllerBase
	{
		private readonly IDecoder _decoder;
		private readonly IReader _reader;
		public DecodeController(IDecoder decoder, IReader reader)
		{
			_decoder= decoder;
			_reader= reader;
		}
		/// <summary>
		/// Decodifica o arquivo financial_transaction_message.dat baseando-se no arquvo financial_transaction_message.txt
		/// </summary>
		/// <remarks>
		/// Decodifica o arquivo financial_transaction_message.dat
		/// </remarks>
		/// <returns>
		///	200 - com o Json dos parâmetros decodificados 
		/// 400 - Arquivo vazio
		/// </returns>
		[HttpGet("Decode financial_transaction_message")]
		public ActionResult<Message> DecodeSimpleMessage()
		{
			string fileReaded = _reader.ReadFile("financial_transaction_message.dat");
			if (fileReaded is null)
			{
				return BadRequest("Arquivo vazio!");
			}

			Message decodedMessage = _decoder.DecodeSimpleMessage(fileReaded);
			return decodedMessage;
		}
		/// <summary>
		/// Decodifica o arquivo message_with_hex_bcd.dat baseando-se no arquvo message_with_hex_bcd.txt
		/// </summary>
		/// <remarks>
		/// Decodifica o arquivo message_with_hex_bcd.dat
		/// </remarks>
		/// <returns>
		///	200 - com o Json dos parâmetros decodificados 
		/// 400 - Arquivo vazio
		/// </returns>
		[HttpGet("Decode message_with_hex_bcd")]
		public ActionResult<Message> DecodeBCDMessage()
		{			
			string fileReaded = _reader.ReadFile("message_with_hex_bcd.dat");
			if (fileReaded is null)
			{
				return BadRequest("Arquivo vazio!");
			}

			Message decodedMessage = _decoder.DecodeBCDMessage(fileReaded);
			return decodedMessage;
		}
		/// <summary>
		/// PROTÓTIPO
		/// Decodifica o uma cadeia string passada pelo usuário. Seu funcionamento não está perfeito.
		/// Decodifica a cadeia baseando-se no Bitmap de 8Bytes da cadeia de caracteres. 
		/// A ideia era quebrar a cadeia de caracteres baseada na posição de cada item no bitmap.
		/// </summary>
		/// <returns>
		///	200 - com o Json dos parâmetros decodificados 
		/// 400 - Arquivo vazio
		/// </returns>
		private ActionResult<IEnumerable<string>> DecodeByBitmap(string message)
		{
			
			if (message is null)
			{
				return BadRequest("Cadeia de caracteres vazia.");
			}
			string parsedMessage = _reader.ParseChar(message);
			var decodedMessage = _decoder.DecodeByBitmap(parsedMessage);
			return Ok(decodedMessage);
		}
		
	}
}

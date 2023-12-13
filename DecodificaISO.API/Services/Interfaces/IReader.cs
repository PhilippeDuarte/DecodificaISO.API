namespace DecodificaISO.API.Services.Interfaces
{
	public interface IReader
	{
		string ReadFile(string path);
		string ParseChar(string readText);
	}
}

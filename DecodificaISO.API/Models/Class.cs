﻿namespace DecodificaISO.API.Models
{
	public class Class
	{
		public static void Main()
		{
			string path = @"c:\temp\MyTest.txt";

			// This text is added only once to the file.
			if (!File.Exists(path))
			{
				// Create a file to write to.
				string createText = "Hello and Welcome" + Environment.NewLine;
				File.WriteAllText(path, createText);
			}

			// This text is always added, making the file longer over time
			// if it is not deleted.
			string appendText = "This is extra text" + Environment.NewLine;
			File.AppendAllText(path, appendText);

			// Open the file to read from.
			string readText = File.ReadAllText(path);
			Console.WriteLine(readText);
		}
	}
}

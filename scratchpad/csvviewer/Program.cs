using System;
using System.IO;
using System.Linq;

namespace csvviewer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var filename = Ask_for_filename ();
			var data = Load_data (filename);
			var table = Process_data (data);
			Display_table (table);
		}


		static string Ask_for_filename() {
			return Environment.GetCommandLineArgs()[1];
		}

		static string[] Load_data(string filename) {
			return File.ReadAllLines (filename);
		}

		static string[] Process_data(string[] data) {
			var page = Extract_data_of_first_page (data);
			return Format (page);
		}

		static string[] Extract_data_of_first_page(string[] data) {
			return data.Take (5).ToArray();
		}
			
		static string[] Format(string[] csvLines) {
			var analysisResult = Analyze (csvLines);
			return Format_as_ASCII_table (analysisResult);
		}

		static void Display_table(string[] table) {
			foreach (var l in table)
				Console.WriteLine (l);
		}

		static dynamic Analyze(string[] csvLines) {
			var csvRecords = Parse (csvLines);
			var colWidths = Determine_col_widths (csvRecords);
			return new { Records = csvRecords, ColWidths = colWidths };
		}

		static string[] Format_as_ASCII_table(dynamic analysisResult) {
			var formattedRecords = Format_records (analysisResult.Records, analysisResult.ColWidths);
			var separator = Format_separator (analysisResult.ColWidths);
			return Build_table (formattedRecords, separator);
		}

		static string[][] Parse(string[] csvLines) {
			return csvLines.Select (l => l.Split (';')).ToArray ();
		}

		static int[] Determine_col_widths(string[][] csvRecords) {
			var colWidths = new int[csvRecords.First ().Length];
			for (var iCol = 0; iCol < colWidths.Length; iCol++)
				colWidths [iCol] = csvRecords.Max (r => r [iCol].Length);
			return colWidths;
		}

		static string[] Format_records(string[][] csvRecords, int[] colWidths) {
			return csvRecords.Select (r => string.Join ("|", r.Select((v, i) => v.PadRight(colWidths[i]))))
				.ToArray ();
		}

		static string Format_separator(int[] colWidths) {
			return string.Join ("+", colWidths.Select (w => new string ('-', w)));
		}

		static string[] Build_table(string[] formattedRecords, string separator) {
			return (new[]{ formattedRecords [0], separator }.Concat(formattedRecords.Skip (1))).ToArray();
		}
	}
}

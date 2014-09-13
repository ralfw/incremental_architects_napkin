using System;
using System.Linq;

namespace csvformatting
{
	public class CsvFormatter
	{
		public string Format(string csvText) {
			var analysisResult = Analyze (csvText);
			return Format_as_ASCII_table (analysisResult);
		}
			
		dynamic Analyze(string csvText) {
			var csvRecords = Parse (csvText);
			var colWidths = Determine_col_widths (csvRecords);
			return new { Records = csvRecords, ColWidths = colWidths };
		}
			
		string Format_as_ASCII_table(dynamic analysisResult) {
			var formattedRecords = Format_records (analysisResult.Records, analysisResult.ColWidths);
			var separator = Format_separator (analysisResult.ColWidths);
			return Build_table (formattedRecords, separator);
		}
			
		string[][] Parse(string csvText) {
			var csvLines = csvText.Split('\n');
			return csvLines.Select (l => l.Split (';')).ToArray ();
		}

		int[] Determine_col_widths(string[][] csvRecords) {
			var colWidths = new int[csvRecords.First ().Length];
			for (var iCol = 0; iCol < colWidths.Length; iCol++)
				colWidths [iCol] = csvRecords.Max (r => r [iCol].Length);
			return colWidths;
		}

		string[] Format_records(string[][] csvRecords, int[] colWidths) {
			return csvRecords.Select (r => string.Join ("|", r.Select((v, i) => v.PadRight(colWidths[i]))))
					         .ToArray ();
		}

		string Format_separator(int[] colWidths) {
			return string.Join ("+", colWidths.Select (w => new string ('-', w)));
		}

		string Build_table(string[] formattedRecords, string separator) {
			return string.Join ("\n", new[]{ formattedRecords [0], separator }.Concat(formattedRecords.Skip (1)));
		}
	}
}


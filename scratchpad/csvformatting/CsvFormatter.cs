using System;
using System.Linq;

namespace csvformatting
{
	public class CsvFormatter
	{
		public string Format(string csvText) {
			var csvRecords = Parse (csvText);
			var colWidths = Determine_col_widths (csvRecords);

			var formattedRecords = Format_records (csvRecords, colWidths);
			var separator = Format_separator (colWidths);

			return Build_table (formattedRecords, separator);
		}

		string[][] Parse(string csvText) {
			var csvLines = csvText.Split('\n');
			return csvLines.Select (l => l.Split (';')).ToArray ();
		}

		int[] Determine_col_widths(string[][] csvRecords) {
			return csvRecords.First ().Select (c => c.Length).ToArray ();
		}

		string[] Format_records(string[][] csvRecords, int[] colWidths) {
			return csvRecords.Select (r => string.Join ("|", r)).ToArray ();
		}

		string Format_separator(int[] colWidths) {
			return "---+---";
		}

		string Build_table(string[] formattedRecords, string separator) {
			return string.Join ("\n",
				new string[0].Concat (new[]{ formattedRecords [0], separator })
							 .Concat(formattedRecords.Skip (1)));
		}
	}
}


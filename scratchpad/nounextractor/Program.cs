using System;
using System.IO;
using System.Linq;

namespace nounextractor
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var text = Read_source_file (args [0]);
			var words = Split_text_into_words (text);
			words = Detect_nouns (words);
			Write_words (args [1], words);
		}

		static string Read_source_file(string filename) {
			return File.ReadAllText (filename);
		}

		static string[] Split_text_into_words(string text) {
			return text.Split (new[]{ ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
		}

		static string[] Detect_nouns(string[] words) {
			return words.Where (w => char.IsUpper (w [0])).ToArray ();
		}

		static void Write_words(string filename, string[] words) {
			File.WriteAllLines (filename, words);
		}
	}
}

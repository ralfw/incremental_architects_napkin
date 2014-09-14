using System;
using System.IO;
using System.Linq;

namespace nounextractor
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var domain = new Domain ();
			var adapter = new Textfileadapter ();

			var text = adapter.Read_source_file (args [0]);
			var words = domain.Split_text_into_words (text);
			words = domain.Detect_nouns (words);
			adapter.Write_words (args [1], words);
		}
	}

	class Domain {
		public string[] Split_text_into_words(string text) {
			return text.Split (new[]{ ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
		}

		public string[] Detect_nouns(string[] words) {
			return words.Where (w => char.IsUpper (w [0])).ToArray ();
		}
	}

	class Textfileadapter {
		public string Read_source_file(string filename) {
			return File.ReadAllText (filename);
		}
	
		public void Write_words(string filename, string[] words) {
			File.WriteAllLines (filename, words);
		}
	}
}

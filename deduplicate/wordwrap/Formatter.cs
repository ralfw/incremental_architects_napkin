using System;
using System.Collections.Generic;

namespace wordwrap
{
    public class Formatter
    {
        public string WordWrap(string text, int maxLineLength) {
            var words = Extract_words(text);
            words = Split_long_words(words, maxLineLength);
            var lines = Reformat(words, maxLineLength);
            return Assemble_text(lines);
        }

        private IEnumerable<string> Split_long_words( IEnumerable<string> words, int maxLineLength) {
            foreach (var w in words) {
                var word = w;
                while (word.Length > maxLineLength) {
                    var head = word.Substring(0, maxLineLength);
                    yield return head;
                    word = word.Substring(maxLineLength);
                }
                if (word != "") yield return word;
            }
        }

        private IEnumerable<string> Extract_words(string text) {
            return text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] Reformat(IEnumerable<string> words, int maxLineLength) {
            var wordlist = new List<string>(words);
            var lines = new List<string>();
            var line = "";
            while (wordlist.Count > 0) {
                var newLineLen = line.Length + 
                                 wordlist[0].Length + 
                                 ((line.Length > 0) ? 1 : 0);
                if (newLineLen <= maxLineLength)
                    line += ((line.Length > 0) ? " " : "") + 
                            wordlist[0];
                else {
                    lines.Add(line);
                    line = wordlist[0];
                }
                wordlist.RemoveAt(0);
            }
            if (line != "") lines.Add(line);

            return lines.ToArray();
        }

        private string Assemble_text(string[] lines) {
            return string.Join("\n", lines);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

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
            return Reformat(new List<string>(words), maxLineLength, "", new List<string>());
        }

        private string[] Reformat(List<string> words, int maxLineLength, string line, List<string> lines) {
            if (!words.Any()) {
                if (line != "") lines.Add(line);
                return lines.ToArray();
            }

            line += (line != "") ? " " : "";
            var word = words[0];
            words.RemoveAt(0);

            if ((line.Length + word.Length) <= maxLineLength)
                return Reformat(words, maxLineLength, line + word, lines);

            lines.Add(line.Trim());
            return Reformat(words, maxLineLength, word, lines);
        }

        private string Assemble_text(string[] lines) {
            return string.Join("\n", lines);
        }
    }
}

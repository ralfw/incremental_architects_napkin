using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordwrap
{
    public class Formatter
    {
        public string WordWrap(string text, int maxLineLength)
        {
            var words = Extract_words(text);
            var lines = Reformat(words, maxLineLength);
            return Assemble_text(lines);
        }

        private string[] Extract_words(string text)
        {
            return text.Split(new[] {' '}, 
                              StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] Reformat(string[] words, int maxLineLength)
        {
            var wordlist = new List<string>(words);
            var lines = new List<string>();
            var line = "";
            while (wordlist.Count > 0)
            {
                var newLineLen = line.Length + wordlist[0].Length + ((line.Length > 0) ? 1 : 0);
                if (newLineLen <= maxLineLength)
                    line += ((line.Length > 0) ? " " : "") + wordlist[0];
                else {
                    lines.Add(line);
                    line = wordlist[0];
                }
                wordlist.RemoveAt(0);
            }
            if (line != "") lines.Add(line);

            return lines.ToArray();
        }

        private string Assemble_text(string[] lines)
        {
            return string.Join("\n", lines);
        }
    }
}

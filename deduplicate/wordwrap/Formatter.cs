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
            return words;
        }

        private string Assemble_text(string[] lines)
        {
            return string.Join("\n", lines);
        }
    }
}

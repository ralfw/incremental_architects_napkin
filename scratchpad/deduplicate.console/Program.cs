using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deduplicate.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Validate_command_line(args, 
                validatedArgs => {
                    var input = Accept_string_list(validatedArgs);
                    var output = Deduplicate(input);
                    Present_deduplicated_string_list(output); 
                },
                Present_error_message);
        }

        private static void Validate_command_line(
                            string[] args, 
                            Action<string[]> onValidity,
                            Action onInvalidity) {
            if (args.Length == 1) onValidity(args);
            else                  onInvalidity();
        }

        private static void Present_error_message()
        {
            var fc = Console.ForegroundColor; Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Usage: deduplicate \"comma separated, list, of strings\"");
            Console.ForegroundColor = fc;
        }


        private static string Accept_string_list(string[] args)
        {
            return args[0];
        }

        private static string[] Deduplicate(string input)
        {
            var strings = Parse_string_list(input);
            var dict = Compile_unique_strings(strings);
            return Serialize_unique_strings(dict);
        }


        private static string[] Parse_string_list(string input)
        {
            return input.Split(',').Select(s => s.Trim()).ToArray();
        }

        private static HashSet<string> 
                       Compile_unique_strings(string[] strings)
        {
            return strings.Aggregate(
                    new HashSet<string>(),
                    (agg, s) => {
                        agg.Add(s);
                        return agg;
                    });
        }

        private static string[] Serialize_unique_strings(HashSet<string> set)
        {
            return set.ToArray();
        }


        private static void Present_deduplicated_string_list(string[] output)
        {
            var line = string.Join(", ", output);
            Console.WriteLine(line);
        }
    }
}

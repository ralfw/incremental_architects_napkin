using NUnit.Framework;
using System;
using System.Linq;

namespace fizzbuzz
{
	public class FizzBuzzer {

		public void FizzBuzz(int first, int last) {
			Check_range (first, last);
			Generate_numbers (first, last,
				number => Classify_number(number, 
								() => {
									var translation = Translate_Fizz();
									Print(translation);
								},
								() => {
									var translation = Translate_Buzz();
									Print(translation);
								},
								() => {
									var translation = Translate_FizzBuzz();
									Print(translation);
								},
								n => {
									var translation = Translate_any_other_number(n);
									Print(translation);
								})
				);
		}

		private void Print(string text) {
			Console.WriteLine (text);
		}

		private void Check_range(int first, int last) {
			if (first > last)
				throw new InvalidOperationException ("First number is larger than last number.");
		}

		private void Generate_numbers(int first, int last, Action<int> onNumber) {
			for (var n = first; n <= last; n++)
				onNumber (n);
		}

		private void Classify_number(int number,
						Action onFizz,
						Action onBuzz,
						Action onFizzBuzz,
						Action<int> onOther) {
			if (number % 5 == 0 && number % 3 == 0)
				onFizzBuzz ();
			else if (number % 3 == 0)
				onFizz ();
			else if (number % 5 == 0)
				onBuzz ();
			else
				onOther (number);

		}

		private string Translate_Fizz() {
			return "Fizz";
		}

		private string Translate_Buzz() {
			return "Buzz";
		}

		private string Translate_FizzBuzz() {
			return "FizzBuzz";
		}

		private string Translate_any_other_number(int number) {
			return number.ToString ();
		}
	}
	
}

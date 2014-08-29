using NUnit.Framework;
using System;
using System.Linq;

namespace fizzbuzz
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void Acceptance ()
		{
			var sut = new FizzBuzzer ();
			sut.FizzBuzz (1, 20);
		}
	}
}


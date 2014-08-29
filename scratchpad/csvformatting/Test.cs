using NUnit.Framework;
using System;

namespace csvformatting
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void Acceptance ()
		{
			const string CSVTEXT =
@"Name;Age;City
Peter;26;Hamburg
Paul;45;London
Mary;38;Copenhagen";

			var sut = new CsvFormatter ();
			var result = sut.Format (CSVTEXT);
			Console.WriteLine (result);
		}
	}
}


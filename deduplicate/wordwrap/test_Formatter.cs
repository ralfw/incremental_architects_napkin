using System;
using NUnit.Framework;

namespace wordwrap
{
    [TestFixture]
    public class test_Formatter
    {
        [Test]
        public void Acceptance()
        {
            var sut = new Formatter();
            var result = sut.WordWrap("the quick brown fox jumps over the lazy dog", 9);
            Console.WriteLine(result);
        }
    }
}
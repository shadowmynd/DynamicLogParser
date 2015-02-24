using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicLogParser.Tests
{
    using Rhino.Mocks;

    [TestClass]
    public class ParserFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod1()
        {
            var obj = ParserFactory.CreateParser(null);
        }

        [TestMethod]
        [ExpectedException(typeof (NotImplementedException))]
        public void ShouldThrowNotImplemented()
        {
            var obj = ParserFactory.CreateParser(MockRepository.GenerateMock<ParserSyntaxBase>());
        }
    }
}

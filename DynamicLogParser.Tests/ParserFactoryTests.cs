// Solution: PersonalLibs
// Project: DynamicLogParser.Tests
// FileName: ParserFactoryTests.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 9:32 PM
// Modified: 02-23-2015 9:44 PM []

namespace DynamicLogParser.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Parser;
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

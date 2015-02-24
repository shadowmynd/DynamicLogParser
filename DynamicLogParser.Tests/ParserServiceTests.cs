// Solution: PersonalLibs
// Project: DynamicLogParser.Tests
// FileName: ParserServiceTests.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 9:15 PM
// Modified: 02-23-2015 9:44 PM []

namespace DynamicLogParser.Tests
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Parser;
    using Rhino.Mocks;

    [TestClass]
    public class ParserServiceTests
    {
        private ParserService parserService;

        [TestInitialize]
        public void TestInit()
        {
            var syntax = MockRepository.GenerateMock<ParserSyntaxBase>();
            this.parserService = MockRepository.GenerateMock<ParserService>(syntax);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowForEmptyContents()
        {
            this.parserService.Parse(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowForNullStream()
        {
            this.parserService.Parse((Stream) null);
        }

        [TestMethod]
        public void ShouldParseStream()
        {
            this.parserService.Stub(x => x.Parse(new MemoryStream()))
                .Return(new DynamicModel());
            this.parserService.Parse(new MemoryStream());
        }
    }
}

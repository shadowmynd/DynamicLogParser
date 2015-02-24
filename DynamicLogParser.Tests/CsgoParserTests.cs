// Solution: PersonalLibs
// Project: DynamicLogParser.Tests
// FileName: CsgoParserTests.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 5:42 PM
// Modified: 02-23-2015 9:44 PM []
namespace DynamicLogParser.Tests
{
    using Adapters;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CsgoParserTests
    {
        [TestMethod]
        public void ShouldParseDeathmatchResults()
        {
            dynamic model = Parser<CsgoParserSyntax>.Load(this.GetDeathmatchFileLocation());
            var obj = model.savefile.timestamp;
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void ShouldParseClassicCompetitiveResults()
        {
            dynamic model = Parser<CsgoParserSyntax>.Load(this.GetClassicCompetitiveResults());
            var obj = model.savefile.timestamp;
            Assert.IsNotNull(obj);
        }


        private string GetClassicCompetitiveResults()
        {
            return @"G:\Development\GitRepos\PersonalLibs\DynamicLogParser.Tests\Resources\csgo\classic_competitive.txt";
        }

        private string GetDeathmatchFileLocation()
        {
            return @"G:\Development\GitRepos\PersonalLibs\DynamicLogParser.Tests\Resources\csgo\deathmatch.txt";
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicLogParser.Tests
{
    using Adapters;

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

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
    using Adapters.Adapter;
    using Adapters.Syntax;
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Parser;

    [TestClass]
    public class CsgoParserTests
    {
        /*
         *  Sample Usage
         *  ----------------------------------------
         *  
           var adapter = new CsgoLogParserAdapter();
            dynamic model = adapter.Parse(this.GetDeathmatchFileLocation());
            model = model.savefile;
            var players = new List<MatchPlayerModel>();
            int bTeam1 = DynamicModel.Count(model.playersonteam1);
            for (var i = 0; i < bTeam1; i++)
            {
                dynamic dPlayer = model.playersonteam1[i];
                var player = new MatchPlayerModel((string) dPlayer.name, Int32.Parse(dPlayer.score));
                players.Add(player);
            }

            int bTeam2 = DynamicModel.Count(model.playersonteam2);
            for (var i = 0; i < bTeam2; i++)
            {
                dynamic dPlayer = model.playersonteam2[i];
                var player = new MatchPlayerModel((string) dPlayer.name, Int32.Parse(dPlayer.score));
                players.Add(player);
            }

            var retModel = new SteamMatchCompleteModel(
                players.Aggregate(((i1, i2) => i1.FinalScore > i2.FinalScore ? i1 : i2))
                    .UserIdentifierKey)
            {
                MatchPlayers = players,
                RecordedTimestamp = model.timestamp
            };
         */
        [TestMethod]
        public void AdapterShouldParseDeathmatchResults()
        {
            var adapter = new CsgoLogParserAdapter();
            dynamic model = adapter.Parse(this.GetDeathmatchFileLocation());
            var obj = model.savefile.timestamp;
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void ShouldParseDeathmatchResults()
        {
            dynamic model = FileParser<CsgoParserSyntax>.Load(this.GetDeathmatchFileLocation());
            var obj = model.savefile.timestamp;
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void ShouldParseClassicCompetitiveResults()
        {
            dynamic model = FileParser<CsgoParserSyntax>.Load(this.GetClassicCompetitiveResults());
            var obj = model.savefile.timestamp;
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void ShouldExtractName()
        {
            dynamic model = FileParser<CsgoParserSyntax>.Load(this.GetDeathmatchFileLocation());
            var obj = model.savefile.playersonteam1[0];
            var name = obj.name;
            Assert.AreEqual((string)name, "chessgeek");
        }

        [TestMethod]
        public void ShouldSafelyIterateProperties()
        {
            dynamic model = FileParser<CsgoParserSyntax>.Load(this.GetDeathmatchFileLocation());
            int bounds = DynamicModel.Count(model.savefile.playersonteam1);
            for (var i = 0; i < bounds; i++)
            {
                var obj = model.savefile.playersonteam1[i].score;
                Assert.IsNotNull(obj);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(RuntimeBinderException))]
        public void ShouldFailExtractScore()
        {
            dynamic model = FileParser<CsgoParserSyntax>.Load(this.GetDeathmatchFileLocation());
            var obj = model.savefile.playersonteam1[3];
            var name = obj.name;
            Assert.AreEqual((string)name, "chessgeek");
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

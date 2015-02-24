// Solution: PersonalLibs
// Project: DynamicLogParser.Adapters
// FileName: CsgoParserSyntax.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 9:37 PM
// Modified: 02-23-2015 9:44 PM []
namespace DynamicLogParser.Adapters.Syntax
{
    using Parser;

    public class CsgoParserSyntax : JsonLikeParserSyntax
    {
        public override string ScopeMarkExitRegex
        {
            get { return "}"; }
        }

        public override string KvpRegex
        {
            get { return "\"(.*?)\"\\s*\"(.*?)\""; }
        }

        public override string HeaderRegex
        {
            get { return "\"(.*?)\"\\s*\\{"; }
        }

        public override bool RemoveSpaces
        {
            get { return true; }
        }

        public override string PreParseCleanupRegex
        {
            get { return "[\t]"; }
        }

        public override bool PreParseCleanup
        {
            get { return true; }
        }

        public override string KvpSeparatorRegex
        {
            get { return "\"(.*?)\""; }
        }

        public override string CleanStringRegex
        {
            get { return @"[^\w\.@-]"; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLogParser.Adapters
{
    public class CsgoParserSyntax : ParserSyntaxBase
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

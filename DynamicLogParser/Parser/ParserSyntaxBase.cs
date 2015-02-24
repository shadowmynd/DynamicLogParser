// Solution: PersonalLibs
// Project: DynamicLogParser
// FileName: ParserSyntaxBase.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 8:47 PM
// Modified: 02-23-2015 9:44 PM []
namespace DynamicLogParser.Parser
{
    public abstract class ParserSyntaxBase
    {
        public abstract string ScopeMarkExitRegex { get; }
        public abstract bool RemoveSpaces { get; }
        public abstract bool PreParseCleanup { get; }
        public abstract string KvpRegex { get; }
        public abstract string HeaderRegex { get; }
        public abstract string PreParseCleanupRegex { get; }
        public abstract string KvpSeparatorRegex { get; }
        public abstract string CleanStringRegex { get; }
    }
}
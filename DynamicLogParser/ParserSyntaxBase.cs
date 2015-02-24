namespace DynamicLogParser
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
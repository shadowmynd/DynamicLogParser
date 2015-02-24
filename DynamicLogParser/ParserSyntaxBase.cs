﻿// Solution: PersonalLibs
// Project: DynamicLogParser
// FileName: ParserSyntaxBase.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 5:36 PM
// Modified: 02-23-2015 8:41 PM []
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
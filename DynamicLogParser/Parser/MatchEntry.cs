// Solution: PersonalLibs
// Project: DynamicLogParser
// FileName: MatchEntry.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 9:05 PM
// Modified: 02-23-2015 9:05 PM []
namespace DynamicLogParser.Parser
{
    using System.Text.RegularExpressions;

    internal class MatchEntry
    {
        public MatchEntry(
            Match entry,
            SyntaxTypes type)
        {
            this.EntryMatch = entry;
            this.EntryType = type;
            this.Checked = false;
        }

        public SyntaxTypes EntryType { get; private set; }
        public Match EntryMatch { get; private set; }
        public bool Checked { get; set; }
    }

}
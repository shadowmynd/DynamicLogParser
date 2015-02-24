// Solution: PersonalLibs
// Project: DynamicLogParser
// FileName: Parser.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 5:38 PM
// Modified: 02-23-2015 8:41 PM []
namespace DynamicLogParser
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class Parser<TParserSyntax>
        where TParserSyntax : ParserSyntaxBase
    {
        private enum SyntaxTypes
        {
            Header = 0,
            ScopeExit,
            Kvp
        }

        private class MatchEntry
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

        public static DynamicModel Load(string filePath)
        {
            return Load(filePath, Encoding.Default);
        }

        public static DynamicModel Load(string filePath, Encoding encoding)
        {
            var model = new DynamicModel();
            var parser = Activator.CreateInstance<TParserSyntax>();
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(fileStream, encoding))
            {
                var text = reader.ReadToEnd();

                //Clean human-friendliness
                if (parser.PreParseCleanup)
                {
                    text = Regex.Replace(text, parser.PreParseCleanupRegex, " ");
                }

                if (parser.RemoveSpaces)
                {
                    text = text.Replace(" ", string.Empty);
                }

                model = Parse(parser, text);
            }

            return model;
        }

        private static DynamicModel Parse(ParserSyntaxBase parser, string text)
        {
            var headerEntries = GetModelHeaders(parser.HeaderRegex, text);
            var scopeExitEntries = GetScopeDecrease(parser.ScopeMarkExitRegex, text);
            var kvpEntries = GetKvpEntries(parser.KvpRegex, text);
            var entries = new List<MatchEntry>();
            AddMatchCollectionToEntries(entries, headerEntries, SyntaxTypes.Header);
            AddMatchCollectionToEntries(entries, scopeExitEntries, SyntaxTypes.ScopeExit);
            AddMatchCollectionToEntries(entries, kvpEntries, SyntaxTypes.Kvp);
            BubbleSortCollection(entries);
            KvpSeparatorRegex = parser.KvpSeparatorRegex;
            CleanStringRegex = parser.CleanStringRegex;
            return CreateModel(entries.GetEnumerator());
        }
        private static DynamicModel CreateModel(List<MatchEntry>.Enumerator orderedMatches)
        {
            var model = new DynamicModel();
            while (orderedMatches.MoveNext())
            {
                var match = orderedMatches.Current;
                Debug.Assert(match != null, "match != null");
                if (match.Checked)
                {
                    continue;
                }

                match.Checked = true;
                switch (match.EntryType)
                {
                    case SyntaxTypes.Header:
                        var subObjectName = GetCleanString(match.EntryMatch.Value);
                        UpdateModel(model, subObjectName, CreateModel(orderedMatches));
                        continue;
                    case SyntaxTypes.Kvp:
                        var pair = Regex.Matches(match.EntryMatch.Value, KvpSeparatorRegex);
                        var propertyName = GetCleanString(pair[0].Value);
                        var propertyValue = GetCleanString(pair[1].Value);
                        UpdateModel(model, propertyName, propertyValue);
                        break;
                    case SyntaxTypes.ScopeExit:
                        return model;
                }
            }

            return model;
        }

        private static string GetCleanString(string p)
        {
            return Regex.Replace(p, CleanStringRegex, string.Empty);
        }

        private static void UpdateModel(DynamicModel model, string name, object value)
        {
            model.TryCreateMember(name, value);
        }

        private static void AddMatchCollectionToEntries(List<MatchEntry> entries, MatchCollection matches, SyntaxTypes entryType)
        {
            for (var i = 0; i < matches.Count; i++)
            {
                entries.Add(new MatchEntry(matches[i], entryType));
            }
        }

        private static void BubbleSortCollection(List<MatchEntry> collection)
        {
            for (var write = 0; write < collection.Count; write++)
            {
                for (var sort = 0; sort < collection.Count - 1; sort++)
                {
                    if (collection[sort].EntryMatch.Index <= collection[sort + 1].EntryMatch.Index)
                    {
                        continue;
                    }

                    var temp = collection[sort + 1];
                    collection[sort + 1] = collection[sort];
                    collection[sort] = temp;
                }
            }
        }

        private static MatchCollection GetKvpEntries(string kvpRegex, string text)
        {
            return GetCollection(kvpRegex, text);
        }

        private static MatchCollection GetScopeDecrease(string scopeExitRegex, string text)
        {
            return GetCollection(scopeExitRegex, text);
        }

        private static MatchCollection GetModelHeaders(string headerRegex, string text)
        {
            return GetCollection(headerRegex, text);
        }

        private static MatchCollection GetCollection(string regexString, string text)
        {
            var regex = new Regex(regexString, RegexOptions.Compiled);
            var matches = regex.Matches(text);
            return matches;
        }
        private static string KvpSeparatorRegex { get; set; }
        private static string CleanStringRegex { get; set; }

    }
}
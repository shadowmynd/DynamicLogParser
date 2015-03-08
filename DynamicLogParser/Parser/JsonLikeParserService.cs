// Solution: PersonalLibs
// Project: DynamicLogParser
// FileName: JsonLikeParserService.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 8:51 PM
// Modified: 02-23-2015 9:44 PM []
namespace DynamicLogParser.Parser
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class JsonLikeParserService : ParserService
    {
        public JsonLikeParserService(JsonLikeParserSyntax syntax)
            : base(syntax)
        {
        }

        public override DynamicModel ParseContent(string content)
        {
            var headerEntries = this.GetModelHeaders(ParserSyntax.HeaderRegex, content);
            var scopeExitEntries = this.GetScopeDecrease(ParserSyntax.ScopeMarkExitRegex, content);
            var kvpEntries = this.GetKvpEntries(ParserSyntax.KvpRegex, content);
            var entries = new List<MatchEntry>();
            this.AddMatchCollectionToEntries(entries, headerEntries, SyntaxTypes.Header);
            this.AddMatchCollectionToEntries(entries, scopeExitEntries, SyntaxTypes.ScopeExit);
            this.AddMatchCollectionToEntries(entries, kvpEntries, SyntaxTypes.Kvp);
            this.BubbleSortCollection(entries);
            return this.CreateModel(entries.GetEnumerator());
        }

        private DynamicModel CreateModel(List<MatchEntry>.Enumerator orderedMatches)
        {
            var model = new DynamicModel();
            while (orderedMatches.MoveNext())
            {
                var match = orderedMatches.Current;
                if (match == null || match.Checked)
                {
                    continue;
                }

                match.Checked = true;
                switch (match.EntryType)
                {
                    case SyntaxTypes.Header:
                        var subObjectName = this.GetCleanString(match.EntryMatch.Value);
                        this.UpdateModel(model, subObjectName, this.CreateModel(orderedMatches));
                        continue;
                    case SyntaxTypes.Kvp:
                        var pair = Regex.Matches(match.EntryMatch.Value, ParserSyntax.KvpSeparatorRegex);
                        var propertyName = this.GetCleanString(pair[0].Value);
                        var propertyValue = this.GetCleanString(pair[1].Value);
                        this.UpdateModel(model, propertyName, propertyValue);
                        break;
                    case SyntaxTypes.ScopeExit:
                        return model;
                }
            }

            return model;
        }

        private string GetCleanString(string p)
        {
            return Regex.Replace(p, ParserSyntax.CleanStringRegex, string.Empty);
        }

        private void UpdateModel(DynamicModel model, string name, object value)
        {
            model.TryCreateMember(name, value);
        }

        private void AddMatchCollectionToEntries(List<MatchEntry> entries, MatchCollection matches, SyntaxTypes entryType)
        {
            for (var i = 0; i < matches.Count; i++)
            {
                entries.Add(new MatchEntry(matches[i], entryType));
            }
        }

        private void BubbleSortCollection(List<MatchEntry> collection)
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

        private MatchCollection GetKvpEntries(string kvpRegex, string text)
        {
            return this.GetCollection(kvpRegex, text);
        }

        private MatchCollection GetScopeDecrease(string scopeExitRegex, string text)
        {
            return this.GetCollection(scopeExitRegex, text);
        }

        private MatchCollection GetModelHeaders(string headerRegex, string text)
        {
            return this.GetCollection(headerRegex, text);
        }

        private MatchCollection GetCollection(string regexString, string text)
        {
            var regex = new Regex(regexString, RegexOptions.Compiled);
            var matches = regex.Matches(text);
            return matches;
        }
    }
}

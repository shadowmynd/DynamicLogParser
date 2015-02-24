// Solution: PersonalLibs
// Project: DynamicLogParser
// FileName: ParserService.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 8:52 PM
// Modified: 02-23-2015 9:44 PM []
namespace DynamicLogParser.Parser
{
    using System;
    using System.IO;

    public abstract class ParserService : IParserService
    {
        protected ParserService(
            ParserSyntaxBase syntax)
        {
            this.ParserSyntax = syntax;
        }

        public ParserSyntaxBase ParserSyntax { get; private set; }

        public abstract DynamicModel ParseContent(string content);

        public DynamicModel Parse(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            using (var reader = new StreamReader(stream))
            {
                var content = reader.ReadToEnd();
                return this.ParseContent(content);
            }
        }

        public DynamicModel Parse(string contents)
        {
            if (string.IsNullOrEmpty(contents))
            {
                throw new ArgumentNullException("contents");
            }

            using (var reader = new StringReader(contents))
            {
                var content = reader.ReadToEnd();
                return this.ParseContent(content);
            }
        }
    }
}
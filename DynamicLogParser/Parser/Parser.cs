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
    using Parser;

    public static class Parser<TParserSyntax>
        where TParserSyntax : ParserSyntaxBase
    {
        public static DynamicModel Load(string filePath)
        {
            return Load(filePath, Encoding.Default);
        }

        public static DynamicModel Load(string filePath, Encoding encoding)
        {
            var model = new DynamicModel();
            var syntax = Activator.CreateInstance<TParserSyntax>();
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(fileStream, encoding))
            {
                var text = reader.ReadToEnd();

                //Clean human-friendliness
                if (syntax.PreParseCleanup)
                {
                    text = Regex.Replace(text, syntax.PreParseCleanupRegex, " ");
                }

                if (syntax.RemoveSpaces)
                {
                    text = text.Replace(" ", string.Empty);
                }

                var service = ParserFactory.CreateParser(syntax);
                model = service.Parse(text);
            }

            return model;
        }
    }
}
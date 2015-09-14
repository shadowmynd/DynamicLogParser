// Solution: PersonalLibs
// Project: DynamicLogParser
// FileName: Parser.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 8:47 PM
// Modified: 02-23-2015 9:44 PM []
namespace DynamicLogParser.Parser
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class FileParser<TParserSyntax>
        where TParserSyntax : ParserSyntaxBase
    {
        public static DynamicModel Load(string filePath)
        {
            return Load(filePath, Encoding.Default);
        }

        public static DynamicModel Load(string filePath, Encoding encoding)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            if (!File.Exists(filePath))
            {
                throw new InvalidOperationException("No such file exists");
            }

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
                return service.Parse(text);
            }
        }
    }
}
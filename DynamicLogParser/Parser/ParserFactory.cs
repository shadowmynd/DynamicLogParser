// Solution: PersonalLibs
// Project: DynamicLogParser
// FileName: ParserFactory.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 8:48 PM
// Modified: 02-23-2015 9:44 PM []
namespace DynamicLogParser.Parser
{
    using System;

    internal static class ParserFactory
    {
        public static ParserService CreateParser(ParserSyntaxBase syntaxModel)
        {
            if (syntaxModel == null)
            {
                throw new ArgumentNullException("syntaxModel");
            }

            var model = syntaxModel as JsonLikeParserSyntax;
            if (model != null)
            {
                return new JsonLikeParserService(model);
            }

            throw new NotImplementedException("No such abstraction currently defined.");
        }
    }
}
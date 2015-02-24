// Solution: PersonalLibs
// Project: DynamicLogParser.Adapters
// FileName: CsgoAdapter.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 9:47 PM
// Modified: 02-23-2015 9:47 PM []
namespace DynamicLogParser.Adapters.Adapter
{
    using Parser;
    using Syntax;

    public class CsgoLogParserAdapter : LogParserAdapterBase
    {
        public override DynamicModel Parse(string filePath)
        {
            return FileParser<CsgoParserSyntax>.Load(filePath);
        }
    }
}
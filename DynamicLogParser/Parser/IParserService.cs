// Solution: PersonalLibs
// Project: DynamicLogParser
// FileName: IParserService.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 8:49 PM
// Modified: 02-23-2015 9:44 PM []

namespace DynamicLogParser.Parser
{
    using System.IO;

    public interface IParserService
    {
        DynamicModel Parse(Stream stream);
        DynamicModel Parse(string contents);
    }
}

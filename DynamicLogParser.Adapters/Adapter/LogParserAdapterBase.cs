// Solution: PersonalLibs
// Project: DynamicLogParser.Adapters
// FileName: LogParserAdapterBase.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 9:55 PM
// Modified: 02-23-2015 9:55 PM []
namespace DynamicLogParser.Adapters.Adapter
{
    public abstract class LogParserAdapterBase
    {
        public abstract DynamicModel Parse(string filePath);
    }
}
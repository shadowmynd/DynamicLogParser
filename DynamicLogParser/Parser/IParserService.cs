using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicLogParser.Parser
{
    using System.IO;

    public interface IParserService
    {
        DynamicModel Parse(Stream stream);
        DynamicModel Parse(string contents);
    }
}

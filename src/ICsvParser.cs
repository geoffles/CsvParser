using System;
using System.Collections.Generic;

namespace Geoffles.Csv
{
    public interface ICsvParser
    {
        IEnumerable<string> ReadFields(string line);
    }
}
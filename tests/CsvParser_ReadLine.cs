using System;
using System.Linq;
using Xunit;
using Geoffles.Csv;

namespace tests
{
    public class CsvParser_Readline
    {
        [Fact]
        public void Readline_MustParseFields()
        {
            var parser = new CsvParser();

            var fields = parser.ReadFields("A field, another field").ToList();

            Assert.Equal(2, fields.Count());
            Assert.Equal("A field", fields[0]);
            Assert.Equal(" another field", fields[1]);
        }

        [Fact]
        public void Readline_MustParseFields_WithEmptyLastRecord()
        {
            var parser = new CsvParser();

            var fields = parser.ReadFields("A field, another field,").ToList();

            Assert.Equal(3, fields.Count());
            Assert.Equal("A field", fields[0]);
            Assert.Equal(" another field", fields[1]);
            Assert.Equal("", fields[2]);
        }

        [Fact]
        public void Readline_MustParseNewLinesInQuotedFields()
        {
            var parser = new CsvParser();

            var fields = parser.ReadFields("A field,\" another\r\n field\"").ToList();

            Assert.Equal(2, fields.Count());
            Assert.Equal("A field", fields[0]);
            Assert.Equal(" another\r\n field", fields[1]);
        }

        [Fact]
        public void Readline_MustIgnoreCommasInAnEscape()
        {
            var parser = new CsvParser();

            var fields = parser.ReadFields("\"An, escaped field\"").ToList();

            Assert.Equal("An, escaped field", fields.Single());
        }

        [Fact]
        public void Readline_MustParseEscapedEscapes()
        {
            var parser = new CsvParser();

            var fields = parser.ReadFields("\"an, escaped field with \"\"quotes,\"\"\"");

            Assert.Equal("an, escaped field with \"quotes,\"", fields.Single());
        }

        [Fact]
        public void Readline_ApplyAllRulesTogether()
        {
            var parser = new CsvParser();

            var fields = parser.ReadFields("A field, another field,\"An, escaped field\",\"an, escaped field with \"\"quotes,\"\"\"").ToList();

            Assert.Equal("A field", fields[0]);
            Assert.Equal(" another field", fields[1]);
            Assert.Equal("An, escaped field", fields[2]);
            Assert.Equal("an, escaped field with \"quotes,\"", fields[3]);
        }
    }
}

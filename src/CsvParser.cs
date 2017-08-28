using System;
using System.Text;
using System.Collections.Generic;

namespace Geoffles.Csv
{
    public class CsvParser : ICsvParser
    {
        public char Delimeter {get; private set; }
        public char Quote {get; private set; }

        public CsvParser(char delimeter = ',', char quote = '"')
        {
            Delimeter = delimeter;
            Quote = quote;
        }
        
        public IEnumerable<string> ReadFields(string line)
        {
            var field = new StringBuilder();

            char? prev = null;
            bool quoted = false;
            bool checkQuote = false;
            int count = 0;

            Action reset = () =>
            {
                prev = null;
                quoted = false;
                checkQuote = false;
                count = 0;
                field.Clear();
            };

            foreach(char c in line)
            {
                if (checkQuote)
                {
                    checkQuote = false;
                    if (c == Quote && prev.Value == Quote)
                    {
                        field.Append(Quote);
                        continue;
                    }
                    else
                    {
                        yield return field.ToString();
                        reset();
                        continue;
                    }
                }


                if (count == 0 && c == Delimeter)
                {
                    yield return string.Empty;

                    reset();
                    
                    continue;
                }
                
                if (count == 0 && c == Quote)
                {
                    quoted = true;
                    count++;
                    
                    continue;
                }

                if (!quoted && c == Delimeter)
                {
                    yield return field.ToString();

                    reset();

                    continue;
                }

                if (quoted && c == Quote && !checkQuote)
                {
                    checkQuote = true;
                    prev = c;
                    count++;
                    continue;
                }

                field.Append(c);
                
                prev = c;
                count++;
            }

            if (prev == Delimeter)
            {
                yield return string.Empty;
            }
            else if (field.Length != 0)
            {
                yield return field.ToString();
            }
        }
    }
}
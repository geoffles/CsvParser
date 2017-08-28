# Geoffles.CsvParser

This is a simple class to parse comma separated fields from a string.

The `CsvParser.ReadFields()` method returns an `Enumerable<string>`, with each element of the collection being a field as per the CSV convention.

## Escaping
The parser supports escaping. Both the escape (aka `Quote`) and the delimeter can be set from the constructor. Note that this parser only supports single character separators.

A double escape includes the escape character. Eg:

```
A field, another field,"An, escaped field", "an,escaped field with ""quotes,"""
```

becomes

1.  `'A field'`
2.  `' another field'`
3.  `'An, escaped field'`
4.  `'an, escaped field with "quotes,"'`

Note that the space on field `2` is parsed as part of the string.

## Usage

```
ICsvParser parser = new CsvParser();
IEnumerable<string> fields = parser.ReadFields("First,Second,123,\"A comment, nothing special\"");

//... you can now parse out each field:

IList<string> listFields = fields.ToList();

return new {
    Field1 = listFields[0],
    Field2 = listFields[1],
    Amount = int.Parse(listFields[2]),
    Comment = listFields[3]
}
```

# License

This software is licensed with the BSD license. Please read `LICENSE` for more information.
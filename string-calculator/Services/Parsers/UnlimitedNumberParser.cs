namespace string_calculator.Services.Parsers
{
    using string_calculator.Extensions;
    using System.Collections.Generic;
    using System.Linq;

    public class UnlimitedNumberParser : IStringParser
    {
        public IEnumerable<int> Parse(string stringToParse)
        {
            if (string.IsNullOrWhiteSpace(stringToParse))
                return new List<int>();

            var parsedNumbers = stringToParse.Split(',').Select(o => o.ConvertToInt());

            return parsedNumbers;
        }
    }
}
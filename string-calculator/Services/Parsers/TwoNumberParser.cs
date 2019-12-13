namespace string_calculator.Services.Parsers
{
    using string_calculator.Exceptions;
    using string_calculator.Extensions;
    using System.Collections.Generic;
    using System.Linq;

    public class TwoNumberParser : IStringParser
    {
        public IEnumerable<int> Parse(string stringToParse)
        {
            if (string.IsNullOrWhiteSpace(stringToParse)) 
                return new List<int>();

            var parsedNumbers = stringToParse.Split(',').Select(o => o.ConvertToInt());

            if (parsedNumbers.Count() > 2)
                throw new MoreThanTwoNumbersException();

            return parsedNumbers;
        }
    }
}
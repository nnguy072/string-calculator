namespace string_calculator.Services.Parsers
{
    using string_calculator.Exceptions;
    using string_calculator.Extensions;
    using System.Collections.Generic;
    using System.Linq;

    public class UnlimitedNumberParser : IStringParser
    {
        public IEnumerable<int> Parse(string stringToParse)
        {
            if (string.IsNullOrWhiteSpace(stringToParse))
                return new List<int>();

            var negativeNumbers = new List<int>();
            var parsedNumbers = stringToParse.Split(new char[] { ',', '\n' })
                                             .Select(o => 
                                             {
                                                 var convertedNum = o.ConvertToInt();

                                                 if (convertedNum < 0)
                                                     negativeNumbers.Add(convertedNum);

                                                 return convertedNum;
                                             })
                                             .ToList(); // need to do this otherwise, this gets evaluated later on and it'll seem like we don't have any negative numbers

            if (negativeNumbers.Any())
                throw new NegativeNumbersException(negativeNumbers);

            return parsedNumbers;
        }
    }
}
namespace string_calculator.Services.Parsers
{
    using string_calculator.Exceptions;
    using string_calculator.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class CustomDelimiter : IStringParser
    {
        private const string DefaultDelimiter = ",";
        private const string NewLineDelimiter = "\n";

        public IEnumerable<int> Parse(string stringToParse)
        {
            if (string.IsNullOrWhiteSpace(stringToParse))
                return new List<int>();

            var negativeNumbers = new List<int>();
            var delimiters = new List<string> { DefaultDelimiter, NewLineDelimiter };

            var customDelimiter = ParseDelimiter(stringToParse, out var stringToParseWithoutDelimiter);
            if (!string.IsNullOrWhiteSpace(customDelimiter))
                delimiters.Add(customDelimiter);

            var parsedNumbers = stringToParseWithoutDelimiter.Split(delimiters.ToArray(), StringSplitOptions.None)
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

        private string ParseDelimiter(string stringToParse, out string stringToParseWithoutDelimiter)
        {
            var delimiter = ParseSingleCharacterDelimiter(stringToParse, out var updatedStringForSingleChar);

            if (!string.IsNullOrWhiteSpace(delimiter))
            {
                stringToParseWithoutDelimiter = updatedStringForSingleChar;
                return delimiter;
            }
            else
            {
                delimiter = ParseAnyLengthDelimiter(stringToParse, out var updatedStringForAnyLength);

                if (!string.IsNullOrWhiteSpace(delimiter))
                {
                    stringToParseWithoutDelimiter = updatedStringForAnyLength;
                    return delimiter;
                }
                else
                {
                    stringToParseWithoutDelimiter = stringToParse;
                    return "";
                }
            }
        }

        private string ParseSingleCharacterDelimiter(string stringToParse, out string stringToParseWithoutDelimiter)
        {
            // \A == starts with 
            // .  == any character except \n
            // \n == newline
            var regex = new Regex(@"\A//.\n");
            var match = regex.Match(stringToParse);

            if (match.Success)
            {
                stringToParseWithoutDelimiter = regex.Replace(stringToParse, "");
                return match.Groups[0].Value.Substring(2, 1);  // want the 3rd next bc 3rd character is the delimiter. e.g. //<delimiter>\n
            }
            else
            {
                stringToParseWithoutDelimiter = stringToParse;
                return "";
            }
        }

        private string ParseAnyLengthDelimiter(string stringToParse, out string stringToParseWithoutDelimiter)
        {
            // \A   == starts with 
            // (.*) == characters
            // \n   == newline
            var regex = new Regex(@"\A//\[(.*)\]\n");
            var match = regex.Match(stringToParse);

            if (match.Success)
            {
                stringToParseWithoutDelimiter = regex.Replace(stringToParse, "");
                return match.Groups[1].Value;  // want the 3rd next bc 3rd character is the delimiter. e.g. //[<delimiter>]\n
            }
            else
            {
                stringToParseWithoutDelimiter = stringToParse;
                return "";
            }
        }
    }
}

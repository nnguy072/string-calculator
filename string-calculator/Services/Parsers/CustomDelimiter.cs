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

            var customDelimiters = ParseDelimiter(stringToParse, out var stringToParseWithoutDelimiter);
            delimiters.AddRange(customDelimiters);

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

        private List<string> ParseDelimiter(string stringToParse, out string stringToParseWithoutDelimiter)
        {
            var delimiters = new List<string>();

            var singleCharDelimiter = ParseSingleCharacterDelimiter(stringToParse, out var updatedStringForSingleChar);

            if (!string.IsNullOrWhiteSpace(singleCharDelimiter))
            {
                stringToParseWithoutDelimiter = updatedStringForSingleChar;
                delimiters.Add(singleCharDelimiter);

                return delimiters;
            }
            else
            {
                delimiters.AddRange(ParseAnyLengthDelimiter(stringToParse, out var updatedStringForAnyLength));

                if (delimiters.Count > 0)
                {
                    stringToParseWithoutDelimiter = updatedStringForAnyLength;
                    return delimiters;
                }
                else
                {
                    stringToParseWithoutDelimiter = stringToParse;
                    return delimiters;
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

        private List<string> ParseAnyLengthDelimiter(string stringToParse, out string stringToParseWithoutDelimiter)
        {
            var delimiters = new List<string>();

            // \A    == starts with 
            // (.*?) == any characters
            // \n    == newline
            var regex = new Regex(@"\A//(\[.*?\])+\n"); // this should give us back //[<delimiter]*
            var match = regex.Match(stringToParse);

            if (match.Success)
            {
                stringToParseWithoutDelimiter = regex.Replace(stringToParse, "");

                var test = match.Groups[0].Value;
                var matches = Regex.Matches(test, @"\[(.*?)\]");    // this should give us everything that is in this format: [*]

                delimiters.AddRange(matches.Select(o => o.Groups[1].Value).ToList());

                return delimiters;
            }
            else
            {
                stringToParseWithoutDelimiter = stringToParse;
                return delimiters;
            }
        }
    }
}
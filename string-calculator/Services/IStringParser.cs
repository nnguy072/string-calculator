namespace string_calculator.Services
{
    using System.Collections.Generic;

    public interface IStringParser
    {
        IEnumerable<int> Parse(string stringToParse);
    }
}
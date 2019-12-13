namespace string_calculator.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    public class NegativeNumbersException : Exception
    {
        public ImmutableList<int> NegativeNumbers { get; }

        public NegativeNumbersException(IEnumerable<int> negativeNumbers) 
            : base($"Calculator does not support negative numbers. {string.Join(", ", negativeNumbers)} are negative.")
        {
            NegativeNumbers = negativeNumbers.ToImmutableList();
        }
    }
}
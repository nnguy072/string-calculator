namespace string_calculator.Exceptions
{
    using System;

    public class MoreThanTwoNumbersException : Exception
    {
        public MoreThanTwoNumbersException() 
            : base("Calculator currently only supports maximum of two numbers!")
        {

        }
    }
}
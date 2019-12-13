namespace string_calculator.test
{
    using string_calculator.Exceptions;
    using string_calculator.Services.Operations;
    using string_calculator.Services.Parsers;
    using System;
    using Xunit;

    public class CalculatorShould
    {
        [Fact]
        public void AddTwoNumbers()
        {
            var testInput = "1,5000";

            try
            {
                var parsedNumbers = new TwoNumberParser().Parse(testInput);
                var result = new AddOperation().Evaluate(parsedNumbers);

                Assert.Equal(5001, result);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void AddNegativeValues()
        {
            var testInput = "4,-3";

            try
            {
                var parsedNumbers = new TwoNumberParser().Parse(testInput);
                var result = new AddOperation().Evaluate(parsedNumbers);

                Assert.Equal(1, result);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void AddSingleValue()
        {
            var testInput = "4";

            try
            {
                var parsedNumbers = new TwoNumberParser().Parse(testInput);
                var result = new AddOperation().Evaluate(parsedNumbers);

                Assert.Equal(4, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Fact]
        public void AddEmpty()
        {
            var testInput = "";

            try
            {
                var parsedNumbers = new TwoNumberParser().Parse(testInput);
                var result = new AddOperation().Evaluate(parsedNumbers);

                Assert.Equal(0, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Fact]
        public void ConvertInvalidNumbersToZero()
        {
            var testInput = "5,tyty";
            var testInput2 = "tyty,5";

            try
            {
                var parsedNumbers = new TwoNumberParser().Parse(testInput);
                var result = new AddOperation().Evaluate(parsedNumbers);

                var parsedNumbers2 = new TwoNumberParser().Parse(testInput2);
                var result2 = new AddOperation().Evaluate(parsedNumbers2);

                Assert.Equal(5, result);
                Assert.Equal(5, result2);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Fact]
        public void ConvertMissingNumbersToZero()
        {
            var testInput = "5,";
            var testInput2 = ",5";

            try
            {
                var parsedNumbers1 = new TwoNumberParser().Parse(testInput);
                var result1 = new AddOperation().Evaluate(parsedNumbers1);

                var parsedNumbers2 = new TwoNumberParser().Parse(testInput2);
                var result2 = new AddOperation().Evaluate(parsedNumbers2);

                Assert.Equal(5, result1);
                Assert.Equal(5, result2);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Fact]
        public void ThrowExceptionIfMoreThanTwoNumbers()
        {
            var testInput = "1,2,3";

            try
            {
                var parsedNumbers = new TwoNumberParser().Parse(testInput);
                var result = new AddOperation().Evaluate(parsedNumbers);

                Assert.False(true);
            }
            catch (Exception e)
            {
                Assert.True(e is MoreThanTwoNumbersException);
            }
        }
    }
}
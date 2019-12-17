namespace string_calculator.test
{
    using string_calculator.Exceptions;
    using string_calculator.Services.Operations;
    using string_calculator.Services.Parsers;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class CalculatorShould
    {
        #region -- Step 1 --
        [Fact]
        public void AddTwoNumbers()
        {
            var testInput = "1,500";

            try
            {
                var parsedNumbers = new TwoNumberParser().Parse(testInput);
                var result = new AddOperation().Evaluate(parsedNumbers);

                Assert.Equal(501, result);
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
        public void ConvertNonNumbersToZero()
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
        #endregion

        #region -- Step 2 --
        [Fact]
        public void AddMoreThanTwoNumbers()
        {
            var testInput = "1,2,3,4,5,6,7,8,9,10,11,12";

            try
            {
                var parsedNumbers = new UnlimitedNumberParser().Parse(testInput);
                var result = new AddOperation().Evaluate(parsedNumbers);

                Assert.Equal(78, result);
            }
            catch (Exception)
            {
                Assert.False(true);
            }
        }
        #endregion

        #region -- Step 3 --
        [Fact]
        public void AllowNewlineAsDelimiter()
        {
            var testInput = "1\n2,3";

            try
            {
                var parsedNumbers = new UnlimitedNumberParser().Parse(testInput);
                var result = new AddOperation().Evaluate(parsedNumbers);

                Assert.Equal(6, result);
            }
            catch (Exception)
            {
                Assert.False(true);
            }
        }

        [Fact]
        public void AllowNewlineAsDelimiterEmptyString()
        {
            var testInput = "\n";

            try
            {
                var parsedNumbers = new UnlimitedNumberParser().Parse(testInput);
                var result = new AddOperation().Evaluate(parsedNumbers);

                Assert.Equal(0, result);
            }
            catch (Exception)
            {
                Assert.False(true);
            }
        }
        #endregion

        #region -- Step 4 --
        [Fact]
        public void ShouldDenyNegativeNumbersByThrowingException()
        {
            var testInput = "4,-3";

            try
            {
                var parsedNumbers = new UnlimitedNumberParser().Parse(testInput);
                var result = new AddOperation().Evaluate(parsedNumbers);

                Assert.True(false);
            }
            catch (NegativeNumbersException e)
            {
                Assert.Contains(-3, e.NegativeNumbers);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
        #endregion

        #region -- step 5 --
        [Fact]
        public void ConvertNumbersGreaterThan1000ToZero()
        {
            var testInput = "2,1001,6";

            try
            {
                var parsedNumbers = new UnlimitedNumberParser().Parse(testInput);
                var result = new AddOperation().Evaluate(parsedNumbers);

                Assert.Equal(8, result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
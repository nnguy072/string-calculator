namespace string_calculator.Services
{
    public class Calculator : ICalculator
    {
        private readonly IStringParser _stringParser;
        private readonly IOperation _operation;

        public Calculator(IStringParser stringParser, IOperation operation)
        {
            _stringParser = stringParser;
            _operation = operation;
        }

        public int Calculate(string userInput)
        {
            var parsedNumbers = _stringParser.Parse(userInput);

            return _operation.Evaluate(parsedNumbers);
        }
    }
}
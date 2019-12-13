namespace string_calculator
{
    using string_calculator.Services.Operations;
    using string_calculator.Services.Parsers;
    using System;

    class Program
    {
        private const string WelcomePrompt = "Welcome to the string calculator. The operations this calcalutor current supports are: adding.";
        private const string InputPrompt = "Enter (at most) two comma-separated, integers. (e.g. 20; 1,5000)";
        private const string ResultPrompt = "The result is: ";

        static void Main(string[] args)
        {
            Console.WriteLine(WelcomePrompt);

            while (true)
            {
                Console.WriteLine(InputPrompt);

                var userInput = Console.ReadLine();

                try
                {
                    var parsedNumbers = new TwoNumberParser().Parse(userInput);
                    var result = new AddOperation().Evaluate(parsedNumbers);

                    Console.WriteLine($"{ResultPrompt}{result}\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
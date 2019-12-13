namespace string_calculator
{
    using string_calculator.Exceptions;
    using string_calculator.Services;
    using System;

    class Program
    {
        private const string WelcomePrompt = "Welcome to the string calculator. The operations this calcalutor current supports are: adding.";
        private const string InputPrompt = "Enter comma-separated integers. (e.g. 20; 1,5000)";
        private const string ResultPrompt = "The result is: ";

        static void Main(string[] _)
        {
            var calculator = Bootstrapper.ServiceProvider.GetService(typeof(ICalculator)) as Calculator;

            Console.WriteLine(WelcomePrompt);

            while (true)
            {
                Console.WriteLine(InputPrompt);

                var userInput = Console.ReadLine();

                try
                {
                    var result = calculator.Calculate(userInput);

                    Console.WriteLine($"{ResultPrompt}{result}\n");
                }
                catch (NegativeNumbersException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
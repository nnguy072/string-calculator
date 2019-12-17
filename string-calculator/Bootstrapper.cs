namespace string_calculator
{
    using Microsoft.Extensions.DependencyInjection;
    using string_calculator.Services;
    using string_calculator.Services.Operations;
    using string_calculator.Services.Parsers;
    using System;

    public static class Bootstrapper
    {
        public static readonly IServiceProvider ServiceProvider;

        static Bootstrapper()
        {
            var services = new ServiceCollection();

            RegisterWithIoCContainer(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private static void RegisterWithIoCContainer(IServiceCollection services)
        {
            services.AddTransient<IOperation, AddOperation>();
            services.AddTransient<IStringParser, CustomDelimiterSingleCharacter>();
            services.AddTransient<ICalculator, Calculator>();
        }
    }
}

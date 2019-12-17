namespace string_calculator.test
{
    using Microsoft.Extensions.DependencyInjection;
    using string_calculator.Services;
    using string_calculator.Services.Operations;
    using string_calculator.Services.Parsers;
    using System;

    public static class TestBootstrapper
    {
        public static readonly IServiceProvider ServiceProvider;

        static TestBootstrapper()
        {
            var services = new ServiceCollection();

            RegisterWithIoCContainer(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private static void RegisterWithIoCContainer(IServiceCollection services)
        {
            services.AddTransient<IOperation, AddOperation>();
            services.AddTransient<IStringParser, CustomDelimiter>();
            services.AddTransient<ICalculator, Calculator>();
        }
    }
}
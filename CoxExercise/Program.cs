using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoxExercise
{
    class Program
    {
        static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            RegisterServices();
            Run();

            Console.ReadKey();
        }


        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddScoped<IConfiguration>(_ => new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build());
            services.AddScoped<ITimeTracker, TimeTracker>();
            services.AddSingleton<ICoxHttpClient, CoxHttpClient>();
            services.AddScoped<ICoxAPI, CoxAPI>();
            services.AddScoped<ApiExercise>();
            serviceProvider = services.BuildServiceProvider();
        }

        private static void Run()
        {
            var service =  serviceProvider.GetService<ApiExercise>();
            service.Notify += Service_Notify;
            service.Run();
        }

        private static void Service_Notify(object sender, string e)
        {
            Console.WriteLine(e);
        }
    }
}

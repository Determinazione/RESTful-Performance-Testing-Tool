using System;
using Microsoft.Extensions.DependencyInjection;

using PerformanceTesting.WebRequest;
using PerformanceTesting.Service.Presentation;
using PerformanceTesting.Service.ClientSimulator;

namespace PerformanceTesting
{
    class Program
    {
        public static int TasksCount = 1000;

        static void Main(string[] args)
        {
            Console.WriteLine("---Welcome to Performance Testing Tool---");
            var services = new ServiceCollection()
                .AddServices();

            var provider = services.BuildServiceProvider();
            for(int i = 0; i < TasksCount; ++i)
            {
                provider.GetService<HttpWebAPIService>();
            }
            Console.ReadLine();
            provider.GetService<IMonitor>().OutputReport();
            Console.ReadLine();
        }
    }

    static class DependencyInjectionMethods
    {
        /// <summary>
        /// Inject required services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IMonitor, Monitor>();
            services.AddSingleton<IWebRequestSender>();
            services.AddSingleton<IWebRequestSender, WebRequestSender>();
            return services;
        }
    }
}
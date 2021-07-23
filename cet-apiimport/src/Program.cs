using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// using NLog.Web;
// using NLog;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;

namespace cet_apiimport
{
    class Program
    {
		static ServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
			
			RegisterServices();

 			IServiceScope scope = _serviceProvider.CreateScope();

			scope.ServiceProvider.GetRequiredService<ConsoleApplication>().Run();

			DisposeServices();
        }

		private static void RegisterServices()
		{
			var services = new ServiceCollection();
            
			var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

			
			services.AddSingleton<IConfiguration>(configuration);
			//.AddFeatureManagement().AddFeatureFilter<PercentageFilter>().AddFeatureFilter<AccountIdFilter>();
			// services.AddSingleton<ICustomer, Customer>();
			services.AddSingleton<ConsoleApplication>();            

			services.AddLogging(builder => {

				builder.SetMinimumLevel(LogLevel.Trace);
				
                builder.AddNLog("nlog.config");

			});

			_serviceProvider = services.BuildServiceProvider(true);
		
		}

		private static void DisposeServices()
		{
		if (_serviceProvider == null)
		{
			return;
		}
		if (_serviceProvider is IDisposable)
		{
			((IDisposable)_serviceProvider).Dispose();
		}
		}		
    }
}

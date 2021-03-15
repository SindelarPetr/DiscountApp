using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscountApp.BusinessLogic;
using DiscountApp.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountApp.Tests
{
	public static class TestManager
	{
		public static IServiceProvider InitializeServices(
			Action<IServiceCollection>? registerTestServices = null)
		{
			var serviceCollection = new ServiceCollection();

			serviceCollection.AddBusinessLogic();

			registerTestServices?.Invoke(serviceCollection);

			var serviceProvider = serviceCollection.BuildServiceProvider();

			return serviceProvider;
		}
	}
}
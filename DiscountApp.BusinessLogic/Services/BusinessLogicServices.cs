using DiscountApp.BusinessLogic.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountApp.BusinessLogic.Services
{
	public static class BusinessLogicServices
	{
		public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection)
			=> serviceCollection
				.AddSingleton<IDiscountService, DiscountService>()
				.AddScoped<ICartService, CartService>();
	}
}
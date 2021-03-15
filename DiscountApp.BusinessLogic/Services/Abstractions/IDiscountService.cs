using DiscountApp.BusinessLogic.Domain;

namespace DiscountApp.BusinessLogic.Services.Abstractions
{
	public interface IDiscountService
	{
		void AddDiscount(Discount discount);

		void RemoveDiscount(Discount discount);

		double CalculatePrice(Item item, int amount);
	}
}
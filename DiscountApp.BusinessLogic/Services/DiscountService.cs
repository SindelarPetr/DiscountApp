using System;
using System.Collections.Generic;
using DiscountApp.BusinessLogic.Domain;
using DiscountApp.BusinessLogic.Services.Abstractions;

namespace DiscountApp.BusinessLogic.Services
{
	internal class DiscountService : IDiscountService
	{
		private readonly IDictionary<Guid, Discount> _discounts;

		public DiscountService()
		{
			_discounts = new Dictionary<Guid, Discount>();
		}

		public void AddDiscount(Discount discount) => _discounts[discount.ItemType] = discount;

		public void RemoveDiscount(Discount discount) => _discounts.Remove(discount.ItemType);

		public double CalculatePrice(Item item, int amount)
		{
			if (!_discounts.TryGetValue(item.Type, out var discount))
				return amount * item.Price;

			// The amount of items, for which the discount is not applied
			var notDiscountedAmount = amount % discount.Amount;

			// The amount of items, for which the discount is applied
			var discountedAmount = amount - notDiscountedAmount;

			var notDiscountedPrice = notDiscountedAmount * item.Price;
			var discountedPrice = discountedAmount / discount.Amount * discount.Price;

			return notDiscountedPrice + discountedPrice;
		}
	}
}
using System;

namespace DiscountApp.BusinessLogic.Domain
{
	public class Discount
	{
		public Guid ItemType { get; }
		public int Amount { get; }
		public double Price { get; }

		public Discount(Guid itemType, int amount, double price)
		{
			ItemType = itemType;
			Amount = amount;
			Price = price;
		}
	}
}
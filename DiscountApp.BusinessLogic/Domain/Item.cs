using System;

namespace DiscountApp.BusinessLogic.Domain
{
	public class Item
	{
		public Guid Type { get; }
		public string Name { get; }
		public double Price { get; }

		public Item(double price, string name, Guid type)
		{
			Price = price;
			Name = name;
			Type = type;
		}
	}
}
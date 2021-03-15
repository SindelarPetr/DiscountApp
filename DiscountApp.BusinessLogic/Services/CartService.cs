using System.Collections.Generic;
using System.Linq;
using DiscountApp.BusinessLogic.Domain;
using DiscountApp.BusinessLogic.Services.Abstractions;

namespace DiscountApp.BusinessLogic.Services
{
	public class CartService : ICartService
	{
		private readonly IDiscountService _discountService;
		private readonly ICollection<Item> _items;

		public CartService(IDiscountService discountService)
		{
			_discountService = discountService;
			_items = new List<Item>();
		}

		public IEnumerable<Item> GetItems() => _items;

		public void Add(Item item) => _items.Add(item);

		public void Remove(Item item) => _items.Remove(item);

		public double GetTotal() => _items
			.OrderBy(i => i.Type)
			.GroupBy(i => i.Type)
			.Select(g => (Item: g.First(), Count: g.Count()))
			.Sum(i => _discountService.CalculatePrice(i.Item, i.Count));
	}
}
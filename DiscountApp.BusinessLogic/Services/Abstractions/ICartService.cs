using System.Collections.Generic;
using DiscountApp.BusinessLogic.Domain;

namespace DiscountApp.BusinessLogic.Services.Abstractions
{
	public interface ICartService
	{
		public IEnumerable<Item> GetItems();

		void Add(Item item);

		void Remove(Item item);

		double GetTotal();
	}
}
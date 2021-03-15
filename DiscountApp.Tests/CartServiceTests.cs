using System;
using System.Linq;
using System.Transactions;
using DiscountApp.BusinessLogic;
using DiscountApp.BusinessLogic.Domain;
using DiscountApp.BusinessLogic.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiscountApp.Tests
{
	[TestClass]
	public class CartServiceTests
	{
		[TestClass]
		public class Add
		{
			[TestMethod]
			public void CanAddSingleItem()
			{
				// Arrange
				var serviceProvider = TestManager.InitializeServices();
				var cartService = serviceProvider.GetRequiredService<ICartService>();
				var item = new Item(3, "Banan", Guid.NewGuid());

				// Act
				cartService.Add(item);

				// Assert
				var actualItems = cartService.GetItems().ToArray();
				Assert.AreEqual(1, actualItems.Length);
				Assert.AreEqual(item, actualItems.First());
			}
		}

		[TestClass]
		public class Remove
		{
			[TestMethod]
			public void CanRemoveSingleItem()
			{
				// Arrange
				var serviceProvider = TestManager.InitializeServices();
				var cartService = serviceProvider.GetRequiredService<ICartService>();
				var item = new Item(3, "Banana", Guid.NewGuid());

				// Act
				cartService.Add(item);
				cartService.Remove(item);

				// Assert
				var actualItems = cartService.GetItems().ToArray();
				Assert.AreEqual(0, actualItems.Length);
			}
		}

		// For simple item
		// For multiple items
		// For a discount
		// For multiple items and a discount
		[TestClass]
		public class GetTotal
		{
			[TestMethod]
			public void ReturnsZeroForNoItems()
			{
				// Arrange
				var serviceProvider = TestManager.InitializeServices();
				var cartService = serviceProvider.GetRequiredService<ICartService>();
				var expectedTotal = 0;

				// Act
				var actualTotal = cartService.GetTotal();

				// Assert
				Assert.AreEqual(expectedTotal, actualTotal);
			}

			[TestMethod]
			public void ReturnsRightAmountForMultipleNonDiscountedItems()
			{
				// Arrange
				var serviceProvider = TestManager.InitializeServices();
				var cartService = serviceProvider.GetRequiredService<ICartService>();
				var item1 = new Item(1, "Apple", Guid.NewGuid());
				var item2 = new Item(10, "Banan", Guid.NewGuid());
				var item3 = new Item(100, "Peach", Guid.NewGuid());
				cartService.Add(item1);
				cartService.Add(item2);
				cartService.Add(item1);
				cartService.Add(item3);
				cartService.Add(item2);
				cartService.Add(item1);
				var expectedTotal = 123;

				// Act
				var actualTotal = cartService.GetTotal();

				// Assert
				Assert.AreEqual(expectedTotal, actualTotal);
			}

			[TestMethod]
			public void ReturnsRightAmountForMultipleDiscountedItems()
			{
				// Arrange
				var serviceProvider = TestManager.InitializeServices();
				var cartService = serviceProvider.GetRequiredService<ICartService>();
				var discountService = serviceProvider.GetRequiredService<IDiscountService>();
				var item1 = new Item(3, "Apple", Guid.NewGuid());
				var item2 = new Item(30, "Banan", Guid.NewGuid());
				var item3 = new Item(300, "Peach", Guid.NewGuid());
				cartService.Add(item1);
				cartService.Add(item1);
				cartService.Add(item1);
				cartService.Add(item1);
				cartService.Add(item2);
				cartService.Add(item2);
				cartService.Add(item3);
				cartService.Add(item3);
				cartService.Add(item3);
				var discount1 = new Discount(item1.Type, 2, 4);
				var discount2 = new Discount(item2.Type, 2, 50);
				var discount3 = new Discount(item3.Type, 3, 700);
				discountService.AddDiscount(discount1);
				discountService.AddDiscount(discount2);
				discountService.AddDiscount(discount3);

				var expectedTotal = 758;

				// Act
				var actualTotal = cartService.GetTotal();

				// Assert
				Assert.AreEqual(expectedTotal, actualTotal);
			}

			[TestMethod]
			public void ReturnsRightAmountForMultipleDiscountedItemsWithMixedAddOrder()
			{
				// Arrange
				var serviceProvider = TestManager.InitializeServices();
				var cartService = serviceProvider.GetRequiredService<ICartService>();
				var discountService = serviceProvider.GetRequiredService<IDiscountService>();
				var item1 = new Item(3, "Apple", Guid.NewGuid());
				var item2 = new Item(30, "Banan", Guid.NewGuid());
				var item3 = new Item(300, "Peach", Guid.NewGuid());
				cartService.Add(item1);
				cartService.Add(item1);
				cartService.Add(item3);
				cartService.Add(item1);
				cartService.Add(item2);
				cartService.Add(item3);
				cartService.Add(item1);
				cartService.Add(item2);
				cartService.Add(item3);
				var discount1 = new Discount(item1.Type, 2, 4);
				var discount2 = new Discount(item2.Type, 2, 50);
				var discount3 = new Discount(item3.Type, 3, 700);
				discountService.AddDiscount(discount1);
				discountService.AddDiscount(discount2);
				discountService.AddDiscount(discount3);

				var expectedTotal = 758;

				// Act
				var actualTotal = cartService.GetTotal();

				// Assert
				Assert.AreEqual(expectedTotal, actualTotal);
			}

			[TestMethod]
			public void ReturnsRightAmountForMultipleDiscountedAndNotDiscountedItems()
			{
				// Arrange
				var serviceProvider = TestManager.InitializeServices();
				var cartService = serviceProvider.GetRequiredService<ICartService>();
				var discountService = serviceProvider.GetRequiredService<IDiscountService>();
				var item1 = new Item(3, "Apple", Guid.NewGuid());
				var item2 = new Item(30, "Banan", Guid.NewGuid());
				var item3 = new Item(300, "Peach", Guid.NewGuid());
				cartService.Add(item1);
				cartService.Add(item3);
				cartService.Add(item1);
				cartService.Add(item1);
				cartService.Add(item1);
				cartService.Add(item2);
				cartService.Add(item3);
				cartService.Add(item2);
				cartService.Add(item1);
				cartService.Add(item3);
				var discount1 = new Discount(item1.Type, 2, 3);
				var discount2 = new Discount(item2.Type, 2, 50);
				var discount3 = new Discount(item3.Type, 2, 500);
				discountService.AddDiscount(discount1);
				discountService.AddDiscount(discount2);
				discountService.AddDiscount(discount3);

				var expectedTotal = 859;

				// Act
				var actualTotal = cartService.GetTotal();

				// Assert
				Assert.AreEqual(expectedTotal, actualTotal);
			}
		}
	}
}
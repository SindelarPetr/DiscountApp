using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscountApp.BusinessLogic.Domain;
using DiscountApp.BusinessLogic.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiscountApp.Tests
{
	[TestClass]
	public class DiscountServiceTests
	{
		[TestClass]
		public class CalculatePrice
		{
			[TestMethod]
			public void ReturnsRegularPriceIfThereIsNoDiscount()
			{
				// Arrange
				var serviceProvider = TestManager.InitializeServices();
				var discountService = serviceProvider.GetRequiredService<IDiscountService>();
				var item = new Item(3, "Banan", Guid.NewGuid());
				var amount = 5;
				var expectedPrice = 15;

				// Act
				var actualPrice = discountService.CalculatePrice(item, amount);

				// Assert
				Assert.AreEqual(expectedPrice, actualPrice);
			}

			[TestMethod]
			public void ReturnsDiscountedPriceIfThereIsDiscount()
			{
				// Arrange
				var serviceProvider = TestManager.InitializeServices();
				var discountService = serviceProvider.GetRequiredService<IDiscountService>();
				var item = new Item(3, "Banan", Guid.NewGuid());
				var amount = 5;
				var discount = new Discount(item.Type, amount, 10);
				discountService.AddDiscount(discount);
				var expectedPrice = 10;

				// Act
				var actualPrice = discountService.CalculatePrice(item, amount);

				// Assert
				Assert.AreEqual(expectedPrice, actualPrice);
			}

			[TestMethod]
			public void ReturnsMixOfDiscountedAndRegularPriceIfThereIsDiscountForPartOfTheAmountOfTheItem()
			{
				// Arrange
				var serviceProvider = TestManager.InitializeServices();
				var discountService = serviceProvider.GetRequiredService<IDiscountService>();
				var item = new Item(price: 6, "Banan", Guid.NewGuid());
				var discount = new Discount(item.Type, amount: 3, price: 10);
				var expectedPrice = 16;
				discountService.AddDiscount(discount);

				// Act
				var actualPrice = discountService.CalculatePrice(item, 4);

				// Assert
				Assert.AreEqual(expectedPrice, actualPrice);
			}
		}
	}
}
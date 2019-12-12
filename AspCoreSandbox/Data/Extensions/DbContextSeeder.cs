using AspCoreSandbox.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AspCoreSandbox.Data.Extensions
{
    public class DbContextSeeder
    {
        private readonly StoreDbContext _storeDbContext;
        private readonly UserManager<StoreUser> _userManager;

        public DbContextSeeder(StoreDbContext storeDbContext, UserManager<StoreUser> userManager)
        {
            _storeDbContext = storeDbContext;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            _storeDbContext.Database.EnsureCreated();

            var user = await _userManager.FindByEmailAsync("user@user.pl");

            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Email = "user@user.pl",
                    UserName = "user@user.pl"
                };

                var result = await _userManager.CreateAsync(user, "#Pp123123");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user");
                }
            }

            if (_storeDbContext.Products.Any()) return;

            var products = new List<Product>()
            {
                new Product() { Title = "Termofor złoty", Category = "Termofory", CountryOfOrigin = "Poland", DateOfProduceStart = DateTime.Now.AddYears(-3), DateOfProduceEnd = DateTime.Now, Description = "Super termofor", Price = 49.99M, Size = "Small"},
                new Product() { Title = "Termofor chiński", Category = "Termofory", CountryOfOrigin = "China", DateOfProduceStart = DateTime.Now.AddYears(-1), DateOfProduceEnd = DateTime.Now, Description = "Super termofor 2", Price = 39.99M, Size = "Medium"},
                new Product() { Title = "Termofor różowy", Category = "Termofory", CountryOfOrigin = "Poland", DateOfProduceStart = DateTime.Now.AddYears(-2), DateOfProduceEnd = DateTime.Now, Description = "Super termofor 3", Price = 25.99M, Size = "Big"},
                new Product() { Title = "Termofor azjatycki", Category = "Termofory", CountryOfOrigin = "Poland", DateOfProduceStart = DateTime.Now.AddYears(-3), DateOfProduceEnd = DateTime.Now, Description = "Super termofor 11", Price = 29.99M, Size = "Small"},
                new Product() { Title = "Termofor wietnamski", Category = "Termofory", CountryOfOrigin = "China", DateOfProduceStart = DateTime.Now.AddYears(-1), DateOfProduceEnd = DateTime.Now, Description = "Super termofor 5", Price = 59.59M, Size = "Small"},
                new Product() { Title = "Termofor różowy 2", Category = "Termofory", CountryOfOrigin = "Poland", DateOfProduceStart = DateTime.Now.AddYears(-2), DateOfProduceEnd = DateTime.Now, Description = "Super termofor 6", Price = 19.99M, Size = "Big"},
                new Product() { Title = "Termofor kolorowy", Category = "Termofory", CountryOfOrigin = "Poland", DateOfProduceStart = DateTime.Now.AddYears(-3), DateOfProduceEnd = DateTime.Now, Description = "Super termofor 7", Price = 69.99M, Size = "Medium"},
                new Product() { Title = "Termofor biały", Category = "Termofory", CountryOfOrigin = "China", DateOfProduceStart = DateTime.Now.AddYears(-1), DateOfProduceEnd = DateTime.Now, Description = "Super termofor 8", Price = 29.19M, Size = "Medium"},
                new Product() { Title = "Termofor różowy", Category = "Termofory", CountryOfOrigin = "Poland", DateOfProduceStart = DateTime.Now.AddYears(-2), DateOfProduceEnd = DateTime.Now, Description = "Super termofor 9", Price = 19.39M, Size = "Big"}
            };

            _storeDbContext.AddRange(products);
            _storeDbContext.SaveChanges();

            if (_storeDbContext.Orders.Any()) return;

            var order = new Order() { OrderDate = DateTime.Now.AddDays(-34), OrderNumber = "13/2019" };
            if (order != null)
            {
                order.User = user;
                order.Items = new List<OrderItem>() { new OrderItem()
                {
                    Product = _storeDbContext.Products.First(),
                    Quantity = 5,
                    UnitPrice = _storeDbContext.Products.First().Price
                } };
            }

            _storeDbContext.AddRange(products);
            _storeDbContext.Add(order);
            _storeDbContext.SaveChanges();
        }
    }
}

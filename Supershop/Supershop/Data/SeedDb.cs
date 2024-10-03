using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supershop.Data.Entities;
using Supershop.Helpers;


namespace Supershop.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;
        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync ()
        {
           await _context.Database.MigrateAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");

            if (!_context.Countries.Any())
            {
                var cities = new List<City>()
                {
                    new City { Name = "Lisboa" },
                    new City { Name = "Porto" },
                    new City { Name = "Faro" }
                };
                _context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Portugal"
                });

                await _context.SaveChangesAsync();
            }


            var user = await _userHelper.GetUserByEmailAsync("hugosb9@gmail.com");
            if ( user == null ) 
            {
                user = new User
                {
                    FirstName = "Hugo",
                    LastName = "Sousa",
                    Email= "hugosb9@gmail.com",
                    UserName= "hugosb9@gmail.com",
                    PhoneNumber="123456789",
                    Address = "Rua de Coimbra",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success) 
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var IsInRole = await _userHelper.IsUserInRoleAsyc(user, "Admin");
            if (!IsInRole) 
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            if (!_context.Products.Any())
            {
                AddProduct("Iphone X", user);
                AddProduct("Magic Mouse", user);
                AddProduct("Iwatch Series 4", user);
                AddProduct("Ipad mini", user);

                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
           _context.Products.Add(new Product
           { 
              Name =name,    
              Price =_random.Next(1000),
              IsAvailable = true,
              stock = _random.Next(100),
              user = user
           });
        }
    }
}

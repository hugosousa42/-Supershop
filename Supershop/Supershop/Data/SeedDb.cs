using System;
using System.Linq;
using System.Threading.Tasks;
using Supershop.Data.Entities;


namespace Supershop.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;
        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedAsync ()
        {
           await _context.Database.EnsureCreatedAsync();

            if (!_context.Products.Any())
            {
                AddProduct("Iphone X");
                AddProduct("Magic Mouse");
                AddProduct("Iwatch Series 4");
                AddProduct("Ipad mini");

                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
           _context.Products.Add(new Product
           { 
              Nome =name,    
              Price =_random.Next(1000),
              IsAvailable = true,
              stock = _random.Next(100),
           });
        }
    }
}

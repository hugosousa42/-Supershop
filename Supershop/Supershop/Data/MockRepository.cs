using Supershop.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supershop.Data
{
    public class MockRepository : IRepository
    {
        public void AddProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetProucts()
        {
            var products = new List<Product>();
            products.Add(new Product {Id = 1, Nome = "um", Price = 10 });
            products.Add(new Product {Id = 2, Nome = "dois", Price = 20 });
            products.Add(new Product {Id = 3, Nome = "três", Price = 30 });
            products.Add(new Product {Id = 4, Nome = "quatro", Price = 40 });
            products.Add(new Product {Id = 5, Nome = "cinco", Price = 50 });

            return products;
        }

        public bool ProductExists(int id)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveProduct(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new System.NotImplementedException();
        }
    }
}

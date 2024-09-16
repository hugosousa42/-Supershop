using Supershop.Data.Entities;
using System.Linq;

namespace Supershop.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public IQueryable GetAllWithUsers();
    }
}

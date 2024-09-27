using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Supershop.Data.Entities;

namespace Supershop.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboProducts();
    }
}

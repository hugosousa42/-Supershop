using System;
using Supershop.Data.Entities;
using Supershop.Models;

namespace Supershop.Helpers
{
    public interface IConverterHelper
    {
        Product ToProduct(ProductViewModel model, string imageUrl, bool IsNew);

        ProductViewModel ToProductViewModel(Product product);
    }
}

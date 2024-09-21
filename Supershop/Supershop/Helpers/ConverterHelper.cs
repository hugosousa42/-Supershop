﻿using System;
using System.IO;
using Supershop.Data.Entities;
using Supershop.Models;

namespace Supershop.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Product ToProduct(ProductViewModel model, string path, bool IsNew)
        {
            return new Product
            {
                Id =IsNew? 0 : model.Id,
                Name = model.Name,
                Price = model.Price,
                ImageUrl = path,
                LastPurchase = model.LastPurchase,
                LastSale = model.LastSale,
                IsAvailable = model.IsAvailable,
                stock = model.stock,
                user = model.user
            };
        }

        public ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                IsAvailable = product.IsAvailable,
                stock = product.stock,
                user = product.user
            };
        }
    }
}

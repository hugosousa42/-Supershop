using System.ComponentModel.DataAnnotations;
using Supershop.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Supershop.Models
{
    public class ProductViewModel : Product
    {
        [Display(Name="Image")]
        public IFormFile ImageFile { get; set; }
    }
}

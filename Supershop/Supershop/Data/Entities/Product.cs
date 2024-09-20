using Supershop.Migrations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Supershop.Data.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The filed {0} can contain {1} characters lenght.")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }

        [Display(Name = "Last Sale")]
        public DateTime? LastSale { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = false)]
        public double stock { get; set; }

        public User user { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://supershop2024.azurewebsites.net/images/noimage.jpg"
            : $"https://blobstoragecinel.blob.core.windows.net/products/{ImageId}";

    }
}


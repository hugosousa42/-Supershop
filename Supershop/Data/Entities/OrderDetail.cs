using System.ComponentModel.DataAnnotations;

namespace Supershop.Data.Entities
{
    public class OrderDetail : IEntity
    {
        public int Id { get; set; }


        [Required]
        public Product Product { get; set; }


        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }


        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = false)]
        public double Quantity { get; set; }

        public decimal Value => Price * (decimal)Quantity;
    }
}

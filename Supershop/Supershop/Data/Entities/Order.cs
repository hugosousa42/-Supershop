﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Features;

namespace Supershop.Data.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }


        [Required]
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime OrderDate { get; set; }

       
        [Display(Name = "Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? DeliveryDate { get; set; }

        [Required]
        public User User { get; set; }

     
        public IEnumerable<OrderDetail> Items { get; set; }

       
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Lines => Items == null ? 0 : Items.Count();


        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Quantity => Items == null ? 0 : Items.Sum(i => i.Quantity);

       
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Value => Items == null ? 0 : Items.Sum(i => i.Value);


        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? OrderDateLocal => this.OrderDate == null ? null : this.OrderDate.ToLocalTime(); 
    }
}

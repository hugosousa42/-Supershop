﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Supershop.Data.Entities
{
    public class Country : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "O campo Nome pode ter no máximo 50 caracteres.")]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }

        [Display(Name = "Number of cities")]
        public int NumberCities => Cities == null ? 0 : Cities.Count;
    }

}

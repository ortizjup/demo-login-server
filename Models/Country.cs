using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Models
{
    public class Country : BaseEntity
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string CountryCode { get; set; }
    }
}

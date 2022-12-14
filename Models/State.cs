using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Models
{
    public class State : BaseEntity
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string StateCode { get; set; }
    }
}

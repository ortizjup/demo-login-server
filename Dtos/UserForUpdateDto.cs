using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Dtos
{
    public class UserForUpdateDto
    {
        [Required(ErrorMessage = "User id was not provided")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Introduction can no be empty")]
        public string Introduction { get; set; }

        [Required(ErrorMessage = "LookingFor field can no be empty")]
        public string LookinFor { get; set; }

        [Required(ErrorMessage = "Interest can no be empty")]
        public string Interests { get; set; }

        [Required(ErrorMessage = "You must select an state")]
        public State State { get; set; }

        [Required(ErrorMessage = "You must select a country")]
        public Country Country { get; set; }

        [Required(ErrorMessage = "You must select a city")]
        public City City { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswrodSalt { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public string Adress2 { get; set; }

        [Required]
        public string Zip { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public int StateId { get; set; }
        public State State { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}

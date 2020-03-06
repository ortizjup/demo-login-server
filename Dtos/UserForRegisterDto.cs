using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "You must provide a gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "You must provide a username")]
       //[StringLength(100, MinimumLength = 4, ErrorMessage = "Username max length allow is between 4 and 100 characters")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "You must provide a nick name")]
       // [StringLength(100, MinimumLength = 4, ErrorMessage = "Nick name max length allow is between 4 and 100 characters")]
        public string KnownAs { get; set; }

        [Required(ErrorMessage = "You must provide a date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Password must be provided")]
      //  [StringLength(50, MinimumLength = 8, ErrorMessage = "Password max length allow is 50 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email must be provided")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone must be provided")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address must be provided")]
        public string Adress { get; set; }

        public string Adress2 { get; set; }

        [Required(ErrorMessage = "Country must be provided")]
        public Country Country { get; set; }

        [Required(ErrorMessage = "City must be provided")]
        public City City { get; set; }

        [Required(ErrorMessage = "State must be provided")]
        public State State { get; set; }

        [Required(ErrorMessage = "Zip code must be provided")]
        public string Zip { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}

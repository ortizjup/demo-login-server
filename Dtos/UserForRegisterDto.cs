using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "You must provide a username")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Username max length allow is between 4 and 100 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password must be provided")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password max length allow is 50 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email must be provided")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone must be provided")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address must be provided")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Adress2 must be provdided")]
        public string Adress2 { get; set; }

        [Required(ErrorMessage = "City must be provided")]
        public int City { get; set; }

        [Required(ErrorMessage = "State must be provided")]
        public int State { get; set; }

        [Required(ErrorMessage = "Zip code must be provided")]
        public string Zip { get; set; }
    }
}

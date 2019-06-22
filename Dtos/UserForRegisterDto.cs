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
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Password max length allow is 50 characters")]
        public string Password { get; set; }
    }
}

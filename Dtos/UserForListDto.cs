using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Gender { get; set; }

        public string Interests { get; set; }

        public int Age { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActived { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Adress { get; set; }

        public string Adress2 { get; set; }

        public string Zip { get; set; }

        public City City { get; set; }

        public State State { get; set; }

        public Country Country { get; set; }

        public string PhotoUrl { get; set; }
    }
}

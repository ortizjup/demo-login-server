using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Models
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswrodSalt { get; set; }
    }
}

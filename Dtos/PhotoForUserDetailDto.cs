using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Dtos
{
    public class PhotoForUserDetailDto
    {
        public int id { get; set; }
        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set; }

        public bool ShowPhoto { get; set; }

        public int UserId { get; set; }
    }
}

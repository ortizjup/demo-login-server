using System;

namespace DatingApp.API.Models
{
    public class Photo : BaseEntity
    {
        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set; }

        public bool ShowPhoto { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

    }
}
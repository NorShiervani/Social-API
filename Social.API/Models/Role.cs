using System.Collections.Generic;

namespace Social.API.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rights { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
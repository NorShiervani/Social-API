using System.Collections.Generic;
using Social.API.Models;

namespace Social.API.Dtos
{
    public class RoleForReturnDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
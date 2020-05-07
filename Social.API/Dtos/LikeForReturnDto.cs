using Social.API.Models;

namespace Social.API.Dtos
{
    public class LikeForReturnDto
    {
        public int Id { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}

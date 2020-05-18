using Social.API.Models;

namespace Social.API.Dtos
{
    public class LikeForReturnDto
    {
        public int Id { get; set; }
        public PostForReturnDto Post { get; set; }
        public UserForReturnDto User { get; set; }
    }
}

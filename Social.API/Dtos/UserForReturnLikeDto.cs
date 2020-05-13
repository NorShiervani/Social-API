namespace Social.API.Dtos
{
    public class UserForReturnLikeDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool IsSuspended { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
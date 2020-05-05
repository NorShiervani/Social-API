namespace Social.API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserConversatorId { get; set; }
    }
}
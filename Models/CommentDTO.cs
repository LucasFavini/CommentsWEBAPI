namespace CommentsApp.Models
{
    public class CommentDTO
    {
        public int UserId { get; set; }
        public string? CommentValue { get; set; }
        public DateTime CommentDateTime { get; set; }
    }
}

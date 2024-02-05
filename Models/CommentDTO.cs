using CommentsApp.Entities;

namespace CommentsApp.Models
{
    public class CommentDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int CommentId { get; set; }
        public string? CommentValue { get; set; }
        public DateTime CommentDateTime { get; set; }
        public IEnumerable<SubComment> SubComments { get; set; }
    }
}

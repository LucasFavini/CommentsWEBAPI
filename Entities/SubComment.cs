using System.ComponentModel.DataAnnotations.Schema;

namespace CommentsApp.Entities
{
    [Table("SubComments")]
    public class SubComment
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public string? UserName { get; set; }
        public string? UserTag { get; set; }
        public string? CommentValue { get; set; }
        public DateTime CommentDateTime { get; set; }
    }
}

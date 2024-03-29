﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CommentsApp.Entities
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? CommentValue { get; set; }
        public DateTime CommentDateTime { get; set; }
        public ICollection<SubComment>? SubComments { get; set; } = new List<SubComment>();

    }
}

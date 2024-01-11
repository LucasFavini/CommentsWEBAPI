using System.ComponentModel.DataAnnotations;

namespace CommentsApp.Models
{
    public class Admin
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CommentsApp.Models
{
    public class Userlogin
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}

using CommentsApp.Entities;

namespace CommentsApp.Models
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime UserCreation { get; set; }
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
    }
}

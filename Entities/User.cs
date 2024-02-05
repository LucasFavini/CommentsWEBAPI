using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CommentsApp.Entities
{
    [Table("Users")]
    public class User
    {
        //En realidad deberia crear otro modelo en vez de usar el ignore
        //Y deberia crear tabla para los roles User / Roles / User_Roles --> esas 3 tablas
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime UserCreation { get; set; }
        [JsonIgnore]
        public bool isAdminUser { get; set; }
        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();

    }
}

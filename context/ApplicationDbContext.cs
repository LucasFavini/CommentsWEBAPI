using CommentsApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentsApp.context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Comment> Comment { get; set; }

    }
}

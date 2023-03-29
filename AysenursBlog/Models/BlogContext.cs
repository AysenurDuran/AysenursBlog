using Microsoft.EntityFrameworkCore;

namespace AysenursBlog.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<PasswordCode> PasswordCode { get; set; }

    }
}

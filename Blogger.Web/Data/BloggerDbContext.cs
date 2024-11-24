using Blogger.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Data
{
    public class BloggerDbContext : DbContext //this comes from a class installed in packages
    {
        public BloggerDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tags> Tags { get; set; }
    }
}

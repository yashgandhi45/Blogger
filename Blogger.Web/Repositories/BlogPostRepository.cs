using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggerDbContext bloggerDbContext;

        public BlogPostRepository(BloggerDbContext bloggerDbContext)
        {
            this.bloggerDbContext = bloggerDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggerDbContext.AddAsync(blogPost);
            await bloggerDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await bloggerDbContext.BlogPosts.FindAsync(id);
            if (existingBlog != null)
            {
                bloggerDbContext.BlogPosts.Remove(existingBlog);
                await bloggerDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggerDbContext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await bloggerDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await bloggerDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await bloggerDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlog!=null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageURL = blogPost.FeaturedImageURL;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Tags = blogPost.Tags;

                await bloggerDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}

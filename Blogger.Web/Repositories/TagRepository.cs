using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggerDbContext _bloggerDbContext;
        public TagRepository(BloggerDbContext bloggerDbContext) //Constructor Dependency Injection
        {
            _bloggerDbContext = bloggerDbContext;
        }
        public async Task<Tags> AddAsync(Tags tag)
        {
            await _bloggerDbContext.Tags.AddAsync(tag);
            await _bloggerDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tags?> DeleteAsync(Guid id)
        {
            var existingTag = await _bloggerDbContext.Tags.FindAsync(id);
            if(existingTag != null)
            {
                _bloggerDbContext.Tags.Remove(existingTag);
                await _bloggerDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tags>> GetAllAsync()
        {
            return await _bloggerDbContext.Tags.ToListAsync();
             
        }

        public async Task<Tags?> GetAsync(Guid id)
        {
            return await _bloggerDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tags?> UpdateAsync(Tags tag)
        {
            var existingTag = await _bloggerDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.Name;
                //Save Changes
                await _bloggerDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;

        }
    }
}

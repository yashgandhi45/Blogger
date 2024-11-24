using Blogger.Web.Models.Domain;

namespace Blogger.Web.Repositories
{
    public interface ITagRepository
    {
            Task<IEnumerable<Tags>> GetAllAsync();
            Task<Tags?> GetAsync(Guid id);
            Task<Tags> AddAsync(Tags tag);
            Task<Tags?> UpdateAsync(Tags tag);//nullable return type
            Task<Tags?> DeleteAsync(Guid id);


    }
}

using ASPCoreWebApplication.Models;

namespace ASPCoreWebApplication.Services
{
    public interface IMusicService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetAsync(string id);
        Task CreateAsync(Category newCategory);
        Task UpdateAsync(string id, Category updatedCategory);
        Task RemoveAsync(string id);
    }
}

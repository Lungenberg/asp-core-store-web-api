using ASPCoreWebApplication.Models;

namespace ASPCoreWebApplication.Services
{
    public interface IMusicService
    {
        Task<List<Category>> GetAllAsync(
            string? title, 
            List<string>? genres, 
            string? sortBy, 
            string? sortDirection,
            int page,
            int limitSize);
        Task<Category?> GetAsync(string id);
        Task CreateAsync(Category newCategory);
        Task UpdateAsync(string id, Category updatedCategory);
        Task RemoveAsync(string id);
        Task<List<Category>> SearchByTitleAsync(string title);

        Task<int> GetCountAsync();

        Task<List<string>> GetDistinctGenresAsync();
    }
}

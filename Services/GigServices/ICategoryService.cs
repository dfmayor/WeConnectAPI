using WeConnectAPI.Models;

namespace WeConnectAPI.Services.GigServices
{
    public interface ICategoryService
    {
        Task<Category> CreateCategory(Category category);
        Task<List<Category>> GetCategoriesList();
        Task<Category> UpdateCategory(string categoryName, string editedName);
        Task<bool> DeleteCategory(string name);
    }
}
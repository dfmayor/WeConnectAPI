using WeConnectAPI.Data;
using WeConnectAPI.Models;

namespace WeConnectAPI.Services.GigServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbcontext;

        public CategoryService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            var result = _dbcontext.Categories.Add(category);
            await _dbcontext.SaveChangesAsync();
            return result.Entity;
        }

        public Task<Category> DeleteCategory(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetCategoriesList()
        {
            List<Category> categoryList = new List<Category>();
            foreach(var category in _dbcontext.Categories)
            {
                categoryList.Add(category);
            }
            return categoryList;
        }

        public Task<Category> UpdateCategory(string categoryName, string editedName)
        {
            throw new NotImplementedException();
        }
    }
}
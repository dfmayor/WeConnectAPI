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

        public async Task<bool> DeleteCategory(string name)
        {
            var category = _dbcontext.Categories.FirstOrDefault(c => c.Name == name);
            if (category != null)
            {
                _dbcontext.Categories.Remove(category);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
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

        public async Task<Category> UpdateCategory(string categoryName, string editedName)
        {
            var category = _dbcontext.Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category != null)
            {
                category.Name = editedName;
                category.UpdatedAt = DateTime.Now;
                await _dbcontext.SaveChangesAsync();
                return category;
            }
            else
            {
                return null;
            }
        }
    }
}
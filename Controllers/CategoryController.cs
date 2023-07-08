using System.Net;
using Microsoft.AspNetCore.Mvc;
using WeConnectAPI.DTOs;
using WeConnectAPI.Models;
using WeConnectAPI.Services.GigServices;

namespace WeConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("add_category")]
        public async Task<GenericResponses> AddCategory([FromBody] string categoryName)
        {
            Category category = new Category
            {
                Name = categoryName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            try
            {
                var result = await _categoryService.CreateCategory(category);
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = $"Category {category.Name} Added Successfully!!!",
                    Data = category
                };
            }
            catch (Exception ex)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = $"Category cannot be created: {ex.Message}",
                    Data = null
                };
            }
        }

        [HttpGet]
        [Route("all_categories")]
        public async Task<GenericResponses> GetCategoriesList()
        {
            var result = await _categoryService.GetCategoriesList();
            if (result.Count > 0)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "All Categories Retrieved Successfully",
                    Data = result
                };
            }
            else
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "No Category Has Been Added!!!",
                    Data = null
                };
            }
        }

        [HttpPut]
        [Route("update_category")]
        public async Task<GenericResponses> UpdateCategory(string categoryName, string editedName)
        {
            var result = await _categoryService.UpdateCategory(categoryName, editedName);
            if (result != null)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Category Name Updated Successfully",
                    Data = result
                };
            }
            else
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed To Update Category",
                    Data = null
                };
            }
        }

        [HttpDelete]
        [Route("delete_category")]
        public async Task<GenericResponses> DeleteCategory(string name)
        {
            var result = await _categoryService.DeleteCategory(name);
            if (result)
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Category Deleted Successfully",
                    Data = result
                };
            }
            else
            {
                return new GenericResponses()
                {
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Category does not exist!!!",
                    Data = result
                };
            }
        }
    }
}
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

    }
}
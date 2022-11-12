using GatewayWebApi.Interfaces.Services;
using GatewayWebApi.ModelsDto;

using Microsoft.AspNetCore.Mvc;

namespace GatewayWebApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategories([FromBody] CategoryDto categoryDto)
        {
            await _categoryService.CreateCategoryAsync(categoryDto);
            return Ok();
        }
    }
}

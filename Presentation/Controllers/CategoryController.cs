using DompifyAPI.Application.Helpers;
using DompifyAPI.Application.Interfaces;
using DompifyAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DompifyAPI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController(ICategoryUseCase categoryUseCase) : Controller
    {
        private readonly ICategoryUseCase _categoryUseCase = categoryUseCase;

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoryUseCase.GetCategoriesAsync();
                if (categories == null || !categories.Any())
                    return ResponseFormatter.Success(StatusCodes.Status200OK, "Categories not found", null);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Categories found", categories);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryUseCase.GetCategoryByIdAsync(id);
                if (category == null)
                    return ResponseFormatter.Success(StatusCodes.Status200OK, "Category not found", null);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Category found", category);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            try
            {
                var createdCategory = await _categoryUseCase.CreateCategoryAsync(category);

                return ResponseFormatter.Success(StatusCodes.Status201Created, "Category created", createdCategory);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            try
            {
                category.Id = id;
                var updatedCategory = await _categoryUseCase.UpdateCategoryAsync(category);
                if (updatedCategory == null)
                    return ResponseFormatter.Success(StatusCodes.Status200OK, "Category not found", null);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Category updated", updatedCategory);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryUseCase.DeleteCategoryAsync(id);
            if (!result)
                return ResponseFormatter.Success(StatusCodes.Status200OK, "Category not found", null);

            return ResponseFormatter.Success(StatusCodes.Status200OK, "Category deleted", null);
        }
    }
}

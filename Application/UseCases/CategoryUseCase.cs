using DompifyAPI.Application.Interfaces;
using DompifyAPI.Domain.Interfaces;
using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Application.UseCases
{
    public class CategoryUseCase(ICategoryRepository categoryRepository) : ICategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<IEnumerable<Category?>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetCategories();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryById(id);
        }

        public async Task<Category?> CreateCategoryAsync(Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
                throw new ArgumentException("Category name cannot be empty.");

            var existingCategory = await _categoryRepository.GetCategoryByName(category.Name);
            if (existingCategory != null)
                throw new ArgumentException("Category name already exists.");

            category.CreatedAt = DateTime.UtcNow;
            category.CreatedBy = "SYSTEM";

            return await _categoryRepository.CreateCategory(category);
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _categoryRepository.GetCategoryById(category.Id);
            if (existingCategory == null)
                return null;

            var existingCategoryByName = await _categoryRepository.GetCategoryByName(existingCategory.Name!);
            if (existingCategoryByName != null)
                throw new ArgumentException("Category name already exists.");

            existingCategory.Name = category.Name ?? existingCategory.Name;
            existingCategory.Description = category.Description ?? existingCategory.Description;
            existingCategory.Icon = category.Icon ?? existingCategory.Icon;
            existingCategory.IsActive = category.IsActive ?? existingCategory.IsActive;
            existingCategory.UpdatedAt = DateTime.UtcNow;
            existingCategory.UpdatedBy = "SYSTEM";

            return await _categoryRepository.UpdateCategory(existingCategory);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
                return false;

            return await _categoryRepository.DeleteCategory(category);
        }
    }
}

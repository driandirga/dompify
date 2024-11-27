using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category?>> GetCategories();
        Task<Category?> GetCategoryById(int id);
        Task<Category?> GetCategoryByName(string name);
        Task<Category?> CreateCategory(Category category);
        Task<Category?> UpdateCategory(Category category);
        Task<bool> DeleteCategory(Category category);
    }
}

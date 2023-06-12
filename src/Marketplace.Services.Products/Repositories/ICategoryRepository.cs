using Marketplace.Services.Products.Entities;

namespace Marketplace.Services.Products.Repositories;

public interface ICategoryRepository
{
    IEnumerable<Category> Categories { get; set; }
    
     Task AddCategory(Category category);
     Task UpdateCategory(Category category);
     Task DeleteCategory(Category category);
     Task<Category> GetCategoryById(int categoryId);
}
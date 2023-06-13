using Marketplace.Services.Products.Entities;

namespace Marketplace.Services.Products.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> Categories { get; set; }
    
     Task AddCategory(Category category);
     Task UpdateCategory(Category category);
     Task DeleteCategory(Category category);
     Task<Category> GetCategoryById(int categoryId);
}

public class CategoryRepository : ICategoryRepository
{
	public Task<IEnumerable<Category>> Categories { get; set; }
	public Task AddCategory(Category category)
	{
		throw new NotImplementedException();
	}

	public Task UpdateCategory(Category category)
	{
		throw new NotImplementedException();
	}

	public Task DeleteCategory(Category category)
	{
		throw new NotImplementedException();
	}

	public Task<Category> GetCategoryById(int categoryId)
	{
		throw new NotImplementedException();
	}
}
using Marketplace.Services.Products.Entities;
using MongoDB.Driver;

namespace Marketplace.Services.Products.Repositories;

public interface ICategoryRepository
{
	Task<List<Category>> GetCategories();
	Task AddCategory(Category category);
	Task UpdateCategory(Category category);
	Task DeleteCategory(Category category);
	Task<Category> GetCategoryById(int categoryId);
}

public class CategoryRepository : ICategoryRepository
{
	private readonly IMongoCollection<Category> _categoryCollection;

	public CategoryRepository()
	{
		var client = new MongoClient("mongodb://root:password@mongodb:27017");
		var database = client.GetDatabase("products");
		_categoryCollection = database.GetCollection<Category>("categories");
	}

	public async Task<List<Category>> GetCategories()
	{
		return await (await _categoryCollection.FindAsync(_ => true)).ToListAsync();
	}

	public async Task AddCategory(Category category)
	{
		await _categoryCollection.InsertOneAsync(category);
	}

	public async Task UpdateCategory(Category category)
	{
		var filter = Builders<Category>.Filter.Eq(c => c.Id, category.Id);
		await _categoryCollection.ReplaceOneAsync(filter, category);
	}

	public async Task DeleteCategory(Category category)
	{
		var filter = Builders<Category>.Filter.Eq(c => c.Id, category.Id);
		await _categoryCollection.DeleteOneAsync(filter);
	}

	public async Task<Category> GetCategoryById(int categoryId)
	{
		var filter = Builders<Category>.Filter.Eq(c => c.Id, categoryId);
		return await _categoryCollection.Find(filter).FirstOrDefaultAsync();
	}
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Categories.Entites;
using Categories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Categories.Controllers;

[Route("api/[controller]")]
public class CategoriesController : Controller
{
	private readonly IMongoCollection<Category> _categories;
	private readonly IMemoryCache _memoryCache;
	private const string _cacheKey = "categories";

	public CategoriesController(IMemoryCache memoryCache)
	{
		var client = new MongoClient("mongodb://root:password@categories_db:27017");
		var database = client.GetDatabase("categories_db");
		_categories = database.GetCollection<Category>("categories");
		_memoryCache = memoryCache;
	}

	private string GenerateKey(string name)
	{
		return Regex.Replace(name.ToLower(), @"\s+", "-");
	}

	[HttpGet]
	public async Task<List<CategoryModel>> GetCategories()
	{
		if (_memoryCache.TryGetValue(_cacheKey, out object? value))
		{
			return (List<CategoryModel>)value!;
		}

		return await GetUpdateCategories();
	}

	private async Task<List<CategoryModel>> GetUpdateCategories()
	{
		var categories = await _categories.Find(_ => true).ToListAsync();
		var categoriesModel = CategoryConverter.ConvertToCategoryModels(categories);

		_memoryCache.Set(_cacheKey, categoriesModel);

		return categoriesModel;
	}

	[HttpGet("soft")]
	public async Task<List<Category>> GetCategoryEntities()
	{
		var categories = await _categories.Find(_ => true).ToListAsync();

		return categories;
	}

	[HttpPost]
	public async Task AddCategory([FromBody] CreateCategoryModel categoryModel)
	{
		if (categoryModel.ParentId is not null)
		{
			var parentCategory = await (await _categories.FindAsync(c => c.ParentId == categoryModel.ParentId))
				.FirstOrDefaultAsync();
			
			if (parentCategory == null)
			{
				throw new Exception("Parent category is not exists");
			}
		}

		var key = GenerateKey(categoryModel.Name);

		var category = new Category()
		{
			Name = categoryModel.Name,
			Key = key,
			ParentId = categoryModel.ParentId
		};

		await _categories.InsertOneAsync(category);

		await GetUpdateCategories();
	}
}
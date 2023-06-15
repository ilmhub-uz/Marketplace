using Marketplace.Services.Products.Entities;
using Marketplace.Services.Products.Models;
using Marketplace.Services.Products.Repositories;

namespace Marketplace.Services.Products.Managers;

public class CategoryManager
{
	private readonly ICategoryRepository _categoryRepository;
	public CategoryManager(ICategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}

	public async Task<CategoryModel> AddCategory(CreateCategoryModel model)
	{
		if (model.ParentId is not null)
		{
			var parentCategory = await _categoryRepository.GetCategoryById(model.ParentId.Value);

			var category = await CreateCategory(model);
			parentCategory.ChildCategories.Add(category);

			await _categoryRepository.UpdateCategory(parentCategory);

			return ParseCategoryModel(category);
		}
		else
		{
			var category = await CreateCategory(model);

			await _categoryRepository.AddCategory(category);
			return ParseCategoryModel(category);
		}
	}

	private async Task<Category> CreateCategory(CreateCategoryModel model)
	{
		var categories = (await _categoryRepository.GetCategories());
		int maxId = 0;
		if (categories is { Count: > 0 })
		{
			maxId = categories.Max(c => c.Id);
			maxId = maxId + 1;
		}

		var category = new Category()
		{
			Id = maxId,
			Name = model.Name
		};
		return category;
	}

	public async Task<CategoryModel> GetById(int categoryId)
	{
		var category = await _categoryRepository.GetCategoryById(categoryId);
		if (category == null) return null!;
		return ParseCategoryModel(category);
	}

	public async Task<IEnumerable<CategoryModel>> GetCategories()
	{
		var categoryModels = new List<CategoryModel>();
		foreach (var category in await _categoryRepository.GetCategories())
		{
			categoryModels.Add(ParseCategoryModel(category));
		}
		return categoryModels;
	}

	public async Task<CategoryModel> UpdateCategory(CreateCategoryModel? model, int categoryId)
	{
		var category = await _categoryRepository.GetCategoryById(categoryId);

		category.Name = model.Name;
		await _categoryRepository.UpdateCategory(category);
		return ParseCategoryModel(category);
	}


	public async Task<string> DeleteCategory(int categoryId)
	{
		var category = _categoryRepository.GetCategoryById(categoryId);
		if (category == null!) return "Not found";

		await _categoryRepository.DeleteCategory(await category);
		return "This category was deleted";
	}
	
	private CategoryModel ParseCategoryModel(Category category)
	{
		var categoryModel =  new CategoryModel()
		{
			Id = category.Id,
			Name = category.Name,
		};

		foreach (var childCategory in category.ChildCategories)
		{
			categoryModel.ChildCategories.Add(ParseCategoryModel(childCategory));
		}

		return categoryModel;
	}
}
using Marketplace.Services.Products.Managers;
using Marketplace.Services.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Products.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
	private readonly CategoryManager _categoryManager;

	public CategoriesController(CategoryManager categoryManager)
	{
		_categoryManager = categoryManager;
	}

	[HttpGet]
	public async Task<IActionResult> GetCategories()
	{
		return Ok(await _categoryManager.GetCategories());
	}

	[HttpGet("{categoryId}")]
	public async Task<IActionResult> GetById(int categoryId)
	{
		return Ok(await _categoryManager.GetById(categoryId));
	}

	[HttpPost]
	public async Task<IActionResult> AddCategory(CreateCategoryModel? model)
	{
		return Ok(await _categoryManager.AddCategory(model));
	}

	[HttpPut("{categoryId}")]
	public async Task<IActionResult> UpdateCategory(CreateCategoryModel? model, int categoryId)
	{
		return Ok(await _categoryManager.UpdateCategory(model, categoryId));
	}

	[HttpDelete("{categoryId}")]
	public async Task<IActionResult> DeleteCategory(int categoryId)
	{
		return Ok(await _categoryManager.DeleteCategory(categoryId));
	}
}
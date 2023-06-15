using Marketplace.Services.Products.Entities;
using Marketplace.Services.Products.Managers;
using Marketplace.Services.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Products.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
	private readonly ProductManager _productManager;
	public ProductsController(ProductManager productManager)
	{
		_productManager = productManager;
	}

	[HttpGet]
	public async Task<IActionResult> GetProducts()
	{
		return Ok(await _productManager.GetProducts());
	}

	[HttpGet("{productId}")]
	public async Task<IActionResult> GetProductById(Guid productId)
	{
		return Ok(await _productManager.GetProductById(productId));
	}

	[HttpPost]
	public async Task<IActionResult> AddProduct([FromForm] CreateProductModel model)
	{
		return Ok(await _productManager.AddProduct( model));
    }
    [HttpPut("{productId}")]
	public async Task<IActionResult> UpdateProduct([FromForm] CreateProductModel model,Guid productId)
	{
		return Ok(await _productManager.UpdateProduct( productId, model));
	}

	[HttpDelete("{productId}")]
	public async Task<IActionResult> DeleteProduct( Guid productId)
	{
		return Ok(await _productManager.DeleteProduct(productId));
	}
}
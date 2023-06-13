using Marketplace.Services.Products.Managers;
using Marketplace.Services.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Products.Controllers;

[Route("api/products/{categoryId}")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductManager _productManager;
    public ProductController(ProductManager productManager)
    {
        _productManager = productManager;
    }
    [HttpGet]
    public async Task<IActionResult> GetProducts(int categoryId)
    {
        return Ok(await _productManager.GetProducts(categoryId));
    }
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(Guid productId, int categoryId)
    {
        return Ok(await _productManager.GetProductById(productId,categoryId));
    }
    [HttpPost]
    public async Task<OkObjectResult> AddProduct(int categoryId, ProductModel model)
    {
        return Ok(await _productManager.AddProduct(categoryId,model));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(int categoryId, Guid productId, ProductModel model)
    {
        return Ok(await _productManager.UpdateProduct(categoryId,productId,model));
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(int categoryId, Guid productId)
    {
        return Ok(await _productManager.DeleteProduct(categoryId,productId));
    }
}
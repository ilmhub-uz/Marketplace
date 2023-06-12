using Marketplace.Services.Products.Entities;
using Marketplace.Services.Products.Models;
using Marketplace.Services.Products.Repositories;

namespace Marketplace.Services.Products.Managers;

public class ProductManager
{
	private readonly IProductRepository _productRepository;

	public ProductManager(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task CreateProduct(CreateProductModel model)
	{
		var product = new Product()
		{
			Name = model.Name
		};

		await _productRepository.Save(product);
	}

	public async Task<ProductModel> GetProductById(Guid productId)
	{
		var product = await _productRepository.GetById(productId);

		return new ProductModel()
		{
			Name = product.Name,
		};
	}
}
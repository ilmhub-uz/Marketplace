using Marketplace.Services.Products.Entities;
using Marketplace.Services.Products.FileServices;
using Marketplace.Services.Products.Models;
using Marketplace.Services.Products.Repositories;

namespace Marketplace.Services.Products.Managers;

public class ProductManager
{
	private readonly IProductRepository _repository;

	public ProductManager(IProductRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<Product>> GetProducts()
	{
		return await _repository.GetProducts();
	}

	public async Task<ProductModel> GetProductById(Guid productId)
	{
		return ParseToProductModel(await _repository.GetProductById( productId));
	}

	public async Task<ProductModel> AddProduct( CreateProductModel model)
	{
		var product = new Product
		{
			Name = model.Name,
			Description = model.Description,
			Price = model.Price,
			CategoryId = model.CategoryId,
            Photo_Path = FileService.ProductImages(model.PhotoFile!)
        };
		await _repository.AddProduct( product);
		return ParseToProductModel(product);
	}


	public async Task<ProductModel> UpdateProduct( Guid productId, CreateProductModel model)
	{
		var product =await  _repository.GetProductById(productId);

		product.Name = model.Name;
		product.Description = model.Description;
		product.Price = model.Price;
		product.CategoryId = model.CategoryId;
        product.Photo_Path =FileService.ProductImages(model.PhotoFile!);

        await _repository.UpdateProduct( product);
		return ParseToProductModel(product);
	}

	public async Task<string> DeleteProduct( Guid productId)
	{
		var product = await _repository.GetProductById(productId);
		
		await _repository.DeleteProduct(product);
		return "Successfully";
	}

	private ProductModel ParseToProductModel(Product product)
	{
		return new ProductModel()
		{
			Id = product.Id,
			Name = product.Name,
			Description = product.Description,
			Price = product.Price,
			CategoryId = product.CategoryId,
			Photo_Path = product.Photo_Path,
		};
	}
}
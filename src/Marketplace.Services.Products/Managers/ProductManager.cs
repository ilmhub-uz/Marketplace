using Marketplace.Services.Products.Entities;
using Marketplace.Services.Products.FileServices;
using Marketplace.Services.Products.Models;
using Marketplace.Services.Products.Repositories;

namespace Marketplace.Services.Products.Managers;

public class ProductManager
{
    private readonly IProductRepository _repository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductManager(IProductRepository repository, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Product>> GetProducts(int categoryId)
    {
        var category = await _categoryRepository.GetCategoryById(categoryId);
        if (category == null!) return null!;
        return await _repository.GetProducts(category);
    }

    public async Task<ProductModel> GetProductById(Guid productId, int categoryId)
    {
        var category = await _categoryRepository.GetCategoryById(categoryId);
        if (category == null) return null!;
        return ParseToProductModel(await _repository.GetProductById(category, productId));
    }

    public async Task<ProductModel> AddProduct(int categoryId, CreateProductModel model)
    {
        var category = await _categoryRepository.GetCategoryById(categoryId);
        if (category == null) return null!;
        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            CategoryId = categoryId,
            Images = (List<ProductImage>?)model.Images.Select(i => new ProductImage()
            {
                ProductId = i.ProductId,
                Path = FileService.ProductImages(i.Image)
            })
        };
        await _repository.AddProduct(category, product);
        return ParseToProductModel(product);
    }


    public async Task<ProductModel> UpdateProduct(int categoryId, Guid productId, CreateProductModel model)
    {
        var category = await _categoryRepository.GetCategoryById(categoryId);
        if (category == null) return null!;
        var product = category.Products.FirstOrDefault(p => p.Id == productId);
        if (product == null) return null!;

        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;
        product.CategoryId = categoryId;
        product.Images = (List<ProductImage>?)model.Images.Select(i => new ProductImage()
        {
            ProductId = i.ProductId,
            Path = FileService.ProductImages(i.Image)
        });
        await _repository.UpdateProduct(category, product);
        return ParseToProductModel(product);
    }

    public async Task<string> DeleteProduct(int categoryId, Guid productId)
    {
        var category = await _categoryRepository.GetCategoryById(categoryId);
        if (category == null) return "Category not found"!;
        var product = category.Products.FirstOrDefault(p => p.Id == productId);
        if (product == null) return "Product not found"!;
        await _repository.DeleteProduct(category, product);
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
            Images = product.Images,
        };

    }
}
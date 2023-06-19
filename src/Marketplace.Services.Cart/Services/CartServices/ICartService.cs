using Marketplace.Services.Cart.Entities;
using Marketplace.Services.Cart.Models;

namespace Marketplace.Services.Cart.Services.CartServices;

public interface ICartService
{
    Task AddProductAsync(string key, CreateProductModel createProductModel);
    Task<List<Product>> GetUserCartAsync(string key);
    Task UpdateProductAsync(string key, UpdateProductModel updateProductModel);
    Task DeleteProductAsync(string key, string productId);
    Task DeleteUserCartAsync(string key);
}
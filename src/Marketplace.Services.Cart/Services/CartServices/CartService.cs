using Mapster;
using Marketplace.Services.Cart.Entities;
using Marketplace.Services.Cart.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Marketplace.Services.Cart.Services.CartServices;

public class CartService : ICartService
{
    private readonly IDistributedCache _distributedCache;

    public CartService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task AddProductAsync(string key, CreateProductModel createProductModel)
    {
        var productsJson = await _distributedCache.GetStringAsync(key);
        var products = JsonConvert.DeserializeObject<List<Product>>(productsJson ?? string.Empty);
        //Product product = new Product()
        //{
        //    Id=createProductModel.Id,
        //    Count = createProductModel.Count,
        //};
        Product product = createProductModel.Adapt<Product>();

        products ??= new List<Product>();

        products.Add(product);

        await SaveCartToRedis(key, JsonConvert.SerializeObject(products));
    }




    public async Task<List<Product>> GetUserCartAsync(string key)
    {
        var productsJson = await _distributedCache.GetStringAsync(key);

        if (productsJson is null)
            return new List<Product>();

        var products = JsonConvert.DeserializeObject<List<Product>>(productsJson);

        return products!;
    }

    public async Task UpdateProductAsync(string key, UpdateProductModel updateProductModel)
    {
        var productsJson = await _distributedCache.GetStringAsync(key);

        if (productsJson is null)
            throw new Exception("Not Found");

        var products = JsonConvert.DeserializeObject<List<Product>>(productsJson);
        var product = products!.FirstOrDefault(product => product.Id == updateProductModel.Id);

        if (product is null)
            throw new Exception("not found");

        products!.Remove(product);
        products.Add(updateProductModel.Adapt<Product>());

        await SaveCartToRedis(key, JsonConvert.SerializeObject(products));
    }

    public async Task DeleteProductAsync(string key, string productId)
    {
        var productsJson = await _distributedCache.GetStringAsync(key);

        if (productsJson is null)
            throw new Exception("Products is null in this user's cart");

        var products = JsonConvert.DeserializeObject<List<Product>>(productsJson);
        var product = products!.FirstOrDefault(product => product.Id == productId);

        if (product is null)
            throw new Exception("Notfound");

        products!.Remove(product!);

        await SaveCartToRedis(key, JsonConvert.SerializeObject(products));
    }

    public async Task DeleteUserCartAsync(string key)
    {
        await _distributedCache.RemoveAsync(key);
    }

    private async Task SaveCartToRedis(string key, string value)
    {
        await _distributedCache.SetStringAsync(key, value, new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromDays(5)
        });
    }

}
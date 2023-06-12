using Marketplace.Services.Products.Entities;

namespace Marketplace.Services.Products.Repositories;

public interface IProductRepository
{
	Task Save(Product  product);
	Task<Product> GetById(Guid id);
}

public class ProductRepository : IProductRepository
{
	public Task Save(Product product)
	{
		// save to postgresdb
	}

	public Task<Product> GetById(Guid id)
	{
		// get from postgresdb
	}
}

public class ProductMongodbRepository : IProductRepository
{
	public Task Save(Product product)
	{
		// save to mongo
	}

	public Task<Product> GetById(Guid id)
	{
		// get from mongo
	}
}
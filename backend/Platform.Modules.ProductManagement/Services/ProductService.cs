using Platform.Core.Domain.Interfaces;
using Platform.Modules.ProductManagement.Domain.Entities;

namespace Platform.Modules.ProductManagement.Services;

public class ProductService
{
    private readonly IRepository<Product> _productRepository;

    public ProductService(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        product.CreatedAt = DateTime.UtcNow;
        return await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        product.UpdatedAt = DateTime.UtcNow;
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }
}

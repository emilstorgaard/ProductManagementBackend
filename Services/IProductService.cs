using ProductManagementBackend.Models.Dtos;
using ProductManagementBackend.Models.Entities;

namespace ProductManagementBackend.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<(int TotalProducts, int TotalPages, List<Product> Products)> GetProductsByPageandSortAsync(int page, int pageSize, string sort);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> AddProductAsync(AddProductDto addProductDto);
        Task<Product> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
        Task<bool> DeleteProductAsync(Guid id);
    }
}

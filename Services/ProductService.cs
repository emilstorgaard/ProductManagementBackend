using Microsoft.EntityFrameworkCore;
using ProductManagementBackend.Data;
using ProductManagementBackend.Models.Dtos;
using ProductManagementBackend.Models.Entities;

namespace ProductManagementBackend.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

        public async Task<(int TotalProducts, int TotalPages, List<Product> Products)> GetProductsByPageandSortAsync(int page, int pageSize, string sort)
        {
            var totalProducts = await _dbContext.Products.CountAsync();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            IQueryable<Product> query = SortProducts(sort);

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalProducts, totalPages, products);
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<Product> AddProductAsync(AddProductDto addProductDto)
        {
            var productEntity = new Product
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _dbContext.Products.AddAsync(productEntity);
            await _dbContext.SaveChangesAsync();

            return productEntity;
        }

        public async Task<Product> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private IQueryable<Product> SortProducts(string sort)
        {
            IQueryable<Product> query = _dbContext.Products;

            query = sort.ToLower() switch
            {
                "a-z" => query.OrderBy(p => p.Name),
                "z-a" => query.OrderByDescending(p => p.Name),
                "newest" => query.OrderByDescending(p => p.CreatedAt),
                "oldest" => query.OrderBy(p => p.CreatedAt),
                _ => query.OrderByDescending(p => p.CreatedAt), // Default sort
            };

            return query;
        }
    }
}

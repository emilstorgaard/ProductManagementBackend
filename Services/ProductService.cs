using Microsoft.EntityFrameworkCore;
using ProductManagementBackend.Data;
using ProductManagementBackend.Models.Dtos;
using ProductManagementBackend.Models.Entities;

namespace ProductManagementBackend.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.OrderByDescending(p => p.CreatedAt).ToList();
        }

        public (int TotalProducts, int TotalPages, List<Product> Products) GetProductsByPage(int page, int pageSize)
        {
            var totalProducts = _dbContext.Products.Count();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            var products = _dbContext.Products
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (totalProducts, totalPages, products);
        }

        public Product GetProductById(Guid id)
        {
            return _dbContext.Products.Find(id);
        }

        public Product AddProduct(AddProductDto addProductDto)
        {
            var productEntity = new Product
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description
            };

            _dbContext.Products.Add(productEntity);
            _dbContext.SaveChanges();

            return productEntity;
        }

        public Product UpdateProduct(Guid id, UpdateProductDto updateProductDto)
        {
            var product = _dbContext.Products.Find(id);

            if (product == null)
            {
                return null;
            }

            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;

            _dbContext.SaveChanges();

            return product;
        }

        public bool DeleteProduct(Guid id)
        {
            var product = _dbContext.Products.Find(id);

            if (product == null)
            {
                return false;
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return true;
        }
    }
}

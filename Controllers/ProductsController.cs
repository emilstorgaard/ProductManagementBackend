using Microsoft.AspNetCore.Mvc;
using ProductManagementBackend.Models.Dtos;
using ProductManagementBackend.Services;

namespace ProductManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProducts = await _productService.GetAllProductsAsync();
            return Ok(allProducts);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsByPage(int page = 1, string sort = "")
        {
            const int pageSize = 10;
            var (totalProducts, totalPages, products) = await _productService.GetProductsByPageandSortAsync(page, pageSize, sort);

            var result = new
            {
                TotalProducts = totalProducts,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Products = products
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDto addProductDto)
        {
            var productEntity = await _productService.AddProductAsync(addProductDto);
            return Ok(productEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto updateProductDto)
        {
            var product = await _productService.UpdateProductAsync(id, updateProductDto);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProductAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

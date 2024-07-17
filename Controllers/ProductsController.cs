using Microsoft.AspNetCore.Cors;
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

        [HttpGet("all")]
        [EnableCors]
        public IActionResult GetAllProducts()
        {
            var allProducts = _productService.GetAllProducts();
            return Ok(allProducts);
        }

        [HttpGet]
        [EnableCors]
        public IActionResult GetAllProductsByPage(int page = 1)
        {
            const int pageSize = 10;
            var (totalProducts, totalPages, products) = _productService.GetProductsByPage(page, pageSize);

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
        [EnableCors]
        [Route("{id:guid}")]
        public IActionResult GetProductById(Guid id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductDto addProductDto)
        {
            var productEntity = _productService.AddProduct(addProductDto);
            return Ok(productEntity);
        }

        [HttpPut]
        [EnableCors]
        [Route("{id:guid}")]
        public IActionResult UpdateProduct(Guid id, UpdateProductDto updateProductDto)
        {
            var product = _productService.UpdateProduct(id, updateProductDto);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete]
        [EnableCors]
        [Route("{id:guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var result = _productService.DeleteProduct(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

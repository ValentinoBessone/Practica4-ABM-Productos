using Microsoft.AspNetCore.Mvc;
using Practica4.Models.DTOs.Requests;
using Practica4.Services.Implementations;

namespace Practica4.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductService _service = new ProductService();

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _service.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductForCreateDto dto)
        {
            var createdProduct = _service.CreateProduct(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductForUpdateDto dto)
        {
            var existingProduct = _service.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _service.UpdateProduct(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingProduct = _service.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _service.DeleteProduct(id);
            return NoContent();
        }
    }
}
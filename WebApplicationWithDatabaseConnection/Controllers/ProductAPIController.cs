using Microsoft.AspNetCore.Mvc;
using WebApplicationWithDatabaseConnection.Interface;
using WebApplicationWithDatabaseConnection.Models;
using WebApplicationWithDatabaseConnection.Services;

namespace WebApplicationWithDatabaseConnection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductAPIController : ControllerBase
    {
        private ILogger _logger;
        private IProduct _service;
        // private SProduct _serviceRepo;
        // private readonly IProduct _service;

        public ProductAPIController(ILogger<ProductAPIController> logger, IProduct servies)
        {
            _logger = logger;
            _service = servies;
            //_serviceRepo = serviceRepo;
        }

        [HttpGet("/api/products")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var product = await _service.GetProducts();
                return Ok(product);
            } 
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name ="ProductById")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _service.GetProduct(id);
                if(product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {
                var productData = await _service.CreateProduct(product);
                if(productData == null)
                {
                    return NotFound();
                }
                // Return a more specific success response with the created product
                return Ok(product);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                var updateData = await _service.UpdateProduct(product);
                if(updateData == null)
                {
                    return NotFound();
                }
                return Ok(updateData);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("/api/products/{id}")]
        public ActionResult<string> DeleteProduct(string id)
        {
            _service.DeleteProduct(id);
            return id;
        }
    }
}

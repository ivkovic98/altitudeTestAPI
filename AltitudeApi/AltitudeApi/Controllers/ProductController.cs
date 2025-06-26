using Altitude.Bussiness.Interfaces;
using Altitude.Bussiness.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AltitudeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _service.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost("add-product")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm]ProductCreateModel model)
        {
            await _service.CreateAsync(model);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromForm] ProductCreateModel model)
        {
            var success = await _service.UpdateAsync(id, model);
            return success ? Ok("Product updated.") : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}

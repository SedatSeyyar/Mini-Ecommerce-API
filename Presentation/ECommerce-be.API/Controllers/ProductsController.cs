using ECommerce_be.Application.Abstractions;
using ECommerce_be.Application.Repositories;
using ECommerce_be.Application.ViewModels.Products;
using ECommerce_be.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce_be.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductService productService, IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productService = productService;
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAsync(VM_Create_Product model)
        {
            Product product = new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
                Description= model.Description,
            };
            await _productWriteRepository.AddAsync(product);
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Price = model.Price;
            product.Stock = model.Stock;
            product.Name = model.Name;
            //_productWriteRepository.Update(product);
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            await _productWriteRepository.RemoveAsync(Id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet]
        public IActionResult Get() => Ok(_productReadRepository.GetAll(tracking: false));

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(Guid Id) => Ok(await _productReadRepository.GetByIdAsync(Id, tracking: false));
    }
}

﻿using ECommerce_be.Application.Abstractions;
using ECommerce_be.Application.Abstractions.Storage;
using ECommerce_be.Application.Features.Queries.GetAllProduct;
using ECommerce_be.Application.Repositories;
using ECommerce_be.Application.RequestParameters;
using ECommerce_be.Application.ViewModels.Products;
using ECommerce_be.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ECommerce_be.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IStorageService _storageService;

        private readonly IMediator _mediator;


        public ProductsController(IProductWriteRepository productWriteRepository,
                                  IProductReadRepository productReadRepository,
                                  IWebHostEnvironment webHostEnvironment,
                                  IStorageService storageService,
                                  IFileWriteRepository fileWriteRepository,
                                  IFileReadRepository fileReadRepository,
                                  IProductImageFileWriteRepository productImageFileWriteRepository,
                                  IProductImageFileReadRepository productImageFileReadRepository,
                                  IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _mediator = mediator;
        }

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public async Task<IActionResult> PostAsync(VM_Create_Product model)
        //{
        //    Product product = new()
        //    {
        //        Name = model.Name,
        //        Price = model.Price,
        //        Stock = model.Stock,
        //        Description = model.Description,
        //    };
        //    Product existedProduct = await _productReadRepository.GetWhere(x => x.Name.Equals(product.Name) && !x.IsDeleted, false).FirstOrDefaultAsync();
        //    if (existedProduct == null)
        //    {
        //        await _productWriteRepository.AddAsync(product);
        //        await _productWriteRepository.SaveAsync();

        //        //Dictionary<string, string> kaynak = new();
        //        //kaynak = model.Images.ToDictionary(x => x.Name, x => x.Base64Source);
        //        //List<IFormFile> formFiles = ConvertBase64FilesToIFormFile(kaynak);

        //        //List<IFormFile> formFilesCollection = ConvertBase64FilesToIFormFile(kaynak);
        //        var datas = await _storageService.UploadAsync("files", null);

        //        await _productImageFileWriteRepository.AddRangeAsync(datas.Select(x => new ProductImageFile
        //        {
        //            Path = x.pathOrContainerName,
        //            Name = x.fileName,
        //            Storage = _storageService.StorageName
        //        }).ToList());

        //        await _productImageFileWriteRepository.SaveAsync();

        //        return StatusCode((int)HttpStatusCode.Created);
        //    }
        //    else
        //    {
        //        return StatusCode((int)HttpStatusCode.BadRequest);
        //    }
        //}

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
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);

            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(Guid Id) => Ok(await _productReadRepository.GetByIdAsync(Id, tracking: false));

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            var datas = await _storageService.UploadAsync("files", Request.Form.Files.ToList());

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(x => new ProductImageFile
            {
                Path = x.pathOrContainerName,
                Name = x.fileName,
                Storage = _storageService.StorageName
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.OK);
        }

        private List<IFormFile> ConvertBase64FilesToIFormFile(Dictionary<string, string> base64Files)
        {
            var formFiles = new List<IFormFile>();
            Regex regex = new(@"^[\w/\:.-]+;base64,");

            foreach (var (fileName, base64Data) in base64Files)
            {
                string tempBase64Data = regex.Replace(base64Data, string.Empty);

                var bytes = Convert.FromBase64String(tempBase64Data);

                using var stream = new MemoryStream(bytes);
                var formFile = new FormFile(stream, 0, bytes.Length, fileName, fileName);
                formFiles.Add(formFile);
            }

            return formFiles;
        }
    }
}

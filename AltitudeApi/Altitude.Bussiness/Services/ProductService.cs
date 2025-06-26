using Altitude.Bussiness.Interfaces;
using Altitude.Bussiness.Models.Product;
using Altitude.Data.Entities;
using Altitude.Data.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IImageUploadService _imageUploadService;

        public ProductService(IProductRepository productRepository, IMapper mapper, IImageUploadService imageUploadService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _imageUploadService = imageUploadService;
        }

        public async Task<List<ProductResponseModel>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductResponseModel>>(products.Where(p => !p.IsDeleted).ToList());
        }

        public async Task<ProductResponseModel?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null || product.IsDeleted)
                return null;

            return _mapper.Map<ProductResponseModel>(product);
        }

        public async Task CreateAsync(ProductCreateModel model)
        {
            var product = _mapper.Map<Product>(model);

            if (model.ProductImage != null)
            {
                var imageUrl = await _imageUploadService.UploadImageAsync(model.ProductImage, "products");
                product.ProductImageUrl = imageUrl;
            }

            await _productRepository.AddAsync(product);
        }

        public async Task<bool> UpdateAsync(Guid id, ProductCreateModel model)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null || product.IsDeleted)
                return false;

            product.Name = model.Name;
            product.Description = model.Description;
            product.Category = model.Category;
            product.Price = model.Price;

            // Handle image update
            if (model.ProductImage != null)
            {
                var imageUrl = await _imageUploadService.UploadImageAsync(model.ProductImage, "products");
                product.ProductImageUrl = imageUrl;
            }

            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null || product.IsDeleted)
                return false;

            product.IsDeleted = true;
            await _productRepository.UpdateAsync(product);
            return true;
        }
    }
}

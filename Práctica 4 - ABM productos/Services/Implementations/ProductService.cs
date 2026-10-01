using Practica4.Entities;
using Practica4.Models.DTOs.Requests;
using Practica4.Models.DTOs.Responses;
using Practica4.Repositories.Implementations;
using Practica4.Services.Interfaces;

namespace Practica4.Services.Implementations
{
    public class ProductService : IProductService
    {
        private ProductRepository _repository = new ProductRepository();

        public List<ProductForReadDto> GetAllProducts()
        {
            var products = _repository.GetAllProducts();
            var dtos = new List<ProductForReadDto>();

            foreach (var p in products)
            {
                dtos.Add(new ProductForReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });
            }

            return dtos;
        }

        public ProductForReadDto? GetProductById(int id)
        {
            var p = _repository.GetProductById(id);
            if (p == null) return null;

            return new ProductForReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            };
        }

        public ProductForReadDto CreateProduct(ProductForCreateDto dto)
        {
            // Mapeo de Request DTO a Entidad
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };

            _repository.AddProduct(product);

            // Mapeo de Entidad a Response DTO
            return new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public void UpdateProduct(int id, ProductForUpdateDto dto)
        {
            var product = _repository.GetProductById(id);
            if (product != null)
            {
                product.Name = dto.Name;
                product.Price = dto.Price;
                _repository.UpdateProduct(product);
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _repository.GetProductById(id);
            if (product != null)
            {
                _repository.DeleteProduct(product);
            }
        }
    }
}
using Practica4.Entities;
using Practica4.Models.DTOs.Requests;
using Practica4.Models.DTOs.Responses;
using Practica4.Repositories.Implementations;
using Practica4.Repositories.Interfaces;
using Practica4.Services.Interfaces;

namespace Practica4.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

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
            
            var existingProducts = _repository.GetAllProducts();
            bool nameExists = existingProducts.Any(p => p.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase));

            if (nameExists)
            {
                
                throw new InvalidOperationException("Ya existe un producto con ese nombre.");
            }

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };

            _repository.AddProduct(product);

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

        public List<ProductForReadDto> SearchProductsByName(string name)
        {
            var products = _repository.SearchProductsByName(name);
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

        public ProductStatsDto GetStats()
        {
            var products = _repository.GetAllProducts();

            
            if (products == null || !products.Any())
            {
                return new ProductStatsDto
                {
                    Total = 0,
                    AveragePrice = 0,
                    MostExpensiveName = "No hay productos"
                };
            }

            return new ProductStatsDto
            {
                Total = products.Count(),
                AveragePrice = products.Average(p => p.Price),
                MostExpensiveName = products.OrderByDescending(p => p.Price).First().Name
            };
        }
    }
}
                    
    
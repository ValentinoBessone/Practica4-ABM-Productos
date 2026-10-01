using Practica4.Models.DTOs.Requests;
using Practica4.Models.DTOs.Responses;

namespace Practica4.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductForReadDto> GetAllProducts();
        ProductForReadDto? GetProductById(int id);
        ProductForReadDto CreateProduct(ProductForCreateDto dto);
        void UpdateProduct(int id, ProductForUpdateDto dto);
        void DeleteProduct(int id);
    }
}

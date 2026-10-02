using Practica4.Entities;

namespace Practica4.Repositories.Interfaces
{
    public interface IProductRepository 
    {
        List<Product> GetAllProducts();
        Product? GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        List<Product> SearchProductsByName(string name);

    }
}

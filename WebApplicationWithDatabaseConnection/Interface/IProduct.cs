using WebApplicationWithDatabaseConnection.Models;
using static WebApplicationWithDatabaseConnection.Interface.IProduct;

namespace WebApplicationWithDatabaseConnection.Interface
{
    public interface IProduct
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(int id);
        public Task<Product> CreateProduct(Product product);
        public Task <Product>UpdateProduct(Product product);
        public String DeleteProduct(String id);
    }
}

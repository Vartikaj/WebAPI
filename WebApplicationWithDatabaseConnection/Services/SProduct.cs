using Dapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Data;
using WebApplicationWithDatabaseConnection.Context;
using WebApplicationWithDatabaseConnection.Interface;
using WebApplicationWithDatabaseConnection.Models;

namespace WebApplicationWithDatabaseConnection.Services
{
    public class SProduct : IProduct
    {
        private readonly DapperContext _context;
        private List<Product> _productItems;

        public SProduct(DapperContext context)
        {
            _productItems = new List<Product>();
            _context = context;
        }

        public Product AddProduct(Product productItem)
        {

            _productItems.Add(productItem);
            return productItem;
        }

        public async Task<Product>  CreateProduct(Product _product)
        {
            try
            {
                var query = "INSERT INTO product(name, brand) VALUES(@name, @brand)";
                var parameters = new DynamicParameters();
                parameters.Add("name", _product.name, DbType.String);
                parameters.Add("brand", _product.brand, DbType.String);

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters);
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message); 
            }
            return _product;
        }

        public async Task<Product> UpdateProduct(Product _product)
        {
            try
            {
                var query = "UPDATE product SET name=@name, brand=@brand WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("id", _product.Id, DbType.Int32);
                parameters.Add("name", _product.name, DbType.String);
                parameters.Add("brand", _product.brand, DbType.String);

                using(var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters);
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return _product;
        }

        public string DeleteProduct(string id)
        {
            for (var index = _productItems.Count - 1; index >= 0; index--)
            {
                /*if (_productItems[index].Id == id)
                {
                    _productItems.RemoveAt(index);
                }*/
            }
            return id;
        }

        public async Task<Product> GetProduct(int id)
        {
            var query = "SELECT * FROM product where Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var product = await connection.QuerySingleOrDefaultAsync<Product>(query, new {id});
                return product;
            }
        }

        async Task<IEnumerable<Product>> IProduct.GetProducts()
        {
            var query = "Select * FROM product";
            using (var connection = _context.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }
    }
}

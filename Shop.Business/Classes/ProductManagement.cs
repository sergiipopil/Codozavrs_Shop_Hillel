using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Classes
{
    public class ProductManagement
    {
        private DapperORM _dapper;
        public ProductManagement()
        {
            _dapper = new DapperORM();
        }
        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            return await _dapper.ExecReturnList<ProductDto>("GetAllProducts", null);
        }
        public async Task<ProductDto> GetProductById(int id)
        {
            return await _dapper.ExecReturnObject<ProductDto>("GetProductById", new { Id = id });
        }
        public async void AddProduct(ProductDto product)
        {
            await _dapper.ExecWithoutReturn("CreateProduct",
                       new
                       {
                           product.Title,
                           product.Price,
                           product.Count,
                           product.Weight,
                           product.Production,
                           product.Expiration
                       });
        }
        public async void DeleteProduct(int id)
        {
            await _dapper.ExecWithoutReturn("DeleteProductById",
                   new
                   {
                       Id = id
                   });
        }
        public async void EditProduct(ProductDto product)
        {
            await _dapper.ExecWithoutReturn("UpdateProductById",
                        new
                        {
                            product.Id,
                            product.Title,
                            product.Price,
                            product.Count,
                            product.Weight,
                            product.Production,
                            product.Expiration,
                        });
        }
    }
}

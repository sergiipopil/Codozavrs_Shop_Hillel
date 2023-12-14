using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Classes
{
    interface IProductManager
    {
        public void ShowProductsList();
        public void GetProductDetail(string title);
        public void GetProductDetail(int id);
        public void AddNewProduct(Product product);
        public bool DeleteProduct(int productId);
        public bool DeleteProduct(string title);
        public bool DeleteExpirationProducts();
    }
}

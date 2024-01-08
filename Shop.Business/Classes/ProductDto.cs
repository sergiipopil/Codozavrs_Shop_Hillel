using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Classes
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int? Weight { get; set; }
        public string Production { get; set; }
        public DateTime? Expiration { get; set; }
    }
}

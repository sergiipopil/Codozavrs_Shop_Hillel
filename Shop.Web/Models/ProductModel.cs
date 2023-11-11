using System.ComponentModel.DataAnnotations;

namespace ShopWeb.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Count { get; set; }
        public int? Weight { get; set; }
        public string Production { get; set; }
        public DateTime? Expiration { get; set; }
    }
}

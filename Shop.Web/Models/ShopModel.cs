namespace Shop.Web.Models
{
    public class ShopModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public bool IsOpened { get; set; }
    }
}

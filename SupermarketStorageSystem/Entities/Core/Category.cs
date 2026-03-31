namespace SupermarketStorageSystem.Entities.Core
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; } // Навігаційна властивість
    }
}
namespace SupermarketStorageSystem.Entities.Core
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Barcode { get; set; }
        public decimal PurchasePrice { get; set; }
        public int CurrentStock { get; set; }
        public int MinStockLevel { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
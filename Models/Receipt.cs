namespace ReceiptProcessorAPI.Models
{
    public class Receipt
    {
        public string Retailer { get; set; } = string.Empty;
        public string PurchaseDate { get; set; } = string.Empty;
        public string PurchaseTime { get; set; } = string.Empty;
        public List<Item> Items { get; set; } = new List<Item>();
        public string Total { get; set; } = string.Empty;
    }

}

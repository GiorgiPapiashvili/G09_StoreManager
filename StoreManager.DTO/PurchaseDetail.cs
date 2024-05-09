namespace StoreManager.DTO
{
    public class PurchaseDetail
    {
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}

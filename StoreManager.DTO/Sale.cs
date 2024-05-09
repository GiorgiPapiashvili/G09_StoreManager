namespace StoreManager.DTO
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public SignStatus Status { get; set; }
        public DateTime SaleDate { get; set; }
    }
}

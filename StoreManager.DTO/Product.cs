namespace StoreManager.DTO
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int EmployeeId { get; set; } 
        public string ProductCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

namespace StoreManager.DTO
{
    public class User
    {
        public int EmployeeId { get; set; }
        public string UserName { get; set; } = null!;
        public string? Password { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
    }
}

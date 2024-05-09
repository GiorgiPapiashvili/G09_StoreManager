namespace StoreManager.DTO
{
    public class Remain
    {
        public int RemainID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}

using StoreManager.DTO;

namespace StoreManager.Service.Interfaces.Services
{
    public interface ISaleService
    {
        int CreateSale(Sale sale, IEnumerable<SaleDetail> saleDetails);
        Sale Get(int id);
        void Update(Sale sale);
    }
}

using StoreManager.DTO;

namespace StoreManager.Service.Interfaces.Services
{
    public interface IPurchaseService
    {
        int CreatePurchase(Purchase purchase, IEnumerable<PurchaseDetail> purchaseDetails);
        Purchase Get(int id);
        void Update(Purchase item);
    }
}

using StoreManager.DTO;
using StoreManager.Service.Interfaces.Repositories;
using StoreManager.Service.Interfaces.Services;
using System.Data;

namespace StoreManager.Service
{
    public sealed class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork<IDbConnection> _unitOfWork;

        public PurchaseService(IUnitOfWork<IDbConnection> unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException(nameof(unitOfWork));
        }

        public int CreatePurchase(Purchase purchase, IEnumerable<PurchaseDetail> purchaseDetails)
        {
            if (purchase == null) throw new ArgumentNullException(nameof(purchase));
            if (purchaseDetails == null) throw new ArgumentNullException(nameof(purchaseDetails));

            try
            {
                _unitOfWork.BeginTransaction();
                int id = _unitOfWork.PurchaseRepository.Insert(purchase);
                purchaseDetails.ToList().ForEach(pd => _unitOfWork.PurchaseDetailRepository.Insert(pd));
                _unitOfWork.CommitTransaction();
                return id;
            }
            catch
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }

        public Purchase Get(int id)
        {
            return _unitOfWork.PurchaseRepository.Get(id);
        }

        public void Update(Purchase purchase)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.PurchaseRepository.Update(purchase);
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }
    }
}

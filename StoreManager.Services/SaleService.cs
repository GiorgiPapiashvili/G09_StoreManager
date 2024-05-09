using StoreManager.DTO;
using StoreManager.Service.Interfaces.Repositories;
using StoreManager.Service.Interfaces.Services;
using System.Data.Common;

namespace StoreManager.Service
{
    public sealed class SaleService : ISaleService
    {
        private readonly IUnitOfWork<DbConnection> _unitOfWork;

        public SaleService(IUnitOfWork<DbConnection> unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException(nameof(unitOfWork));
        }

        public int CreateSale(Sale sale, IEnumerable<SaleDetail> saleDetails)
        {

            try
            {
                _unitOfWork.BeginTransaction();
                int saleId = _unitOfWork.SaleRepository.Insert(sale);
                saleDetails.ToList().ForEach(sd => _unitOfWork.SaleDetailRepository.Insert(sd));
                _unitOfWork.CommitTransaction();
                return saleId;
            }
            catch
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }


        public Sale Get(int id)
        {
            return _unitOfWork.SaleRepository.Get(id);
        }

        public void Update(Sale sale)
        {

            if (sale == null) throw new ArgumentNullException(nameof(sale));

            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.SaleRepository.Update(sale);
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

using StoreManager.DTO;
using StoreManager.Service.Interfaces.Repositories;
using StoreManager.Service.Interfaces.Services;
using System.Data;
using System.Linq.Expressions;


namespace StoreManager.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork<IDbConnection> _unitOfWork;

        public SupplierService(IUnitOfWork<IDbConnection> unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException(nameof(unitOfWork));
        }

        public void Delete(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.SupplierRepository.Delete(id);
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }

        public Supplier Get(int id)
        {
            return _unitOfWork.SupplierRepository.Get(id);
        }

        public int Insert(Supplier employee)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                int id = _unitOfWork.SupplierRepository.Insert(employee);
                _unitOfWork.CommitTransaction();
                return id;
            }
            catch
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }

        public IEnumerable<Supplier> Load(Expression<Func<Supplier, bool>>? predicate = null)
        {
            return _unitOfWork.SupplierRepository.Load(predicate);
        }

        public void Update(Supplier employee)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.SupplierRepository.Update(employee);
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

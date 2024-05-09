using StoreManager.DTO;
using StoreManager.Service.Interfaces.Repositories;
using StoreManager.Service.Interfaces.Services;
using System.Data;
using System.Linq.Expressions;

namespace StoreManager.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork<IDbConnection> _unitOfWork;

        public EmployeeService(IUnitOfWork<IDbConnection> unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException(nameof(unitOfWork));
        }

        public void CreateEmployeeType(EmployeeType employeeType)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.EmployeeTypeRepository.Insert(employeeType);
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.EmployeeRepository.Delete(id);
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }

        public Employee Get(int id)
        {
            return _unitOfWork.EmployeeRepository.Get(id);
        }

        public int Insert(Employee employee)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                int id =_unitOfWork.EmployeeRepository.Insert(employee);
                _unitOfWork.CommitTransaction();
                return id;
            }
            catch
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }

        public IEnumerable<Employee> Load(Expression<Func<Employee, bool>>? predicate = null)
        {
            return _unitOfWork.EmployeeRepository.Load(predicate);
        }

        public void Update(Employee employee)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.EmployeeRepository.Update(employee);
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

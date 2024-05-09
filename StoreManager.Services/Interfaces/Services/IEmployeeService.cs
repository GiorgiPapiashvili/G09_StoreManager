using StoreManager.DTO;
using System.Linq.Expressions;

namespace StoreManager.Service.Interfaces.Services
{
    public interface IEmployeeService
    {
        int Insert(Employee employee);
        void CreateEmployeeType(EmployeeType employeeType);
        void Update(Employee employee);
        void Delete(int id);
        Employee Get(int id);
        IEnumerable<Employee> Load(Expression<Func<Employee, bool>>? predicate = null);
    }
}

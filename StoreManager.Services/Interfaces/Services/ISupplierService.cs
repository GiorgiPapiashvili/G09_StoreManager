using StoreManager.DTO;
using System.Linq.Expressions;

namespace StoreManager.Service.Interfaces.Services
{
    public interface ISupplierService
    {
        int Insert(Supplier supplier);
        void Update(Supplier supplier);
        void Delete(int id);
        Supplier Get(int id);
        IEnumerable<Supplier> Load(Expression<Func<Supplier, bool>>? predicate = null);
    }
}


using StoreManager.DTO;
using System.Linq.Expressions;

namespace StoreManager.Service.Interfaces.Services
{
    public interface IProductService
    {
        int Insert(Product item);
        void Update(Product item);
        void Delete(int id);
        Product Get(int id);
        IEnumerable<Product> Load(Expression<Func<Product, bool>>? predicate = null);
    }
}

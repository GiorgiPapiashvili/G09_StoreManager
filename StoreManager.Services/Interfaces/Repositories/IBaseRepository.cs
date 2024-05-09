using System.Linq.Expressions;

namespace StoreManager.Service.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        T Get(int id);
        IEnumerable<T> Load(Expression<Func<T, bool>>? predicate = null);
        int Insert(T item);
        void Update(T item);
        void Delete(int id);
    }
}

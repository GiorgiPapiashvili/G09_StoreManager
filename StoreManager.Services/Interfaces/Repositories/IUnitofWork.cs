using System.Data;

namespace StoreManager.Service.Interfaces.Repositories
{
    public interface IUnitOfWork<Tconnection> : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        ICountryRepository CountryRepository { get; }
        ICityRepository CityRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IEmployeeTypeRepository EmployeeTypeRepository { get; }
        IProductRepository ProductRepository { get; }
        IPurchaseRepository PurchaseRepository { get; }
        IPurchaseDetailRepository PurchaseDetailRepository { get; }
        ISaleRepository SaleRepository { get; }
        ISaleDetailRepository SaleDetailRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IRemainRepository RemainRepostory { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        IDbConnection GetConnection();
        new void Dispose();
    }
}

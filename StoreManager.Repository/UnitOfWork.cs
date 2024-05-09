using StoreManager.Service.Interfaces.Repositories;
using System.Data;

namespace StoreManager.Repository
{
	public sealed class UnitOfWork<TConnection> : IUnitOfWork<TConnection>
		where TConnection : IDbConnection, new()
	{
		private readonly string _connectionString;
		private TConnection? _connection;
		private IDbTransaction? _transaction;
		private bool _isDisposed;

		private readonly Lazy<ICategoryRepository> _categoryRepository;
		private readonly Lazy<ICountryRepository> _countryRepository;
		private readonly Lazy<ICityRepository> _cityRepository;
		private readonly Lazy<ICustomerRepository> _customerRepository;
		private readonly Lazy<IEmployeeRepository> _employeeRepository;
		private readonly Lazy<IEmployeeTypeRepository> _employeeTypeRepository;
		private readonly Lazy<IProductRepository> _productRepository;
		private readonly Lazy<IPurchaseRepository> _purchaseRepository;
		private readonly Lazy<IPurchaseDetailRepository> _purchaseDetailRepository;
		private readonly Lazy<ISaleRepository> _saleRepository;
		private readonly Lazy<ISaleDetailRepository> _saleDetailRepository;
		private readonly Lazy<ISupplierRepository> _supplierRepository;
		private readonly Lazy<IUserRepository> _userRepository;
		private readonly Lazy<IRoleRepository> _roleRepository;
		private readonly Lazy<IUserRoleRepository> _userRoleRepository;
		private readonly Lazy<IRemainRepository> _remainRepository;

        public static Func<int>? GetEmployeeID;

		public UnitOfWork(string connectionString)
        {
            int? employeeID = GetEmployeeID?.Invoke();
			_connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
			_categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(GetConnection(), _transaction, employeeID));
			_countryRepository = new Lazy<ICountryRepository>(() => new CountryRepository(GetConnection(), _transaction));
			_cityRepository = new Lazy<ICityRepository>(() => new CityRepository(GetConnection(), _transaction));
			_customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(GetConnection(), _transaction));
			_employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(GetConnection(), _transaction));
			_employeeTypeRepository = new Lazy<IEmployeeTypeRepository>(() => new EmployeeTypeRepository(GetConnection(), _transaction));
			_productRepository = new Lazy<IProductRepository>(() => new ProductRepository(GetConnection(), _transaction));
			_purchaseRepository = new Lazy<IPurchaseRepository>(() => new PurchaseRepository(GetConnection(), _transaction));
			_purchaseDetailRepository = new Lazy<IPurchaseDetailRepository>(() => new PurchaseDetailRepository(GetConnection(), _transaction));
			_saleRepository = new Lazy<ISaleRepository>(() => new SaleRepository(GetConnection(), _transaction));
			_saleDetailRepository = new Lazy<ISaleDetailRepository>(() => new SaleDetailRepository(GetConnection(), _transaction));
			_supplierRepository = new Lazy<ISupplierRepository>(() => new SupplierRepository(GetConnection(), _transaction));
			_userRepository = new Lazy<IUserRepository>(() => new UserRepository(GetConnection(), _transaction));
			_roleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(GetConnection(), _transaction));
			_userRoleRepository = new Lazy<IUserRoleRepository>(() => new UserRoleRepository(GetConnection(), _transaction));
			_remainRepository = new Lazy<IRemainRepository>(() => new RemainRepository(GetConnection(), _transaction));
		}

		public ICategoryRepository CategoryRepository => _categoryRepository.Value;

		public ICountryRepository CountryRepository => _countryRepository.Value;

		public ICityRepository CityRepository => _cityRepository.Value;

		public ICustomerRepository CustomerRepository => _customerRepository.Value;

		public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

		public IEmployeeTypeRepository EmployeeTypeRepository => _employeeTypeRepository.Value;

		public IProductRepository ProductRepository => _productRepository.Value;

		public IPurchaseRepository PurchaseRepository => _purchaseRepository.Value;

		public IPurchaseDetailRepository PurchaseDetailRepository => _purchaseDetailRepository.Value;

		public ISaleRepository SaleRepository => _saleRepository.Value;

		public ISaleDetailRepository SaleDetailRepository => _saleDetailRepository.Value;

		public ISupplierRepository SupplierRepository => _supplierRepository.Value;

		public IUserRepository UserRepository => _userRepository.Value;

		public IRoleRepository RoleRepository => _roleRepository.Value;

		public IUserRoleRepository UserRoleRepository => _userRoleRepository.Value;

		public IRemainRepository RemainRepostory => _remainRepository.Value;

		public void BeginTransaction()
		{
			if (_transaction != null)
			{
				throw new InvalidOperationException("Transaction is already started!");
			}

			_transaction = GetConnection().BeginTransaction();
		}

		public void CommitTransaction()
		{
			if (_transaction == null)
			{
				throw new InvalidOperationException("Transaction is not started!");
			}

			_transaction.Commit();
			_transaction = null;
		}

		public void RollBackTransaction()
		{
			if (_transaction == null)
			{
				throw new InvalidOperationException("Transaction is not started!");
			}

			_transaction.Rollback();
			_transaction = null;
		}

		public IDbConnection GetConnection()
		{
			if (_connection == null)
			{
				_connection = new TConnection();
				_connection.ConnectionString = _connectionString;
				_connection.Open();
			}

			return _connection;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				if (disposing)
				{
					_connection?.Dispose();
					_transaction?.Dispose();
				}

				_transaction = null;
				_connection = default;
			}

			_isDisposed = true;
		}

		~UnitOfWork()
		{
			Dispose(false);
		}
	}
}

using StoreManager.DTO;
using StoreManager.Service.Interfaces.Repositories;
using StoreManager.Service.Interfaces.Services;
using System.Data;
using System.Linq.Expressions;

namespace StoreManager.Service
{
    public sealed class ProductService : IProductService
    {     
        private readonly IUnitOfWork<IDbConnection> _unitOfWork;

        public ProductService(IUnitOfWork<IDbConnection> unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new NullReferenceException(nameof(unitOfWork));
        }

        public int Insert(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            try
            {
                _unitOfWork.BeginTransaction();
                int id = _unitOfWork.ProductRepository.Insert(product);
                _unitOfWork.CommitTransaction();
                return id;
            }
            catch
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }

        public int CreateCategory(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            try
            {
                _unitOfWork.BeginTransaction();
                int id = _unitOfWork.CategoryRepository.Insert(category);
                _unitOfWork.CommitTransaction();
                return id;
            }
            catch
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }

        public void Insert(IEnumerable<Product> products)
        {
            if (products == null) throw new ArgumentNullException(nameof(products));

            try
            {
                _unitOfWork.BeginTransaction();
                products
                    .ToList()
                    .ForEach(p => _unitOfWork.ProductRepository.Insert(p));
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }

        public void Update(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.ProductRepository.Update(product);
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
                _unitOfWork.ProductRepository.Delete(id);
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollBackTransaction();
                throw;
            }
        }

        public Product Get(int id)
        {
            return _unitOfWork.ProductRepository.Get(id);
        }

        public IEnumerable<Product> Load(Expression<Func<Product, bool>>? predicate = null)
        {
            return _unitOfWork.ProductRepository.Load(predicate);
        }
    }
}

using StoreManager.DTO;
using StoreManager.Service.Interfaces.Repositories;
using System.Data;

namespace StoreManager.Repository;

internal class EmployeeTypeRepository : BaseRepository<EmployeeType>, IEmployeeTypeRepository
{
    public EmployeeTypeRepository(IDbConnection connection, IDbTransaction? transaction = null) : base(connection, transaction)
    {
      
    }
}

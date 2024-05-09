using StoreManager.DTO;
using StoreManager.Service.Interfaces.Repositories;
using System.Data;

namespace StoreManager.Repository;

internal class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(IDbConnection connection, IDbTransaction? transaction = null) : base(connection, transaction)
    {
        
    }
    protected override IEnumerable<string> IgnoredPropertiesForInsert
    {
        get
        {
            List<string> salesIgnoredProperties = base.IgnoredPropertiesForInsert.ToList();

            salesIgnoredProperties.Add("SaleDate");

            return salesIgnoredProperties;
        }
    }

    protected override IEnumerable<string> IgnoredPropertiesForUpdate
    {
        get
        {
            List<string> salesIgnoredProperties = base.IgnoredPropertiesForUpdate.ToList();
            salesIgnoredProperties.AddRange(new List<string>() { "SaleDate", "CustomerId", "EmployeeId" });

            return salesIgnoredProperties;
        }
    }
}

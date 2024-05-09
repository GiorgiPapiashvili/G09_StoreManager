using Dapper;
using StoreManager.DTO;
using StoreManager.Service.Interfaces.Repositories;
using System.Data;

namespace StoreManager.Repository;

internal class PurchaseDetailRepository : BaseRepository<PurchaseDetail>, IPurchaseDetailRepository
{
    public PurchaseDetailRepository(IDbConnection connection, IDbTransaction? transaction = null) : base(connection, transaction)
    {
        
    }
    public override int Insert(PurchaseDetail item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("PurchaseId", item.PurchaseId, DbType.Int32);
        parameters.Add("ProductId", item.ProductId, DbType.Int32);
        parameters.Add("UnitPrice", item.UnitPrice, DbType.Decimal);
        parameters.Add("Quantity", item.Quantity, DbType.Int32);

        _connection.Execute("sp_InsertPurchaseDetail", parameters, commandType: CommandType.StoredProcedure);

        return item.PurchaseId;
    }
}

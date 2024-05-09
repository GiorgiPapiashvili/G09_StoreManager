using Dapper;
using StoreManager.DTO;
using StoreManager.Service.Interfaces.Repositories;
using System.Data;

namespace StoreManager.Repository;

internal class SaleDetailRepository : BaseRepository<SaleDetail>, ISaleDetailRepository
{
    public SaleDetailRepository(IDbConnection connection, IDbTransaction? transaction = null) : base(connection, transaction)
    {
        
    }
    public override int Insert(SaleDetail item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("SaleId", item.SaleId, DbType.Int32);
        parameters.Add("ProductId", item.ProductId, DbType.Int32);
        parameters.Add("UnitPrice", item.UnitPrice, DbType.Decimal);
        parameters.Add("Quantity", item.Quantity, DbType.Int32);

        _connection.Execute("sp_InsertSaleDetail", parameters, commandType: CommandType.StoredProcedure);
        return item.SaleId;
    }
}

using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests;

[Collection("Database collection")]
public class SaleRepositoryTest : RepositoryTestBase
{
    private DatabaseFixture _fixture;
    private readonly IUnitOfWork<SqlConnection> _unitOfWork;

    public SaleRepositoryTest(DatabaseFixture fixture)
    {
        this._fixture = fixture;
        UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
        _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    }

    [Theory]
    [InlineData(1, 1, SignStatus.InProgress)]
    [InlineData(2, 1, SignStatus.Completed)]
    [InlineData(1, 2, SignStatus.Canceled)]
    public void InsertSales(int customerId, int employeeId, SignStatus status)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Sale sale = new() { CustomerId = customerId, EmployeeId = employeeId, Status = status };

        int id = unitOfWork.SaleRepository.Insert(sale);

        Assert.NotEqual(0, id);
        Sale retriavedSale = unitOfWork.SaleRepository.Get(id);
        Assert.Equal(retriavedSale.EmployeeId, sale.EmployeeId);
        Assert.Equal(retriavedSale.CustomerId, sale.CustomerId);
        Assert.Equal(retriavedSale.Status, sale.Status);
    }

    [Theory]
    [InlineData(1, SignStatus.Canceled)]
    [InlineData(2, SignStatus.Canceled)]
    [InlineData(3, SignStatus.Canceled)]
    public void UpdateSales(int saleId, SignStatus status)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Sale updatedSale = new() { SaleId = saleId, Status = status };
        unitOfWork.SaleRepository.Update(updatedSale);

        Sale retriavedSale = unitOfWork.SaleRepository.Get(saleId);
        Assert.Equal(retriavedSale.Status, updatedSale.Status);

    }
}

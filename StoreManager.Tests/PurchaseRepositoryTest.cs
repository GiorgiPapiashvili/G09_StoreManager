using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests;

[Collection("Database collection")]
public class PurchaseRepositoryTest : RepositoryTestBase
{
    private DatabaseFixture _fixture;
    private readonly IUnitOfWork<SqlConnection> _unitOfWork;

    public PurchaseRepositoryTest(DatabaseFixture fixture)
    {
        this._fixture = fixture;
        UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
        _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    }

    [Theory]
    [InlineData(1, 1, SignStatus.InProgress)]
    [InlineData(2, 2, SignStatus.Canceled)]
    [InlineData(3, 1, SignStatus.Completed)]
    public void InsertPurchase(int employeeId, int supplierId, SignStatus status)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Purchase purchase = new() { EmployeeId = employeeId, SupplierId = supplierId, Status = status };

        int id = unitOfWork.PurchaseRepository.Insert(purchase);

        Assert.NotEqual(0, id);
        Purchase retriavedPurchase = unitOfWork.PurchaseRepository.Get(id);
        Assert.Equal(retriavedPurchase.SupplierId, purchase.SupplierId);
        Assert.Equal(retriavedPurchase.EmployeeId, purchase.EmployeeId);
        Assert.Equal(retriavedPurchase.Status, purchase.Status);
    }

    [Theory]
    [InlineData(1, SignStatus.Canceled)]
    [InlineData(2, SignStatus.Canceled)]
    [InlineData(3, SignStatus.Canceled)]
    public void UpdatePurchase(int purchaseId, SignStatus signStatus)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);

        Purchase updatedPurchase = new() { PurchaseId = purchaseId, Status = signStatus};
        unitOfWork.PurchaseRepository.Update(updatedPurchase);

        Purchase retriavedPurchase = unitOfWork.PurchaseRepository.Get(purchaseId);
        Assert.Equal(retriavedPurchase.Status, updatedPurchase.Status);
    }

}

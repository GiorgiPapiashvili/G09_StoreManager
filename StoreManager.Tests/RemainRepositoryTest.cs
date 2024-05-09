using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests;

[Collection("collection")]
public class RemainRepositoryTest : RepositoryTestBase
{
    private DatabaseFixture _fixture;
    private readonly IUnitOfWork<SqlConnection> _unitOfWork;

    public RemainRepositoryTest(DatabaseFixture fixture)
    {
        this._fixture = fixture;
        UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
        _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    }

    [Theory]
    [InlineData(1, 1)]
    public void InsertRemain(int productId, int quantity)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Remain remain = new Remain() { ProductID = productId, Quantity = quantity };

        int id = unitOfWork.RemainRepostory.Insert(remain);

        Assert.NotEqual(0, id);
        Remain retriavedRemain = unitOfWork.RemainRepostory.Get(id);
        Assert.Equal(remain.ProductID, retriavedRemain.ProductID);
        Assert.Equal(remain.Quantity, retriavedRemain.Quantity);
    }

    [Fact]
    public void UpdateRemain()
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Remain remain = new Remain() { ProductID = 1, Quantity = 1 };

        int id = unitOfWork.RemainRepostory.Insert(remain);

        Remain updatedRemain = new Remain() { RemainID = id, ProductID = 2, Quantity = 2 };
        unitOfWork.RemainRepostory.Update(updatedRemain);

        Remain retrievedRemain = unitOfWork.RemainRepostory.Get(id);
        Assert.Equal(retrievedRemain.ProductID, updatedRemain.ProductID);
        Assert.Equal(retrievedRemain.Quantity, updatedRemain.Quantity);
    }

    [Fact]
    public void DeleteRemain()
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Remain remain = new Remain() { ProductID = 1, Quantity = 1 };

        int id = unitOfWork.RemainRepostory.Insert(remain);

        Remain retriavedRemain = unitOfWork.RemainRepostory.Get(id);
        Assert.Equal(remain.ProductID, retriavedRemain.ProductID);
        Assert.Equal(remain.Quantity, retriavedRemain.Quantity);

        unitOfWork.RemainRepostory.Delete(id);

        Assert.Throws<InvalidOperationException>(() => unitOfWork.RemainRepostory.Get(id));
    }

    public override void Dispose()
    {
        SqlCommand command = _connection.CreateCommand();
        _connection.Open();
        command.CommandText = "Delete categories; DBCC CHECKIDENT ('categories', RESEED, 0)" +
            "Delete products; DBCC CHECKIDENT ('products', RESEED, 0)]" +
            "Delete remains; DBCC CHECKIDENT ('remains', RESEED, 0)]";
        command.ExecuteNonQuery();
        _connection.Dispose();
    }
}

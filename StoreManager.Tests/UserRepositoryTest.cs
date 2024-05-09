using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests;

[Collection("Database collection")]
public class UserRepositoryTest : RepositoryTestBase
{
    private DatabaseFixture _fixture;
    private readonly IUnitOfWork<SqlConnection> _unitOfWork;

    public UserRepositoryTest(DatabaseFixture fixture)
    {
        this._fixture = fixture;
        UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
        _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    }

    [Fact]
    public void InsertUser()
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        User user = new User() { EmployeeId = 1, UserName = "Username1", Password = "Password1" };

        int id = unitOfWork.UserRepository.Insert(user);

        Assert.NotEqual(0, id);
        User retrievedUser = unitOfWork.UserRepository.Get(id);
        Assert.Equal(user.UserName, retrievedUser.UserName);
        Assert.Equal(user.Password, retrievedUser.Password);
    }

    public override void Dispose()
    {
        SqlCommand command = _connection.CreateCommand();
        command.CommandText = "Delete Users " +
                        "Delete Employees;  DBCC CHECKIDENT('Employees', RESEED, 0) " +
                        "Delete Cities;  DBCC CHECKIDENT('Cities', RESEED, 0) " +
                        "Delete Countries; DBCC CHECKIDENT('Countries', RESEED, 0) " +
                        "Delete EmployeeTypes;  DBCC CHECKIDENT('EmployeeTypes', RESEED, 0)";

        command.ExecuteNonQuery();
        base.Dispose();
    }
}

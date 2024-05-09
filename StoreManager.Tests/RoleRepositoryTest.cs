using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests;

[Collection("collection")]
public class RoleRepositoryTest : RepositoryTestBase
{
    private DatabaseFixture _fixture;
    private readonly IUnitOfWork<SqlConnection> _unitOfWork;

    public RoleRepositoryTest(DatabaseFixture fixture)
    {
        this._fixture = fixture;
        UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
        _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    }

    [Theory]
    [InlineData("Role1", "111")]
    [InlineData("Roele2", "222")]
    [InlineData("Roele3", "333")]
    public void InsertRole(string roleName, string roleCode)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Role role = new Role() { RoleName = roleName, RoleCode = roleCode };

        int id = unitOfWork.RoleRepository.Insert(role);

        Assert.NotEqual(0, id);
        Role retreivedRole = unitOfWork.RoleRepository.Get(id);
        Assert.Equal(retreivedRole.RoleName, role.RoleName);
        Assert.Equal(retreivedRole.RoleCode, role.RoleCode);
    }
    [Fact]
    public void UpdateRole()
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Role role = new Role() { RoleName = "Role1", RoleCode = "111" };

        int id = unitOfWork.RoleRepository.Insert(role);

        Role updatedRole = new Role() { RoleID = id, RoleName = "Role2", RoleCode = "222" };
        unitOfWork.RoleRepository.Update(updatedRole);

        Role retreivedRole = unitOfWork.RoleRepository.Get(id);
        Assert.Equal(retreivedRole.RoleName, updatedRole.RoleName);
        Assert.Equal(retreivedRole.RoleCode, updatedRole.RoleCode);
    }

    [Fact]
    public void DeleteRole()
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Role role = new Role() { RoleName = "Role1", RoleCode = "111" };

        int id = unitOfWork.RoleRepository.Insert(role);

        Role retreivedRole = unitOfWork.RoleRepository.Get(id);
        Assert.Equal(retreivedRole.RoleName, role.RoleName);
        Assert.Equal(retreivedRole.RoleCode, role.RoleCode);

        unitOfWork.RoleRepository.Delete(id);

        Assert.Throws<InvalidOperationException>(() => unitOfWork.RoleRepository.Get(id));
    }

    public override void Dispose()
    {
        SqlCommand command = _connection.CreateCommand();
        _connection.Open();
        command.CommandText = "Delete roles; DBCC CHECKIDENT ('roles', RESEED, 0)";
        command.ExecuteNonQuery();
        _connection.Dispose();
    }
}


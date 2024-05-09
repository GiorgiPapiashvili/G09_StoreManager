using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests;

[Collection("Database collection")]
public class EmployeeTypeRepositoryTest : RepositoryTestBase
{
    private DatabaseFixture _fixture;
    private readonly IUnitOfWork<SqlConnection> _unitOfWork;

    public EmployeeTypeRepositoryTest(DatabaseFixture fixture)
    {
        this._fixture = fixture;
        UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
        _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    }

    [Theory]
    [InlineData("Emptype1")]
    [InlineData("Emptype2")]
    [InlineData("Emptype3")]
    public void InsertEmployeeType(string name)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        EmployeeType employeeType = new() { Name = name };

        int id = unitOfWork.EmployeeTypeRepository.Insert(employeeType);

        Assert.NotEqual(0, id);
        EmployeeType retriavedEmployeeType = unitOfWork.EmployeeTypeRepository.Get(id);
        Assert.Equal(employeeType.Name, retriavedEmployeeType.Name);
    }

    [Theory]
    [InlineData(1, "Type1", "description1")]
    [InlineData(2, "Type2", "description2")]
    public void UpdateEmployeeType(int employeeTypeId, string name, string description)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);

        EmployeeType updatedEmployeeType = new() { EmployeeTypeId = employeeTypeId, Name = name, Description = description };
        unitOfWork.EmployeeTypeRepository.Update(updatedEmployeeType);

        EmployeeType retriavedtype = unitOfWork.EmployeeTypeRepository.Get(employeeTypeId);
        Assert.Equal(retriavedtype.Name, updatedEmployeeType.Name);
        Assert.Equal(retriavedtype.Description, updatedEmployeeType.Description);
    }

    [Theory]
    [InlineData(4, "Type4")]
    [InlineData(3, "Type3")]
    public void DeleteEmployeeType(int employeeTypeId, string name)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
     
        EmployeeType retriavedEmployeeType = unitOfWork.EmployeeTypeRepository.Get(employeeTypeId);
        Assert.Equal(employeeTypeId, retriavedEmployeeType.EmployeeTypeId);
        Assert.Equal(name, retriavedEmployeeType.Name);

        unitOfWork.EmployeeTypeRepository.Delete(employeeTypeId);

        Assert.Throws<InvalidOperationException>(() => unitOfWork.EmployeeTypeRepository.Get(employeeTypeId));
    }
}

using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests;

[Collection("Database collection")]
public class EmployeeRepositoryTest : RepositoryTestBase
{
    private DatabaseFixture _fixture;
    private readonly IUnitOfWork<SqlConnection> _unitOfWork;

    public EmployeeRepositoryTest(DatabaseFixture fixture)
    {
        this._fixture = fixture;
        UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
        _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    }

    [Theory]
    [InlineData(1, 2, "FirstName1", "LastName1", "111", "@Email1", "Phone1")]
    [InlineData(2, 1, "FirstName2", "LastName2", "222", "@Email2", "Phone2")]
    [InlineData(3, 2, "FirstName3", "LastName3", "333", "@Email3", "Phone3")]
    public void InsertEmployee(int employeeType, int cityId, string firstName, string lastName, string identityNumber, string email, string phone)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Employee employee = new() { EmployeeTypeId = employeeType, CityId = cityId, FirstName = firstName, LastName = lastName, IdentityNumber = identityNumber, Email = email, Phone = phone };

        int id = unitOfWork.EmployeeRepository.Insert(employee);

        Assert.NotEqual(0, id);
        Employee retriavedEmployee = unitOfWork.EmployeeRepository.Get(id);

        Assert.Equal(employee.EmployeeTypeId, retriavedEmployee.EmployeeTypeId);
        Assert.Equal(employee.CityId, retriavedEmployee.CityId);
        Assert.Equal(employee.FirstName, retriavedEmployee.FirstName);
        Assert.Equal(employee.LastName, retriavedEmployee.LastName);
        Assert.Equal(employee.IdentityNumber, retriavedEmployee.IdentityNumber);
        Assert.Equal(employee.Email, retriavedEmployee.Email);
        Assert.Equal(employee.Phone, retriavedEmployee.Phone);
    }

    [Theory]
    [InlineData(1,1,1, "Name1", "LastName1", "@Email11", "11112","-")]
    [InlineData(2,2,2, "Name2", "LastName2", "@Email22", "11113","-")]
    public void UpdateEmployee(int employeeId, int employeeTypeId, int cityId, string firstName, string lastName, string email, string identityNumber, string phone)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);

        Employee updatedEmployee = new() { EmployeeId = employeeId, EmployeeTypeId = employeeTypeId, CityId = cityId, FirstName = firstName, LastName = lastName, Email = email, IdentityNumber = identityNumber, Phone = phone};
        unitOfWork.EmployeeRepository.Update(updatedEmployee);

        Employee retriavedEmployee = unitOfWork.EmployeeRepository.Get(employeeId);
        Assert.Equal(retriavedEmployee.FirstName, updatedEmployee.FirstName);
        Assert.Equal(retriavedEmployee.LastName, updatedEmployee.LastName);
        Assert.Equal(retriavedEmployee.IdentityNumber, updatedEmployee.IdentityNumber);
        Assert.Equal(retriavedEmployee.Email, updatedEmployee.Email);
        Assert.Equal(retriavedEmployee.Phone, updatedEmployee.Phone);

    }

    [Theory]
    [InlineData(4, 4, 4, "Name4", "LastName4")]
    [InlineData(3, 3, 3, "Name3", "LastName3")]
    public void DeleteEmployee(int employeeId, int employeeTypeId, int cityId, string firstName, string lastName)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);

        Employee retriavedEmployee = unitOfWork.EmployeeRepository.Get(employeeId);

        Assert.Equal(employeeTypeId, retriavedEmployee.EmployeeTypeId);
        Assert.Equal(cityId, retriavedEmployee.CityId);
        Assert.Equal(firstName, retriavedEmployee.FirstName);
        Assert.Equal(lastName, retriavedEmployee.LastName);

        unitOfWork.EmployeeRepository.Delete(employeeId);

        Assert.Throws<InvalidOperationException>(() => unitOfWork.EmployeeRepository.Get(employeeId));
    }
}

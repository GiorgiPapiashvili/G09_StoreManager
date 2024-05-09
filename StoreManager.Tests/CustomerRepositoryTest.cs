using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests;

[Collection("Database collection")]
public class CustomerRepositoryTest : RepositoryTestBase
{
    private DatabaseFixture _fixture;
    private readonly IUnitOfWork<SqlConnection> _unitOfWork;

    public CustomerRepositoryTest(DatabaseFixture fixture)
    {
        this._fixture = fixture;
        UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
        _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    }

    [Theory]
    [InlineData("FirstName1", "lastName4", 1, "1@Email")]
    [InlineData("FirstName2", "lastName1", 2, "2@Email")]
    [InlineData("FirstName3", "lastName2", 3, "3@Email")]
    public void InsertCustomer(string firstName, string lastName, int cityId, string email)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Customer customer = new() { FirstName = firstName, LastName = lastName, CityId = cityId, Email = email};

        int id = unitOfWork.CustomerRepository.Insert(customer);

        Assert.NotEqual(0, id);
        Customer retriavedCustomer = unitOfWork.CustomerRepository.Get(id);

        Assert.Equal(retriavedCustomer.FirstName, customer.FirstName);
        Assert.Equal(retriavedCustomer.LastName, customer.LastName);
        Assert.Equal(retriavedCustomer.CityId, customer.CityId);
    }

    [Theory]
    [InlineData(2, "FirstName2", "LastName2", 2)]
    [InlineData(2, "FirstName3", "LastName4", 3)]
    public void UpdateCustomer(int customerId, string firstName, string lastName, int cityId)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);

        Customer updatedCustomer = new() { CustomerId = customerId, FirstName = firstName, LastName = lastName, CityId = cityId };
        unitOfWork.CustomerRepository.Update(updatedCustomer);

        Customer retriavedCustomer = unitOfWork.CustomerRepository.Get(customerId);

        Assert.Equal(retriavedCustomer.FirstName, updatedCustomer.FirstName);
        Assert.Equal(retriavedCustomer.LastName, updatedCustomer.LastName);
    }

    [Theory]
    [InlineData(3, "Name3", "LastName3")]
    [InlineData(4, "Name4", "LastName4")]
    public void DeleteCustomer(int customerId, string name, string lastName)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        
        Customer retriavedCustomer = unitOfWork.CustomerRepository.Get(customerId);

        Assert.Equal(name, retriavedCustomer.FirstName);
        Assert.Equal(lastName, retriavedCustomer.LastName);

        unitOfWork.CustomerRepository.Delete(customerId);

        Assert.Throws<InvalidOperationException>(() => unitOfWork.CustomerRepository.Get(customerId));
    }
}

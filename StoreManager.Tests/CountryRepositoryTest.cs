using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests;

[Collection("Database collection")]
public class CountryRepositoryTest : RepositoryTestBase
{
    private DatabaseFixture _fixture;
    private readonly IUnitOfWork<SqlConnection> _unitOfWork;

    public CountryRepositoryTest(DatabaseFixture fixture)
    {
        this._fixture = fixture;
        UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
        _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    }

    [Theory]
    [InlineData("Georgia")]
    [InlineData("United-Kingdom")]
    [InlineData("France")]
    public void InsertCountry(string name)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Country country = new() { Name = name };

        int id = unitOfWork.CountryRepository.Insert(country);

        Assert.NotEqual(0, id);
        Country retriavedCountry = unitOfWork.CountryRepository.Get(id);
        Assert.Equal(retriavedCountry.Name, country.Name);
    }

    [Theory]
    [InlineData(4, "Spain")]
    [InlineData(3, "Brazil")]
    public void UpdateCountry(int countryId, string name)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);

        Country updatedCountry = new() { CountryId = countryId, Name = name };
        unitOfWork.CountryRepository.Update(updatedCountry);

        Country retriavedCountry = unitOfWork.CountryRepository.Get(countryId);

        Assert.Equal(retriavedCountry.Name, updatedCountry.Name);

    }

    [Theory]
    [InlineData(1,"Country1")]
    [InlineData(2,"Country2")]
    public void DeleteCountry(int countryId, string name)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    
        Country retriavedCountry = unitOfWork.CountryRepository.Get(countryId);
        Assert.Equal(countryId, retriavedCountry.CountryId);
        Assert.Equal(name, retriavedCountry.Name);

        unitOfWork.CountryRepository.Delete(countryId);

        Assert.Throws<InvalidOperationException>(() => unitOfWork.CountryRepository.Get(countryId));
    }
}

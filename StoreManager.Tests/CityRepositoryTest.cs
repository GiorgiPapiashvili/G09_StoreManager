using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests;

[Collection("Database collection")]
public class CityRepositoryTest : RepositoryTestBase
{
    private DatabaseFixture _fixture;
    private readonly IUnitOfWork<SqlConnection> _unitOfWork;
    public CityRepositoryTest(DatabaseFixture fixture)
    {
        this._fixture = fixture;
        UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
        _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    }

    [Theory]
    [InlineData(1,"City5")]
    [InlineData(2,"City6")]
    [InlineData(3,"City9")]
    public void InsertCity(int countryId, string name)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        City city = new() { CountryId = countryId, Name = name };

        int id = unitOfWork.CityRepository.Insert(city);

        Assert.NotEqual(0, id);
        var retriavedCity = unitOfWork.CityRepository.Get(id);
        Assert.Equal(city.CountryId, retriavedCity.CountryId);
        Assert.Equal(retriavedCity.Name, city.Name);
    }

    [Theory]
    [InlineData(1,"Tbilisi",2)]
    [InlineData(2, "New-York", 1)]
    [InlineData(1, "London", 2)]

    public void UpdateCity(int cityId, string name, int countriId)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);

        City updatedCity = new() { CityId = cityId, Name = name, CountryId = countriId };
        unitOfWork.CityRepository.Update(updatedCity);

        City retriavedCity = unitOfWork.CityRepository.Get(cityId);

        Assert.Equal(retriavedCity.Name, updatedCity.Name);
    }

    [Theory]
    [InlineData(4,"City4")]
    [InlineData(3,"City3")]
    public void DeleteCity(int cityId, string name)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);

        City retriavedCity = unitOfWork.CityRepository.Get(cityId);
        Assert.Equal(cityId, retriavedCity.CityId);
        Assert.Equal(name, retriavedCity.Name);

        unitOfWork.CityRepository.Delete(cityId);

        Assert.Throws<InvalidOperationException>(() => unitOfWork.CityRepository.Get(cityId));
    }
}

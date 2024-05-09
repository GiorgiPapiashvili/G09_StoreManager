using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests;

[Collection("Database collection")]
public class SupplierRepositoryTest : RepositoryTestBase
{
    private DatabaseFixture _fixture;
    private readonly IUnitOfWork<SqlConnection> _unitOfWork;

    public SupplierRepositoryTest(DatabaseFixture fixture)
    {
        this._fixture = fixture;
        UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
        _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    }

    [Theory]
    [InlineData(1, "Supplier1", "code11", "email1", "phone11")]
    [InlineData(2, "Supplier2", "code22", "email2", "phone12")]
    [InlineData(3, "Supplier3", "code33", "email3", "phone13")]
    public void InsertSupplier(int cityId, string supplierName, string taxCode, string email, string phone)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Supplier supplier = new() { CityId = cityId, Name = supplierName, TaxCode = taxCode, Email = email, Phone = phone };

        int id = unitOfWork.SupplierRepository.Insert(supplier);

        Assert.NotEqual(0, id);
        Supplier retriavedSupplier = unitOfWork.SupplierRepository.Get(id);
        Assert.Equal(retriavedSupplier.Name, supplier.Name);
        Assert.Equal(retriavedSupplier.Email, supplier.Email);
        Assert.Equal(retriavedSupplier.CityId, supplier.CityId);
    }

    [Theory]
    [InlineData(1, 1, "Supplier1", "code111", "@email1", "phone11")]
    [InlineData(2, 2, "Supplier2", "code222", "@email2", "phone12")]
    public void UpdateSupplier(int supplierId, int cityId, string name, string taxcode, string email, string phone)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);

        Supplier updatedSupplier = new() { SupplierId = supplierId, CityId = cityId, Name = name, Email = email, Phone = phone, TaxCode = taxcode };
        unitOfWork.SupplierRepository.Update(updatedSupplier);

        Supplier retriavedSupplier = unitOfWork.SupplierRepository.Get(supplierId);
        Assert.Equal(retriavedSupplier.Name, updatedSupplier.Name);
        Assert.Equal(retriavedSupplier.CityId, updatedSupplier.CityId);
        Assert.Equal(retriavedSupplier.Email, updatedSupplier.Email);
    }

    [Theory]
    [InlineData(3, 3, "Supplier3")]
    [InlineData(4, 4, "Supplier4")]
    public void DeleteSupplier(int supplierId, int cityId, string name)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);

        Supplier retriavedSupplier = unitOfWork.SupplierRepository.Get(supplierId);
        Assert.Equal(retriavedSupplier.Name, name);
        Assert.Equal(retriavedSupplier.CityId, cityId);

        unitOfWork.SupplierRepository.Delete(supplierId);

        Assert.Throws<InvalidOperationException>(() => unitOfWork.SupplierRepository.Get(supplierId));
    }
}


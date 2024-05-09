using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests;

[Collection("collection")]
public class ProductRepositoryTest : RepositoryTestBase
{
    private DatabaseFixture _fixture;
    private readonly IUnitOfWork<SqlConnection> _unitOfWork;

    public ProductRepositoryTest(DatabaseFixture fixture)
    {
        this._fixture = fixture;
        UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
        _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
    }

    [Theory]
    [InlineData("111", "Product1", 11.11)]
    [InlineData("222", "Product2", 22.22)]
    [InlineData("333", "Product3", 33.33)]
    public void InsertProduct(string productCode, string name, decimal price)
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Product product = new() { CategoryId = 1, ProductCode = productCode, Name = name, UnitPrice = price};

        int id = unitOfWork.ProductRepository.Insert(product);

        Assert.NotEqual(0, id);
        Product retriavedProduct = unitOfWork.ProductRepository.Get(id);
        Assert.Equal(product.Name, retriavedProduct.Name);
        Assert.Equal(product.UnitPrice, retriavedProduct.UnitPrice);
        Assert.Equal(product.CategoryId, retriavedProduct.CategoryId);
    }

    [Fact]
    public void UpdateProduct()
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Product product = new() { CategoryId = 1, ProductCode = "111", Name = "Product1", UnitPrice = 12.22m };

        int id = unitOfWork.ProductRepository.Insert(product);
        Product updatedProduct = new() { ProductId = id, CategoryId = 1, Name = "UpdatedProduct", ProductCode = "UpdatedCode", UnitPrice = 12.32m, Description = "Description" };
        unitOfWork.ProductRepository.Update(updatedProduct);

        Product retriavedProduct = unitOfWork.ProductRepository.Get(id);
        Assert.Equal(retriavedProduct.Name, updatedProduct.Name);
        Assert.Equal(retriavedProduct.Description, updatedProduct.Description);
    }

    [Fact]
    public void DeleteProduct()
    {
        IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        Product product = new() { CategoryId = 1, ProductCode = "111", Name = "Product1", UnitPrice = 12.22m };

        int id = unitOfWork.ProductRepository.Insert(product);

        Product retriavedProduct = unitOfWork.ProductRepository.Get(id);
        Assert.Equal(retriavedProduct.Name, product.Name);
        Assert.Equal(retriavedProduct.Description, product.Description);

        unitOfWork.ProductRepository.Delete(id);

        Assert.Throws<InvalidOperationException>(() => unitOfWork.ProductRepository.Get(id));
    }

    public override void Dispose()
    {
        SqlCommand command = _connection.CreateCommand();
        command.CommandText = "Delete Products; DBCC CHECKIDENT('Products', RESEED, 0)" +
                        "Delete Categories; DBCC CHECKIDENT('Categories', RESEED, 0)";
        command.ExecuteNonQuery();
        base.Dispose();
    }
}

using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.Tests
{
    [Collection("Database collection")]
    public sealed class CategoryRepositoryTest : RepositoryTestBase
    {
        private readonly DatabaseFixture _fixture;
        private readonly IUnitOfWork<SqlConnection> _unitOfWork;

        public CategoryRepositoryTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
            UnitOfWork<SqlConnection>.GetEmployeeID = () => 1;
            _unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);
        }

        [Theory]
        [InlineData("Category11")]
        [InlineData("Category22")]
        [InlineData("Category33")]
        public void InsertCategory(string name)
        {
            Category category = new() { Name = name };

            int id = _unitOfWork.CategoryRepository.Insert(category);

            Assert.NotEqual(0, id);
            var retriavedCategory = _unitOfWork.CategoryRepository.Get(id);
            Assert.Equal(retriavedCategory.Name, category.Name);
        }

        [Theory]
        [InlineData(1, "CategoryNew")]
        [InlineData(2, "CategoryNew1")]
        public void UpdateCategory(int categoryId, string name)
        {
            IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);

            Category updatedCategory = new() { CategoryId = categoryId, Name = name, Description = "Description" };
            unitOfWork.CategoryRepository.Update(updatedCategory);

            Category retriavedCategory = unitOfWork.CategoryRepository.Get(categoryId);

            Assert.Equal(retriavedCategory.Description, updatedCategory.Description);
            Assert.Equal(retriavedCategory.Name, updatedCategory.Name);
        }

        [Theory]
        [InlineData(3, "Category3")]
        [InlineData(4, "Category4")]
        public void DeleteCategory(int categoryId, string name)
        {
            IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(ConnectionString);

            Category retriavedCategory = unitOfWork.CategoryRepository.Get(categoryId);
            Assert.Equal(categoryId, retriavedCategory.CategoryId);
            Assert.Equal(name, retriavedCategory.Name);

            unitOfWork.CategoryRepository.Delete(categoryId);

            Assert.Throws<InvalidOperationException>(() => unitOfWork.CategoryRepository.Get(categoryId));
        }
    }
}
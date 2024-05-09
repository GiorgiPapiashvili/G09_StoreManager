using Microsoft.Data.SqlClient;

namespace StoreManager.Tests
{
    public abstract class RepositoryTestBase : IDisposable
    {
        protected const string ConnectionString = "Server = .; Database = StoreManagerTest; Integrated Security = true; TrustServerCertificate = true;";
        protected readonly SqlConnection _connection;

        public RepositoryTestBase()
        {
            _connection = new SqlConnection(ConnectionString);
        }

        public virtual void Dispose()
        {
            _connection.Dispose();
        }
    }
}

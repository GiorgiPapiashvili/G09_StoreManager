using Dapper;
using StoreManager.Service.Interfaces.Repositories;
using System.Data;
using System.Reflection;
using Humanizer;
using System.Linq.Expressions;

namespace StoreManager.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
    {
        protected readonly IDbConnection _connection;
        protected readonly IDbTransaction? _transaction;
        protected readonly int? _employeeId;

        protected BaseRepository(IDbConnection connection, IDbTransaction? transaction = null, int? employeeId = null)
        {
            _connection = connection ?? throw new NullReferenceException(nameof(connection));
            _employeeId = employeeId;
            _transaction = transaction;
        }

        public T Get(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", id);

            T item = _connection.QueryFirst<T>(
                $"sp_Get{typeof(T).Name}",
                param: parameters,
                commandType: CommandType.StoredProcedure);

            return item;
        }

        public IEnumerable<T> Load(Expression<Func<T, bool>>? predicate = null)
        {
            string selectQuery = $"SELECT * FROM {typeof(T).Name.Pluralize()}";

            if (predicate != null)
            {
                var parameters = new DynamicParameters();
                string whereClause = ExpressionHelper.Translate(predicate.Body, parameters);
                selectQuery += $" WHERE {whereClause}";

                return _connection.Query<T>(selectQuery, parameters, commandType: CommandType.Text);
            }

            return _connection.Query<T>(selectQuery);
        }

        public virtual int Insert(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var parameters = GetParameters(item, IgnoredPropertiesForInsert);
            parameters.Add("ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            if (_employeeId.HasValue)
            {
                parameters.Add("EmployeeID", _employeeId);
            }

            _connection.Execute(
                $"sp_insert{typeof(T).Name}",
                param: parameters,
                transaction: _transaction,
                commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("ID");
        }

        public void Update(T item)
        {
            var parameters = GetParameters(item, IgnoredPropertiesForUpdate);

            _connection.Execute($"sp_Update{typeof(T).Name}",
                param: parameters,
                transaction: _transaction,
                commandType: CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            _connection.Execute(
                $"sp_Delete{typeof(T).Name}", new { ID = id },
                transaction: _transaction,
                commandType: CommandType.StoredProcedure);
        }

        private DynamicParameters GetParameters(T item, IEnumerable<string> ignoredParameters)
        {
            Type type = item!.GetType();

            PropertyInfo[] properties = type.GetProperties();
            DynamicParameters parameters = new();

            foreach (var property in properties)
            {
                if (ignoredParameters.Contains(property.Name))
                {
                    continue;
                }

                parameters.Add(property.Name, property.GetValue(item));
            }

            return parameters;
        }

        protected virtual IEnumerable<string> IgnoredPropertiesForInsert =>
            new[] { $"{typeof(T).Name}Id", "CreatedDate", "CreateDate", "UpdatedDate", "IsDeleted" };

        protected virtual IEnumerable<string> IgnoredPropertiesForUpdate =>
            new[] { "CreatedDate", "UpdatedDate", "IsDeleted" };
    }
}

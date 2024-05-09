using Dapper;
using StoreManager.DTO;
using StoreManager.Service.Interfaces.Repositories;
using System.Data;
using System.Data.Common;

namespace StoreManager.Repository;

internal class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IDbConnection connection, IDbTransaction? transaction = null) : base(connection, transaction)
    {
        
    }

    public override int Insert(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        DynamicParameters parameters = new();
        parameters.Add("EmployeeId", user.EmployeeId, dbType: DbType.Int32);
        parameters.Add("UserName", user.UserName, dbType: DbType.String);
        parameters.Add("Password", user.Password, dbType: DbType.String);

        _connection.Execute("sp_InsertUser", parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);

        return 0;
    }

    public void Register(int employeeId, string username, string password)
    {
        DynamicParameters parameters = new();
        parameters.Add("EmployeeId", employeeId, DbType.Int32);
        parameters.Add("UserName", username, DbType.String);
        parameters.Add("Password", password, DbType.String);

        _connection.Execute("sp_RegisterUser", parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
    }

    public bool Login(string username, string password)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@UserName", username, DbType.String);
        parameters.Add("Password", password, DbType.String);
        parameters.Add("SuccessfullLogin", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        _connection.Execute("sp_userLogin", parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);

        return parameters.Get<bool>("SuccessfullLogin");
    }

    public void ResetPassword(string username, string currentPassword, string newPassword)
    {
        DynamicParameters parameters = new();
        parameters.Add("UserName", username, DbType.String);
        parameters.Add("CurrentPassword", currentPassword, DbType.String);
        parameters.Add("NewPassword", newPassword, DbType.String);

        _connection.Execute("sp_ChangePassword_User", parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _transaction);
    }

    public int GetId(string username)
    {
        DynamicParameters parameters = new();
        parameters.Add("UserName", username, DbType.String);
        parameters.Add("ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

        _connection.Execute("sp_GetUserId", parameters, commandType: CommandType.StoredProcedure);

        return parameters.Get<int>("ID");
    }

    public void LockUser(string username)
    {
        DynamicParameters parameters = new();
        parameters.Add("UserName", username, DbType.String);

        _connection.Execute("sp_LockUser", parameters, commandType: CommandType.StoredProcedure);
    }

    public void UnlockUser(string username)
    {
        DynamicParameters parameters = new();
        parameters.Add("UserName", username, DbType.String);

        _connection.Execute("sp_UnlockUser", parameters, commandType: CommandType.StoredProcedure);
    }

    protected override IEnumerable<string> IgnoredPropertiesForInsert
    {
        get
        {
            List<string> ignoredParameters = base.IgnoredPropertiesForInsert.ToList();
            ignoredParameters.Add("IsActive");
            return ignoredParameters;
        }
    }

    protected override IEnumerable<string> IgnoredPropertiesForUpdate
    {
        get
        {
            List<string> ignoredParameters = base.IgnoredPropertiesForInsert.ToList();
            ignoredParameters.Add("Password");
            return ignoredParameters;
        }
    }
}

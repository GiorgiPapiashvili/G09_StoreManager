using StoreManager.DTO;

namespace StoreManager.Service.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        void Register(int employeeId, string username, string password);
        bool Login(string username, string password);
        void ResetPassword(string username, string currentPassword, string newPassword);
        int GetId(string username);
        void LockUser(string username);
        void UnlockUser(string username);
    }
}

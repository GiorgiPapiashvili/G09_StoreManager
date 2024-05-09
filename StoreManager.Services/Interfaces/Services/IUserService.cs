namespace StoreManager.Service.Interfaces.Services
{
    public interface IUserService
    {
        void Register(int employeeId, string username, string password);
        bool Login(string username, string password);
        void ResetPassword(string username, string currentPassword, string newPassword);
        int GetId(string username);
        void LockUser(string username);
        void UnlockUser(string username);
    }
}

namespace Doamin.Service.Users
{
    using Domain.Model.Users;

    public interface IUserService
    {
        User GetUserByName(string userName);
        UserLoginResults ValidateUser(string userName, string password);
        UserRole GetUserRoleBySystemName(string systemName);
        UserRole GetUserRoleById(int roleId);
        void InsertUserRole(UserRole role);
        void ResetCheckoutData(User user);
        void UpdateUser(User user);
    }
}
namespace Doamin.Service.Authentication
{
    using Domain.Model.Users;

    /// <summary>
    /// Authentication service interface
    /// </summary>
    public interface IAuthenticationService
    {
        void SignIn(User user, bool createPersistentCookie);
        void SignOut();
        User GetAuthenticatedUser();
    }
}
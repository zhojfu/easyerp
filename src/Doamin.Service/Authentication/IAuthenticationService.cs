namespace Doamin.Service.Authentication
{
    using Domain.Model.Base;

    /// <summary>
    /// Authentication service interface
    /// </summary>
    public partial interface IAuthenticationService
    {
        void SignIn(User user, bool createPersistentCookie);

        void SignOut();

        User GetAuthenticatedUser();
    }
}
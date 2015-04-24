namespace Doamin.Service.Authentication
{
    using System;
    using System.Web;
    using System.Web.Security;
    using Doamin.Service.Users;
    using Domain.Model.Users;

    /// <summary>
    /// Authentication service
    /// </summary>
    public class FormsAuthenticationService : IAuthenticationService
    {
        private readonly IUserService _customerService;

        private readonly TimeSpan _expirationTimeSpan;

        private readonly HttpContextBase _httpContext;

        private User cachedUser;

        public FormsAuthenticationService(HttpContextBase httpContext, IUserService customerService)
        {
            _httpContext = httpContext;
            _customerService = customerService;
            _expirationTimeSpan = FormsAuthentication.Timeout;
        }

        public virtual void SignIn(User user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1,
                user.Name,
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                user.Name,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true
            };
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);
            cachedUser = user;
        }

        public virtual void SignOut()
        {
            cachedUser = null;
            FormsAuthentication.SignOut();
        }

        public virtual User GetAuthenticatedUser()
        {
            if (cachedUser != null)
            {
                return cachedUser;
            }

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var user = GetAuthenticatedUserFromTicket(formsIdentity.Ticket);
            if (user != null &&
                user.Active &&
                !user.Deleted)
            {
                cachedUser = user;
            }

            return cachedUser;
        }

        public virtual User GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException("ticket");
            }

            var username = ticket.UserData;

            if (String.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            return _customerService.GetUserByName(username);
        }
    }
}
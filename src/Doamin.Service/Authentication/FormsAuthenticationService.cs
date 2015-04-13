namespace Doamin.Service.Authentication
{
    using Doamin.Service.Users;
    using Domain.Model.Users;
    using System;
    using System.Web;
    using System.Web.Security;

    /// <summary>
    /// Authentication service
    /// </summary>
    public partial class FormsAuthenticationService : IAuthenticationService
    {
        private readonly HttpContextBase _httpContext;
        private readonly IUserService _customerService;
        private readonly TimeSpan _expirationTimeSpan;

        private User cachedUser;

        public FormsAuthenticationService(HttpContextBase httpContext, IUserService customerService)
        {
            this._httpContext = httpContext;
            this._customerService = customerService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }

        public virtual void SignIn(User user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(1, user.Name, now, now.Add(this._expirationTimeSpan), createPersistentCookie, user.Name, FormsAuthentication.FormsCookiePath);

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

            this._httpContext.Response.Cookies.Add(cookie);
            this.cachedUser = user;
        }

        public virtual void SignOut()
        {
            this.cachedUser = null;
            FormsAuthentication.SignOut();
        }

        public virtual User GetAuthenticatedUser()
        {
            if (this.cachedUser != null)
            {
                return this.cachedUser;
            }

            if (this._httpContext == null ||
                this._httpContext.Request == null ||
                !this._httpContext.Request.IsAuthenticated ||
                !(this._httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)this._httpContext.User.Identity;
            var user = this.GetAuthenticatedUserFromTicket(formsIdentity.Ticket);
            if (user != null &&
                user.Active &&
                !user.Deleted)
            {
                this.cachedUser = user;
            }

            return this.cachedUser;
        }

        public virtual User GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var username = ticket.UserData;

            if (String.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            return this._customerService.GetUserByName(username);
        }
    }
}
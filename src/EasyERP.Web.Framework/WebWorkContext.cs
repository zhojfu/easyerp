namespace EasyERP.Web.Framework
{
    using Doamin.Service.Authentication;
    using Doamin.Service.Helpers;
    using Doamin.Service.Stores;
    using Doamin.Service.Users;
    using Doamin.Service.Vendors;
    using Domain.Model.Users;
    using Domain.Model.Vendors;
    using EasyErp.Core;
    using System;
    using System.Linq;
    using System.Web;

    public partial class WebWorkContext : IWorkContext
    {
        private readonly HttpContextBase httpContext;
        private const string UserCookieName = "Easyerp.user";

        private readonly IUserService userService;
        private readonly IAuthenticationService authenticationService;

        private User cachedUser;

        public WebWorkContext(HttpContextBase httpContext, IUserService userService, IAuthenticationService authenticationService)
        {
            this.httpContext = httpContext;
            this.userService = userService;
            this.authenticationService = authenticationService;
        }

        public User CurrentUser
        {
            get
            {
                if (this.cachedUser != null)
                {
                    return this.cachedUser;
                }
                var user = this.authenticationService.GetAuthenticatedUser();
                if (user.Active &&
                    !user.Deleted)
                {
                    this.SetUserCookie(user.UseGuid);
                    this.cachedUser = user;
                }
                return this.cachedUser;
            }
            set
            {
                this.SetUserCookie(value.UseGuid);
                this.cachedUser = value;
            }
        }

        protected virtual void SetUserCookie(Guid userGuid)
        {
            if (httpContext != null && httpContext.Response != null)
            {
                var cookie = new HttpCookie(UserCookieName);
                cookie.HttpOnly = true;
                cookie.Value = userGuid.ToString();
                if (userGuid == Guid.Empty)
                {
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                }
                else
                {
                    cookie.Expires = DateTime.Now.AddHours(2);
                }

                httpContext.Response.Cookies.Remove(UserCookieName);
                httpContext.Response.Cookies.Add(cookie);
            }
        }
    }
}
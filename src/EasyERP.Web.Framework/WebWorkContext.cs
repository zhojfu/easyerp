namespace EasyERP.Web.Framework
{
    using Doamin.Service.Authentication;
    using Doamin.Service.Users;
    using Domain.Model.Users;
    using EasyErp.Core;
    using System;
    using System.Web;

    public class WebWorkContext : IWorkContext
    {
        private const string UserCookieName = "Easyerp.user";

        private readonly IAuthenticationService authenticationService;

        private readonly HttpContextBase httpContext;

        private readonly IUserService userService;

        private User cachedUser;

        public WebWorkContext(
            HttpContextBase httpContext,
            IUserService userService,
            IAuthenticationService authenticationService)
        {
            this.httpContext = httpContext;
            this.userService = userService;
            this.authenticationService = authenticationService;
        }

        public User CurrentUser
        {
            get
            {
                if (cachedUser != null)
                {
                    return cachedUser;
                }
                var user = authenticationService.GetAuthenticatedUser();

                if (user == null)
                {
                    return null;
                }

                if (user.Active &&
                    !user.Deleted)
                {
                    SetUserCookie(user.UseGuid);
                    cachedUser = user;
                }

                return cachedUser;
            }
            set
            {
                SetUserCookie(value.UseGuid);
                cachedUser = value;
            }
        }

        protected virtual void SetUserCookie(Guid userGuid)
        {
            if (httpContext != null &&
                httpContext.Response != null)
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
namespace EasyERP.Web.Framework.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Doamin.Service.Security;
    using EasyErp.Core.Infrastructure;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AdminAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool dontValidate;

        public AdminAuthorizeAttribute()
            : this(false)
        {
        }

        public AdminAuthorizeAttribute(bool dontValidate)
        {
            this.dontValidate = dontValidate;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (dontValidate)
            {
                return;
            }

            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
            {
                throw new InvalidOperationException(
                    "You cannot use [AdminAuthorize] attribute when a child action cache is active");
            }

            if (IsAdminPageRequested(filterContext))
            {
                if (!HasAdminAccess(filterContext))
                {
                    HandleUnauthorizedRequest(filterContext);
                }
            }
        }

        private void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }

        private IEnumerable<AdminAuthorizeAttribute> GetAdminAuthorizeAttributes(ActionDescriptor descriptor)
        {
            return descriptor.GetCustomAttributes(typeof(AdminAuthorizeAttribute), true)
                             .Concat(
                                 descriptor.ControllerDescriptor.GetCustomAttributes(
                                     typeof(AdminAuthorizeAttribute),
                                     true))
                             .OfType<AdminAuthorizeAttribute>();
        }

        private bool IsAdminPageRequested(AuthorizationContext filterContext)
        {
            var adminAttributes = GetAdminAuthorizeAttributes(filterContext.ActionDescriptor);
            return adminAttributes != null && adminAttributes.Any();
        }

        public virtual bool HasAdminAccess(AuthorizationContext filterContext)
        {
            var permissionService = EngineContext.Current.Resolve<IPermissionService>();
            var result = permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel);
            return result;
        }
    }
}
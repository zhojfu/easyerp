namespace EasyERP.Web.Framework.Mvc.Routes
{
    using System;
    using System.Web;
    using System.Web.Routing;

    public class GuidConstraint : IRouteConstraint
    {
        private readonly bool allowEmpty;

        public GuidConstraint(bool allowEmpty)
        {
            this.allowEmpty = allowEmpty;
        }

        public bool Match(
            HttpContextBase httpContext,
            Route route,
            string parameterName,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (!values.ContainsKey(parameterName))
            {
                return false;
            }

            var stringValue = values[parameterName] != null ? values[parameterName].ToString() : null;

            if (string.IsNullOrEmpty(stringValue))
            {
                return false;
            }

            Guid guidValue;

            return Guid.TryParse(stringValue, out guidValue) &&
                   (allowEmpty || guidValue != Guid.Empty);
        }
    }
}
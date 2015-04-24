namespace EasyERP.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// If form name exists, then specified "actionParameterName" will be set to "true"
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParameterBasedOnFormNameAttribute : FilterAttribute, IActionFilter
    {
        private readonly string actionParameterName;

        private readonly string name;

        public ParameterBasedOnFormNameAttribute(string name, string actionParameterName)
        {
            this.name = name;
            this.actionParameterName = actionParameterName;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var formValue = filterContext.RequestContext.HttpContext.Request.Form[name];
            filterContext.ActionParameters[actionParameterName] = !string.IsNullOrEmpty(formValue);
        }
    }
}
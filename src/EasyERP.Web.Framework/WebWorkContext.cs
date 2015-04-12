namespace EasyERP.Web.Framework
{
    using Doamin.Service.Authentication;
    using Doamin.Service.Helpers;
    using Doamin.Service.Stores;
    using Doamin.Service.Vendors;
    using Domain.Model.Base;
    using Domain.Model.Vendors;
    using EasyErp.Core;
    using System;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Work context for web application
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        public bool IsAdmin
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
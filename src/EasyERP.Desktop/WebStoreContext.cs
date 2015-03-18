namespace Nop.Desktop
{
    using Nop.Core;
    using Nop.Core.Domain.Stores;
    using Nop.Services.Stores;
    using System;
    using System.Linq;

    /// <summary>
    /// Store context for web application
    /// </summary>
    public partial class WebStoreContext : IStoreContext
    {
        private readonly IStoreService _storeService;
        private readonly IWebHelper _webHelper;

        private Store _cachedStore;

        public WebStoreContext(IStoreService storeService, IWebHelper webHelper)
        {
            this._storeService = storeService;
            this._webHelper = webHelper;
        }

        /// <summary>
        /// Gets or sets the current store
        /// </summary>
        public virtual Store CurrentStore
        {
            get
            {
                if (this._cachedStore != null)
                    return this._cachedStore;

                //ty to determine the current store by HTTP_HOST
                var host = this._webHelper.ServerVariables("HTTP_HOST");
                var allStores = this._storeService.GetAllStores();
                var store = allStores.FirstOrDefault(s => s.ContainsHostValue(host));

                if (store == null)
                {
                    //load the first found store
                    store = allStores.FirstOrDefault();
                }
                if (store == null)
                    throw new Exception("No store could be loaded");

                this._cachedStore = store;
                return this._cachedStore;
            }
        }
    }
}
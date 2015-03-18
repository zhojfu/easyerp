namespace Nop.Desktop
{
    using Nop.Core;
    using Nop.Core.Domain.Customers;
    using Nop.Core.Domain.Directory;
    using Nop.Core.Domain.Localization;
    using Nop.Core.Domain.Tax;
    using Nop.Core.Domain.Vendors;
    using Nop.Services.Authentication;
    using Nop.Services.Common;
    using Nop.Services.Customers;
    using Nop.Services.Directory;
    using Nop.Services.Helpers;
    using Nop.Services.Localization;
    using Nop.Services.Stores;
    using Nop.Services.Vendors;
    using System;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Work context for web application
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        #region Const

        private const string CustomerCookieName = "Nop.customer";

        #endregion Const

        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly ICustomerService _customerService;
        private readonly IVendorService _vendorService;
        private readonly IStoreContext _storeContext;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILanguageService _languageService;
        private readonly ICurrencyService _currencyService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly TaxSettings _taxSettings;
        private readonly CurrencySettings _currencySettings;
        private readonly LocalizationSettings _localizationSettings;
        private readonly IUserAgentHelper _userAgentHelper;
        private readonly IStoreMappingService _storeMappingService;

        private Customer _cachedCustomer;
        private Customer _originalCustomerIfImpersonated;
        private Vendor _cachedVendor;
        private Language _cachedLanguage;
        private Currency _cachedCurrency;
        private TaxDisplayType? _cachedTaxDisplayType;

        #endregion Fields

        #region Ctor

        public WebWorkContext(HttpContextBase httpContext,
            ICustomerService customerService,
            IVendorService vendorService,
            IStoreContext storeContext,
            IAuthenticationService authenticationService,
            ILanguageService languageService,
            ICurrencyService currencyService,
            IGenericAttributeService genericAttributeService,
            TaxSettings taxSettings,
            CurrencySettings currencySettings,
            LocalizationSettings localizationSettings,
            IUserAgentHelper userAgentHelper,
            IStoreMappingService storeMappingService)
        {
            this._httpContext = httpContext;
            this._customerService = customerService;
            this._vendorService = vendorService;
            this._storeContext = storeContext;
            this._authenticationService = authenticationService;
            this._languageService = languageService;
            this._currencyService = currencyService;
            this._genericAttributeService = genericAttributeService;
            this._taxSettings = taxSettings;
            this._currencySettings = currencySettings;
            this._localizationSettings = localizationSettings;
            this._userAgentHelper = userAgentHelper;
            this._storeMappingService = storeMappingService;
        }

        #endregion Ctor

        #region Utilities

        protected virtual HttpCookie GetCustomerCookie()
        {
            if (this._httpContext == null || this._httpContext.Request == null)
                return null;

            return this._httpContext.Request.Cookies[CustomerCookieName];
        }

        protected virtual void SetCustomerCookie(Guid customerGuid)
        {
            if (this._httpContext != null && this._httpContext.Response != null)
            {
                var cookie = new HttpCookie(CustomerCookieName);
                cookie.HttpOnly = true;
                cookie.Value = customerGuid.ToString();
                if (customerGuid == Guid.Empty)
                {
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                }
                else
                {
                    int cookieExpires = 24 * 365; //TODO make configurable
                    cookie.Expires = DateTime.Now.AddHours(cookieExpires);
                }

                this._httpContext.Response.Cookies.Remove(CustomerCookieName);
                this._httpContext.Response.Cookies.Add(cookie);
            }
        }

        protected virtual Language GetLanguageFromUrl()
        {
            if (this._httpContext == null || this._httpContext.Request == null)
                return null;

            string virtualPath = this._httpContext.Request.AppRelativeCurrentExecutionFilePath;
            string applicationPath = this._httpContext.Request.ApplicationPath;
            //if (!virtualPath.IsLocalizedUrl(applicationPath, false))
            //    return null;

            //var seoCode = virtualPath.GetLanguageSeoCodeFromUrl(applicationPath, false);
            //if (String.IsNullOrEmpty(seoCode))
            //    return null;

            //var language = this._languageService
            //    .GetAllLanguages()
            //    .FirstOrDefault(l => seoCode.Equals(l.UniqueSeoCode, StringComparison.InvariantCultureIgnoreCase));
            //if (language != null && language.Published && this._storeMappingService.Authorize(language))
            //{
            //    return language;
            //}

            return null;
        }

        protected virtual Language GetLanguageFromBrowserSettings()
        {
            if (this._httpContext == null ||
                this._httpContext.Request == null ||
                this._httpContext.Request.UserLanguages == null)
                return null;

            var userLanguage = this._httpContext.Request.UserLanguages.FirstOrDefault();
            if (String.IsNullOrEmpty(userLanguage))
                return null;

            var language = this._languageService
                .GetAllLanguages()
                .FirstOrDefault(l => userLanguage.Equals(l.LanguageCulture, StringComparison.InvariantCultureIgnoreCase));
            if (language != null && language.Published && this._storeMappingService.Authorize(language))
            {
                return language;
            }

            return null;
        }

        #endregion Utilities

        #region Properties

        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        public virtual Customer CurrentCustomer
        {
            get
            {
                if (this._cachedCustomer != null)
                    return this._cachedCustomer;

                Customer customer = null;
                //if (this._httpContext == null || this._httpContext is FakeHttpContext)
                //{
                //check whether request is made by a background task
                //in this case return built-in customer record for background task
                customer = this._customerService.GetCustomerBySystemName(SystemCustomerNames.BackgroundTask);
                //}

                //check whether request is made by a search engine
                //in this case return built-in customer record for search engines
                //or comment the following two lines of code in order to disable this functionality
                if (customer == null || customer.Deleted || !customer.Active)
                {
                    if (this._userAgentHelper.IsSearchEngine())
                        customer = this._customerService.GetCustomerBySystemName(SystemCustomerNames.SearchEngine);
                }

                //registered user
                if (customer == null || customer.Deleted || !customer.Active)
                {
                    customer = this._authenticationService.GetAuthenticatedCustomer();
                }

                //impersonate user if required (currently used for 'phone order' support)
                if (customer != null && !customer.Deleted && customer.Active)
                {
                    var impersonatedCustomerId = customer.GetAttribute<int?>(SystemCustomerAttributeNames.ImpersonatedCustomerId);
                    if (impersonatedCustomerId.HasValue && impersonatedCustomerId.Value > 0)
                    {
                        var impersonatedCustomer = this._customerService.GetCustomerById(impersonatedCustomerId.Value);
                        if (impersonatedCustomer != null && !impersonatedCustomer.Deleted && impersonatedCustomer.Active)
                        {
                            //set impersonated customer
                            this._originalCustomerIfImpersonated = customer;
                            customer = impersonatedCustomer;
                        }
                    }
                }

                //load guest customer
                if (customer == null || customer.Deleted || !customer.Active)
                {
                    var customerCookie = this.GetCustomerCookie();
                    if (customerCookie != null && !String.IsNullOrEmpty(customerCookie.Value))
                    {
                        Guid customerGuid;
                        if (Guid.TryParse(customerCookie.Value, out customerGuid))
                        {
                            var customerByCookie = this._customerService.GetCustomerByGuid(customerGuid);
                            if (customerByCookie != null &&
                                //this customer (from cookie) should not be registered
                                !customerByCookie.IsRegistered())
                                customer = customerByCookie;
                        }
                    }
                }

                //create guest if not exists
                if (customer == null || customer.Deleted || !customer.Active)
                {
                    customer = this._customerService.InsertGuestCustomer();
                }

                //validation
                if (!customer.Deleted && customer.Active)
                {
                    this.SetCustomerCookie(customer.CustomerGuid);
                    this._cachedCustomer = customer;
                }

                return this._cachedCustomer;
            }
            set
            {
                this.SetCustomerCookie(value.CustomerGuid);
                this._cachedCustomer = value;
            }
        }

        /// <summary>
        /// Gets or sets the original customer (in case the current one is impersonated)
        /// </summary>
        public virtual Customer OriginalCustomerIfImpersonated
        {
            get
            {
                return this._originalCustomerIfImpersonated;
            }
        }

        /// <summary>
        /// Gets or sets the current vendor (logged-in manager)
        /// </summary>
        public virtual Vendor CurrentVendor
        {
            get
            {
                if (this._cachedVendor != null)
                    return this._cachedVendor;

                var currentCustomer = this.CurrentCustomer;
                if (currentCustomer == null)
                    return null;

                var vendor = this._vendorService.GetVendorById(currentCustomer.VendorId);

                //validation
                if (vendor != null && !vendor.Deleted && vendor.Active)
                    this._cachedVendor = vendor;

                return this._cachedVendor;
            }
        }

        /// <summary>
        /// Get or set current user working language
        /// </summary>
        public virtual Language WorkingLanguage
        {
            get
            {
                if (this._cachedLanguage != null)
                    return this._cachedLanguage;

                Language detectedLanguage = null;
                if (this._localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
                {
                    //get language from URL
                    detectedLanguage = this.GetLanguageFromUrl();
                }
                if (detectedLanguage == null && this._localizationSettings.AutomaticallyDetectLanguage)
                {
                    //get language from browser settings
                    //but we do it only once
                    if (!this.CurrentCustomer.GetAttribute<bool>(SystemCustomerAttributeNames.LanguageAutomaticallyDetected,
                        this._genericAttributeService, this._storeContext.CurrentStore.Id))
                    {
                        detectedLanguage = this.GetLanguageFromBrowserSettings();
                        if (detectedLanguage != null)
                        {
                            this._genericAttributeService.SaveAttribute(this.CurrentCustomer, SystemCustomerAttributeNames.LanguageAutomaticallyDetected,
                                 true, this._storeContext.CurrentStore.Id);
                        }
                    }
                }
                if (detectedLanguage != null)
                {
                    //the language is detected. now we need to save it
                    if (this.CurrentCustomer.GetAttribute<int>(SystemCustomerAttributeNames.LanguageId,
                        this._genericAttributeService, this._storeContext.CurrentStore.Id) != detectedLanguage.Id)
                    {
                        this._genericAttributeService.SaveAttribute(this.CurrentCustomer, SystemCustomerAttributeNames.LanguageId,
                            detectedLanguage.Id, this._storeContext.CurrentStore.Id);
                    }
                }

                var allLanguages = this._languageService.GetAllLanguages(storeId: this._storeContext.CurrentStore.Id);
                //find current customer language
                var languageId = this.CurrentCustomer.GetAttribute<int>(SystemCustomerAttributeNames.LanguageId,
                    this._genericAttributeService, this._storeContext.CurrentStore.Id);
                var language = allLanguages.FirstOrDefault(x => x.Id == languageId);
                if (language == null)
                {
                    //it not specified, then return the first (filtered by current store) found one
                    language = allLanguages.FirstOrDefault();
                }
                if (language == null)
                {
                    //it not specified, then return the first found one
                    language = this._languageService.GetAllLanguages().FirstOrDefault();
                }

                //cache
                this._cachedLanguage = language;
                return this._cachedLanguage;
            }
            set
            {
                var languageId = value != null ? value.Id : 0;
                this._genericAttributeService.SaveAttribute(this.CurrentCustomer,
                    SystemCustomerAttributeNames.LanguageId,
                    languageId, this._storeContext.CurrentStore.Id);

                //reset cache
                this._cachedLanguage = null;
            }
        }

        /// <summary>
        /// Get or set current user working currency
        /// </summary>
        public virtual Currency WorkingCurrency
        {
            get
            {
                if (this._cachedCurrency != null)
                    return this._cachedCurrency;

                //return primary store currency when we're in admin area/mode
                if (this.IsAdmin)
                {
                    var primaryStoreCurrency = this._currencyService.GetCurrencyById(this._currencySettings.PrimaryStoreCurrencyId);
                    if (primaryStoreCurrency != null)
                    {
                        //cache
                        this._cachedCurrency = primaryStoreCurrency;
                        return primaryStoreCurrency;
                    }
                }

                var allCurrencies = this._currencyService.GetAllCurrencies(storeId: this._storeContext.CurrentStore.Id);
                //find a currency previously selected by a customer
                var currencyId = this.CurrentCustomer.GetAttribute<int>(SystemCustomerAttributeNames.CurrencyId,
                    this._genericAttributeService, this._storeContext.CurrentStore.Id);
                var currency = allCurrencies.FirstOrDefault(x => x.Id == currencyId);
                if (currency == null)
                {
                    //it not found, then let's load the default currency for the current language (if specified)
                    currencyId = this.WorkingLanguage.DefaultCurrencyId;
                    currency = allCurrencies.FirstOrDefault(x => x.Id == currencyId);
                }
                if (currency == null)
                {
                    //it not found, then return the first (filtered by current store) found one
                    currency = allCurrencies.FirstOrDefault();
                }
                if (currency == null)
                {
                    //it not specified, then return the first found one
                    currency = this._currencyService.GetAllCurrencies().FirstOrDefault();
                }

                //cache
                this._cachedCurrency = currency;
                return this._cachedCurrency;
            }
            set
            {
                var currencyId = value != null ? value.Id : 0;
                this._genericAttributeService.SaveAttribute(this.CurrentCustomer,
                    SystemCustomerAttributeNames.CurrencyId,
                    currencyId, this._storeContext.CurrentStore.Id);

                //reset cache
                this._cachedCurrency = null;
            }
        }

        /// <summary>
        /// Get or set current tax display type
        /// </summary>
        public virtual TaxDisplayType TaxDisplayType
        {
            get
            {
                //cache
                if (this._cachedTaxDisplayType != null)
                    return this._cachedTaxDisplayType.Value;

                TaxDisplayType taxDisplayType;
                if (this._taxSettings.AllowCustomersToSelectTaxDisplayType && this.CurrentCustomer != null)
                {
                    taxDisplayType = (TaxDisplayType)this.CurrentCustomer.GetAttribute<int>(
                        SystemCustomerAttributeNames.TaxDisplayTypeId,
                        this._genericAttributeService,
                        this._storeContext.CurrentStore.Id);
                }
                else
                {
                    taxDisplayType = this._taxSettings.TaxDisplayType;
                }

                //cache
                this._cachedTaxDisplayType = taxDisplayType;
                return this._cachedTaxDisplayType.Value;
            }
            set
            {
                if (!this._taxSettings.AllowCustomersToSelectTaxDisplayType)
                    return;

                this._genericAttributeService.SaveAttribute(this.CurrentCustomer,
                    SystemCustomerAttributeNames.TaxDisplayTypeId,
                    (int)value, this._storeContext.CurrentStore.Id);

                //reset cache
                this._cachedTaxDisplayType = null;
            }
        }

        /// <summary>
        /// Get or set value indicating whether we're in admin area
        /// </summary>
        public virtual bool IsAdmin { get; set; }

        #endregion Properties
    }
}
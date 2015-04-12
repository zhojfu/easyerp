namespace EasyErp.Core.Infrastructure
{
    using EasyErp.Core.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web;
    using System.Web.Hosting;

    /// <summary>
    /// Provides information about types in the current web application.
    /// Optionally this class can look at all assemblies in the bin folder.
    /// </summary>
    public class WebAppTypeFinder : AppDomainTypeFinder
    {
        #region Fields

        private bool ensureBinFolderAssembliesLoaded = true;
        private bool binFolderAssembliesLoaded;

        #endregion Fields

        #region Ctor

        public WebAppTypeFinder(EasyErpConfig config)
        {
            this.ensureBinFolderAssembliesLoaded = config.DynamicDiscovery;
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets or sets wether assemblies in the bin folder of the web application should be specificly checked for beeing loaded on application load. This is need in situations where plugins need to be loaded in the AppDomain after the application been reloaded.
        /// </summary>
        public bool EnsureBinFolderAssembliesLoaded
        {
            get { return this.ensureBinFolderAssembliesLoaded; }
            set { this.ensureBinFolderAssembliesLoaded = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets a physical disk path of \Bin directory
        /// </summary>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public virtual string GetBinDirectory()
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HttpRuntime.BinDirectory;
            }

            //not hosted. For example, run either in unit tests
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (this.EnsureBinFolderAssembliesLoaded && !this.binFolderAssembliesLoaded)
            {
                this.binFolderAssembliesLoaded = true;
                string binPath = this.GetBinDirectory();
                //binPath = _webHelper.MapPath("~/bin");
                this.LoadMatchingAssemblies(binPath);
            }

            return base.GetAssemblies();
        }

        #endregion Methods
    }
}
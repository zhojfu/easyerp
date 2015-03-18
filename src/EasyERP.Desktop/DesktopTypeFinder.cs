namespace EasyERP.Desktop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class DesktopTypeFinder : AppDomainTypeFinder
    {
        private bool binFolderAssembliesLoaded;

        public DesktopTypeFinder()
        {
            this.EnsureBinFolderAssembliesLoaded = true;
        }

        public bool EnsureBinFolderAssembliesLoaded { get; set; }

        public virtual string GetBinDirectory()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (!this.EnsureBinFolderAssembliesLoaded ||
                this.binFolderAssembliesLoaded)
            {
                return base.GetAssemblies();
            }
            this.binFolderAssembliesLoaded = true;
            var binPath = this.GetBinDirectory();

            //binPath = _webHelper.MapPath("~/bin");
            this.LoadMatchingAssemblies(binPath);

            return base.GetAssemblies();
        }
    }
}
namespace EasyERP.Web.Framework.UI
{
    using System.Collections.Generic;
    using System.Web.Optimization;

    public class AsIsBundleOrderer : IBundleOrderer
    {
        public virtual IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
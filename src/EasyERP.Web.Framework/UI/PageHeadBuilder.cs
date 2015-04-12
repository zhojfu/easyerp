namespace EasyERP.Web.Framework.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;

    public partial class PageHeadBuilder : IPageHeadBuilder
    {
        #region Fields

        private static readonly object SLock = new object();

        private readonly List<string> titleParts;
        private readonly List<string> metaDescriptionParts;
        private readonly List<string> metaKeywordParts;
        private readonly Dictionary<ResourceLocation, List<ScriptReferenceMeta>> scriptParts;
        private readonly Dictionary<ResourceLocation, List<string>> cssParts;
        private readonly List<string> canonicalUrlParts;
        private readonly List<string> headCustomParts;

        #endregion Fields

        #region Ctor

        public PageHeadBuilder()
        {
            this.titleParts = new List<string>();
            this.metaDescriptionParts = new List<string>();
            this.metaKeywordParts = new List<string>();
            this.scriptParts = new Dictionary<ResourceLocation, List<ScriptReferenceMeta>>();
            this.cssParts = new Dictionary<ResourceLocation, List<string>>();
            this.canonicalUrlParts = new List<string>();
            this.headCustomParts = new List<string>();
        }

        #endregion Ctor

        #region Utilities

        protected virtual string GetBundleVirtualPath(string prefix, string extension, string[] parts)
        {
            if (parts == null || parts.Length == 0)
                throw new ArgumentException("parts");

            //calculate hash
            string hash;
            using (SHA256 sha = new SHA256Managed())
            {
                // string concatenation
                var hashInput = "";
                foreach (var part in parts)
                {
                    hashInput += part;
                    hashInput += ",";
                }

                byte[] input = sha.ComputeHash(Encoding.Unicode.GetBytes(hashInput));
                hash = HttpServerUtility.UrlTokenEncode(input);
            }

            var sb = new StringBuilder(prefix);
            sb.Append(hash);
            //we used "extension" when we had "runAllManagedModulesForAllRequests" set to "true" in web.config
            //now we disabled it. hence we should not use "extension"
            //sb.Append(extension);
            return sb.ToString();
        }

        protected virtual IItemTransform GetCssTranform()
        {
            return new CssRewriteUrlTransform();
        }

        #endregion Utilities

        #region Methods

        public virtual void AddTitleParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            this.titleParts.Add(part);
        }

        public virtual void AppendTitleParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            this.titleParts.Insert(0, part);
        }

        public virtual string GenerateTitle(bool addDefaultTitle)
        {
            var specificTitle = string.Join("/",
                this.titleParts.AsEnumerable().Reverse().ToArray());
            return string.IsNullOrEmpty(specificTitle) ? string.Empty : specificTitle;
        }

        public virtual void AddMetaDescriptionParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            this.metaDescriptionParts.Add(part);
        }

        public virtual void AppendMetaDescriptionParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            this.metaDescriptionParts.Insert(0, part);
        }

        public virtual string GenerateMetaDescription()
        {
            var metaDescription = string.Join(", ", this.metaDescriptionParts.AsEnumerable().Reverse().ToArray());
            return !String.IsNullOrEmpty(metaDescription) ? metaDescription : string.Empty;
        }

        public virtual void AddMetaKeywordParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            this.metaKeywordParts.Add(part);
        }

        public virtual void AppendMetaKeywordParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            this.metaKeywordParts.Insert(0, part);
        }

        public virtual string GenerateMetaKeywords()
        {
            var metaKeyword = string.Join(", ", this.metaKeywordParts.AsEnumerable().Reverse().ToArray());
            return !String.IsNullOrEmpty(metaKeyword) ? metaKeyword : string.Empty;
        }

        public virtual void AddScriptParts(ResourceLocation location, string part, bool excludeFromBundle)
        {
            if (!this.scriptParts.ContainsKey(location))
                this.scriptParts.Add(location, new List<ScriptReferenceMeta>());

            if (string.IsNullOrEmpty(part))
                return;

            this.scriptParts[location].Add(new ScriptReferenceMeta
            {
                ExcludeFromBundle = excludeFromBundle,
                Part = part
            });
        }

        public virtual void AppendScriptParts(ResourceLocation location, string part, bool excludeFromBundle)
        {
            if (!this.scriptParts.ContainsKey(location))
                this.scriptParts.Add(location, new List<ScriptReferenceMeta>());

            if (string.IsNullOrEmpty(part))
                return;

            this.scriptParts[location].Insert(0, new ScriptReferenceMeta
            {
                ExcludeFromBundle = excludeFromBundle,
                Part = part
            });
        }

        public virtual string GenerateScripts(UrlHelper urlHelper, ResourceLocation location, bool? bundleFiles = null)
        {
            if (!this.scriptParts.ContainsKey(location) || this.scriptParts[location] == null)
                return "";

            if (this.scriptParts.Count == 0)
                return "";

            if (!bundleFiles.HasValue)
            {
                //use setting if no value is specified
                bundleFiles = BundleTable.EnableOptimizations;
            }
            if (bundleFiles.Value)
            {
                var partsToBundle = this.scriptParts[location]
                    .Where(x => !x.ExcludeFromBundle)
                    .Select(x => x.Part)
                    .Distinct()
                    .ToArray();
                var partsToDontBundle = this.scriptParts[location]
                    .Where(x => x.ExcludeFromBundle)
                    .Select(x => x.Part)
                    .Distinct()
                    .ToArray();

                var result = new StringBuilder();

                if (partsToBundle.Length > 0)
                {
                    //IMPORTANT: Do not use bundling in web farms or Windows Azure
                    string bundleVirtualPath = this.GetBundleVirtualPath("~/bundles/scripts/", ".js", partsToBundle);
                    //create bundle
                    lock (SLock)
                    {
                        var bundleFor = BundleTable.Bundles.GetBundleFor(bundleVirtualPath);
                        if (bundleFor == null)
                        {
                            var bundle = new ScriptBundle(bundleVirtualPath);
                            //bundle.Transforms.Clear();

                            //"As is" ordering
                            bundle.Orderer = new AsIsBundleOrderer();
                            //disable file extension replacements. renders scripts which were specified by a developer
                            bundle.EnableFileExtensionReplacements = false;
                            bundle.Include(partsToBundle);
                            BundleTable.Bundles.Add(bundle);
                        }
                    }

                    //parts to bundle
                    result.AppendLine(Scripts.Render(bundleVirtualPath).ToString());
                }

                //parts to do not bundle
                foreach (var path in partsToDontBundle)
                {
                    result.AppendFormat("<script src=\"{0}\" type=\"text/javascript\"></script>", urlHelper.Content(path));
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
            else
            {
                //bundling is disabled
                var result = new StringBuilder();
                foreach (var path in this.scriptParts[location].Select(x => x.Part).Distinct())
                {
                    result.AppendFormat("<script src=\"{0}\" type=\"text/javascript\"></script>", urlHelper.Content(path));
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
        }

        public virtual void AddCssFileParts(ResourceLocation location, string part)
        {
            if (!this.cssParts.ContainsKey(location))
                this.cssParts.Add(location, new List<string>());

            if (string.IsNullOrEmpty(part))
                return;

            this.cssParts[location].Add(part);
        }

        public virtual void AppendCssFileParts(ResourceLocation location, string part)
        {
            if (!this.cssParts.ContainsKey(location))
                this.cssParts.Add(location, new List<string>());

            if (string.IsNullOrEmpty(part))
                return;

            this.cssParts[location].Insert(0, part);
        }

        public virtual string GenerateCssFiles(UrlHelper urlHelper, ResourceLocation location, bool? bundleFiles = null)
        {
            if (!this.cssParts.ContainsKey(location) || this.cssParts[location] == null)
                return "";

            //use only distinct rows
            var distinctParts = this.cssParts[location].Distinct().ToList();
            if (distinctParts.Count == 0)
                return "";
            if (!bundleFiles.HasValue)
            {
                //use setting if no value is specified
                bundleFiles = BundleTable.EnableOptimizations;
            }
            if (bundleFiles.Value)
            {
                //bundling is enabled
                var result = new StringBuilder();

                var partsToBundle = distinctParts.ToArray();
                if (partsToBundle.Length > 0)
                {
                    //IMPORTANT: Do not use bundling in web farms or Windows Azure
                    //IMPORTANT: Do not use CSS bundling in virtual categories
                    string bundleVirtualPath = this.GetBundleVirtualPath("~/bundles/styles/", ".css", partsToBundle);

                    //create bundle
                    lock (SLock)
                    {
                        var bundleFor = BundleTable.Bundles.GetBundleFor(bundleVirtualPath);
                        if (bundleFor == null)
                        {
                            var bundle = new StyleBundle(bundleVirtualPath);
                            //bundle.Transforms.Clear();

                            //"As is" ordering
                            bundle.Orderer = new AsIsBundleOrderer();
                            //disable file extension replacements. renders scripts which were specified by a developer
                            bundle.EnableFileExtensionReplacements = false;
                            foreach (var ptb in partsToBundle)
                            {
                                bundle.Include(ptb, this.GetCssTranform());
                            }
                            BundleTable.Bundles.Add(bundle);
                        }
                    }

                    //parts to bundle
                    result.AppendLine(Styles.Render(bundleVirtualPath).ToString());
                }

                return result.ToString();
            }
            else
            {
                //bundling is disabled
                var result = new StringBuilder();
                foreach (var path in distinctParts)
                {
                    result.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", urlHelper.Content(path));
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
        }

        public virtual void AddCanonicalUrlParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            this.canonicalUrlParts.Add(part);
        }

        public virtual void AppendCanonicalUrlParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            this.canonicalUrlParts.Insert(0, part);
        }

        public virtual string GenerateCanonicalUrls()
        {
            var result = new StringBuilder();
            foreach (var canonicalUrl in this.canonicalUrlParts)
            {
                result.AppendFormat("<link rel=\"canonical\" href=\"{0}\" />", canonicalUrl);
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }

        public virtual void AddHeadCustomParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            this.headCustomParts.Add(part);
        }

        public virtual void AppendHeadCustomParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            this.headCustomParts.Insert(0, part);
        }

        public virtual string GenerateHeadCustom()
        {
            //use only distinct rows
            var distinctParts = this.headCustomParts.Distinct().ToList();
            if (distinctParts.Count == 0)
                return "";

            var result = new StringBuilder();
            foreach (var path in distinctParts)
            {
                result.Append(path);
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }

        #endregion Methods

        #region Nested classes

        private class ScriptReferenceMeta
        {
            public bool ExcludeFromBundle { get; set; }

            public string Part { get; set; }
        }

        #endregion Nested classes
    }
}
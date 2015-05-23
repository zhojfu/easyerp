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

    public class PageHeadBuilder : IPageHeadBuilder
    {
        #region Ctor

        public PageHeadBuilder()
        {
            titleParts = new List<string>();
            metaDescriptionParts = new List<string>();
            metaKeywordParts = new List<string>();
            scriptParts = new Dictionary<ResourceLocation, List<ScriptReferenceMeta>>();
            cssParts = new Dictionary<ResourceLocation, List<string>>();
            canonicalUrlParts = new List<string>();
            headCustomParts = new List<string>();
        }

        #endregion Ctor

        #region Nested classes

        private class ScriptReferenceMeta
        {
            public bool ExcludeFromBundle { get; set; }

            public string Part { get; set; }
        }

        #endregion Nested classes

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

        #region Utilities

        protected virtual string GetBundleVirtualPath(string prefix, string extension, string[] parts)
        {
            if (parts == null ||
                parts.Length == 0)
            {
                throw new ArgumentException("parts");
            }

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

                var input = sha.ComputeHash(Encoding.Unicode.GetBytes(hashInput));
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
            {
                return;
            }

            titleParts.Add(part);
        }

        public virtual void AppendTitleParts(string part)
        {
            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            titleParts.Insert(0, part);
        }

        public virtual string GenerateTitle(bool addDefaultTitle)
        {
            var specificTitle = string.Join(
                "/",
                titleParts.AsEnumerable().Reverse().ToArray());
            return string.IsNullOrEmpty(specificTitle) ? string.Empty : specificTitle;
        }

        public virtual void AddMetaDescriptionParts(string part)
        {
            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            metaDescriptionParts.Add(part);
        }

        public virtual void AppendMetaDescriptionParts(string part)
        {
            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            metaDescriptionParts.Insert(0, part);
        }

        public virtual string GenerateMetaDescription()
        {
            var metaDescription = string.Join(", ", metaDescriptionParts.AsEnumerable().Reverse().ToArray());
            return !string.IsNullOrEmpty(metaDescription) ? metaDescription : string.Empty;
        }

        public virtual void AddMetaKeywordParts(string part)
        {
            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            metaKeywordParts.Add(part);
        }

        public virtual void AppendMetaKeywordParts(string part)
        {
            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            metaKeywordParts.Insert(0, part);
        }

        public virtual string GenerateMetaKeywords()
        {
            var metaKeyword = string.Join(", ", metaKeywordParts.AsEnumerable().Reverse().ToArray());
            return !string.IsNullOrEmpty(metaKeyword) ? metaKeyword : string.Empty;
        }

        public virtual void AddScriptParts(ResourceLocation location, string part, bool excludeFromBundle)
        {
            if (!scriptParts.ContainsKey(location))
            {
                scriptParts.Add(location, new List<ScriptReferenceMeta>());
            }

            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            scriptParts[location].Add(
                new ScriptReferenceMeta
                {
                    ExcludeFromBundle = excludeFromBundle,
                    Part = part
                });
        }

        public virtual void AppendScriptParts(ResourceLocation location, string part, bool excludeFromBundle)
        {
            if (!scriptParts.ContainsKey(location))
            {
                scriptParts.Add(location, new List<ScriptReferenceMeta>());
            }

            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            scriptParts[location].Insert(
                0,
                new ScriptReferenceMeta
                {
                    ExcludeFromBundle = excludeFromBundle,
                    Part = part
                });
        }

        public virtual string GenerateScripts(UrlHelper urlHelper, ResourceLocation location, bool? bundleFiles = null)
        {
            if (!scriptParts.ContainsKey(location) ||
                scriptParts[location] == null)
            {
                return "";
            }

            if (scriptParts.Count == 0)
            {
                return "";
            }

            if (!bundleFiles.HasValue)
            {
                //use setting if no value is specified
                bundleFiles = BundleTable.EnableOptimizations;
            }
            if (bundleFiles.Value)
            {
                var partsToBundle = scriptParts[location]
                    .Where(x => !x.ExcludeFromBundle)
                    .Select(x => x.Part)
                    .Distinct()
                    .ToArray();
                var partsToDontBundle = scriptParts[location]
                    .Where(x => x.ExcludeFromBundle)
                    .Select(x => x.Part)
                    .Distinct()
                    .ToArray();

                var result = new StringBuilder();

                if (partsToBundle.Length > 0)
                {
                    //IMPORTANT: Do not use bundling in web farms or Windows Azure
                    var bundleVirtualPath = GetBundleVirtualPath("~/bundles/scripts/", ".js", partsToBundle);

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
                    result.AppendFormat(
                        "<script src=\"{0}\" type=\"text/javascript\"></script>",
                        urlHelper.Content(path));
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
            else
            {
                //bundling is disabled
                var result = new StringBuilder();
                foreach (var path in scriptParts[location].Select(x => x.Part).Distinct())
                {
                    result.AppendFormat(
                        "<script src=\"{0}\" type=\"text/javascript\"></script>",
                        urlHelper.Content(path));
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
        }

        public virtual void AddCssFileParts(ResourceLocation location, string part)
        {
            if (!cssParts.ContainsKey(location))
            {
                cssParts.Add(location, new List<string>());
            }

            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            cssParts[location].Add(part);
        }

        public virtual void AppendCssFileParts(ResourceLocation location, string part)
        {
            if (!cssParts.ContainsKey(location))
            {
                cssParts.Add(location, new List<string>());
            }

            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            cssParts[location].Insert(0, part);
        }

        public virtual string GenerateCssFiles(UrlHelper urlHelper, ResourceLocation location, bool? bundleFiles = null)
        {
            if (!cssParts.ContainsKey(location) ||
                cssParts[location] == null)
            {
                return "";
            }

            //use only distinct rows
            var distinctParts = cssParts[location].Distinct().ToList();
            if (distinctParts.Count == 0)
            {
                return "";
            }
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
                    var bundleVirtualPath = GetBundleVirtualPath("~/bundles/styles/", ".css", partsToBundle);

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
                                bundle.Include(ptb, GetCssTranform());
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
                    result.AppendFormat(
                        "<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />",
                        urlHelper.Content(path));
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
        }

        public virtual void AddCanonicalUrlParts(string part)
        {
            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            canonicalUrlParts.Add(part);
        }

        public virtual void AppendCanonicalUrlParts(string part)
        {
            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            canonicalUrlParts.Insert(0, part);
        }

        public virtual string GenerateCanonicalUrls()
        {
            var result = new StringBuilder();
            foreach (var canonicalUrl in canonicalUrlParts)
            {
                result.AppendFormat("<link rel=\"canonical\" href=\"{0}\" />", canonicalUrl);
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }

        public virtual void AddHeadCustomParts(string part)
        {
            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            headCustomParts.Add(part);
        }

        public virtual void AppendHeadCustomParts(string part)
        {
            if (string.IsNullOrEmpty(part))
            {
                return;
            }

            headCustomParts.Insert(0, part);
        }

        public virtual string GenerateHeadCustom()
        {
            //use only distinct rows
            var distinctParts = headCustomParts.Distinct().ToList();
            if (distinctParts.Count == 0)
            {
                return "";
            }

            var result = new StringBuilder();
            foreach (var path in distinctParts)
            {
                result.Append(path);
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }

        #endregion Methods
    }
}
namespace EasyERP.Web.Framework.Themes
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public abstract class ThemeableVirtualPathProviderViewEngine : VirtualPathProviderViewEngine
    {
        #region Ctor

        protected ThemeableVirtualPathProviderViewEngine()
        {
            GetExtensionThunk = VirtualPathUtility.GetExtension;
        }

        #endregion Ctor

        #region Fields

        internal Func<string, string> GetExtensionThunk;

        private readonly string[] _emptyLocations = null;

        #endregion Fields

        #region Utilities

        protected virtual string GetPath(
            ControllerContext controllerContext,
            string[] locations,
            string[] areaLocations,
            string locationsPropertyName,
            string name,
            string controllerName,
            string theme,
            string cacheKeyPrefix,
            bool useCache,
            out string[] searchedLocations)
        {
            searchedLocations = _emptyLocations;
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            var areaName = GetAreaName(controllerContext.RouteData);

            //little hack to get nop's admin area to be in /Administration/ instead of /Nop/Admin/ or Areas/Admin/
            if (!string.IsNullOrEmpty(areaName) &&
                areaName.Equals("storeadmin", StringComparison.InvariantCultureIgnoreCase))
            {
                var newLocations = areaLocations.ToList();
                newLocations.Insert(0, "~/EasyERP.StoreAdmin/Views/{1}/{0}.cshtml");
                newLocations.Insert(0, "~/EasyERP.StoreAdmin/Views/Shared/{0}.cshtml");
                areaLocations = newLocations.ToArray();
            }

            var flag = !string.IsNullOrEmpty(areaName);
            var viewLocations = GetViewLocations(locations, flag ? areaLocations : null);
            if (viewLocations.Count == 0)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "Properties cannot be null or empty.",
                        locationsPropertyName));
            }
            var flag2 = IsSpecificPath(name);
            var key = CreateCacheKey(cacheKeyPrefix, name, flag2 ? string.Empty : controllerName, areaName, theme);
            if (useCache)
            {
                var cached = ViewLocationCache.GetViewLocation(controllerContext.HttpContext, key);
                if (cached != null)
                {
                    return cached;
                }
            }
            if (!flag2)
            {
                return GetPathFromGeneralName(
                    controllerContext,
                    viewLocations,
                    name,
                    controllerName,
                    areaName,
                    theme,
                    key,
                    ref searchedLocations);
            }
            return GetPathFromSpecificName(controllerContext, name, key, ref searchedLocations);
        }

        protected virtual bool FilePathIsSupported(string virtualPath)
        {
            if (FileExtensions == null)
            {
                return true;
            }
            var str = GetExtensionThunk(virtualPath).TrimStart('.');
            return FileExtensions.Contains(str, StringComparer.OrdinalIgnoreCase);
        }

        protected virtual string GetPathFromSpecificName(
            ControllerContext controllerContext,
            string name,
            string cacheKey,
            ref string[] searchedLocations)
        {
            var virtualPath = name;
            if (!FilePathIsSupported(name) ||
                !FileExists(controllerContext, name))
            {
                virtualPath = string.Empty;
                searchedLocations = new[] { name };
            }
            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
            return virtualPath;
        }

        protected virtual string GetPathFromGeneralName(
            ControllerContext controllerContext,
            List<ViewLocation> locations,
            string name,
            string controllerName,
            string areaName,
            string theme,
            string cacheKey,
            ref string[] searchedLocations)
        {
            var virtualPath = string.Empty;
            searchedLocations = new string[locations.Count];
            for (var i = 0; i < locations.Count; i++)
            {
                var str2 = locations[i].Format(name, controllerName, areaName, theme);
                if (FileExists(controllerContext, str2))
                {
                    searchedLocations = _emptyLocations;
                    virtualPath = str2;
                    ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
                    return virtualPath;
                }
                searchedLocations[i] = str2;
            }
            return virtualPath;
        }

        protected virtual string CreateCacheKey(
            string prefix,
            string name,
            string controllerName,
            string areaName,
            string theme)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:{5}",
                GetType().AssemblyQualifiedName,
                prefix,
                name,
                controllerName,
                areaName,
                theme);
        }

        protected virtual List<ViewLocation> GetViewLocations(
            string[] viewLocationFormats,
            string[] areaViewLocationFormats)
        {
            var list = new List<ViewLocation>();
            if (areaViewLocationFormats != null)
            {
                list.AddRange(
                    areaViewLocationFormats.Select(str => new AreaAwareViewLocation(str)).Cast<ViewLocation>());
            }
            if (viewLocationFormats != null)
            {
                list.AddRange(viewLocationFormats.Select(str2 => new ViewLocation(str2)));
            }
            return list;
        }

        protected virtual bool IsSpecificPath(string name)
        {
            var ch = name[0];
            if (ch != '~')
            {
                return (ch == '/');
            }
            return true;
        }

        protected virtual string GetAreaName(RouteData routeData)
        {
            object obj2;
            if (routeData.DataTokens.TryGetValue("area", out obj2))
            {
                return (obj2 as string);
            }
            return GetAreaName(routeData.Route);
        }

        protected virtual string GetAreaName(RouteBase route)
        {
            var area = route as IRouteWithArea;
            if (area != null)
            {
                return area.Area;
            }
            var route2 = route as Route;
            if ((route2 != null) &&
                (route2.DataTokens != null))
            {
                return (route2.DataTokens["area"] as string);
            }
            return null;
        }

        protected virtual ViewEngineResult FindThemeableView(
            ControllerContext controllerContext,
            string viewName,
            string masterName,
            bool useCache)
        {
            string[] strArray;
            string[] strArray2;
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException("View name cannot be null or empty.", "viewName");
            }
            var theme = string.Empty;
            var requiredString = controllerContext.RouteData.GetRequiredString("controller");
            var str2 = GetPath(
                controllerContext,
                ViewLocationFormats,
                AreaViewLocationFormats,
                "ViewLocationFormats",
                viewName,
                requiredString,
                theme,
                "View",
                useCache,
                out strArray);
            var str3 = GetPath(
                controllerContext,
                MasterLocationFormats,
                AreaMasterLocationFormats,
                "MasterLocationFormats",
                masterName,
                requiredString,
                theme,
                "Master",
                useCache,
                out strArray2);
            if (!string.IsNullOrEmpty(str2) &&
                (!string.IsNullOrEmpty(str3) || string.IsNullOrEmpty(masterName)))
            {
                return new ViewEngineResult(CreateView(controllerContext, str2, str3), this);
            }
            if (strArray2 == null)
            {
                strArray2 = new string[0];
            }
            return new ViewEngineResult(strArray.Union(strArray2));
        }

        protected virtual ViewEngineResult FindThemeablePartialView(
            ControllerContext controllerContext,
            string partialViewName,
            bool useCache)
        {
            string[] strArray;
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (string.IsNullOrEmpty(partialViewName))
            {
                throw new ArgumentException("Partial view name cannot be null or empty.", "partialViewName");
            }
            var theme = string.Empty;
            var requiredString = controllerContext.RouteData.GetRequiredString("controller");
            var str2 = GetPath(
                controllerContext,
                PartialViewLocationFormats,
                AreaPartialViewLocationFormats,
                "PartialViewLocationFormats",
                partialViewName,
                requiredString,
                theme,
                "Partial",
                useCache,
                out strArray);
            if (string.IsNullOrEmpty(str2))
            {
                return new ViewEngineResult(strArray);
            }
            return new ViewEngineResult(CreatePartialView(controllerContext, str2), this);
        }

        //protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        //{
        //    return BuildManager.GetObjectFactory(virtualPath, false) != null;
        //}

        #endregion Utilities

        #region Methods

        public override ViewEngineResult FindView(
            ControllerContext controllerContext,
            string viewName,
            string masterName,
            bool useCache)
        {
            var result = FindThemeableView(controllerContext, viewName, masterName, useCache);
            return result;
        }

        public override ViewEngineResult FindPartialView(
            ControllerContext controllerContext,
            string partialViewName,
            bool useCache)
        {
            var result = FindThemeablePartialView(controllerContext, partialViewName, useCache);
            return result;
        }

        #endregion Methods
    }

    public class AreaAwareViewLocation : ViewLocation
    {
        public AreaAwareViewLocation(string virtualPathFormatString)
            : base(virtualPathFormatString)
        {
        }

        public override string Format(string viewName, string controllerName, string areaName, string theme)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                _virtualPathFormatString,
                viewName,
                controllerName,
                areaName,
                theme);
        }
    }

    public class ViewLocation
    {
        protected readonly string _virtualPathFormatString;

        public ViewLocation(string virtualPathFormatString)
        {
            _virtualPathFormatString = virtualPathFormatString;
        }

        public virtual string Format(string viewName, string controllerName, string areaName, string theme)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                _virtualPathFormatString,
                viewName,
                controllerName,
                theme);
        }
    }
}
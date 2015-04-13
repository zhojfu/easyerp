namespace Doamin.Service.Security
{
    using Doamin.Service.Users;
    using Domain.Model.Security;
    using System;
    using System.Collections.Generic;

    public class StandardPermissionProvider : IPermissionProvider
    {
        public static readonly PermissionRecord ManageProducts = new PermissionRecord
        {
            Name = "Admin area. Manage Products",
            SystemName = "ManageProducts",
            Category = "Catalog"
        };

        public static readonly PermissionRecord ManageStores = new PermissionRecord
        {
            Name = "Admin area. Manage Stores",
            SystemName = "ManageStores",
            Category = "Catalog"
        };

        public static readonly PermissionRecord ManageOrders = new PermissionRecord
        {
            Name = "Admin area. Manage Orders",
            SystemName = "ManageOrders",
            Category = "Catalog"
        };

        public static readonly PermissionRecord EnableShoppingCart = new PermissionRecord
        {
            Name = "Public store. Enable shopping cart",
            SystemName = "EnableShoppingCart",
            Category = "PublicStore"
        };

        public static readonly PermissionRecord ViewProductList = new PermissionRecord
        {
            Name = "View product list",
            SystemName = "ViewProductList",
            Category = "StoreAdmin"
        };

        public IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[] { ManageProducts, ManageStores, ManageOrders };
        }

        public IEnumerable<DefaultPermissionRecord> GetDefaultPermissions()
        {
            return new[]
            {
                new DefaultPermissionRecord
                {
                    UserRoleSystemName = SystemUserRoleNames.Administrators,
                    PermissionRecords = new[] { ManageProducts, ManageStores, ManageOrders }
                },
                new DefaultPermissionRecord
                {
                    UserRoleSystemName = SystemUserRoleNames.StoreAdmin,
                    PermissionRecords = new []{ManageStores}
                },
                new DefaultPermissionRecord
                {
                    UserRoleSystemName = SystemUserRoleNames.FactoryAdmin
                }
            };
        }
    }
}
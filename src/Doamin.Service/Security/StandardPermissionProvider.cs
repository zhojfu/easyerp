namespace Doamin.Service.Security
{
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

        public IEnumerable<PermissionRecord> GetPermissions()
        {
            throw new NotImplementedException();
        }
    }
}
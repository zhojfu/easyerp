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

        public IEnumerable<PermissionRecord> GetPermissions()
        {
            throw new NotImplementedException();
        }
    }
}
namespace Doamin.Service.Security
{
    using Domain.Model.Security;
    using System.Collections.Generic;

    public interface IPermissionProvider
    {
        /// <summary>
        /// Get permissions
        /// </summary>
        /// <returns>Permissions</returns>
        IEnumerable<PermissionRecord> GetPermissions();
    }
}
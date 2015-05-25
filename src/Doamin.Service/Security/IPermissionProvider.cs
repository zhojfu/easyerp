namespace Doamin.Service.Security
{
    using System.Collections.Generic;
    using Domain.Model.Security;

    public interface IPermissionProvider
    {
        IEnumerable<PermissionRecord> GetPermissions();
        IEnumerable<DefaultPermissionRecord> GetDefaultPermissions();
    }
}
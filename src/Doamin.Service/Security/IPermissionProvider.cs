namespace Doamin.Service.Security
{
    using Domain.Model.Security;
    using System.Collections.Generic;

    public interface IPermissionProvider
    {
        IEnumerable<PermissionRecord> GetPermissions();

        IEnumerable<DefaultPermissionRecord> GetDefaultPermissions();
    }
}
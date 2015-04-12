namespace Doamin.Service.Security
{
    using Domain.Model.Base;
    using Domain.Model.Security;
    using System.Collections.Generic;

    public interface IPermissionService
    {
        void DeletePermissionRecord(PermissionRecord permission);

        PermissionRecord GetPermissionRecordById(int permissionId);

        PermissionRecord GetPermissionRecordBySystemName(string systemName);

        IList<PermissionRecord> GetAllPermissionRecords();

        void InsertPermissionRecord(PermissionRecord permission);

        void UpdatePermissionRecord(PermissionRecord permission);

        bool Authorize(PermissionRecord permission);

        bool Authorize(PermissionRecord permission, User user);

        bool Authorize(string permissionRecordSystemName);

        bool Authorize(string permissionRecordSystemName, User user);
    }
}
namespace Doamin.Service.Security
{
    using Domain.Model.Base;
    using Domain.Model.Security;
    using Domain.Model.Users;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Permission service
    /// </summary>
    public class PermissionService : IPermissionService
    {
        public void DeletePermissionRecord(PermissionRecord permission)
        {
            throw new NotImplementedException();
        }

        public PermissionRecord GetPermissionRecordById(int permissionId)
        {
            throw new NotImplementedException();
        }

        public PermissionRecord GetPermissionRecordBySystemName(string systemName)
        {
            throw new NotImplementedException();
        }

        public IList<PermissionRecord> GetAllPermissionRecords()
        {
            throw new NotImplementedException();
        }

        public void InsertPermissionRecord(PermissionRecord permission)
        {
            throw new NotImplementedException();
        }

        public void UpdatePermissionRecord(PermissionRecord permission)
        {
            throw new NotImplementedException();
        }

        public bool Authorize(PermissionRecord permission)
        {
            return true;
        }

        public bool Authorize(PermissionRecord permission, User user)
        {
            return true;
            throw new NotImplementedException();
        }

        public bool Authorize(string permissionRecordSystemName)
        {
            return true;
            throw new NotImplementedException();
        }

        public bool Authorize(string permissionRecordSystemName, User user)
        {
            return true;
            throw new NotImplementedException();
        }
    }
}
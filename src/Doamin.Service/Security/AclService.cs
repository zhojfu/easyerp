namespace Doamin.Service.Security
{
    using Domain.Model.Security;
    using Domain.Model.Users;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class AclService : IAclService
    {
        public void DeleteAclRecord(AclRecord aclRecord)
        {
            throw new NotImplementedException();
        }

        public AclRecord GetAclRecordById(int aclRecordId)
        {
            throw new NotImplementedException();
        }

        public IList<AclRecord> GetAclRecords<T>(T entity) where T : BaseEntity, IAclSupported
        {
            throw new NotImplementedException();
        }

        public void InsertAclRecord(AclRecord aclRecord)
        {
            throw new NotImplementedException();
        }

        public void InsertAclRecord<T>(T entity, int customerRoleId) where T : BaseEntity, IAclSupported
        {
            throw new NotImplementedException();
        }

        public void UpdateAclRecord(AclRecord aclRecord)
        {
            throw new NotImplementedException();
        }

        public int[] GetCustomerRoleIdsWithAccess<T>(T entity) where T : BaseEntity, IAclSupported
        {
            throw new NotImplementedException();
        }

        public bool Authorize<T>(T entity) where T : BaseEntity, IAclSupported
        {
            throw new NotImplementedException();
        }

        public bool Authorize<T>(T entity, User user) where T : BaseEntity, IAclSupported
        {
            throw new NotImplementedException();
        }
    }
}
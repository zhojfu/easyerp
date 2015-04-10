namespace Doamin.Service.Security
{
    using Domain.Model.Security;
    using Infrastructure.Domain.Model;
    using System.Collections.Generic;

    public class AclService : IAclService
    {
        public void DeleteAclRecord(AclRecord aclRecord)
        {
            throw new System.NotImplementedException();
        }

        public AclRecord GetAclRecordById(int aclRecordId)
        {
            throw new System.NotImplementedException();
        }

        public IList<AclRecord> GetAclRecords<T>(T entity) where T : BaseEntity, IAclSupported
        {
            throw new System.NotImplementedException();
        }

        public void InsertAclRecord(AclRecord aclRecord)
        {
            throw new System.NotImplementedException();
        }

        public void InsertAclRecord<T>(T entity, int customerRoleId) where T : BaseEntity, IAclSupported
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAclRecord(AclRecord aclRecord)
        {
            throw new System.NotImplementedException();
        }

        public int[] GetCustomerRoleIdsWithAccess<T>(T entity) where T : BaseEntity, IAclSupported
        {
            throw new System.NotImplementedException();
        }

        public bool Authorize<T>(T entity) where T : BaseEntity, IAclSupported
        {
            throw new System.NotImplementedException();
        }
    }
}
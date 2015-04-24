namespace Doamin.Service.Security
{
    using System.Collections.Generic;
    using Domain.Model.Security;
    using Domain.Model.Users;
    using Infrastructure.Domain.Model;

    public interface IAclService
    {
        void DeleteAclRecord(AclRecord aclRecord);
        AclRecord GetAclRecordById(int aclRecordId);
        IList<AclRecord> GetAclRecords<T>(T entity) where T : BaseEntity, IAclSupported;
        void InsertAclRecord(AclRecord aclRecord);
        void InsertAclRecord<T>(T entity, int customerRoleId) where T : BaseEntity, IAclSupported;
        void UpdateAclRecord(AclRecord aclRecord);
        int[] GetCustomerRoleIdsWithAccess<T>(T entity) where T : BaseEntity, IAclSupported;
        bool Authorize<T>(T entity) where T : BaseEntity, IAclSupported;
        bool Authorize<T>(T entity, User user) where T : BaseEntity, IAclSupported;
    }
}
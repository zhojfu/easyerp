namespace Domain.Model.Users
{
    using System.Collections.Generic;
    using Domain.Model.Security;
    using Infrastructure.Domain.Model;

    public class UserRole : BaseEntity, IAggregateRoot
    {
        private ICollection<PermissionRecord> permissionRecords;

        public string Name { get; set; }

        public bool Active { get; set; }

        public bool IsSystemRole { get; set; }

        public string SystemName { get; set; }

        public virtual ICollection<PermissionRecord> PermissionRecords
        {
            get { return permissionRecords ?? (permissionRecords = new List<PermissionRecord>()); }
            set { permissionRecords = value; }
        }
    }
}
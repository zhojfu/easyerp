namespace Domain.Model.Users
{
    using Domain.Model.Security;
    using Infrastructure.Domain.Model;
    using System.Collections.Generic;

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
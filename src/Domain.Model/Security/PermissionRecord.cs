namespace Domain.Model.Security
{
    using Domain.Model.Users;
    using Infrastructure.Domain.Model;
    using System.Collections.Generic;

    public class PermissionRecord : BaseEntity, IAggregateRoot
    {
        private ICollection<UserRole> customerRoles;

        /// <summary>
        /// Gets or sets the permission name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the permission system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the permission category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        public virtual ICollection<UserRole> CustomerRoles
        {
            get { return this.customerRoles ?? (this.customerRoles = new List<UserRole>()); }
            protected set { this.customerRoles = value; }
        }
    }
}
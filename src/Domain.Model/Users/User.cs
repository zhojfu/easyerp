namespace Domain.Model.Users
{
    using Domain.Model.Stores;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class User : BaseEntity, IAggregateRoot
    {
        private ICollection<UserRole> userRoles;

        public string Name { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public bool Active { get; set; }

        public int StoreId { get; set; }

        public bool Deleted { get; set; }

        public Guid UseGuid { get; set; }

        public Store Store { get; set; }

        public virtual ICollection<UserRole> UserRoles
        {
            get { return this.userRoles ?? (this.userRoles = new List<UserRole>()); }
            set { this.userRoles = value; }
        }

        public DateTime CreatedOn { get; set; }

        public DateTime LastLoginDate { get; set; }
    }
}
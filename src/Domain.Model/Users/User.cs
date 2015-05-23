namespace Domain.Model.Users
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Orders;
    using Domain.Model.Stores;
    using Infrastructure.Domain.Model;

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
            get { return userRoles ?? (userRoles = new List<UserRole>()); }
            set { userRoles = value; }
        }

        public DateTime CreatedOn { get; set; }

        public DateTime LastLoginDate { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
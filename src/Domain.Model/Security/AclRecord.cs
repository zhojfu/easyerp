namespace Domain.Model.Security
{
    using Domain.Model.Users;
    using Infrastructure.Domain.Model;

    public class AclRecord : BaseEntity, IAggregateRoot
    {
        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public int UserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the customer role
        /// </summary>
        public virtual UserRole CustomerRole { get; set; }
    }
}
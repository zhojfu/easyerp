namespace Domain.Model.Security
{
    using Domain.Model.Users;
    using Infrastructure.Domain.Model;

    public class AclRecord : BaseEntity, IAggregateRoot
    {
        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public int UserRoleId { get; set; }

        public virtual UserRole CustomerRole { get; set; }
    }
}
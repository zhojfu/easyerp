namespace Domain.Model.Base
{
    using Infrastructure.Domain.Model;

    public class AccessGroups : BaseEntity
    {
        public string Name { get; set; }

        public string Comment { get; set; }
    }
}
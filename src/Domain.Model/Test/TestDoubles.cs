namespace Domain.Model
{
    using Infrastructure.Domain.Model;

    public class TestDoubles : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
    }
}
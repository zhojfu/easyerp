namespace Infrastructure.Domain.Model
{
    using System;

    public interface IEntity
    {
        Guid Id { get; }
    }
}
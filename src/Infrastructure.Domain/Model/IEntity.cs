using System;

namespace Infrastructure.Domain.Model
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}

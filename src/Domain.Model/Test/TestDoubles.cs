using System;
using Infrastructure.Domain.Model;

namespace Domain.Model
{
    public class TestDoubles : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
    }
}

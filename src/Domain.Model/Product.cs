using Infrastructure.Domain.Model;

namespace Domain.Model
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public string Upc { get; set; } //条形码
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Cost { get; set; }
        public double Volume { get; set; }
        public string Origin { get; set; } //产地
    }
}

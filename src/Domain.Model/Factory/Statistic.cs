using System;

namespace Domain.Model.Factory
{
    using Infrastructure.Domain.Model;

    public class Statistic : BaseEntity
    {
        public DateTime Date { get; set; }

        public double Value { get; set; }

       
    }
}

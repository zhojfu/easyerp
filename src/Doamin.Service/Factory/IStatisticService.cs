
namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;

    using Domain.Model.Factory;

    public interface IStatisticService<TModel> where TModel : Statistic
    {
        IEnumerable<TModel> GetStatisticsByDate(DateTime from);

        void BatchUpdateStatisitcs(IEnumerable<TModel> workTimeItems);
    }
}

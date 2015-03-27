
namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;

    using Domain.Model.Factory;

    public interface IStatisticService<TModel> where TModel : Statistic
    {
        IEnumerable<TModel> GetWorkTimeStatisticsByDate(DateTime from);

        void BatchUpdateStatisitcs(IEnumerable<TModel> workTimeItems);
    }
}

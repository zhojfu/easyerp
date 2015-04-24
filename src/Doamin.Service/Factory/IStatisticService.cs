namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Factory;
    using Infrastructure.Utility;

    public interface IStatisticService<TModel> where TModel : Statistic
    {
        PagedResult<TModel> GetStatisticsByDate(DateTime from, int page, int pageSize);
        void BatchUpdateStatisitcs(IEnumerable<TModel> workTimeItems);
    }
}
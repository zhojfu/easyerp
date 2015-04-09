
namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;

    using Domain.Model.Factory;

    using Infrastructure.Domain;
    using Infrastructure.Utility;

    public class DaliyStatisticService<T> : IStatisticService<T>
        where T : Statistic
    {
        private readonly IRepository<T> repository;

        private readonly IUnitOfWork unitOfWork;

        public DaliyStatisticService(IRepository<T> repository, IUnitOfWork unitOfWork )
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public PagedResult<T> GetStatisticsByDate(DateTime date, int page, int pageSize)
        {
            var dateRange = DateHelper.GetWeekRangeOfCurrentDate(date);

            return this.repository.FindAll()
            return this.repository.FindAll(m => (m.Date >= dateRange.Item1 && m.Date <= dateRange.Item2));
        }

        public void BatchUpdateStatisitcs(IEnumerable<T> statisticItems)
        {
            foreach (var statisticItem in statisticItems)
            {
                this.UpdateStatistics(statisticItem);
            }

            this.unitOfWork.Commit();
        }

        private void UpdateStatistics(T statisticItem)
        {
            if (this.repository.Exist(statisticItem))
            {
                this.repository.Update(statisticItem);
            }
            else
            {
                this.repository.Add(statisticItem);
            }
        }
    }
}

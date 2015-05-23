﻿namespace Doamin.Service.Factory
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

        public DaliyStatisticService(IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public PagedResult<T> GetStatisticsByDate(DateTime date, int page, int pageSize)
        {
            return null;
        }

        public void BatchUpdateStatisitcs(IEnumerable<T> statisticItems)
        {
            foreach (var statisticItem in statisticItems)
            {
                UpdateStatistics(statisticItem);
            }

            unitOfWork.Commit();
        }

        private void UpdateStatistics(T statisticItem)
        {
            if (repository.Exist(statisticItem))
            {
                repository.Update(statisticItem);
            }
            else
            {
                repository.Add(statisticItem);
            }
        }
    }
}
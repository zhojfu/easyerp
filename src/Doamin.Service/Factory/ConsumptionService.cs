
namespace Doamin.Service.Factory
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Model.Factory;

    using Infrastructure.Domain;
    using Infrastructure.Utility;

    public class ConsumptionService : IConsumptionService
    {
        private readonly IRepository<Consumption> repository;

        private readonly IUnitOfWork unitOfWork;

        public ConsumptionService(IRepository<Consumption> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void UpdateConsumptionCategory(Consumption consumption)
        {
            if (!this.repository.Exist(consumption))
            {
                return;
            }

            this.repository.Update(consumption);
            this.unitOfWork.Commit();
        }

        public void AddConsumptionCategory(Consumption consumption)
        {
            if (IsConsumptionExist(consumption))
                return;
            this.repository.Add(consumption);
            this.unitOfWork.Commit();
        }

        public void DeleteConsumptionCategory(Consumption consumption)
        {
            var cons = this.repository.GetByKey(consumption.Id);
            if (cons == null)
                return;
            this.repository.Remove(cons);
            this.unitOfWork.Commit();
        }

        public PagedResult<Consumption> GetConsumptionCategories(int page, int pageSize)
        {
            return this.repository.FindAll(pageSize, page, c=> true, c=>c.Name, SortOrder.Ascending);
        }

        private bool IsConsumptionExist(Consumption consumption)
        {
            return this.repository.FindAll(m => m.Name == consumption.Name).Any();
        }
    }
}

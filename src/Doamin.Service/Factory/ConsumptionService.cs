namespace Doamin.Service.Factory
{
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
            if (!repository.Exist(consumption))
            {
                return;
            }

            repository.Update(consumption);
            unitOfWork.Commit();
        }

        public void AddConsumptionCategory(Consumption consumption)
        {
            if (IsConsumptionExist(consumption))
            {
                return;
            }
            repository.Add(consumption);
            unitOfWork.Commit();
        }

        public void DeleteConsumptionCategory(Consumption consumption)
        {
            var cons = repository.GetByKey(consumption.Id);
            if (cons == null)
            {
                return;
            }
            repository.Remove(cons);
            unitOfWork.Commit();
        }

        public PagedResult<Consumption> GetConsumptionCategories(int page, int pageSize)
        {
            return repository.FindAll(pageSize, page, c => true, c => c.Name, SortOrder.Ascending);
        }

        private bool IsConsumptionExist(Consumption consumption)
        {
            return repository.FindAll(m => m.Name == consumption.Name).Any();
        }
    }
}
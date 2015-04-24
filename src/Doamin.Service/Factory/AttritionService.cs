namespace Doamin.Service.Factory
{
    using System.Collections.Generic;
    using Domain.Model.Factory;
    using Infrastructure.Domain;

    public class AttritionService : IAttritionService
    {
        private readonly IRepository<Attrition> repository;

        private readonly IUnitOfWork unitOfWork;

        public AttritionService(IRepository<Attrition> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void UpdateAttritionCategory(Attrition attrition)
        {
            repository.Update(attrition);
            unitOfWork.Commit();
        }

        public void AddAttritionCategory(Attrition attrition)
        {
            repository.Add(attrition);
            unitOfWork.Commit();
        }

        public IEnumerable<Attrition> GetAttritionCategories()
        {
            return repository.FindAll(a => true);
        }
    }
}
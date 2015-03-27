
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
            this.repository.Update(attrition);
            this.unitOfWork.Commit();
        }

        public void AddAttritionCategory(Attrition attrition)
        {
            this.repository.Add(attrition);
            this.unitOfWork.Commit();
        }

        public IEnumerable<Attrition> GetAttritionCategories()
        {
            return this.repository.FindAll(a => true);
        }
    }
}

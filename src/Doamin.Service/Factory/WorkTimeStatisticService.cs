
namespace Doamin.Service.Factory
{
    using Domain.Model.Factory;

    using Infrastructure.Domain;

    public class WorkTimeStatisticService : DaliyStatisticService<WorkTimeStatistic>
    {
        public WorkTimeStatisticService(IRepository<WorkTimeStatistic> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}

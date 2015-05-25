namespace Doamin.Service.Factory
{
    using Domain.Model.Factory;
    using Infrastructure.Utility;

    public interface IConsumptionService
    {
        void UpdateConsumptionCategory(Consumption c);
        void AddConsumptionCategory(Consumption c);
        void DeleteConsumptionCategory(Consumption c);
        PagedResult<Consumption> GetConsumptionCategories(int page, int pageSize);
    }
}
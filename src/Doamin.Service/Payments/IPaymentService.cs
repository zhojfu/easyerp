namespace Doamin.Service.Payments
{
    using Domain.Model.Payments;

    public interface IPaymentService
    {
        Payment GetPaymentByInventoryId(int id);

        Payment GetPaymentByOrderId(int id);
    }
}
namespace Doamin.Service.Payments
{
    using Domain.Model.Payments;
    using Domain.Model.Products;
    using Infrastructure;
    using Infrastructure.Domain;
    using System;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;

    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> paymentRepository;
        private readonly IRepository<Inventory> inventoryRepository;

        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IRepository<Payment> paymentRepository, IRepository<Inventory> inventoryRepository, IUnitOfWork unitOfWork)
        {
            this.paymentRepository = paymentRepository;
            this.inventoryRepository = inventoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public Payment GetPaymentByInventoryId(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var inventory = inventoryRepository.FindAll(i => i.Id == id).FirstOrDefault();

            return inventory.IfNotNull(i => i.Payment);
        }

        public Payment GetPaymentByOrderId(int id)
        {
            return id <= 0 ? null : paymentRepository.FindAll(i => i.OrderId == id).FirstOrDefault();
        }

        public Payment GetPaymentById(int id)
        {
            return id <= 0 ? null : paymentRepository.FindAll(i => i.Id == id).FirstOrDefault();
        }

        public void UpdatePayment(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException("payment");
            }

            paymentRepository.Update(payment);
            unitOfWork.Commit();
        }
    }
}
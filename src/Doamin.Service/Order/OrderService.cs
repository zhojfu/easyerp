namespace Doamin.Service.Order
{
    using Domain.Model.Orders;
    using EasyErp.Core;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;

        private readonly IUnitOfWork unitOfWork;
        private readonly IWorkContext workContext;

        public OrderService(IRepository<Order> orderRepository,
            IUnitOfWork unitOfWork,
            IWorkContext workContext
            )
        {
            this.orderRepository = orderRepository;
            this.unitOfWork = unitOfWork;
            this.workContext = workContext;
        }

        public IPagedList<Order> SearchOrders(
            int customerId = 0,
            int productId = 0,
            OrderStatus? os = null,
            PaymentStatus? ps = null,
            int pageIndex = 0,
            int pageSize = Int32.MaxValue)
        {
            int? orderStatusId = null;
            if (os.HasValue)
            {
                orderStatusId = (int)os.Value;
            }

            int? paymentStatusId = null;
            if (ps.HasValue)
            {
                paymentStatusId = (int)ps.Value;
            }
            var orders = orderRepository.FindAll(x => x.Id > 0);
            if (customerId > 0)
            {
                orders = orders.Where(o => o.CustomerId == customerId);
            }
            if (productId > 0)
            {
                orders = orders.Where(o => o.OrderItems.Any(item => item.Product.Id == productId));
            }

            if (orderStatusId.HasValue)
            {
                orders = orders.Where(o => o.OrderStatusId == orderStatusId.Value);
            }
            if (paymentStatusId.HasValue)
            {
                orders = orders.Where(o => o.PaymentStatusId == paymentStatusId.Value);
            }

            orders = orders.Where(o => !o.Deleted);
            orders = orders.OrderByDescending(o => o.CreatedOnUtc);

            //database layer paging
            return new PagedList<Order>(orders, pageIndex, pageSize);
        }

        public Order GetOrderById(int orderId)
        {
            return orderId == 0 ? null : orderRepository.GetByKey(orderId);
        }

        public Order GetOrderByGuid(Guid guid)
        {
            return guid == Guid.Empty ? null : orderRepository.FindAll(o => o.OrderGuid == guid).FirstOrDefault();
        }

        public virtual IList<Order> GetOrdersByIds(int[] orderIds)
        {
            if (orderIds == null ||
                orderIds.Length == 0)
            {
                return new List<Order>();
            }

            var query = orderRepository.FindAll(o => orderIds.Contains(o.Id));
            var orders = query.ToList();

            //sort by passed identifiers
            return orderIds.Select(id => orders.Find(x => x.Id == id)).Where(order => order != null).ToList();
        }

        public void InsertOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            orderRepository.Add(order);
            unitOfWork.Commit();
        }

        public void CreateOrderByOrderItems(IEnumerable<OrderItem> orderItems)
        {
            if (orderItems == null)
            {
                throw new ArgumentNullException("orderItems");
            }

            var order = new Order()
            {
                OrderGuid = Guid.NewGuid(),
                OrderStatus = OrderStatus.Pending,
                CreatedOnUtc = DateTime.Now,
                CustomerId = workContext.CurrentUser.StoreId,
                PaymentStatus = PaymentStatus.Pending
            };
            orderRepository.Add(order);
        }
    }
}
namespace Doamin.Service.Order
{
    using Domain.Model.Orders;
    using EasyErp.Core;
    using System;
    using System.Collections.Generic;

    public interface IOrderService
    {
        IPagedList<Order> SearchOrders(
            int storeId = 0,
            int productId = 0,
            OrderStatus? os = null,
            PaymentStatus? ps = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue);

        Order GetOrderById(int id);

        Order GetOrderByGuid(Guid guid);

        IList<Order> GetOrdersByIds(int[] orderIds);

        void InsertOrder(Order order);

        void CreateOrderByOrderItems(IEnumerable<OrderItem> orderItems);
    }
}
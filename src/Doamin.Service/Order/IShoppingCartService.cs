namespace Doamin.Service.Order
{
    using System.Collections.Generic;
    using Domain.Model.Orders;
    using Domain.Model.Products;
    using Domain.Model.Users;

    public interface IShoppingCartService
    {
        ShoppingCartItem FindShoppingCartItemInTheCart(
            IList<ShoppingCartItem> shoppingCart,
            Product product
            );

        void AddToCart(User users, Product product, int quantity);
    }
}
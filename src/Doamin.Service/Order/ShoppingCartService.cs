namespace Doamin.Service.Order
{
    using Doamin.Service.Users;
    using Domain.Model.Orders;
    using Domain.Model.Products;
    using Domain.Model.Users;
    using EasyErp.Core;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCartItem> cartRepository;

        private readonly IUserService userService;

        private readonly IWorkContext workContext;

        public ShoppingCartService(
            IWorkContext workContext,
            IRepository<ShoppingCartItem> cartRepository,
            IUserService userService)
        {
            this.workContext = workContext;
            this.cartRepository = cartRepository;
            this.userService = userService;
        }

        public ShoppingCartItem FindShoppingCartItemInTheCart(IList<ShoppingCartItem> shoppingCart, Product product)
        {
            if (shoppingCart == null)
            {
                throw new ArgumentNullException("shoppingCart");
            }

            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            return shoppingCart.FirstOrDefault(s => s.ProductId == product.Id);
        }

        public void AddToCart(User user, Product product, int quantity)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            var shoppingCartItem = FindShoppingCartItemInTheCart(
                workContext.CurrentUser.ShoppingCartItems.ToList(),
                product);

            if (shoppingCartItem != null)
            {
                var newQuantity = shoppingCartItem.Quantity + quantity;

                shoppingCartItem.Quantity = newQuantity;
                shoppingCartItem.UpdatedOnUtc = DateTime.UtcNow;
                userService.UpdateUser(user);
            }
            else
            {
                var now = DateTime.UtcNow;
                shoppingCartItem = new ShoppingCartItem
                {
                    Product = product,
                    Quantity = quantity,
                    CreatedOnUtc = now,
                    UpdatedOnUtc = now
                };
                user.ShoppingCartItems.Add(shoppingCartItem);
                userService.UpdateUser(user);
            }
        }
    }
}
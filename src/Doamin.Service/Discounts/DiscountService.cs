namespace Doamin.Service.Discounts
{
    using Doamin.Service.Events;
    using Domain.Model.Discounts;
    using Domain.Model.Users;
    using EasyErp.Core;
    using EasyErp.Core.Caching;
    using Infrastructure.Domain;
    using Nop.Core.Domain.Discounts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Discount service
    /// </summary>
    public partial class DiscountService : IDiscountService
    {
        public void DeleteDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }

        public Discount GetDiscountById(int discountId)
        {
            throw new NotImplementedException();
        }

        public IList<Discount> GetAllDiscounts(DiscountType? discountType, string couponCode = "", bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public void InsertDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }

        public void UpdateDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }

        public void DeleteDiscountRequirement(DiscountRequirement discountRequirement)
        {
            throw new NotImplementedException();
        }

        public Discount GetDiscountByCouponCode(string couponCode, bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public bool IsDiscountValid(Discount discount, User user)
        {
            throw new NotImplementedException();
        }

        public bool IsDiscountValid(Discount discount, User user, string couponCodeToValidate)
        {
            throw new NotImplementedException();
        }

        public DiscountUsageHistory GetDiscountUsageHistoryById(int discountUsageHistoryId)
        {
            throw new NotImplementedException();
        }

        public IPagedList<DiscountUsageHistory> GetAllDiscountUsageHistory(int? discountId, int? customerId, int? orderId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void InsertDiscountUsageHistory(DiscountUsageHistory discountUsageHistory)
        {
            throw new NotImplementedException();
        }

        public void UpdateDiscountUsageHistory(DiscountUsageHistory discountUsageHistory)
        {
            throw new NotImplementedException();
        }

        public void DeleteDiscountUsageHistory(DiscountUsageHistory discountUsageHistory)
        {
            throw new NotImplementedException();
        }
    }
}
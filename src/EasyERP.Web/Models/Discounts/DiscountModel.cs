namespace EasyERP.Web.Models.Discounts
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using EasyERP.Web.Validators.Discounts;
    using FluentValidation.Attributes;
    using Infrastructure.Domain.Model;

    [Validator(typeof(DiscountValidator))]
    public class DiscountModel : BaseEntity
    {
        public DiscountModel()
        {
            AppliedToCategoryModels = new List<AppliedToCategoryModel>();
            AppliedToProductModels = new List<AppliedToProductModel>();
            AvailableDiscountRequirementRules = new List<SelectListItem>();
            DiscountRequirementMetaInfos = new List<DiscountRequirementMetaInfo>();
        }

        [AllowHtml]
        public string Name { get; set; }

        public int DiscountTypeId { get; set; }
        public bool UsePercentage { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal? MaximumDiscountAmount { get; set; }
        public string PrimaryStoreCurrencyCode { get; set; }
        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public bool RequiresCouponCode { get; set; }

        [AllowHtml]
        public string CouponCode { get; set; }

        public int DiscountLimitationId { get; set; }
        public int LimitationTimes { get; set; }
        public int? MaximumDiscountedQuantity { get; set; }
        public IList<AppliedToCategoryModel> AppliedToCategoryModels { get; set; }
        public IList<AppliedToProductModel> AppliedToProductModels { get; set; }
        public string AddDiscountRequirement { get; set; }
        public IList<SelectListItem> AvailableDiscountRequirementRules { get; set; }
        public IList<DiscountRequirementMetaInfo> DiscountRequirementMetaInfos { get; set; }

        #region Nested classes

        public class DiscountRequirementMetaInfo : BaseEntity
        {
            public int DiscountRequirementId { get; set; }
            public string RuleName { get; set; }
            public string ConfigurationUrl { get; set; }
        }

        public class DiscountUsageHistoryModel : BaseEntity
        {
            public int DiscountId { get; set; }
            public int OrderId { get; set; }
            public DateTime CreatedOn { get; set; }
        }

        public class AppliedToCategoryModel : BaseEntity
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }
        }

        public class AppliedToProductModel : BaseEntity
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
        }

        #endregion Nested classes
    }
}
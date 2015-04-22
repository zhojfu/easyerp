namespace Domain.Model.Discounts
{
    using Infrastructure.Domain.Model;

    /// <summary>
    /// Represents a discount requirement
    /// </summary>
    public class DiscountRequirement : BaseEntity
    {
        /// <summary>
        /// Gets or sets the discount identifier
        /// </summary>
        public int DiscountId { get; set; }

        /// <summary>
        /// Gets or sets the discount requirement rule system name
        /// </summary>
        public string DiscountRequirementRuleSystemName { get; set; }

        /// <summary>
        /// Gets or sets the discount
        /// </summary>
        public virtual Discount Discount { get; set; }
    }
}
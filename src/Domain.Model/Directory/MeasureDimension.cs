namespace Domain.Model.Directory
{
    using Infrastructure.Domain.Model;

    /// <summary>
    /// Represents a measure dimension
    /// </summary>
    public class MeasureDimension : BaseEntity
    {
        /// <summary>
        /// Gets or sets the system keyword
        /// </summary>
        public string SystemKeyword { get; set; }

        /// <summary>
        /// Gets or sets the ratio
        /// </summary>
        public decimal Ratio { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
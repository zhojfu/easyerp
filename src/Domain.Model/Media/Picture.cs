namespace Domain.Model.Media
{
    using global::Domain.Model.Products;
    using global::Infrastructure.Domain.Model;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a picture
    /// </summary>
    public partial class Picture : BaseEntity
    {
        private ICollection<ProductPicture> _productPictures;

        /// <summary>
        /// Gets or sets the picture binary
        /// </summary>
        public byte[] PictureBinary { get; set; }

        /// <summary>
        /// Gets or sets the picture mime type
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the SEO friednly filename of the picture
        /// </summary>
        public string SeoFilename { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the picture is new
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Gets or sets the product pictures
        /// </summary>
        public virtual ICollection<ProductPicture> ProductPictures
        {
            get { return this._productPictures ?? (this._productPictures = new List<ProductPicture>()); }
            protected set { this._productPictures = value; }
        }
    }
}
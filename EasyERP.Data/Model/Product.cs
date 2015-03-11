namespace EasyERP.Data.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductInfo")]
    public class Product
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(32)]
        public string ProductId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Timestamp]
        [Required]
        public byte[] Created { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        public string CreateBy { get; set; }

        [Required]
        public byte[] Updated { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        public string Updatedy { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(60)]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required]
        public string Sku { get; set; }

        [Required]
        public bool IsStocked { get; set; }

        [Required]
        public bool IsPurchased { get; set; }

        [Required]
        public bool IsSold { get; set; }

        public decimal Weight { get; set; }

        public decimal Volume { get; set; }

        [Required]
        [StringLength(60)]
        public string ProductType { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(120)]
        public string ImageUrl { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(120)]
        public string DescriptionUrl { get; set; }

        [DataType("decimal(10 ,0")]
        public decimal GuaranteDays { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(20)]
        public string VersionNo { get; set; }

        [DataType("decimal(10 ,0")]
        public decimal StockMin { get; set; }

        [Required]
        public bool IsVerified { get; set; }

        #region Foreign Keys

        public string ProductCategoryId { get; set; }

        [ForeignKey("ProductCategoryId")]
        public ProductCategory ProductCategory { get; set; }

        public string LocatorId { get; set; }

        [ForeignKey("LocatorId")]
        public Locator Locator { get; set; }

        public string ExpenseTypeId { get; set; }

        public ExpenseType ExpenseType { get; set; }

        public string BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        public string ImageId { get; set; }

        [ForeignKey("ImageId")]
        public Image Image { get; set; }

        #endregion Foreign Keys
    }
}
namespace EasyERP.Domain.Model
{
    public class Product
    {
        public string ProductId { get; set; }

        public bool IsActive { get; set; }

        public byte[] Created { get; set; }

        public string CreateBy { get; set; }

        public byte[] Updated { get; set; }

        public string Updatedy { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Sku { get; set; }

        public bool IsStocked { get; set; }

        public bool IsPurchased { get; set; }

        public bool IsSold { get; set; }

        public decimal Weight { get; set; }

        public decimal Volume { get; set; }

        public string ProductType { get; set; }

        public string ImageUrl { get; set; }

        public string DescriptionUrl { get; set; }

        public decimal GuaranteDays { get; set; }

        public string VersionNo { get; set; }

        public decimal StockMin { get; set; }

        public bool IsVerified { get; set; }

        #region Foreign Keys

       /* public string ProductCategoryId { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public string TaxCategoryId { get; set; }

        public TaxCategory TaxCategory { get; set; }

        public int LocatorId { get; set; }

        public Locator Locator { get; set; }

        public string ExpenseTypeId { get; set; }

        public ExpenseType ExpenseType { get; set; }

        public string BrandId { get; set; }

        public Brand Brand { get; set; }

        public string ImageId { get; set; }

        public Image Image { get; set; }*/

        #endregion Foreign Keys
    }
}
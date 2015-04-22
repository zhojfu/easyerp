namespace EasyErp.StoreAdmin.Models.Products
{
    using System.Web.Mvc;
    using Infrastructure.Domain.Model;

    public class ProductModel : BaseEntity
    {
        public override int Id { get; set; }

        [AllowHtml]
        public string Name { get; set; }

        [AllowHtml]
        public string ShortDescription { get; set; }

        [AllowHtml]
        public string FullDescription { get; set; }

        [AllowHtml]
        public virtual string Gtin { get; set; }

        public decimal Price { get; set; }
    }
}
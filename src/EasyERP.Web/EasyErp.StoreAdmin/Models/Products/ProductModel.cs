namespace EasyErp.StoreAdmin.Models.Products
{
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class ProductModel : BaseEntity
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
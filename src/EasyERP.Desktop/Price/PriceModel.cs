namespace EasyERP.Desktop.Price
{
    using System;

    public class PriceModel
    {
        public double Cost { get; set; }

        public double SalePrice { get; set; } // define by company

        public double Discount { get; set; }

        public DateTime UpdataTime { get; set; }

        //public Product Product { get; set; }

        public Guid StoreId { get; set; }

        public Guid ProductId { get; set; }
    }
}
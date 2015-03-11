namespace EasyERP
{
    using EasyERP.Data;
    using EasyERP.Data.Model;
    using System;
    using System.Windows;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new EasyErpContext())
            {
                var brand = new Brand
                {
                    BrandId = "1",
                    Name = "test",
                    CreateBy = "pancake",
                    Created = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Updated = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Description = "description",
                    IsActive = true,
                    Updatedy = "pancake"
                };
                var currency = new Currency
                {
                    CurrencyId = "1",
                    CurrencySymbol = "￥",
                    Name = "RMB",
                    CreateBy = "pancake",
                    Created = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Updated = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Description = "RMB",
                    IsActive = true,
                    IsoCode = "zh",
                    PricePrecision = 2,
                    CostingPrecision = 2,
                    StdPrecision = 2,
                    Updatedy = "pancake"
                };

                var image = new Image
                {
                    ImageId = "1",
                    Name = "test",
                    CreateBy = "pancake",
                    Created = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Updated = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Description = "description",
                    IsActive = true,
                    Updatedy = "pancake"
                };

                var invoice = new Invoice
                {
                    InvoiceId = "1",
                    Name = "test",
                    CreateBy = "pancake",
                    Created = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Updated = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Description = "description",
                    IsActive = true,
                    Updatedy = "pancake",
                    Processed = false,
                    Processing = false,
                    Order = new Order
                    {
                        OrderId = "1"
                    },
                    OrderId = "1"
                };

                var locator = new Locator
                {
                    LocatorId = "1",
                    Name = "test",
                    CreateBy = "pancake",
                    Created = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Updated = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Description = "description",
                    IsActive = true,
                    Updatedy = "pancake",
                    WareHouseId = "1",
                    WareHouse = new WareHouse
                    {
                        WarHouseId = "1",
                        Name = "test",
                        CreateBy = "pancake",
                        Created = BitConverter.GetBytes(DateTime.Now.Ticks),
                        Updated = BitConverter.GetBytes(DateTime.Now.Ticks),
                        Description = "description",
                        IsActive = true,
                        Updatedy = "pancake",
                        IsAllocated = false,
                        IsShipper = false
                    }
                };

                var orgnize = new Organize
                {
                    OrgnizeId = "1",
                    Name = "test",
                    CreateBy = "pancake",
                    Created = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Updated = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Description = "description",
                    IsActive = true,
                    Updatedy = "pancake",
                    OrgnizeTypeId = "1",
                    OrgnizeType = new OrganizeType
                    {
                        OrgnizeTypeId = "1",
                        Name = "test",
                        CreateBy = "pancake",
                        Created = BitConverter.GetBytes(DateTime.Now.Ticks),
                        Updated = BitConverter.GetBytes(DateTime.Now.Ticks),
                        Description = "description",
                        IsActive = true,
                        Updatedy = "pancake",
                        IsBusnessUnit = true
                    },
                    Currency = currency,
                    CurrencyId = "1"
                };

                var product = new Product
                {
                    ProductId = "1",
                    Name = "test",
                    CreateBy = "pancake",
                    Created = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Updated = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Description = "description",
                    IsActive = true,
                    Updatedy = "pancake",
                    Sku = "69***********",
                    IsSold = false,
                    IsPurchased = false,
                    IsStocked = false,
                    IsVerified = false,
                    GuaranteDays = 180,
                    Brand = brand,
                    BrandId = "1",
                    ExpenseType = new ExpenseType
                    {
                        ExpenseTypeId = "1",
                        Name = "test",
                        CreateBy = "pancake",
                        Created = BitConverter.GetBytes(DateTime.Now.Ticks),
                        Updated = BitConverter.GetBytes(DateTime.Now.Ticks),
                        Description = "description",
                        IsActive = true,
                        Updatedy = "pancake"
                    },
                    ExpenseTypeId = "1"
                };

                var user = new User
                {
                    UserId = "11",
                    Name = "test",
                    CreateBy = "pancake",
                    Created = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Updated = BitConverter.GetBytes(DateTime.Now.Ticks),
                    Description = "description",
                    IsActive = true,
                    Updatedy = "pancake",
                    Password = "1234***",
                    IsStocked = false
                };

                db.Users.Add(user);

                db.SaveChanges();
            }
        }
    }
}
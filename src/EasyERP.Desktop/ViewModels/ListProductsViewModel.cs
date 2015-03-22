namespace EasyERP.Desktop.ViewModels
{
    using System;
    using Caliburn.Micro;
    using Doamin.Service;
    using Domain.Model;
    using EasyERP.Desktop.Contacts;
    using EasyERP.Desktop.Extensions;
    using NullGuard;
    using PropertyChanged;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Dynamic;
    using System.Windows;

    [ImplementPropertyChanged]
    public class ListProductsViewModel : Screen, IViewModel
    {
        private readonly ProductService productService;

        public ListProductsViewModel(ProductService productService)
        {
            this.productService = productService;
        }

        public override string DisplayName
        {
            get { return "Product Management"; }
            set { }
        }

        [AllowNull]
        public string SearchProductName { get; set; }

        public int SearchCategoryId { get; set; }

        [AllowNull]
        public IList<string> CategoryList { get; set; }

        public bool SearchIncludeSubCategories { get; set; }

        [AllowNull]
        public IList<string> ManufactureList { get; set; }

        public int SearchManufacturerId { get; set; }

        [AllowNull]
        public IList<string> StoreList { get; set; }

        public int SearchStoreId { get; set; }

        [AllowNull]
        public IList<string> VendorList { get; set; }

        public int SearchVendorId { get; set; }

        [AllowNull]
        public IList<string> WarehouseList { get; set; }

        public int SearchWarehouseId { get; set; }

        [AllowNull]
        public IList<string> ProductTypeList { get; set; }

        public int SearchProductTypeId { get; set; }

        public int SearchPublishedId { get; set; }

        [AllowNull]
        public string GoDirectlyToSku { get; set; }

        public ObservableCollection<Product> Products
        {
            get
            {
                var a = new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "cake description",
                    Unit = "t",
                    Name = "cake",
                    Upc = "690193901",
                };
                //this.productService.AddNewProduct(a);
                //var t = new TestDoubles
                //{
                //    Id = Guid.NewGuid(),
                //    Name = "test"
                //};
                //this.productService.AddTestDouble(t);

                // var ids = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                //return null;
                return new ObservableCollection<Product>(this.productService.GetAllProducts());
            }
            set { }
        }

        public string Tag
        {
            get { return "ProductManagement"; }
        }

        public void AddProduct()
        {
            var edit = new EditProductViewModel
            {
                Product = new ProductViewModel()
            };

            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.AllowsTransparency = false;

            var result = IoC.Get<IWindowManager>().ShowDialog(edit, null, settings);

            if (result)
            {
                // this.productService.AddNewProduct(edit.Product.ToEntity());
            }
        }

        public void Delete()
        {
            var edit = new EditProductViewModel
            {
                Product = new ProductViewModel()
            };

            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.AllowsTransparency = false;

            IoC.Get<IWindowManager>().ShowPopup(edit, null, settings);
        }

        public void Import()
        {
            var edit = new EditProductViewModel
            {
                Product = new ProductViewModel()
            };

            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.AllowsTransparency = false;

            IoC.Get<IWindowManager>().ShowWindow(edit, null, settings);
        }

        public void Export()
        {
        }

        public void GoToSku()
        {
            //var product = this.productService.GetProductBySku(this.GoDirectlyToSku);
        }
    }
}
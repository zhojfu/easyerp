namespace EasyERP.Desktop.ViewModels
{
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
    using System.Linq;
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

        public ObservableCollection<ProductViewModel> Products
        {
            get
            {
                var ids = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                return new ObservableCollection<ProductViewModel>(this.productService.GetProductsByIds(ids).Select(p => p.ToEntity()));
            }
            set { }
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
                this.productService.AddNewProduct(edit.Product.ToEntity());
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
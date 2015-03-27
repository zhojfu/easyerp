namespace EasyERP.Desktop.Stock
{
    using Caliburn.Micro;
    using Doamin.Service;
    using EasyERP.Desktop.Contacts;
    using EasyERP.Desktop.Extensions;
    using PropertyChanged;
    using System.Collections.ObjectModel;
    using System.Linq;

    [ImplementPropertyChanged]
    public class StockManagerViewModel : Screen, IViewModel
    {
        private readonly ProductService productService;

        private readonly StockService stockService;

        public StockManagerViewModel(StockService stockService, ProductService productService)
        {
            this.stockService = stockService;
            this.productService = productService;
        }

        public override string DisplayName
        {
            get { return "Stock Manager"; }
            set { base.DisplayName = value; }
        }

        public ObservableCollection<StockItemViewModel> StockList
        {
            get
            {
                return
                    new ObservableCollection<StockItemViewModel>(
                        this.stockService.GetAllStockList().Select(i => i.ToModel()));
            }
        }

        public string Tag
        {
            get { return "Stock Manager"; }
        }

        public void Add()
        {
            var products = this.productService.GetAllProducts();
            var productsName = products.Select(p => p.Name).ToList();

            var vm = new EditStockItemViewModel
            {
                StockItem = new StockItemViewModel(),
                Products = productsName
            };
            var result = IoC.Get<IWindowManager>().ShowDialog(vm);
            if (result.GetValueOrDefault(false))
            {
                var entity = vm.StockItem.ToEntity();
                entity.ProductId = products.First(p => p.Name == vm.StockItem.ProductName).Id;
                this.stockService.AddStock(entity);
            }
        }
    }
}
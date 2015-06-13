using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using WebGrease.Css.Extensions;

namespace EasyERP.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Doamin.Service.ExportImport;
    using Doamin.Service.Helpers;
    using Doamin.Service.Products;
    using Doamin.Service.Security;
    using Doamin.Service.Stores;
    using Domain.Model.Payments;
    using Domain.Model.Products;
    using EasyERP.Web.Extensions;
    using EasyERP.Web.Framework.Kendoui;
    using EasyERP.Web.Framework.Mvc;
    using EasyERP.Web.Models.Products;
    using Infrastructure;

    public class ProductController : BaseAdminController
    {
        private readonly ICategoryService categoryService;

        private readonly IDateTimeHelper dateTimeHelper;

        private readonly IExportManager exportManager;

        private readonly IInventoryService inventoryService;

        private readonly IPermissionService permissionService;

        private readonly IProductPriceService productPriceService;

        private readonly IProductService productService;

        private readonly IStoreService storeService;

        public ProductController(
            IPermissionService permissionService,
            ICategoryService categoryService,
            IProductService productService,
            IStoreService storeService,
            IProductPriceService productPriceService,
            IInventoryService inventoryService,
            IDateTimeHelper dateTimeHelper,
            IExportManager exportManager)
        {
            this.permissionService = permissionService;
            this.categoryService = categoryService;
            this.productService = productService;
            this.storeService = storeService;
            this.productPriceService = productPriceService;
            this.inventoryService = inventoryService;
            this.dateTimeHelper = dateTimeHelper;
            this.exportManager = exportManager;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult ProductList(DataSourceRequest command, ProductListModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.GetProductList))
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var categoryIds = new List<int>
                {
                    model.SearchCategoryId
                };

                var storeIds = new List<int>
                {
                    0
                };

                var products = productService.SearchProducts(
                    categoryIds: categoryIds,
                    storeIds: storeIds,
                    keywords: model.SearchProductName,
                    pageIndex: command.Page - 1,
                    pageSize: command.PageSize
                    );
                var gridModel = new DataSourceResult
                {
                    Data = products.Select(
                        x =>
                        {
                            var productModel = x.ToModel();
                            var price = productPriceService.GetProductPrice(model.SearchStoreId, x.Id);
                            productModel.Price = price.SalePrice;
                            productModel.ProductCost = price.CostPrice;
                            productModel.FullDescription = "";

                            return productModel;
                        }),
                    Total = products.TotalCount
                };

                return Json(gridModel);
            }
            return Json(false);
        }

        public ActionResult List()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.GetProductList))
            {
                return AccessDeniedView();
            }
            var model = new ProductListModel();

            //categories
            model.AvailableCategories.Add(
                new SelectListItem
                {
                    Text = "所有目录",
                    Value = "0"
                });

            var categories = categoryService.GetAllCategories();
            foreach (var c in categories)
            {
                model.AvailableCategories.Add(
                    new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    });
            }

            //stores
            //model.AvailableStores.Add(
            //    new SelectListItem
            //    {
            //        Text = "所有店面",
            //        Value = "0"
            //    });
            var stores = storeService.GetAllStores();
            foreach (var store in stores)
            {
                model.AvailableStores.Add(
                    new SelectListItem
                    {
                        Text = store.Name,
                        Value = store.Id.ToString()
                    });
            }

            model.AvailablePublishedOptions.Add(
                new SelectListItem
                {
                    Text = "所有",
                    Value = "0"
                });
            model.AvailablePublishedOptions.Add(
                new SelectListItem
                {
                    Text = "已发布",
                    Value = "1"
                });
            model.AvailablePublishedOptions.Add(
                new SelectListItem
                {
                    Text = "未发布",
                    Value = "2"
                });

            return View(model);
        }

        public ActionResult Inventory()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.InventoryProduct))
            {
                return AccessDeniedView();
            }
            var model = new ProductListModel();

            //categories
            model.AvailableCategories.Add(
                new SelectListItem
                {
                    Text = "所有目录",
                    Value = "0"
                });

            var categories = categoryService.GetAllCategories();
            foreach (var c in categories)
            {
                model.AvailableCategories.Add(
                    new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    });
            }

            //stores
            //model.AvailableStores.Add(
            //    new SelectListItem
            //    {
            //        Text = "所有店面",
            //        Value = "0"
            //    });
            var stores = storeService.GetAllStores();
            foreach (var store in stores)
            {
                model.AvailableStores.Add(
                    new SelectListItem
                    {
                        Text = store.Name,
                        Value = store.Id.ToString()
                    });
            }

            model.AvailablePublishedOptions.Add(
                new SelectListItem
                {
                    Text = "所有",
                    Value = "0"
                });
            model.AvailablePublishedOptions.Add(
                new SelectListItem
                {
                    Text = "已发布",
                    Value = "1"
                });
            model.AvailablePublishedOptions.Add(
                new SelectListItem
                {
                    Text = "未发布",
                    Value = "2"
                });

            return View(model);
        }
        
        [HttpPost]
        public ActionResult StockInfo(DataSourceRequest command, ProductListModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.InventoryProduct))
            {
                return AccessDeniedView();
            }
            if (ModelState.IsValid)
            {
                var psm = inventoryService.GetProductInventories(model.SearchProductName,
                    model.SearchCategoryId, model.SearchStoreId);


                var gridModel = new DataSourceResult
                {
                    Data = psm.Select(
                        x => new
                        {
                            ProductName = x.Product.Name,
                            StoreName = x.Store.Name,
                            Quantity = x.Quantity
                        }),
                    Total = psm.Count
                };

                return Json(gridModel);
            }
            return Json(false);
        }

        public ActionResult Warehousing()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.InventoryProduct))
            {
                return AccessDeniedView();
            }

            var model = new InventoryModel();

            var allCategories = categoryService.GetAllCategories();
            foreach (var category in allCategories)
            {
                model.AvailableCategories.Add(
                    new SelectListItem
                    {
                        Text = category.Name,
                        Value = category.Id.ToString()
                    });
            }

            var products = productService.GetAllProducts();

            if (products == null ||
                !products.Any())
            {
                return RedirectToAction("Create");
            }

            products.ToList().ForEach(
                p =>
                {
                    model.AvailableProducts.Add(
                        new SelectListItem
                        {
                            Text = p.Name,
                            Value = p.Id.ToString()
                        });
                });
            
            var stores = storeService.GetAllStores();
            foreach (var store in stores)
            {
                model.AvailableStores.Add(
                    new SelectListItem
                    {
                        Text = store.Name,
                        Value = store.Id.ToString()
                    });
            }

            model.Paid = 0;
            model.DueDateTime = DateTime.Now + TimeSpan.FromDays(30);

            return View(model);
        }

        [HttpPost]
        public ActionResult Warehousing(InventoryModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.InventoryProduct))
            {
                return AccessDeniedView();
            }
            if (ModelState.IsValid)
            {
                var inventory = new Inventory
                {
                    StoreId = model.StoreId,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity, 
                    Notes = model.Note
                };
                inventory.InStockTime = DateTime.Now;
                var payment = new Payment
                {
                    DueDateTime = model.DueDateTime,
                    TotalAmount = model.TotalAmount
                };

                if (model.Paid > 0)
                {
                    payment.Items.Add(
                        new PayItem
                        {
                            Paid = model.Paid,
                            PayDataTime = DateTime.Now
                        });
                }
                inventory.Payment = payment;

                inventoryService.InsertInventory(inventory);
            }

            return RedirectToAction("Inventory");
        }

        public ActionResult InventoryRecords()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.InventoryProduct))
            {
                return AccessDeniedView();
            }
            var model = new ProductListModel();

            //categories
            model.AvailableCategories.Add(
                new SelectListItem
                {
                    Text = "所有目录",
                    Value = "0"
                });

            var categories = categoryService.GetAllCategories();
            foreach (var c in categories)
            {
                model.AvailableCategories.Add(
                    new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    });
            }

            //stores
            //model.AvailableStores.Add(
            //    new SelectListItem
            //    {
            //        Text = "所有店面",
            //        Value = "0"
            //    });
            var stores = storeService.GetAllStores();
            foreach (var store in stores)
            {
                model.AvailableStores.Add(
                    new SelectListItem
                    {
                        Text = store.Name,
                        Value = store.Id.ToString()
                    });
            }

            model.AvailablePublishedOptions.Add(
                new SelectListItem
                {
                    Text = "所有",
                    Value = "0"
                });
            model.AvailablePublishedOptions.Add(
                new SelectListItem
                {
                    Text = "已发布",
                    Value = "1"
                });
            model.AvailablePublishedOptions.Add(
                new SelectListItem
                {
                    Text = "未发布",
                    Value = "2"
                });

            return View(model);
        }

        [HttpPost]
        public ActionResult InventoryRecords(DataSourceRequest command, ProductListModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.InventoryProduct))
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var inventories = inventoryService.GetProductInventoryRecords(model.SearchProductName, model.SearchCategoryId,
                    model.SearchStoreId);

            if (inventories == null ||
                !inventories.Any())
            {
                return Json(new DataSourceResult());
            }

            var inventoryDataSource = inventories.Select(
                i => new
                {
                    i.Id,
                    i.PaymentId,
                    ProductName = i.Product.Name,
                    i.Quantity,
                    InventoryTime = i.InStockTime,
                    Payment = i.Payment.IfNotNull(pp=>
                    {
                        return new
                        {
                            Total = pp.TotalAmount,
                            DueDate = pp.DueDateTime,
                            Items = pp.Items.Select(
                                p => new
                                {
                                    PayDate = p.PayDataTime,
                                    p.Paid
                                })
                        };
                    }),
                    Note = i.Notes,
                    UnPaid = i.Payment.IfNotNull(pp =>pp.TotalAmount - pp.Items.Sum(iii => iii.Paid)),
                    IsPaid = i.Payment.IfNotNull(
                        pp => { return (pp.TotalAmount - pp.Items.Sum(iii => iii.Paid)).Equals(0);})
                }).ToList();

            //if (showUnPaidOnly)
            //{
            //    inventoryDataSource = inventoryDataSource.Where(i => !i.IsPaid).ToList();
            //}
            var gridModel = new DataSourceResult
            {
                Data = inventoryDataSource,
                Total = inventoryDataSource.Count
            };

            return Json(gridModel);
            }

            return Json(false);
        }

        [HttpPost]
        public ActionResult Categories()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.GetCategoryList))
            {
                return AccessDeniedView();
            }

            var categories = categoryService.GetAllCategories();

            if (categories == null ||
                !categories.Any())
            {
                return new JsonResult();
            }

            var data = categories.Select(
                i => new
                {
                    i.Id,
                    i.Name
                });

            return Json(data);
        }

        //create product
        public ActionResult Create()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.CreateProduct))
            {
                return AccessDeniedView();
            }

            var model = new ProductModel();
            PrepareProductModel(model, null, true, true);
            PrepareAclModel(model, null, false);
            PrepareStoresMappingModel(model, null, false);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(ProductModel model, bool continueEditing)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.CreateProduct))
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                //product
                var product = model.ToEntity();
                product.CreatedOnUtc = DateTime.UtcNow;
                product.UpdatedOnUtc = DateTime.UtcNow;
                var category = categoryService.GetCategoryById(product.CategoryId);
                product.ItemNo = category.ItemNo + product.ItemNo;

                productService.InsertProduct(product);

                return continueEditing
                           ? RedirectToAction(
                               "Edit",
                               new
                               {
                                   id = product.Id
                               })
                           : RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form
            PrepareProductModel(model, null, false, true);
            PrepareAclModel(model, null, true);
            PrepareStoresMappingModel(model, null, true);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.UpdateProduct))
            {
                return AccessDeniedView();
            }

            var product = productService.GetProductById(id);

            //No product found with the specified id
            if (product == null ||
                product.Deleted)
            {
                return RedirectToAction("List");
            }

            var model = product.ToModel();
            PrepareProductModel(model, product, false, false);
            PrepareAclModel(model, product, false);
            PrepareStoresMappingModel(model, product, false);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(ProductModel model, bool continueEditing)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.UpdateProduct))
            {
                return AccessDeniedView();
            }

            var product = productService.GetProductById(model.Id);
            if (product == null ||
                product.Deleted)
            {
                //No product found with the specified id
                return RedirectToAction("List");
            }

            if (ModelState.IsValid)
            {
                //product
                product = model.ToEntity(product);
                var category = categoryService.GetCategoryById(product.CategoryId);
                product.ItemNo = category.ItemNo + product.ItemNo;
                productService.UpdateProduct(product);

                if (continueEditing)
                {
                    return RedirectToAction(
                        "Edit",
                        new
                        {
                            id = product.Id
                        });
                }
                return RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form
            PrepareProductModel(model, product, false, true);
            PrepareAclModel(model, product, true);
            PrepareStoresMappingModel(model, product, true);
            return View(model);
        }

        public ActionResult Detail(int productId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Destroy(DataSourceRequest request, int id)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.DeleteProduct))
            {
                return AccessDeniedView();
            }

            if (id < 1)
            {
                return Json( new { Result = false });
            }

            var product = productService.GetProductById(id);
            product.DoIfNotNull(p => productService.DeleteProduct(p));
            return Json(new { Result = true });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.DeleteProduct))
            {
                return AccessDeniedView();
            }

            var product = productService.GetProductById(id);
            if (product == null)
            {
                //No product found with the specified id
                return RedirectToAction("List");
            }

            productService.DeleteProduct(product);

            return RedirectToAction("List");
        }

        public ActionResult Price()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PriceList(DataSourceRequest command, int storeId, string storeName)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.SetProductPrice))
            {
                return AccessDeniedView();
            }
            var products = productService.SearchProducts();

            if (products == null)
            {
                return new JsonResult();
            }

            var gridModel = new DataSourceResult
            {
                Data = products.Select(
                    x =>
                    {
                        var price = GetPrice(storeId, x.Id);
                        var priceModel = new PriceModel
                        {
                            StoreName = storeName,
                            ProductId = x.Id,
                            ProductName = x.Name,
                            CostPrice = price != null ? price.CostPrice : x.ProductCost,
                            SalePrice = price != null ? price.SalePrice : x.Price,
                            StoreId = storeId
                        };

                        return priceModel;
                    }),
                Total = products.Count
            };

            return Json(gridModel);
        }

        private ProductPrice GetPrice(int storeId, int productId)
        {
            var prices = productPriceService.GetProductPriceList(storeId, productId).ToList();

            return prices == null ? null : prices.OrderByDescending(p => p.DateTime).FirstOrDefault();
        }

        [HttpPost]
        public ActionResult PriceUpdate(
            DataSourceRequest request,
            PriceModel priceModel)
        {
            if (priceModel != null &&
                ModelState.IsValid)
            {
                var price = new ProductPrice()
                {
                    DateTime = DateTime.Now,
                    CostPrice = priceModel.CostPrice,
                    SalePrice = priceModel.SalePrice,
                    StoreId = priceModel.StoreId,
                    ProductId = priceModel.ProductId
                }; 
                productPriceService.InsertPrice(price); 
                return Json(true);
            }
            
            return Json(false);
        }

        public ActionResult Orders()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExportProducts()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ExportProduct))
            {
                return AccessDeniedView();
            }

            var products = productService.GetAllProducts();

            var xml = exportManager.ExportProductsToXml(products);
            return new XmlDownloadResult(xml, "products.xml");
        }

        [NonAction]
        protected virtual void PrepareProductModel(
            ProductModel model,
            Product product,
            bool setPredefinedValues,
            bool excludeProperties)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            model.ItemNo.DoIfNotNull(a=>model.ItemNo=a.Substring(2));

            var allCategories = categoryService.GetAllCategories();
            foreach (var category in allCategories)
            {
                model.AvailableCategories.Add(
                    new SelectListItem
                    {
                        Text = category.Name,
                        Value = category.Id.ToString()
                    });
            }

            if (product != null)
            {
                model.CreatedOn = dateTimeHelper.ConvertToUserTime(product.CreatedOnUtc, DateTimeKind.Utc);
                model.UpdatedOn = dateTimeHelper.ConvertToUserTime(product.UpdatedOnUtc, DateTimeKind.Utc);
            }

            //default values
            if (setPredefinedValues)
            {
                model.StockQuantity = 10000;

                model.Published = true;
            }
        }

        [NonAction]
        protected virtual void PrepareAclModel(ProductModel model, Product product, bool excludeProperties)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
        }

        [NonAction]
        protected virtual void PrepareStoresMappingModel(ProductModel model, Product product, bool excludeProperties)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
        }
    }
}
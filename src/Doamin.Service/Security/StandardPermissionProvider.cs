namespace Doamin.Service.Security
{
    using System.Collections.Generic;
    using Doamin.Service.Users;
    using Domain.Model.Security;

    public class StandardPermissionProvider : IPermissionProvider
    {
        public static readonly PermissionRecord CreateProduct = new PermissionRecord
        {
            Name = "CreateProduct",
            SystemName = "Create Product",
            Category = "Product"
        };

        public static readonly PermissionRecord DeleteProduct = new PermissionRecord
        {
            Name = "Delete Product",
            SystemName = "Delete Product",
            Category = "Product"
        };

        public static readonly PermissionRecord InventoryProduct = new PermissionRecord
        {
            Name = "Inventory Product",
            SystemName = "Inventory Product",
            Category = "Product"
        };

        public static readonly PermissionRecord UpdateProduct = new PermissionRecord
        {
            Name = "Edit Product",
            SystemName = "Edit Product",
            Category = "Product"
        };

        public static readonly PermissionRecord ExportProduct = new PermissionRecord
        {
            Name = "Export Product",
            SystemName = "Export Product",
            Category = "Product"
        };

        public static readonly PermissionRecord GetProductList = new PermissionRecord
        {
            Name = "Get Product",
            SystemName = "Get Product",
            Category = "Product"
        };

        public static readonly PermissionRecord ManageStoreProducts = new PermissionRecord
        {
            Name = "Manage Store Products",
            SystemName = "Manage Store Products",
            Category = "Product"
        };

        public static readonly PermissionRecord SetProductPrice = new PermissionRecord
        {
            Name = "Set Product Price",
            SystemName = "Set Product Price",
            Category = "Product"
        };

        public static readonly PermissionRecord CreateStore = new PermissionRecord
        {
            Name = "Create Store",
            SystemName = "Create Store",
            Category = "Store",
        };

        public static readonly PermissionRecord DeleteStore = new PermissionRecord
        {
            Name = "Delete Store",
            SystemName = "Delete   Store",
            Category = "Store",
        };

        public static readonly PermissionRecord UpdateStore = new PermissionRecord
        {
            Name = "Update Store",
            SystemName = "Update Store",
            Category = "Store",
        };

        public static readonly PermissionRecord GetStoreList = new PermissionRecord
        {
            Name = "Get Store List",
            SystemName = "Get Store List",
            Category = "Store",
        };

        public static readonly PermissionRecord GetCategoryList = new PermissionRecord
        {
            Name = "GetCategoryList",
            SystemName = "GetCategoryList",
            Category = "Catalog"
        };

        public static readonly PermissionRecord GetOrderList = new PermissionRecord
        {
            Name = "Get Order List",
            SystemName = "Get Order List",
            Category = "Order"
        };

        public static readonly PermissionRecord CreateOrder = new PermissionRecord
        {
            Name = "Create Order",
            SystemName = "Create Order",
            Category = "Order"
        };

        public static readonly PermissionRecord ReviewOrder = new PermissionRecord
        {
            Name = "Review Order",
            SystemName = "Review Order",
            Category = "Order",
        }; 
        
        public static readonly PermissionRecord ApproveOrder = new PermissionRecord
        {
            Name = "Approve Order",
            SystemName = "Approve Order",
            Category = "Order"
        };

        public static readonly PermissionRecord RejectOrder = new PermissionRecord
        {
            Name = "Reject Order",
            SystemName = "Reject Order",
            Category = "Order"
        };

        public static readonly PermissionRecord ConfirmOrder = new PermissionRecord
        {
            Name = "Confirm Order",
            SystemName = "Confirm Order",
            Category = "Order"
        };

        public static readonly PermissionRecord DeleteOrder = new PermissionRecord
        {
            Name = "Delete Record",
            SystemName = "Delete Order",
            Category = "Order"
        };

        public static readonly PermissionRecord UpdateOrder = new PermissionRecord
        {
            Name = "Update Order",
            SystemName = "Update Order",
            Category = "Order"
        };


        public static readonly PermissionRecord ViewOrder = new PermissionRecord
        {
            Name = "View Order",
            SystemName = "View Order",
            Category = "Order"
        };
        
        public static readonly PermissionRecord CreateEmployee = new PermissionRecord
        {
            Name = "Add Employee",
            SystemName = "Add Employee",
            Category = "Employee"
        };

        public static readonly PermissionRecord DeleteEmployee = new PermissionRecord
        {
            Name = "Delete Employee",
            SystemName = "Delete Employee",
            Category = "Employee"
        };
        public static readonly PermissionRecord UpdateEmployee = new PermissionRecord
        {
            Name = "Update Employee",
            SystemName = "Update Employee",
            Category = "Employee"
        };
        public static readonly PermissionRecord GetEmployee = new PermissionRecord
        {
            Name = "Get Employee",
            SystemName = "Get Employee",
            Category = "Employee"
        };


        public static readonly PermissionRecord UpdateTimeSheet = new PermissionRecord
        {
            Name = "UpdateTimeSheet",
            SystemName = "UpdateTimeSheet",
            Category = "Employee"
        };
        public static readonly PermissionRecord GetTimeSheet = new PermissionRecord
        {
            Name = "GetTimeSheet",
            SystemName = "GetTimeSheet",
            Category = "Employee"
        };

        public static readonly PermissionRecord GetCustomerList = new PermissionRecord
        {
            Name = "Get Customer List",
            SystemName = "Get Customer List",
            Category = "Customer"
        }; 

        public static readonly PermissionRecord CreateCustomer = new PermissionRecord
        {
            Name = "Create Customer",
            SystemName = "Create Customer",
            Category = "Customer"
        }; 
        public static readonly PermissionRecord DeleteCustomer = new PermissionRecord
        {
            Name = "Delete Customer",
            SystemName = "Delete Customer",
            Category = "Customer"
        }; 
        public static readonly PermissionRecord UpdateCustomer = new PermissionRecord
        {
            Name = "Update Customer",
            SystemName = "Update Customer",
            Category = "Customer"
        }; 

        public static readonly PermissionRecord CreateConsumptionRecord = new PermissionRecord
        {
            Name = "CreateConsumptionRecord",
            SystemName = "CreateConsumptionRecord",
            Category = "Consumption"
        }; 

        public static readonly PermissionRecord GetConsumption = new PermissionRecord
        {
            Name = "Get Consumption",
            SystemName = "Get Consumption",
            Category = "Consumption"
        }; 
        public static readonly PermissionRecord DeleteConsumptionRecord = new PermissionRecord
        {
            Name = "DeleteConsumptionRecord",
            SystemName = "DeleteConsumptionRecord",
            Category = "Consumption"
        }; 
        public static readonly PermissionRecord UpdateConsumptionRecord = new PermissionRecord
        {
            Name = "UpdateConsumptionRecord",
            SystemName = "UpdateConsumptionRecord",
            Category = "Consumption"
        }; 

        public static readonly PermissionRecord StatisticConsumption = new PermissionRecord
        {
            Name = "StatisticConsumption",
            SystemName = "StatisticConsumption",
            Category = "Consumption"
        }; 
        public static readonly PermissionRecord ViewProductList = new PermissionRecord
        {
            Name = "View product list",
            SystemName = "ViewProductList",
            Category = "StoreAdmin"
        };


        public static readonly PermissionRecord AccessAdminPanel = new PermissionRecord
        {
            Name = "AccessAdminPanel",
            SystemName = "Admin",
            Category = "Catalog"
        };

        public static readonly PermissionRecord GetStoreSalesRecord = new PermissionRecord()
        {
            Name = "GetStoreSales",
            SystemName = "GetStoreSales",
            Category = "StoreSales"
        };

        public static readonly PermissionRecord CreateStoreSalesRecord = new PermissionRecord()
        {
            Name = "CreateStoreSales",
            SystemName = "CreateStoreSales",
            Category = "StoreSales"
        };
        public static readonly PermissionRecord DeleteStoreSalesRecord = new PermissionRecord()
        {
            Name = "DeleteStoreSales",
            SystemName = "DeleteStoreSales",
            Category = "StoreSales"
        };
        public static readonly PermissionRecord UpdateStoreSalesRecord = new PermissionRecord()
        {
            Name = "UpdateStoreSales",
            SystemName = "UpdateStoreSales",
            Category = "StoreSales"
        };

        public static readonly PermissionRecord GetStoreProducts = new PermissionRecord()
        {
            Name = "Get Product in Store",
            SystemName = "Get Product in Store",
            Category = "Product"
        };


        public static readonly PermissionRecord GetCustomerOrder = new PermissionRecord()
        {
            Name = "GetCustomerOrder",
            SystemName = "GetCustomerOrder",
            Category = "Customer Order"
        };
        
        public static readonly PermissionRecord CreateCustomerOrder = new PermissionRecord()
        {
            Name = "CreateCustomerOrder",
            SystemName = "CreateCustomerOrder",
            Category = "Customer Order"
        };
        public static readonly PermissionRecord UpdateCustomerOrder = new PermissionRecord()
        {
            Name = "UpdateCustomerOrder",
            SystemName = "UpdateCustomerOrder",
            Category = "Customer Order"
        };
        public static readonly PermissionRecord DeleteCustomerOrder = new PermissionRecord()
        {
            Name = "DeleteCustomerOrder",
            SystemName = "DeleteCustomerOrder",
            Category = "Customer Order"
        };

        public IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[]
            {
                CreateProduct, DeleteProduct, InventoryProduct, UpdateProduct, GetProductList, ManageStoreProducts,
                SetProductPrice, CreateStore, DeleteStore, UpdateStore, GetStoreList, GetCategoryList, GetOrderList,
                CreateOrder, ReviewOrder, ApproveOrder, RejectOrder, ConfirmOrder, DeleteOrder, UpdateOrder, CreateEmployee,
                DeleteEmployee, UpdateEmployee, GetEmployee, ViewProductList, AccessAdminPanel
            };
        }

        public IEnumerable<DefaultPermissionRecord> GetDefaultPermissions()
        {
            return new[]
            {
                new DefaultPermissionRecord
                {
                    UserRoleSystemName = SystemUserRoleNames.Administrators,
                    PermissionRecords = this.GetPermissions()
                },
                new DefaultPermissionRecord
                {
                    UserRoleSystemName = SystemUserRoleNames.StoreAdmin,
                    PermissionRecords = this.GetPermissions()
                },
                new DefaultPermissionRecord
                {
                    UserRoleSystemName = SystemUserRoleNames.FactoryAdmin
                }
            };
        }
    }
}
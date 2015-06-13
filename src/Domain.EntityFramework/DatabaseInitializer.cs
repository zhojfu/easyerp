
namespace Domain.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Doamin.Service.Security;
    using Domain.Model.Company;
    using Domain.Model.Products;
    using Domain.Model.Stores;
    using Domain.Model.Users;
    using Doamin.Service.Users; 
    using EasyErp.Core.Infrastructure;

    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<EntityFrameworkDbContext>
    {
        protected override void Seed(EntityFrameworkDbContext context)
        {
            var c1 = new Category
            {
                Name = "米",
                ItemNo = "RC",
                Description = "米粮",
                UpdatedOnUtc = DateTime.Now,
                CreatedOnUtc = DateTime.Now
            };

            var c2  = new Category
            {
                Name = "面",
                ItemNo = "ND",
                Description = "面",
                UpdatedOnUtc = DateTime.Now,
                CreatedOnUtc = DateTime.Now
            };
            var c3 = new Category
            {
                Name = "油",
                ItemNo = "OL",
                Description = "食物油",
                UpdatedOnUtc = DateTime.Now,
                CreatedOnUtc = DateTime.Now
            };
            var c4 = new Category
            {
                Name = "调味料",
                ItemNo = "SP",
                Description = "调味料",
                UpdatedOnUtc = DateTime.Now,
                CreatedOnUtc = DateTime.Now
            };

            var c5 = new Category
            {
                Name = "其他",
                ItemNo = "OT",
                Description = "其他",
                UpdatedOnUtc = DateTime.Now,
                CreatedOnUtc = DateTime.Now
            };
            context.Entry(c1).State = EntityState.Added;
            context.Entry(c2).State = EntityState.Added;
            context.Entry(c3).State = EntityState.Added;
            context.Entry(c4).State = EntityState.Added;
            context.Entry(c5).State = EntityState.Added;

            var company = new Company
            {
                Id = 1,
                Name = "MIGU"
            };
            context.Entry(company).State = EntityState.Added;

            var adminStore = new Store
            {
                Id = 1,
                Name = "MIGU",
                CompanyId = company.Id,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            var s1 = new Store
            {
                Id = 2,
                Name = "Store1",
                CompanyId = company.Id,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            var s2 = new Store
            {
                Id = 3,
                Name = "Store2",
                CompanyId = company.Id,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
            
            var s3 = new Store
            {
                Id = 4,
                Name = "Store3",
                CompanyId = company.Id,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            var products = new List<Product>
            {
                new Product
                {
                    CategoryId = 1,
                    CreatedOnUtc = DateTime.Now,
                    UpdatedOnUtc = DateTime.Now,
                    FullDescription = "小米",
                    ShortDescription = "小米",
                    Name = "小米",
                    Price = 15,
                    ProductCost = 10,
                    ItemNo = "RI0001",
                    Gtin = "9019339641569"
                },
                new Product
                {
                    CategoryId = 2,
                    CreatedOnUtc = DateTime.Now,
                    UpdatedOnUtc = DateTime.Now,
                    FullDescription = "菜油",
                    ShortDescription = "菜油",
                    Name = "菜油",
                    Price = 20,
                    ProductCost = 15,
                    ItemNo = "OL0002",
                    Gtin = "9019339641579"
                },
                new Product
                {
                    CategoryId = 3,
                    CreatedOnUtc = DateTime.Now,
                    UpdatedOnUtc = DateTime.Now,
                    FullDescription = "老干妈",
                    ShortDescription = "老干妈",
                    Name = "老干妈",
                    Price = 20,
                    ItemNo = "OT0003",
                    ProductCost = 15,
                    Gtin = "9019339641580"
                },
                new Product
                {
                    CategoryId = 1,
                    CreatedOnUtc = DateTime.Now,
                    UpdatedOnUtc = DateTime.Now,
                    FullDescription = "大米",
                    ShortDescription = "大米",
                    Name = "大米",
                    Price = 20,
                    ProductCost = 15,
                    ItemNo = "RI0004",
                    Gtin = "9019339641581"
                },
                new Product
                {
                    CategoryId = 1,
                    CreatedOnUtc = DateTime.Now,
                    UpdatedOnUtc = DateTime.Now,
                    FullDescription = "大豆",
                    ShortDescription = "大豆",
                    Name = "大豆",
                    Price = 20,
                    ProductCost = 15,
                    ItemNo = "OT0005",
                    Gtin = "9019339641582"
                },
                new Product
                {
                    CategoryId = 2,
                    CreatedOnUtc = DateTime.Now,
                    UpdatedOnUtc = DateTime.Now,
                    FullDescription = "豆油",
                    ShortDescription = "豆油",
                    Name = "豆油",
                    Price = 20,
                    ProductCost = 15,
                    ItemNo = "OT0006",
                    Gtin = "9019339641592"
                },
                new Product
                {
                    CategoryId = 3,
                    CreatedOnUtc = DateTime.Now,
                    UpdatedOnUtc = DateTime.Now,
                    FullDescription = "辣椒酱",
                    ShortDescription = "辣椒酱",
                    Name = "辣椒酱",
                    Price = 20,
                    ItemNo = "OT0007",
                    ProductCost = 15,
                    Gtin = "9019339641502"
                }
            };

            context.Entry(adminStore).State = EntityState.Added;
            context.Entry(s1).State = EntityState.Added;
            context.Entry(s2).State = EntityState.Added;
            context.Entry(s3).State = EntityState.Added;

            products.ForEach(p => context.Entry(p).State = EntityState.Added);

            var enryptionService = EngineContext.Current.Resolve<IEncryptionService>();
            var saltKey = enryptionService.CreateSaltKey(5);

            var permissionProvider = EngineContext.Current.Resolve<IPermissionProvider>();
            var defaultPermissions = permissionProvider.GetDefaultPermissions();

            var defaultRoles = new List<UserRole>();
            foreach (var permission in defaultPermissions)
            {
                var userRole = new UserRole
                {
                    Name = permission.UserRoleSystemName,
                    SystemName = permission.UserRoleSystemName,
                    Active = true,
                    PermissionRecords = permission.PermissionRecords.ToList()
                };
                defaultRoles.Add(userRole);
            }
            defaultRoles.ForEach(r => context.Entry(r).State = EntityState.Added);

            var admin = new User
            {
                Name = "pancake",
                Active = true,
                StoreId = adminStore.Id,
                UseGuid = Guid.NewGuid(),
                PasswordSalt = saltKey,
                Password = enryptionService.CreatePasswordHash("pancake", saltKey),
                IsAdmin = true,
                CreatedOn = DateTime.Now,
                LastLoginDate = DateTime.Now,
                UserRoles = defaultRoles
            };
            
            var store1 = new User
            {
                Name = "Store1",
                Active = true,
                StoreId = s1.Id,
                UseGuid = Guid.NewGuid(),
                PasswordSalt = saltKey,
                Password = enryptionService.CreatePasswordHash("Store1", saltKey),
                IsAdmin = false,
                CreatedOn = DateTime.Now,
                LastLoginDate = DateTime.Now,
                UserRoles = defaultRoles.FindAll(i=>i.Name == SystemUserRoleNames.StoreAdmin)
            };

            var store2 = new User
            {
                Name = "Store2",
                Active = true,
                StoreId = s2.Id,
                UseGuid = Guid.NewGuid(),
                PasswordSalt = saltKey,
                Password = enryptionService.CreatePasswordHash("Store2", saltKey),
                IsAdmin = false,
                CreatedOn = DateTime.Now,
                LastLoginDate = DateTime.Now,
                UserRoles = defaultRoles.FindAll(i=>i.Name == SystemUserRoleNames.StoreAdmin)
            };

            context.Entry(admin).State = EntityState.Added;
            context.Entry(store1).State = EntityState.Added;
            context.Entry(store2).State = EntityState.Added;
            context.SaveChanges();
        }

        private void InstallUserRolesAndPermission(EntityFrameworkDbContext context)
        {
            var permissionProvider = EngineContext.Current.Resolve<IPermissionProvider>();
            var defaultPermissions = permissionProvider.GetDefaultPermissions();

            var defaultRoles = new List<UserRole>();
            foreach (var permission in defaultPermissions)
            {
                var userRole = new UserRole
                {
                    Name = permission.UserRoleSystemName,
                    SystemName = permission.UserRoleSystemName,
                    Active = true,
                    PermissionRecords = permission.PermissionRecords.ToList()
                };
                defaultRoles.Add(userRole);
            }
            defaultRoles.ForEach(r => context.Entry(r).State = EntityState.Added);
        }
    }
}
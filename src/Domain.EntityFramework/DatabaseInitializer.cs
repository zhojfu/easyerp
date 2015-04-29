namespace Domain.EntityFramework
{
    using Doamin.Service.Security;
    using Domain.Model.Company;
    using Domain.Model.Products;
    using Domain.Model.Stores;
    using Domain.Model.Users;
    using EasyErp.Core.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class DatabaseInitializer : DropCreateDatabaseAlways<EntityFrameworkDbContext>
    {
        protected override void Seed(EntityFrameworkDbContext context)
        {
            var c1 = new Category
            {
                Name = "Rice",
                Description = "Rice category",
                UpdatedOnUtc = DateTime.Now,
                CreatedOnUtc = DateTime.Now
            };
            var c2 = new Category
            {
                Name = "Food Oil",
                Description = "Food Oil category",
                UpdatedOnUtc = DateTime.Now,
                CreatedOnUtc = DateTime.Now
            };
            var c3 = new Category
            {
                Name = "Other",
                Description = "Other category",
                UpdatedOnUtc = DateTime.Now,
                CreatedOnUtc = DateTime.Now
            };
            context.Entry(c1).State = EntityState.Added;
            context.Entry(c2).State = EntityState.Added;
            context.Entry(c3).State = EntityState.Added;

            var company = new Company
            {
                Id = 1,
                Name = "MIGU"
            };
            context.Entry(company).State = EntityState.Added;

            var s1 = new Store
            {
                Id = 1,
                Name = "Store1",
                CompanyId = company.Id,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            var p1 = new Product
            {
                CategoryId = 1,
                CreatedOnUtc = DateTime.Now,
                UpdatedOnUtc = DateTime.Now,
                FullDescription = "Rice",
                ShortDescription = "rice",
                Name = "rice",
                Price = 15,
                ProductCost = 10,
                Gtin = "690000121"
            };

            p1.Stores.Add(s1);
            s1.Products.Add(p1);

            context.Entry(s1).State = EntityState.Added;

            context.Entry(p1).State = EntityState.Added;

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

            var u1 = new User
            {
                Name = "pancake",
                Active = true,
                StoreId = s1.Id,
                UseGuid = Guid.NewGuid(),
                PasswordSalt = saltKey,
                Password = enryptionService.CreatePasswordHash("pancake", saltKey),
                CreatedOn = DateTime.Now,
                LastLoginDate = DateTime.Now,
                UserRoles = defaultRoles
            };

            context.Entry(u1).State = EntityState.Added;
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
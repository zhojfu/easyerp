namespace EasyERP.Data
{
    using EasyERP.Data.Model;
    using System.Data.Entity;

    public class EasyErpContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Locator> Locators { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Organize> Orgnizes { get; set; }

        public DbSet<OrganizeType> OrgnizeTypes { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<TaxCategory> TaxCategories { get; set; }

        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<WareHouse> WareHouses { get; set; }
    }
}
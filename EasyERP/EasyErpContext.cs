namespace EasyERP
{
    using Model;
    using System.Data.Entity;

    internal class EasyErpContext : DbContext
    {
        public DbSet<Product> Employees { get; set; }
    }
}
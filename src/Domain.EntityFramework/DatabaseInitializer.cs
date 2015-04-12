namespace Domain.EntityFramework
{
    using Domain.Model.Products;
    using System;
    using System.Data.Entity;

    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<EntityFrameworkDbContext>
    {
        protected override void Seed(EntityFrameworkDbContext context)
        {
            var c1 = new Category
            {
                Name = "Rice",
                Description = "Rice",
                UpdatedOnUtc = DateTime.Now,
                CreatedOnUtc = DateTime.Now

                //Descriiption = "Rice category"
            };
            var c2 = new Category
            {
                Name = "Food Oil",
                Description = "Food Oil",
                UpdatedOnUtc = DateTime.Now,
                CreatedOnUtc = DateTime.Now

                //Descriiption = "Food Oil category"
            };
            var c3 = new Category
            {
                Name = "Other",
                UpdatedOnUtc = DateTime.Now,
                CreatedOnUtc = DateTime.Now

                //Descriiption = "Other category"
            };
            context.Entry(c1).State = EntityState.Added;
            context.Entry(c2).State = EntityState.Added;
            context.Entry(c3).State = EntityState.Added;
            context.SaveChanges();
        }
    }
}
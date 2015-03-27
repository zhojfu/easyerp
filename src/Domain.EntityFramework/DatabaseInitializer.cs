namespace Domain.EntityFramework
{
    using Domain.Model;
    using System;
    using System.Data.Entity;

    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<EntityFrameworkDbContext>
    {
        protected override void Seed(EntityFrameworkDbContext context)
        {
            var c1 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Rice",
                Descriiption = "Rice category"
            };
            var c2 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Food Oil",
                Descriiption = "Food Oil category"
            };
            var c3 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Other",
                Descriiption = "Other category"
            };
            context.Entry(c1).State = EntityState.Added;
            context.Entry(c2).State = EntityState.Added;
            context.Entry(c3).State = EntityState.Added;
            context.SaveChanges();
        }
    }
}
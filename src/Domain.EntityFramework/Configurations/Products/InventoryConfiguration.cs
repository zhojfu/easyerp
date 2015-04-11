using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityFramework.Configurations.Products
{
    using Domain.Model.Products;
    using System.Data.Entity.ModelConfiguration;
    using System.Runtime.CompilerServices;

    internal class InventoryConfiguration : EntityTypeConfiguration<Inventory>
    {
        public InventoryConfiguration()
        {
            this.HasRequired(i => i.Product).WithMany().HasForeignKey(i => i.ProductId);
        }
    }
}
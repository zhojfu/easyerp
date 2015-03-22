using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    class MaterialStatisticConfiguration : EntityTypeConfiguration<MaterialStatisitc>
    {
        public MaterialStatisticConfiguration()
        {
            this.HasKey(m => m.Id);
            this.Property(m => m.Date);
            this.Property(m => m.ComsumeQuantity);
            this.HasRequired(m => m.Material).WithMany(p => p.MaterialComsumptions).HasForeignKey(m => m.MaterialId);
        }
    }
}

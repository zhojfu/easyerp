using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    class AttritionConfiguration : EntityTypeConfiguration<Attrition>
    {
        public AttritionConfiguration()
        {
            this.HasKey(a => a.Id);
            this.Property(a => a.Name);
            this.Property(a => a.Unit);
            this.Property(a => a.Cost);
        }
    }
}

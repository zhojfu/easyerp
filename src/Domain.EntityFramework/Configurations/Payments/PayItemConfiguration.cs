namespace Domain.EntityFramework.Configurations.Payments
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Payments;

    public class PayItemConfiguration : EntityTypeConfiguration<PayItem>
    {
        public PayItemConfiguration()
        {
            HasRequired(p => p.Payment).WithMany(i => i.Items).HasForeignKey(p => p.PaymentId);
        }
    }
}
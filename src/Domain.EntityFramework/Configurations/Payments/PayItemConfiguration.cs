namespace Domain.EntityFramework.Configurations.Payments
{
    using Domain.Model.Payments;
    using System.Data.Entity.ModelConfiguration;

    public class PayItemConfiguration : EntityTypeConfiguration<PayItem>
    {
        public PayItemConfiguration()
        {
            HasRequired(p => p.Payment).WithMany(i => i.Items).HasForeignKey(p => p.PaymentId);
        }
    }
}
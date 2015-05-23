namespace Domain.EntityFramework.Configurations.Payments
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Payments;

    public class PaymentConfiguration : EntityTypeConfiguration<Payment>
    {
        public PaymentConfiguration()
        {
            HasKey(p => p.Id);
            HasOptional(i => i.Inventory).WithRequired();
            HasOptional(i => i.Order).WithRequired();
        }
    }
}
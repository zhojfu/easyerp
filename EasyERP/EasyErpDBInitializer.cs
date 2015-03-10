namespace EasyERP
{
    using System.Data.Entity;

    public class EasyErpDBInitializer : DropCreateDatabaseIfModelChanges<EasyErpContext>
    {
        protected override void Seed(EasyErpContext context)
        {
            base.Seed(context);
        }
    }
}
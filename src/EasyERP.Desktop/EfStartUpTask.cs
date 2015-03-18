namespace EasyERP.Desktop
{
    using Caliburn.Micro;
    using Infrastructure;
    using Infrastructure.Domain.Data;
    using System;

    public class EfStartUpTask : IStartupTask
    {
        public int Order
        {
            //ensure that this task is run first
            get { return -1000; }
        }

        public void Execute()
        {
            var settings = IoC.Get<DataSettings>();
            if (settings == null ||
                !settings.IsValid())
            {
                return;
            }
            var provider = IoC.Get<IDataProvider>();
            if (provider == null)
            {
                throw new Exception("No IDataProvider found");
            }
            provider.SetDatabaseInitializer();
        }
    }
}
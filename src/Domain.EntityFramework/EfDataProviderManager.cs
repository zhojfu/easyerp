namespace Domain.EntityFramework
{
    using Infrastructure.Domain.Data;
    using System;

    public class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager(DataSettings settings)
            : base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {
            var providerName = this.Settings.DataProvider;
            if (String.IsNullOrWhiteSpace(providerName))
            {
                throw new Exception("Data Settings doesn't contain a providerName");
            }

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();

                case "sqlce":
                    return new SqlCeDataProvider();

                default:
                    throw new Exception(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }
    }
}
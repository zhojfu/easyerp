namespace Infrastructure.Domain.Data
{
    using System;

    public class DataSettingsHelper
    {
        private static bool? _databaseIsInstalled;

        public static bool DatabaseIsInstalled()
        {
            if (_databaseIsInstalled.HasValue)
            {
                return _databaseIsInstalled.Value;
            }
            var manager = new DataSettingsManager();
            var settings = manager.LoadSettings();
            _databaseIsInstalled = settings != null && !String.IsNullOrEmpty(settings.DataConnectionString);
            return _databaseIsInstalled.Value;
        }

        public static void ResetCache()
        {
            _databaseIsInstalled = null;
        }
    }
}
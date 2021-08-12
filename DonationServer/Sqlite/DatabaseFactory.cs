using DonationServer.Utils;
using System;
using System.IO;

namespace DonationServer.Sqlite
{
    internal sealed class DatabaseFactory
    {
        #region Fields

        private const string DATABASE_BASE_DIRECTORY = "_Databases";

        #endregion Fields

        #region Methods

        internal string BuildDatabaseConnectionString(string databaseName)
        {
            try
            {
                databaseName = databaseName.Replace("Data Source=", "");

                if (!databaseName.EndsWith(".db"))
                    databaseName += ".db";

                #region Build directories

                // _Databases/{subDir}
                string subDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    DATABASE_BASE_DIRECTORY);

                PathControl.CreateIfNotExists(subDirPath);

                #endregion Build directories

                // _Databases/{subDir}/{buffer}.db
                string databaseFullPath = Path.Combine(subDirPath, databaseName);

                return $"Data Source={databaseFullPath}";
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Methods
    }
}
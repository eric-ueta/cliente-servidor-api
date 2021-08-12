using System;
using System.IO;
using System.Threading;

namespace DonationServer.Utils
{
    public static class PathControl
    {
        #region Methods

        public static void CreateIfNotExists(string path)
        {
            try
            {
                Thread.BeginCriticalRegion();

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                Thread.EndCriticalRegion();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Methods
    }
}
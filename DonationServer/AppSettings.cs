using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DonationServer
{
    public class AppSettings
    {
        #region Fields

        private static readonly IConfigurationRoot configuration;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Hosts que serão utilizados pelo Kestrel
        /// </summary>
        public static string[] Hosts => configuration.GetSection(nameof(Hosts)).Get<string[]>();

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Inicializa as configurações
        /// </summary>
        static AppSettings()
        {
            configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                 .AddJsonFile("appsettings.json", false, true)
                 .Build();
        }

        #endregion Constructors
    }
}
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonationServer.Middlewares
{
    public static class AuthManager
    {
        #region Properties

        public static ConcurrentDictionary<int, string> LoggedUsers { get; set; }

        #endregion Properties

        #region Constructors

        static AuthManager()
        {
            LoggedUsers = new();
        }

        #endregion Constructors
    }
}
using DonationServer.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonationServer.Services
{
    public class UserService
    {
        #region Fields

        private readonly SQLiteManager _sqliteManager;

        #endregion Fields

        #region Constructors

        public UserService() => _sqliteManager = new();

        #endregion Constructors
    }
}
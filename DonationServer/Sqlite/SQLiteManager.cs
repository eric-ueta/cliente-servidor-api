using Dapper;
using DonationServer.Domain;
using DonationServer.Utils;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonationServer.Sqlite
{
    /// <summary>
    /// SQLite facade
    /// </summary>
    public sealed class SQLiteManager
    {
        #region Fields

        /// <summary>
        /// Connection string for SQLite
        /// </summary>
        private readonly string _connectionString;

        private readonly DatabaseFactory databaseFactory;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="databaseName"> database name </param>
        public SQLiteManager()
        {
            try
            {
                this.databaseFactory = new();

                this._connectionString = databaseFactory.BuildDatabaseConnectionString("donation_db");

                CreateUserTable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Constructors

        #region Methods

        public void CreateUserTable()
        {
            try
            {
                using (var conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Users (
                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                              Nome VARCHAR(500),
                              Cpf VARCHAR(500),
                              Senha VARCHAR(500),
                              Email VARCHAR(500),
                              Telefone VARCHAR(500),
                              Endereco VARCHAR(500),
                              Tipo INTEGER,
                              TipoDoacao VARCHAR(500)
                            );";

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            try
            {
                IEnumerable<User> users;

                using (var conn = new SqliteConnection(_connectionString))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    string query = "SELECT * FROM Users";

                    users = await conn.QueryAsync<User>(query);
                }

                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserByDocAndPass(string cpf, string senha)
        {
            try
            {
                using (var conn = new SqliteConnection(_connectionString))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    string query = @$"SELECT * FROM Users
                                     WHERE Cpf = '{cpf}'
                                        AND Senha = '{senha}'";

                    return await conn.QueryFirstOrDefaultAsync<User>(query);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserById(int userId)
        {
            try
            {
                using (var conn = new SqliteConnection(_connectionString))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    string query = @$"SELECT * FROM Users
                                     WHERE Id = {userId}";

                    return await conn.QueryFirstOrDefaultAsync<User>(query);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> SaveUser(User user)
        {
            try
            {
                bool created = false;

                using (var conn = new SqliteConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            $@"INSERT INTO Users (
                                Nome,
                                Cpf,
                                Senha,
                                Email,
                                Telefone,
                                Endereco,
                                Tipo,
                                TipoDoacao
                            )
                            VALUES (
                                @{nameof(user.Nome)},
                                @{nameof(user.Cpf)},
                                @{nameof(user.Senha)},
                                @{nameof(user.Email)},
                                @{nameof(user.Telefone)},
                                @{nameof(user.Endereco)},
                                @{nameof(user.Tipo)},
                                @{nameof(user.TipoDoacao)}
                            );
                            SELECT last_insert_rowid();";

                        cmd.Parameters.AddWithValue(nameof(user.Nome), user?.Nome ?? "");
                        cmd.Parameters.AddWithValue(nameof(user.Cpf), user?.Cpf ?? "");
                        cmd.Parameters.AddWithValue(nameof(user.Senha), user?.Senha ?? "");
                        cmd.Parameters.AddWithValue(nameof(user.Email), user?.Email ?? "");
                        cmd.Parameters.AddWithValue(nameof(user.Telefone), user?.Telefone ?? "");
                        cmd.Parameters.AddWithValue(nameof(user.Endereco), user?.Endereco ?? "");
                        cmd.Parameters.AddWithValue(nameof(user.Tipo), user?.Tipo);
                        cmd.Parameters.AddWithValue(nameof(user.TipoDoacao), user?.TipoDoacao ?? "");

                        var obj = await cmd.ExecuteScalarAsync();

                        if (obj is int || obj is long)
                        {
                            user.Id = Convert.ToInt32(obj);
                            created = true;
                        }
                    }
                }

                return created
                    ? user
                    : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion Methods
    }
}
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
        public SQLiteManager()
        {
            try
            {
                this.databaseFactory = new();

                this._connectionString = databaseFactory.BuildDatabaseConnectionString("donation_db");

                CreateUserTable();
                CreateDoacaoTable();
                CreateSolicitacaoTable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Constructors

        #region Methods

        #region Create Tables

        public void CreateDoacaoTable()
        {
            try
            {
                using (var conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Doacoes (
                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                              Data VARCHAR(500),
                              DoadorId INTEGER,
                              Local VARCHAR(500),
                              QuantidadeRestante INTEGER,
                              QuantidadeTotal INTEGER,
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

        public void CreateSolicitacaoTable()
        {
            try
            {
                using (var conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Solicitacoes (
                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                              Data VARCHAR(500),
                              DoacaoId INTEGER,
                              ReceptorId INTEGER,
                              Status INTEGER,
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

        #endregion Create Tables

        #region Doacoes

        public async Task<bool> DeleteDonation(int donationId
            )
        {
            try
            {
                using (var conn = new SqliteConnection(_connectionString))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @$"DELETE FROM Doacoes
                                     WHERE Id = {donationId}";

                        return (await cmd.ExecuteNonQueryAsync()) > 0;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Doacao>> GetAllDonations(int userId)
        {
            try
            {
                IEnumerable<Doacao> donations;

                using (var conn = new SqliteConnection(_connectionString))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    string query = $"SELECT * FROM Doacoes WHERE DoadorId = { userId }";

                    donations = await conn.QueryAsync<Doacao>(query);
                }

                return donations;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Doacao> GetDonationById(int donationId)
        {
            try
            {
                using (var conn = new SqliteConnection(_connectionString))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    string query = @$"SELECT * FROM Doacoes
                                     WHERE Id = {donationId}";

                    return await conn.QueryFirstOrDefaultAsync<Doacao>(query);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Doacao> SaveDonation(Doacao donation)
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
                            $@"INSERT INTO Doacoes (
                                Data,
                                DoadorId,
                                Local,
                                QuantidadeRestante,
                                QuantidadeTotal,
                                TipoDoacao
                            )
                            VALUES (
                                @{nameof(donation.Data)},
                                @{nameof(donation.DoadorId)},
                                @{nameof(donation.Local)},
                                @{nameof(donation.QuantidadeRestante)},
                                @{nameof(donation.QuantidadeTotal)},
                                @{nameof(donation.TipoDoacao)}
                            );
                            SELECT last_insert_rowid();";

                        cmd.Parameters.AddWithValue(nameof(donation.Data), donation?.Data ?? "");
                        cmd.Parameters.AddWithValue(nameof(donation.DoadorId), donation?.DoadorId ?? 0);
                        cmd.Parameters.AddWithValue(nameof(donation.Local), donation?.Local ?? "");
                        cmd.Parameters.AddWithValue(nameof(donation.QuantidadeRestante), donation?.QuantidadeRestante ?? 0);
                        cmd.Parameters.AddWithValue(nameof(donation.QuantidadeTotal), donation?.QuantidadeTotal ?? 0);
                        cmd.Parameters.AddWithValue(nameof(donation.TipoDoacao), donation?.TipoDoacao ?? "");

                        var obj = await cmd.ExecuteScalarAsync();

                        if (obj is int || obj is long)
                        {
                            donation.Id = Convert.ToInt32(obj);
                            created = true;
                        }
                    }
                }

                return created
                    ? donation
                    : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Doacao> UpdateDonation(Doacao donation)
        {
            try
            {
                int rows = 0;

                using (var conn = new SqliteConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            $@"UPDATE Doacoes SET
                                Data = @{nameof(donation.Data)},
                                DoadorId = @{nameof(donation.DoadorId)},
                                Local = @{nameof(donation.Local)},
                                QuantidadeRestante = @{nameof(donation.QuantidadeRestante)},
                                QuantidadeTotal = @{nameof(donation.QuantidadeTotal)},
                                TipoDoacao = @{nameof(donation.TipoDoacao)}
                            WHERE Id = @{nameof(donation.Id)};";

                        cmd.Parameters.AddWithValue(nameof(donation.Data), donation?.Data ?? "");
                        cmd.Parameters.AddWithValue(nameof(donation.DoadorId), donation?.DoadorId ?? 0);
                        cmd.Parameters.AddWithValue(nameof(donation.Local), donation?.Local ?? "");
                        cmd.Parameters.AddWithValue(nameof(donation.QuantidadeRestante), donation?.QuantidadeRestante ?? 0);
                        cmd.Parameters.AddWithValue(nameof(donation.QuantidadeTotal), donation?.QuantidadeTotal ?? 0);
                        cmd.Parameters.AddWithValue(nameof(donation.TipoDoacao), donation?.TipoDoacao ?? "");
                        cmd.Parameters.AddWithValue(nameof(donation.Id), donation?.Id ?? 0);

                        rows = await cmd.ExecuteNonQueryAsync();
                    }
                }

                return rows > 0
                    ? donation
                    : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion Doacoes

        #region Solicitacoes

        public async Task<bool> DeleteSolicitation(int solicitationId)
        {
            try
            {
                using (var conn = new SqliteConnection(_connectionString))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @$"DELETE FROM Solicitacoes
                                     WHERE Id = {solicitationId}";

                        return (await cmd.ExecuteNonQueryAsync()) > 0;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Solicitacao>> GetAllSolcitations(int userId)
        {
            try
            {
                IEnumerable<Solicitacao> solicitations;

                using (var conn = new SqliteConnection(_connectionString))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    string query = $"SELECT * FROM Solicitacoes WHERE ReceptorId = { userId }";

                    solicitations = await conn.QueryAsync<Solicitacao>(query);
                }

                return solicitations;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Solicitacao> GetSolicitationById(int solicitationId)
        {
            try
            {
                using (var conn = new SqliteConnection(_connectionString))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    string query = @$"SELECT * FROM Solicitacoes
                                     WHERE Id = {solicitationId}";

                    return await conn.QueryFirstOrDefaultAsync<Solicitacao>(query);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Solicitacao> SaveSolicitation(Solicitacao solicitation)
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
                            $@"INSERT INTO Solicitacoes (
                                Data,
                                DoacaoId,
                                ReceptorId,
                                Status,
                                TipoDoacao
                            )
                            VALUES (
                                @{nameof(solicitation.Data)},
                                @{nameof(solicitation.DoacaoId)},
                                @{nameof(solicitation.ReceptorId)},
                                @{nameof(solicitation.Status)},
                                @{nameof(solicitation.TipoDoacao)}
                            );
                            SELECT last_insert_rowid();";

                        cmd.Parameters.AddWithValue(nameof(solicitation.Data), solicitation?.Data ?? "");
                        cmd.Parameters.AddWithValue(nameof(solicitation.DoacaoId), solicitation?.DoacaoId ?? 0);
                        cmd.Parameters.AddWithValue(nameof(solicitation.ReceptorId), solicitation?.ReceptorId ?? 0);
                        cmd.Parameters.AddWithValue(nameof(solicitation.Status), solicitation?.Status ?? 0);
                        cmd.Parameters.AddWithValue(nameof(solicitation.TipoDoacao), solicitation?.TipoDoacao ?? "");

                        var obj = await cmd.ExecuteScalarAsync();

                        if (obj is int || obj is long)
                        {
                            solicitation.Id = Convert.ToInt32(obj);
                            created = true;
                        }
                    }
                }

                return created
                    ? solicitation
                    : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Solicitacao> UpdateSolicitation(Solicitacao solicitation)
        {
            try
            {
                int rows = 0;

                using (var conn = new SqliteConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            $@"UPDATE Solicitacoes SET
                                Data = @{nameof(solicitation.Data)},
                                DoacaoId = @{nameof(solicitation.DoacaoId)},
                                ReceptorId = @{nameof(solicitation.ReceptorId)},
                                Status = @{nameof(solicitation.Status)},
                                TipoDoacao = @{nameof(solicitation.TipoDoacao)}
                            WHERE Id = @{nameof(solicitation.Id)};";

                        cmd.Parameters.AddWithValue(nameof(solicitation.Data), solicitation?.Data ?? "");
                        cmd.Parameters.AddWithValue(nameof(solicitation.DoacaoId), solicitation?.DoacaoId ?? 0);
                        cmd.Parameters.AddWithValue(nameof(solicitation.ReceptorId), solicitation?.ReceptorId ?? 0);
                        cmd.Parameters.AddWithValue(nameof(solicitation.Status), solicitation?.Status ?? 0);
                        cmd.Parameters.AddWithValue(nameof(solicitation.TipoDoacao), solicitation?.TipoDoacao ?? "");
                        cmd.Parameters.AddWithValue(nameof(solicitation.Id), solicitation?.Id ?? 0);

                        rows = await cmd.ExecuteNonQueryAsync();
                    }
                }

                return rows > 0
                    ? solicitation
                    : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion Solicitacoes

        #region Users

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
                                Tipo
                            )
                            VALUES (
                                @{nameof(user.Nome)},
                                @{nameof(user.Cpf)},
                                @{nameof(user.Senha)},
                                @{nameof(user.Email)},
                                @{nameof(user.Telefone)},
                                @{nameof(user.Endereco)},
                                @{nameof(user.Tipo)}
                            );
                            SELECT last_insert_rowid();";

                        cmd.Parameters.AddWithValue(nameof(user.Nome), user?.Nome ?? "");
                        cmd.Parameters.AddWithValue(nameof(user.Cpf), user?.Cpf ?? "");
                        cmd.Parameters.AddWithValue(nameof(user.Senha), user?.Senha ?? "");
                        cmd.Parameters.AddWithValue(nameof(user.Email), user?.Email ?? "");
                        cmd.Parameters.AddWithValue(nameof(user.Telefone), user?.Telefone ?? "");
                        cmd.Parameters.AddWithValue(nameof(user.Endereco), user?.Endereco ?? "");
                        cmd.Parameters.AddWithValue(nameof(user.Tipo), user?.Tipo);
                        //cmd.Parameters.AddWithValue(nameof(user.TipoDoacao), user?.TipoDoacao ?? "");

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

        #endregion Users

        #endregion Methods
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using RentCarApp.Commons.Concretes.Data;
using RentCarApp.Commons.Concretes.Helpers;
using RentCarApp.Commons.Concretes.Logger;
using RentCarApp.DataAccess.Abstractions;
using RentCarApp.Models;
using RentCarApp.Models.Concretes;


namespace RentCarApp.DataAccess.Concretes
{
    public class SirketRepository : IRepository<Sirket>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public SirketRepository()
        {
            _connectionString = DBHelper.GetConnectionString();
            _dbProviderName = DBHelper.GetConnectionProvider();
            _dbProviderFactory = DbProviderFactories.GetFactory(_dbProviderName);
        }

        public bool DeletedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("DELETE ");
                query.Append("FROM [dbo].[tbl_Sirket] ");
                query.Append("WHERE ");
                query.Append("[Id] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Sirket] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();
                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception(
                                "Deleting Error for entity [tbl_Sirket] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("SirketRepository::Insert:Error occured.", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check the Dispose method called before.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Clean the resources used.
                    _dbProviderFactory = null;
                }

                _bDisposed = true;
            }
        }

        public bool Insert(Sirket entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Sirket] ");
                query.Append("( [SirketAdi], [Sehir], [Adres], [AracSayisi], [SirketKullaniciAdi] , [SirketSifre]) ");
                query.Append("VALUES ");
                query.Append(
                    "( @SirketAdi, @Sehir, @Adres, @AracSayisi, @SirketKullaniciAdi, @SirketSifre ) ");
                query.Append("SELECT @intErrorCode=@@ERROR;");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Sirket] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@SirketAdi", CsType.String, ParameterDirection.Input, entity.SirketAdi);
                        DBHelper.AddParameter(dbCommand, "@Sehir", CsType.String, ParameterDirection.Input, entity.Sehir);
                        DBHelper.AddParameter(dbCommand, "@Adres", CsType.String, ParameterDirection.Input, entity.Adres);
                        DBHelper.AddParameter(dbCommand, "@AracSayisi", CsType.Int, ParameterDirection.Input, entity.AracSayisi);
                        DBHelper.AddParameter(dbCommand, "@SirketKullaniciAdi", CsType.String, ParameterDirection.Input, entity.SirketKullaniciAdi);
                        DBHelper.AddParameter(dbCommand, "@SirketSifre", CsType.String, ParameterDirection.Input, entity.SirketSifre);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Sirket] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("SirketRepository::Insert:Error occured.", ex);
            }
        }

        public IList<Sirket> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Sirket> sirket = new List<Sirket>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[Id], [SirketAdi], [Sehir], [Adres], [AracSayisi], [SirketPuani] ");
                query.Append("FROM [dbo].[tbl_Sirket] ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Sirket] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int,
                            ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Sirket();
                                    entity.Id = reader.GetInt32(0);
                                    entity.SirketAdi = reader.GetString(1);
                                    entity.Sehir = reader.GetString(2);
                                    entity.Adres = reader.GetString(3);
                                    entity.SirketPuani = reader.GetInt32(0);
                                    entity.AracSayisi = reader.GetInt32(0);

                                    sirket.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Sirket] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return sirket;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Sirketepository::SelectAll:Error occured.", ex);
            }
        }

        public Sirket SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Sirket sirket = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[Id], [SirketAdi], [Sehir], [Adres], [AracSayisi], [SirketPuani] ");
                query.Append("FROM [dbo].[tbl_Sirket] ");
                query.Append("WHERE ");
                query.Append("[Id] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db SelectById command for entity [tbl_Sirket] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@id", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Sirket();
                                    entity.Id = reader.GetInt32(0);
                                    entity.SirketAdi = reader.GetString(1);
                                    entity.Sehir = reader.GetString(2);
                                    entity.Adres = reader.GetString(3);
                                    entity.SirketPuani = reader.GetInt32(0);
                                    entity.AracSayisi = reader.GetInt32(0);
                                    sirket = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Sirket] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("SirketRepository::SelectById:Error occured.", ex);
            }
            return sirket;
        }

        public bool Update(Sirket entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Sirket] ");
                query.Append(" SET [SirketAdi] = @SirketAdi, [Sehir] = @Sehir, [Adres] =  @Adres, [AracSayisi] = @AracSayisi, [SirketPuani] = @SirketPuani, [SirketKullanıcıAdi] = @SirketKullanıcıAdi ,[SirketSifre] = @SirketSifre");
                query.Append(" WHERE ");
                query.Append(" [Id] = @Id ");
                query.Append(" SELECT @intErrorCode = @@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Sirket] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@SirketAdi", CsType.String, ParameterDirection.Input, entity.SirketAdi);
                        DBHelper.AddParameter(dbCommand, "@Sehir", CsType.String, ParameterDirection.Input, entity.Sehir);
                        DBHelper.AddParameter(dbCommand, "@Adres", CsType.String, ParameterDirection.Input, entity.Adres);
                        DBHelper.AddParameter(dbCommand, "@AracSayisi", CsType.Int, ParameterDirection.Input, entity.AracSayisi);
                        DBHelper.AddParameter(dbCommand, "@SirketKullaniciAdi", CsType.String, ParameterDirection.Input, entity.SirketKullaniciAdi);
                        DBHelper.AddParameter(dbCommand, "@SirketSifre", CsType.String, ParameterDirection.Input, entity.SirketSifre);
                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Sirket] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("SirketRepository::Update:Error occured.", ex);
            }
        }

        public bool Login(string sirketKullaniciAdi, string sirketSifre)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Sirket sirket = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[Id], [SirketAdi], [Sehir], [Adres], [AracSayisi], [SirketPuani] , [SirketKullaniciAdi],[SirketSifre] ");
                query.Append("FROM [dbo].[tbl_Sirket] ");
                query.Append("WHERE ");
                query.Append("[SirketKullaniciAdi] = @sirketKullaniciAdi AND [SirketSifre] = @sirketSifre ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException(
                                "dbCommand" + " The db Login command for entity [tbl_Musteri] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        DBHelper.AddParameter(dbCommand, "@sirketKullaniciAdi", CsType.String, ParameterDirection.Input, sirketKullaniciAdi);
                        DBHelper.AddParameter(dbCommand, "@sirketSifre", CsType.String, ParameterDirection.Input, sirketSifre);
                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Sirket();
                                    entity.Id = reader.GetInt32(0);
                                    entity.SirketAdi = reader.GetString(1);
                                    entity.SirketKullaniciAdi = reader.GetString(2);
                                    entity.SirketSifre = reader.GetString(3);

                                    sirket = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Sirket] reported the Database ErrorCode: " + _errorCode);
                        }
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("SirketRepository::SelectById:Error occured.", ex);
            }

        }
    }
}

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
    public class MusteriRepository : IRepository<Customers>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public MusteriRepository()
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
                query.Append("FROM [dbo].[tbl_Musteri] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Musteri] can't be null. ");

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
                                "Deleting Error for entity [tbl_Musteri] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("MusteriRepository::Insert:Error occured.", ex);
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

        public bool Insert(Customers entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Musteri] ");
                query.Append("( [Ad], [Soyad], [Telefon], [Adres], [Email] , [KullaniciAdi],[Sifre]) ");
                query.Append("VALUES ");
                query.Append(
                    "( @Ad, @Soyad, @Telefon, @Adres, @Email, @KullaniciAdi,@Sifre ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Musteri] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;
                        dbConnection.Open();
                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@Ad", CsType.String, ParameterDirection.Input, entity.Ad);
                        DBHelper.AddParameter(dbCommand, "@Soyad", CsType.String, ParameterDirection.Input, entity.Soyad);
                        DBHelper.AddParameter(dbCommand, "@Telefon", CsType.String, ParameterDirection.Input, entity.Telefon);
                        DBHelper.AddParameter(dbCommand, "@Adres", CsType.String, ParameterDirection.Input, entity.Adres);
                        DBHelper.AddParameter(dbCommand, "@Email", CsType.String, ParameterDirection.Input, entity.Email);
                        DBHelper.AddParameter(dbCommand, "@KullaniciAdi", CsType.String, ParameterDirection.Input, entity.KullaniciAdi);
                        DBHelper.AddParameter(dbCommand, "@Sifre", CsType.String, ParameterDirection.Input, entity.Sifre);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Musteri] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::Insert:Error occured.", ex);
            }
        }

        public IList<Customers> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Customers> musteri = new List<Customers>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[Id], [Ad], [Soyad], [Telefon], [Adres], [Email] , [KullaniciAdi],[Sifre] ");
                query.Append("FROM [dbo].[tbl_Musteri] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Musteri] can't be null. ");

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
                                    var entity = new Customers();

                                    entity.Id = reader.GetInt32(0);
                                    entity.Ad = reader.GetString(1);
                                    entity.Soyad = reader.GetString(2);
                                    entity.Adres = reader.GetString(3);


                                    musteri.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Musteri] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return musteri;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("MusteriRepository::SelectAll:Error occured.", ex);
            }
        }

        public Customers SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Customers musteri = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[Id], [Ad], [Soyad], [Telefon], [Adres], [Email] , [KullaniciAdi],[Sifre] ");
                query.Append("FROM [dbo].[tbl_Musteri] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Musteri] can't be null. ");

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
                                    var entity = new Customers();
                                    entity.Id = reader.GetInt32(0);
                                    entity.Ad = reader.GetString(1);
                                    entity.Soyad = reader.GetString(2);
                                    entity.Adres = reader.GetString(3);


                                    musteri = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Musteri] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("MusteriRepository::SelectById:Error occured.", ex);
            }
            return musteri;
        }

        public bool Update(Customers entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Musteri] ");
                query.Append(" SET [Ad] = @Ad, [Soyad] = @Soyad, [Telefon] =  @Telefon, [Adres] = @Adres, [Email] = @Email, [KullanıcıAdi] = @KullanıcıAdi ,[Sifre] = @Sifre");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Musteri] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@Ad", CsType.String, ParameterDirection.Input, entity.Ad);
                        DBHelper.AddParameter(dbCommand, "@Soyad", CsType.String, ParameterDirection.Input, entity.Soyad);
                        DBHelper.AddParameter(dbCommand, "@Telefon", CsType.String, ParameterDirection.Input, entity.Telefon);
                        DBHelper.AddParameter(dbCommand, "@Adres", CsType.String, ParameterDirection.Input, entity.Adres);
                        DBHelper.AddParameter(dbCommand, "@Email", CsType.String, ParameterDirection.Input, entity.Email);
                        DBHelper.AddParameter(dbCommand, "@KullaniciAdi", CsType.String, ParameterDirection.Input, entity.KullaniciAdi);
                        DBHelper.AddParameter(dbCommand, "@Sifre", CsType.String, ParameterDirection.Input, entity.Sifre);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Musteri] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("MusteriRepository::Update:Error occured.", ex);
            }
        }

        public bool Login(string kullaniciAdi , string sifre)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Customers musteri = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[Id], [Ad], [Soyad], [Telefon], [Adres], [Email] , [KullaniciAdi],[Sifre] ");
                query.Append("FROM [dbo].[tbl_Musteri] ");
                query.Append("WHERE ");
                query.Append("[KullaniciAdi] = @kullaniciAdi AND [Sifre] = @sifre ");
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
                        DBHelper.AddParameter(dbCommand, "@kullaniciAdi", CsType.String, ParameterDirection.Input, kullaniciAdi);
                        DBHelper.AddParameter(dbCommand, "@sifre", CsType.String, ParameterDirection.Input, sifre);
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
                                    var entity = new Customers();
                                    entity.Id = reader.GetInt32(0);
                                    entity.Ad = reader.GetString(1);
                                    entity.Soyad = reader.GetString(2);
                                    entity.Adres = reader.GetString(3);

                                    musteri = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Musteri] reported the Database ErrorCode: " + _errorCode);
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
                throw new Exception("CustomersRepository::SelectById:Error occured.", ex);
            }

        }

    }
}

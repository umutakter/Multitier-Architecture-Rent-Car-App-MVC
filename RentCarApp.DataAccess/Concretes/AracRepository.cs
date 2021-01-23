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
    public class AracRepository : IRepository<Arac>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public AracRepository()
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
                query.Append("FROM [dbo].[tbl_Araclar] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Araclar] can't be null. ");

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
                                "Deleting Error for entity [tbl_Araclar] reported the Database ErrorCode: " +
                                _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AracRepository::Insert:Error occured.", ex);
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

        public bool Insert(Arac entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tbl_Araclar] ");
                query.Append("( [AracAdi], [AracModeli], [EhliyetYasi], [MinYasSiniri], [GunlukKmSiniri] , [AnlikKm], [Airbag] , [BagajHacmi] , [KoltukSayisi], [GunlukFiyat], [SirketId], [MusaitlikDurumu]) ");
                query.Append("VALUES ");
                query.Append(
                    "( @AracAdi, @AracModeli, @EhliyetYasi, @MinYasSiniri, @GunlukKmSiniri, @AnlikKm , @Airbag , @BagajHacmi , @KoltukSayisi , @GunlukFiyat , @SirketId , @MusaitlikDurumu ) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Araclar] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@AracAdi", CsType.String, ParameterDirection.Input, entity.AracAdi);
                        DBHelper.AddParameter(dbCommand, "@AracModeli", CsType.String, ParameterDirection.Input, entity.AracModeli);
                        DBHelper.AddParameter(dbCommand, "@EhliyetYasi", CsType.String, ParameterDirection.Input, entity.EhliyetYasi);
                        DBHelper.AddParameter(dbCommand, "@MinYasSiniri", CsType.String, ParameterDirection.Input, entity.MinYasSiniri);
                        DBHelper.AddParameter(dbCommand, "@GunlukKmSiniri", CsType.String, ParameterDirection.Input, entity.GunlukKmSiniri);
                        DBHelper.AddParameter(dbCommand, "@AnlikKm", CsType.String, ParameterDirection.Input, entity.AnlikKm);
                        DBHelper.AddParameter(dbCommand, "@Airbag", CsType.String, ParameterDirection.Input, entity.AirBag);
                        DBHelper.AddParameter(dbCommand, "@BagajHacmi", CsType.String, ParameterDirection.Input, entity.BagajHacmi);
                        DBHelper.AddParameter(dbCommand, "@KoltukSayisi", CsType.String, ParameterDirection.Input, entity.KoltukSayisi);
                        DBHelper.AddParameter(dbCommand, "@GunlukFiyat", CsType.String, ParameterDirection.Input, entity.GunlukFiyat);
                        DBHelper.AddParameter(dbCommand, "@SirketId", CsType.Int, ParameterDirection.Input, entity.SirketId);
                        DBHelper.AddParameter(dbCommand, "@MusaitlikDurumu", CsType.String, ParameterDirection.Input, entity.MusaitlikDurumu);
                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Inserting Error for entity [tbl_Araclar] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AracRepository::Insert:Error occured.", ex);
            }
        }

        public IList<Arac> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Arac> arac = new List<Arac>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[Id], [AracAdi], [AracModeli], [EhliyetYasi], [MinYasSiniri], [GunlukKmSiniri] , [AnlikKm], [Airbag] , [BagajHacmi], [KoltukSayisi], [GunlukFiyat], [MusaitlikDurumu]");
                query.Append("FROM [dbo].[tbl_Araclar] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Araclar] can't be null. ");

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
                                    var entity = new Arac();
                                    entity.Id = reader.GetInt32(0);
                                    entity.AracAdi = reader.GetString(1);
                                    entity.AracModeli = reader.GetString(2);
                                    entity.EhliyetYasi = reader.GetString(3);
                                    entity.MinYasSiniri = reader.GetString(0);
                                    entity.GunlukKmSiniri = reader.GetString(0);
                                    entity.AnlikKm = reader.GetString(0);
                                    entity.AirBag = reader.GetString(1);
                                    entity.BagajHacmi = reader.GetString(2);
                                    entity.KoltukSayisi = reader.GetString(3);
                                    entity.GunlukFiyat = reader.GetString(0);
                                    entity.MusaitlikDurumu = reader.GetString(0);

                                    arac.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Araclar] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return arac;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AracRepository::SelectAll:Error occured.", ex);
            }
        }

        public Arac SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Arac arac = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[Id],  [AracAdi], [AracModeli], [EhliyetYasi], [MinYasSiniri], [GunlukKmSiniri] , [AnlikKm], [Airbag] , [BagajHacmi], [KoltukSayisi], [GunlukFiyat], [SirketId], [MusaitlikDurumu]");
                query.Append("FROM [dbo].[tbl_Araclar] ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Araclar] can't be null. ");

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
                                    var entity = new Arac();
                                    entity.Id = reader.GetInt32(0);
                                    entity.AracAdi = reader.GetString(1);
                                    entity.AracModeli = reader.GetString(2);
                                    entity.EhliyetYasi = reader.GetString(3);
                                    entity.MinYasSiniri = reader.GetString(4);
                                    entity.GunlukKmSiniri = reader.GetString(5);
                                    entity.AnlikKm = reader.GetString(6);
                                    entity.AirBag = reader.GetString(7);
                                    entity.BagajHacmi = reader.GetString(8);
                                    entity.KoltukSayisi = reader.GetString(9);
                                    entity.GunlukFiyat = reader.GetString(10);
                                    entity.SirketId = reader.GetInt32(11);
                                    entity.MusaitlikDurumu = reader.GetString(12);
                                    arac = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting Error for entity [tbl_Araclar] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AracRepository::SelectById:Error occured.", ex);
            }
            return arac;
        }

        public bool Update(Arac entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;
         
            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Araclar] ");
                query.Append(" SET [AracAdi] = @AracAdi, [AracModeli] = @AracModeli, [EhliyetYasi] =  @EhliyetYasi, [MinYasSiniri] = @MinYasSiniri, [GunlukKmSiniri] = @GunlukKmSiniri, [AnlikKm] = @AnlikKm ,[Airbag] = @Airbag,[BagajHacmi] = @BagajHacmi,[KoltukSayisi] = @KoltukSayisi,[GunlukFiyat] = @GunlukFiyat,[MusaitlikDurumu] = @MusaitlikDurumu");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Araclar] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@AracAdi", CsType.String, ParameterDirection.Input, entity.AracAdi);
                        DBHelper.AddParameter(dbCommand, "@AracModeli", CsType.String, ParameterDirection.Input, entity.AracModeli);
                        DBHelper.AddParameter(dbCommand, "@EhliyetYasi", CsType.Int, ParameterDirection.Input, entity.EhliyetYasi);
                        DBHelper.AddParameter(dbCommand, "@MinYasSiniri", CsType.Int, ParameterDirection.Input, entity.MinYasSiniri);
                        DBHelper.AddParameter(dbCommand, "@GunlukKmSiniri", CsType.Int, ParameterDirection.Input, entity.GunlukKmSiniri);
                        DBHelper.AddParameter(dbCommand, "@AnlikKm", CsType.Int, ParameterDirection.Input, entity.AnlikKm);
                        DBHelper.AddParameter(dbCommand, "@Airbag", CsType.Boolean, ParameterDirection.Input, entity.AirBag);
                        DBHelper.AddParameter(dbCommand, "@BagajHacmi", CsType.Int, ParameterDirection.Input, entity.BagajHacmi);
                        DBHelper.AddParameter(dbCommand, "@KoltukSayisi", CsType.Int, ParameterDirection.Input, entity.KoltukSayisi);
                        DBHelper.AddParameter(dbCommand, "@GunlukFiyat", CsType.Int, ParameterDirection.Input, entity.GunlukFiyat);
                        DBHelper.AddParameter(dbCommand, "@MusaitlikDurumu", CsType.Boolean, ParameterDirection.Input, entity.MusaitlikDurumu);
                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Araclar] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AracRepository::Update:Error occured.", ex);
            }
        }

        public IList<Arac> SelectAllSirketCar(int sirketId)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Arac> arac = new List<Arac>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[Id], [AracAdi], [AracModeli], [EhliyetYasi], [MinYasSiniri], [GunlukKmSiniri] , [AnlikKm], [Airbag] , [BagajHacmi], [KoltukSayisi], [GunlukFiyat], [SirketId], [MusaitlikDurumu]");
                query.Append("FROM [dbo].[tbl_Araclar] ");
                query.Append("WHERE ");
                query.Append("[SirketId] = @sirketId ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Araclar] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@sirketId", CsType.Int, ParameterDirection.Input, sirketId);
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
                                    var entity = new Arac();
                                    entity.Id = reader.GetInt32(0);
                                    entity.AracAdi = reader.GetString(1);
                                    entity.AracModeli = reader.GetString(2);
                                    entity.EhliyetYasi = reader.GetString(3);
                                    entity.MinYasSiniri = reader.GetString(4);
                                    entity.GunlukKmSiniri = reader.GetString(5);
                                    entity.AnlikKm = reader.GetString(6);
                                    entity.AirBag = reader.GetString(7);
                                    entity.BagajHacmi = reader.GetString(8);
                                    entity.KoltukSayisi = reader.GetString(9);
                                    entity.GunlukFiyat = reader.GetString(10);
                                    entity.MusaitlikDurumu = reader.GetString(12);

                                    arac.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Araclar] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return arac;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AracRepository::SelectAll:Error occured.", ex);
            }
        }

        public IList<Arac> SelectAllMusaitCar()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Arac> arac = new List<Arac>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[Id], [AracAdi], [AracModeli], [EhliyetYasi], [MinYasSiniri], [GunlukKmSiniri] , [AnlikKm], [Airbag] , [BagajHacmi], [KoltukSayisi], [GunlukFiyat], [SirketId], [MusaitlikDurumu]");
                query.Append("FROM [dbo].[tbl_Araclar] ");
                query.Append("WHERE ");
                query.Append("[MusaitlikDurumu] = '1' ");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Araclar] can't be null. ");

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
                                    var entity = new Arac();
                                    entity.Id = reader.GetInt32(0);
                                    entity.AracAdi = reader.GetString(1);
                                    entity.AracModeli = reader.GetString(2);
                                    entity.EhliyetYasi = reader.GetString(3);
                                    entity.MinYasSiniri = reader.GetString(4);
                                    entity.GunlukKmSiniri = reader.GetString(5);
                                    entity.AnlikKm = reader.GetString(6);
                                    entity.AirBag = reader.GetString(7);
                                    entity.BagajHacmi = reader.GetString(8);
                                    entity.KoltukSayisi = reader.GetString(9);
                                    entity.GunlukFiyat = reader.GetString(10);
                                    entity.MusaitlikDurumu = reader.GetString(12);

                                    arac.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Araclar] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return arac;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AracRepository::SelectAll:Error occured.", ex);
            }
        }

        public IList<Arac> SelectAllRezervasyonCar(int sirketId)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Arac> arac = new List<Arac>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append(
                    "[Id], [AracAdi], [AracModeli], [EhliyetYasi], [MinYasSiniri], [GunlukKmSiniri] , [AnlikKm], [Airbag] , [BagajHacmi], [KoltukSayisi], [GunlukFiyat], [SirketId], [MusaitlikDurumu]");
                query.Append("FROM [dbo].[tbl_Araclar] ");
                query.Append("WHERE ");
                query.Append("[SirketId] = @sirketId AND [MusaitlikDurumu]='0'");
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
                                "dbCommand" + " The db SelectById command for entity [tbl_Araclar] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        DBHelper.AddParameter(dbCommand, "@sirketId", CsType.Int, ParameterDirection.Input, sirketId);
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
                                    var entity = new Arac();
                                    entity.Id = reader.GetInt32(0);
                                    entity.AracAdi = reader.GetString(1);
                                    entity.AracModeli = reader.GetString(2);
                                    entity.EhliyetYasi = reader.GetString(3);
                                    entity.MinYasSiniri = reader.GetString(4);
                                    entity.GunlukKmSiniri = reader.GetString(5);
                                    entity.AnlikKm = reader.GetString(6);
                                    entity.AirBag = reader.GetString(7);
                                    entity.BagajHacmi = reader.GetString(8);
                                    entity.KoltukSayisi = reader.GetString(9);
                                    entity.GunlukFiyat = reader.GetString(10);
                                    entity.MusaitlikDurumu = reader.GetString(12);

                                    arac.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("Selecting All Error for entity [tbl_Araclar] reported the Database ErrorCode: " + _errorCode);

                        }
                    }
                }
                // Return list
                return arac;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AracRepository::SelectAll:Error occured.", ex);
            }
        }

        public bool UpdateMusaitlikDurumu(int id)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Araclar] ");
                query.Append(" SET [MusaitlikDurumu] = '1'");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Araclar] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@Id", CsType.String, ParameterDirection.Input, id);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Araclar] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AracRepository::Update:Error occured.", ex);
            }
        }
        public bool AracRezervasyonu(int id)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tbl_Araclar] ");
                query.Append(" SET [MusaitlikDurumu] = '0'");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tbl_Araclar] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Params
                        DBHelper.AddParameter(dbCommand, "@Id", CsType.String, ParameterDirection.Input, id);

                        //Output Params
                        DBHelper.AddParameter(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                            throw new Exception("Updating Error for entity [tbl_Araclar] reported the Database ErrorCode: " + _errorCode);
                    }
                }
                //Return the results of query/ies
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("AracRepository::Update:Error occured.", ex);
            }
        }
    }
}

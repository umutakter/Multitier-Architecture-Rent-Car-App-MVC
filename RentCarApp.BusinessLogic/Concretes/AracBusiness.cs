using System;
using System.Collections.Generic;
using RentCarApp.Commons.Concretes.Helpers;
using RentCarApp.Commons.Concretes.Logger;
using RentCarApp.DataAccess.Concretes;
using RentCarApp.Models.Concretes;

namespace RentCarApp.BusinessLogic
{
    /// <summary>
    ///     <english>
    ///         This class opens our gates of Customers to the real world and evolves our datas to informations and combines them with business rules that developed by customer.
    ///     </english>
    ///     <turkish>
    ///        Bu sınıf verilerimizi bilgiye dönüştürür ve onları müşterinin istediği iş kurallarıyla harmanlayıp Customer işlemlerini dış dünyaya açtığımız yerdir. 
    ///     </turkish>
    /// </summary>
    public class AracBusiness : IDisposable
    {
        public bool InsertArac(Arac entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new AracRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:AracBusiness::InsertArac::Error occured.", ex);
            }
        }

        public bool UpdateArac(Arac entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new AracRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:AracBusiness::UpdateArac::Error occured.", ex);
            }
        }

        public bool DeleteAracById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new AracRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:AracBusiness::AracCustomer::Error occured.", ex);
            }
        }

        public Arac SelectAracById(int Id)
        {
            try
            {
                Arac responseEntitiy;
                using (var repo = new AracRepository())
                {
                    responseEntitiy = repo.SelectedById(Id);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Sirket doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:AracBusiness::SelectAracById::Error occured.", ex);
            }
        }

        public List<Arac> SelectAllArac()
        {
            var responseEntities = new List<Arac>();

            try
            {
                using (var repo = new AracRepository())
                {
                    foreach (var entity in repo.SelectAll())
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:AracBusiness::SelectAllCArac::Error occured.", ex);
            }
        }

        public List<Arac> SelectAllSirketArac(int SirketId)
        {
            var responseEntities = new List<Arac>();

            try
            {
                using (var repo = new AracRepository())
                {
                    foreach (var entity in repo.SelectAllSirketCar(SirketId))
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:AracBusiness::SelectAllCArac::Error occured.", ex);
            }
        }
        public List<Arac> SelectAllMusaitArac()
        {
            var responseEntities = new List<Arac>();

            try
            {
                using (var repo = new AracRepository())
                {
                    foreach (var entity in repo.SelectAllMusaitCar())
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:AracBusiness::SelectAllCArac::Error occured.", ex);
            }
        }

        public List<Arac> SelectAllRezervasyonArac(int SirketId)
        {
            var responseEntities = new List<Arac>();

            try
            {
                using (var repo = new AracRepository())
                {
                    foreach (var entity in repo.SelectAllRezervasyonCar(SirketId))
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:AracBusiness::SelectAllCArac::Error occured.", ex);
            }
        }

        public AracBusiness()
        {
            //Auto-generated Code   
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public bool UpdateAracMusaitlikDurumu(int id)
        {
            try
            {
                bool isSuccess;
                using (var repo = new AracRepository())
                {
                    isSuccess = repo.UpdateMusaitlikDurumu(id);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:AracBusiness::UpdateArac::Error occured.", ex);
            }
        }

        public bool AracRezervasyonu(int id)
        {
            try
            {
                bool isSuccess;
                using (var repo = new AracRepository())
                {
                    isSuccess = repo.AracRezervasyonu(id);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:AracBusiness::UpdateArac::Error occured.", ex);
            }
        }
    }
}


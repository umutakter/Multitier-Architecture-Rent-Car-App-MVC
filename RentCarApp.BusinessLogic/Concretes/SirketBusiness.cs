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
    public class SirketBusiness : IDisposable
    {
        public bool InsertSirket(Sirket entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new SirketRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:SirketBusiness::InsertSirket::Error occured.", ex);
            }
        }

        public bool UpdateCustomer(Sirket entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new SirketRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:SirketBusiness::UpdateSirket::Error occured.", ex);
            }
        }

        public bool DeleteCustomerById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new SirketRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:SirketBusiness::SirketCustomer::Error occured.", ex);
            }
        }

        public Sirket SelectCustomerById(int Id)
        {
            try
            {
                Sirket responseEntitiy;
                using (var repo = new SirketRepository())
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
                throw new Exception("BusinessLogic:SirketBusiness::SelectSirketById::Error occured.", ex);
            }
        }

        public bool SirketLogin(string sirketKullaniciAdi, string sirketSifre)
        {
            try
            {
                bool isSucces;
                using (var repo = new SirketRepository())
                {
                    isSucces = repo.Login(sirketKullaniciAdi, sirketSifre);
                    if (isSucces != true)
                        throw new NullReferenceException("Sirket doesnt exists!");
                }
                return isSucces;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:SirketBusiness::SirketLogin::Error occured.", ex);
            }
        }

        public List<Sirket> SelectAllCustomers()
        {
            var responseEntities = new List<Sirket>();

            try
            {
                using (var repo = new SirketRepository())
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
                throw new Exception("BusinessLogic:SirketBusiness::SelectAllCSirket::Error occured.", ex);
            }
        }

        public SirketBusiness()
        {
            //Auto-generated Code   
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}


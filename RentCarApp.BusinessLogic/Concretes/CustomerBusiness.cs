﻿using System;
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
    public class CustomersBusiness : IDisposable
    {
        public bool InsertCustomer(Customers entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new MusteriRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::InsertCustomer::Error occured.", ex);
            }
        }

        public bool UpdateCustomer(Customers entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new MusteriRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::UpdateCustomer::Error occured.", ex);
            }
        }

        public bool DeleteCustomerById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new MusteriRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::DeleteCustomer::Error occured.", ex);
            }
        }

        public Customers SelectCustomerById(int customerId)
        {
            try
            {
                Customers responseEntitiy;
                using (var repo = new MusteriRepository())
                {
                    responseEntitiy = repo.SelectedById(customerId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Customer doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::SelectCustomerById::Error occured.", ex);
            }
        }

        public bool CustomerLogin (string kullaniciAdi, string sifre)
        {
            try
            {
                bool isSucces;
                using (var repo = new MusteriRepository())
                {
                    isSucces = repo.Login(kullaniciAdi,sifre);
                    if (isSucces != true)
                        throw new NullReferenceException("Customer doesnt exists!");
                }
                return isSucces;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::SelectCustomerById::Error occured.", ex);
            }
        }

        public List<Customers> SelectAllCustomers()
        {
            var responseEntities = new List<Customers>();

            try
            {
                using (var repo = new MusteriRepository())
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
                throw new Exception("BusinessLogic:CustomerBusiness::SelectAllCustomers::Error occured.", ex);
            }
        }

        public CustomersBusiness()
        {
            //Auto-generated Code   
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}


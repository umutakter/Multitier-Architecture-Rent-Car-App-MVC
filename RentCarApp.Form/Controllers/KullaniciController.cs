using RentCarApp.BusinessLogic;
using RentCarApp.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RentCarApp.Form.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        public ActionResult KullaniciGiris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KullaniciGiris(Customers model)
        {
            try
            {
                Customers musteri = null;
                using (var musteriBusiness = new CustomersBusiness())
                {
                    musteri = musteriBusiness.CustomerLogin(model.KullaniciAdi, model.Sifre);
                }
                if (musteri != null)
                {
                    Session.Add("Musteri", musteri);
                }
                List<Arac> arac = null;
                using (var aracBusiness = new AracBusiness())
                {
                    arac = aracBusiness.SelectAllMusaitArac();
                    Session.Add("Arac", arac);
                }
              
                return RedirectToAction("KullaniciSayfasi","Kullanici");
            }
            catch (Exception)
            {
                return View("KullaniciGiris");
            }
        }
        public ActionResult KullaniciKayit()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciKayit(Customers model)
        {
            try
            {
                bool success;
                using (var customerBusiness = new CustomersBusiness())
                {
                    success = customerBusiness.InsertCustomer(new Customers()
                    {
                        Ad = model.Ad,
                        Soyad = model.Soyad,
                        Adres = model.Adres,
                        KullaniciAdi = model.KullaniciAdi,
                        Sifre = model.Sifre,
                        Telefon = model.Telefon,
                        Email = model.Email
                    });
                }
                var message = success ? "done" : "failed";

                Console.WriteLine("Operation " + message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error happened: " + ex.Message);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult KullaniciSayfasi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KullaniciSayfasi(string aracId, string musteriId)
        {
            try
            {
                bool success;
                using (var aracBusiness = new AracBusiness())
                {
                    success = aracBusiness.AracRezervasyonu(int.Parse(aracId));
                }
                var message = success ? "done" : "failed";
                Console.WriteLine("Operation " + message);
                List<Arac> arac = null;
                using (var aracBusiness = new AracBusiness())
                {
                    arac = aracBusiness.SelectAllMusaitArac();
                    Session.Add("Arac", arac);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error happened: " + ex.Message);
            }
            return RedirectToAction("KullaniciSayfasi", "Kullanici");
        }
       

    }
}
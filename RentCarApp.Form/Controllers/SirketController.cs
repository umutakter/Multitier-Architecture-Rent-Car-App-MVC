using RentCarApp.BusinessLogic;
using RentCarApp.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentCarApp.Form.Controllers
{
    public class SirketController : Controller
    {
        // GET: Sirket
        public ActionResult SirketGiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SirketGiris(Sirket model)
        {
            try
            {
                bool success;
                using (var sirketBusiness = new SirketBusiness())
                {
                    success = sirketBusiness.SirketLogin(model.SirketKullaniciAdi, model.SirketSifre);
                }
                if (success == true)
                {
                    return View("SirketSayfasi");
                }
                else
                {
                    return View("SirketGiris");
                }
            }
            catch (Exception)
            {
                return View("SirketGiris");
            }

        }
        public ActionResult SirketKayit()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SirketKayit(Sirket model)
        {
            try
            {
                bool success;
                using (var sirketBusiness = new SirketBusiness())
                {
                    success = sirketBusiness.InsertSirket(new Sirket()
                    {
                        SirketAdi = model.SirketAdi,
                        Sehir = model.Sehir,
                        Adres = model.Adres,
                        AracSayisi = model.AracSayisi,
                        SirketKullaniciAdi = model.SirketKullaniciAdi,
                        SirketSifre = model.SirketSifre,
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
    }
    
}
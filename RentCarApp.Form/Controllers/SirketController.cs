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
                Sirket sirket = null;
                using (var sirketBusiness = new SirketBusiness())
                {
                    sirket = sirketBusiness.SirketLogin(model.SirketKullaniciAdi, model.SirketSifre);
                }
                if (sirket != null)
                {
                    Session.Add("Sirket", sirket);
                }
                List<Arac> arac = null;
                using (var aracBusiness = new AracBusiness())
                {
                    arac = aracBusiness.SelectAllSirketArac(sirket.Id);
                    Session.Add("Arac", arac);
                }
                List<Arac> rezervasyon = null;
                using (var aracBusiness = new AracBusiness())
                {
                    rezervasyon = aracBusiness.SelectAllRezervasyonArac(sirket.Id);
                    Session.Add("Razervasyon", rezervasyon);
                }

                return RedirectToAction("SirketSayfasi", "Sirket");
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
        [HttpGet]
        public ActionResult SirketSayfasi()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName("AracEkle")]
        public ActionResult SirketSayfasi(Arac model)
        {
            try
            {
                bool success;
                using (var aracBusiness = new AracBusiness())
                {
                    success = aracBusiness.InsertArac(new Arac()
                    {
                        AracAdi = model.AracAdi,
                        AracModeli = model.AracModeli,
                        EhliyetYasi = model.EhliyetYasi,
                        MinYasSiniri = model.MinYasSiniri,
                        GunlukKmSiniri = model.GunlukKmSiniri,
                        AnlikKm = model.AnlikKm,
                        AirBag = model.AirBag,
                        BagajHacmi = model.BagajHacmi,
                        KoltukSayisi = model.KoltukSayisi,
                        GunlukFiyat = model.GunlukFiyat,
                        SirketId = model.SirketId,
                        MusaitlikDurumu = model.MusaitlikDurumu,
                    });
                }
                List<Arac> arac = null;
                using (var aracBusiness = new AracBusiness())
                {
                    arac = aracBusiness.SelectAllSirketArac(model.SirketId);
                    Session.Add("Arac", arac);
                }
                List<Arac> rezervasyon = null;
                using (var aracBusiness = new AracBusiness())
                {
                    rezervasyon = aracBusiness.SelectAllRezervasyonArac(model.SirketId);
                    Session.Add("Razervasyon", rezervasyon);
                }
                var message = success ? "done" : "failed";

                Console.WriteLine("Operation " + message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error happened: " + ex.Message);
            }
            return RedirectToAction("SirketSayfasi", "Sirket");
        }

        [HttpPost]
        [ActionName("AracTeslim")]
        public ActionResult SirketSayfasi(string aracId, string sirketId)
        {
            try
            {
                bool success;
                Arac arac = null;
                using (var aracBusiness = new AracBusiness())
                {
                    success = aracBusiness.UpdateAracMusaitlikDurumu(int.Parse(aracId));
                    arac = aracBusiness.SelectAracById(int.Parse(aracId));

                }
                var message = success ? "done" : "failed";
         

                List<Arac> rezervasyon = null;
                using (var aracBusiness = new AracBusiness())
                {
                    rezervasyon = aracBusiness.SelectAllRezervasyonArac(arac.SirketId);
                    Session.Add("Razervasyon", rezervasyon);
                }
                Console.WriteLine("Operation " + message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error happened: " + ex.Message);
            }


            return RedirectToAction("SirketSayfasi", "Sirket");
        }
    }
    
}
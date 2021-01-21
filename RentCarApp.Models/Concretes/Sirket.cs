using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApp.Models.Concretes
{
    public class Sirket : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public int Id { get; set; }
        public string SirketAdi { get; set; }
        public string Sehir { get; set; }
        public string Adres { get; set; }
        public int AracSayisi { get; set; }
        public int SirketPuani { get; set; }
        public string SirketKullaniciAdi { get; set; }
        public string SirketSifre { get; set; }
    }
}

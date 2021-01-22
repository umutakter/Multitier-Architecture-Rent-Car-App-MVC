using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApp.Models.Concretes
{
    public class Arac : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public int Id { get; set; }
        public string AracAdi { get; set; }
        public string EhliyetYasi { get; set; }
        public string AracModeli { get; set; }
        public string MinYasSiniri { get; set; }
        public string GunlukKmSiniri { get; set; }
        public string AnlikKm { get; set; }
        public string AirBag { get; set; }
        public string BagajHacmi { get; set; }
        public string KoltukSayisi { get; set; }
        public string GunlukFiyat { get; set; }
        public string MusaitlikDurumu { get; set; }
        public int SirketId { get; set; }
    }
}

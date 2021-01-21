using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RentCarApp.Models.Concretes
{
    public class Customers : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public int Id { get; set; }
        [DisplayName("Ad"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Ad { get; set; }
        [DisplayName("Soyad"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Soyad { get; set; }
        [DisplayName("Telefon Numarası"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Telefon { get; set; }
        [DisplayName("Adres"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Adres { get; set; }
        [DisplayName("e-mail"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Email { get; set; }
        [DisplayName("Kullanıcı adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string KullaniciAdi { get; set; }
        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Sifre { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApp.Models.Concretes
{
    public class SirketLogin
    {
        [DisplayName("Şirket Kullanıcı adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string SirketKullaniciAdi { get; set; }

        [DisplayName("Şirket Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string SirketSifre { get; set; }
    }
}

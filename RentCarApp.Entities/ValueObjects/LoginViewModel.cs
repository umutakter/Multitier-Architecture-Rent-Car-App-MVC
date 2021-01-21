using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApp.Entities.ValueObjects
{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı adı")]
        public string KullaniciAdi { get; set; }

        [DisplayName("Şifre")]
        public string Sifre { get; set; }
    }
}

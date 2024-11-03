using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _220601002_donemOdevi
{
    public class PreiumKullanici:Kullanici
    {

        public int filmDegerlendir { get; set; }

        public PreiumKullanici(string ad, string soyAd, int tcNo, string dogumTarihi, string cinsiyet, string uyelikTur, string sifre) : base(ad, soyAd, tcNo, dogumTarihi, cinsiyet, uyelikTur,sifre)
        {

        }

        public override void ucretHesapla()
        {
            base.ucretHesapla();
            ucret = ucret + (ucret * 25 / 100);
        }

        public override void izlemeListeEkle(Film film)
        {
            base.izlemeListeEkle(film);
        }

        public override void izlemeListeCikar(Film film)
        {
            base.izlemeListeCikar(film);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _220601002_donemOdevi
{
    public abstract class Kullanici
    {
        public string ad { get; set; }

        public string soyAd { get; set; }

        public int tcNo { get; set; }

        public string dogumTarihi { get; set; }

        public string cinsiyet { get; set; }

        public string uyelikTur { get; set; }

        public int ucret { get; set; }

        public string sifre { get; set; }

        private List<Film> izlemeListe = new List<Film>();


        public Kullanici(string ad, string soyAd, int tcNo, string dogumTarihi, string cinsiyet, string uyelikTur, string sifre)
        {
            this.ad = ad;
            this.soyAd = soyAd;
            this.tcNo = tcNo;
            this.dogumTarihi = dogumTarihi;
            this.cinsiyet = cinsiyet;
            this.uyelikTur = uyelikTur;
            this.sifre = sifre;
        }

        public virtual void ucretHesapla()
        {
            ucret = 100;
        }

        public virtual void izlemeListeEkle(Film film)
        {
            izlemeListe.Add(film);
        }

        
        public List<Film> listeDondur()
        {

            return izlemeListe;
        }

  
        public virtual void izlemeListeCikar(Film film)
        {
            izlemeListe.Remove(film);
        }




    }
}

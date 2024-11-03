using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _220601002_donemOdevi
{
    public class Yonetici
    {

        public string kullaniciAdi { get; set; }

        public string sifre { get; set; }

       
        private List<Film> filmler = new List<Film>();

        public void filmEkle(Film film)
        {
            filmler.Add(film);
        }

        private List<Yonetici> yoneticiler = new List<Yonetici>();

        public void yoneticiEkle(Yonetici yonetici)
        {
            yoneticiler.Add(yonetici);
        }

        public List<Yonetici> yoneticiDondur()
        {
            return yoneticiler;
        }

        public Yonetici()
        {
            
        }

    }
}

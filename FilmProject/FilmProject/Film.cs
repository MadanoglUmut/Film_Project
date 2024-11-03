using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _220601002_donemOdevi
{
    public class Film
    {
        public string ad { get; set; }

        public string yonetmenAd { get; set; }

        private List<string> Oyuncular = new List<string>();

        public string filmTur { get; set; }

        public int yayinYili { get; set; }

        public float degerlendirPuan { get; set; }

        private List<string> yorumlar = new List<string>();

        public string resim {  get; set; }

        public void oyuncuEkle(string oyuncu)
        {
            Oyuncular.Add(oyuncu);
        }

        public void yorumEkle(string yorum)
        {
            yorumlar.Add(yorum);
        }

        public Film(string ad, string yonetmenAd, string filmTur, int yayinYili)
        {
            this.ad = ad;
            this.yonetmenAd = yonetmenAd;
            this.filmTur = filmTur;
            this.yayinYili = yayinYili;
        }

        public List<string> oyuncuDondur()
        {
            return Oyuncular;   
        }

       
        
        public string yorumDondur()
        {
            if(yorumlar.Count == 0)
            {
                return "";
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < yorumlar.Count; i++)
                {
                    stringBuilder.AppendLine(yorumlar[i] + "-");
                }
                return stringBuilder.ToString();
            }

        }
     


    }
}

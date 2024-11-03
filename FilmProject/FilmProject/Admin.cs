using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _220601002_donemOdevi
{
    public partial class Admin : Form
    {
        SqlConnection connection = new SqlConnection("server=.; Initial Catalog=nypveri1;Integrated Security=SSPI");
        SqlCommand command;
        SqlDataAdapter da;
        SqlDataReader dr;
        Yonetici admin = new Yonetici();
        public Admin()
        {
            InitializeComponent();
            admin.kullaniciAdi = "admin";
            admin.sifre = "123";
            admin.yoneticiEkle(admin);
        }

        public Film dondurFilm;

        private void adminFilmEkleBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);

        }

        private void filmEkleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (filmAdiTxt.Text.Trim().Length == 0 || yonetmenTxt.Text.Trim().Length == 0 || filmTurcomboBox.Text == "" || filmYilTxt.Text.Trim().Length == 0 || oyuncuIsımRichTxt.Text.Trim().Length == 0 || secilenResimTxt.Text.Trim().Length == 0 || degerlendirTxt.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Tüm Film Bilgilerini Doldurulmalısınız!!!");
                }
                else
                {
                    Film film = new Film(filmAdiTxt.Text.ToString(), yonetmenTxt.Text.ToString(), filmTurcomboBox.Text.ToString(), Convert.ToInt32(filmYilTxt.Text));
                    film.degerlendirPuan = Convert.ToInt32(degerlendirTxt.Text);
                    admin.filmEkle(film);
                    string[] oyuncular = oyuncuIsımRichTxt.Text.Split(new[] { '\n', '\r', ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string oyuncu in oyuncular)
                    {
                        film.oyuncuEkle(oyuncu);
                    }
                    string resim = secilenResimTxt.Text.ToString();
                    veriEkle(film.ad, film.yonetmenAd, film.filmTur, film.yayinYili, resim, film.degerlendirPuan);
                    MessageBox.Show("Film Ekleme İşlemi Başarılı");

                    List<string> filmVeri = film.oyuncuDondur();

                    for (int i = 0; i < filmVeri.Count; i++)
                    {
                        veriOyuncuEkle(filmVeri[i], film.ad);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Film Bilgileri Hatalı Formatta Girildi!!!");
            }
        }

        private void filmGetirBtn_Click(object sender, EventArgs e)
        {
            try
            {
                filmGetir();
                guncelleAdTxt.Text = dondurFilm.ad;
                guncelleYntmnTxt.Text = dondurFilm.yonetmenAd;
                guncelTurCombo.Text = dondurFilm.filmTur;
                guncelleYilTxt.Text = dondurFilm.yayinYili.ToString();
                guncelResimTxt.Text = dondurFilm.resim;
                guncellePuanTxt.Text = dondurFilm.degerlendirPuan.ToString();
                for (int i = 0; i < oyuncuGetir().Count; i++)
                {
                    guncelleOyuncuTxt.Text += oyuncuGetir()[i] + ",";
                }
                pictureBox2.ImageLocation = guncelResimTxt.Text;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Film Bulunamadı");
            }

        }

        public List<String> oyuncuGetir()
        {
            using (SqlConnection connection3 = new SqlConnection("server=.; Initial Catalog=nypveri1;Integrated Security=SSPI"))
            {
                connection3.Open();
                command = connection3.CreateCommand();
                command.CommandText = "SELECT * FROM OyuncularDeneme2 WHERE filmAd = @filmAd";
                command.Parameters.AddWithValue("@filmAd", textBox1.Text.ToString());
                dr = command.ExecuteReader();
                List<String> dondurOyuncular = new List<String>();

                while (dr.Read())
                {
                    string dondurOyuncu = dr["oyuncuAd"].ToString();
                    dondurOyuncular.Add(dondurOyuncu);
                }

                if (dondurOyuncular.Count > 0)
                {

                    return dondurOyuncular;

                }
                else
                {
                    return null;
                }
            }
        }

        public Film filmGetir()
        {
            using (SqlConnection connection2 = new SqlConnection("server=.; Initial Catalog=nypveri1;Integrated Security=SSPI"))
            {
                connection2.Open();
                command = connection2.CreateCommand();
                command.CommandText = "SELECT * FROM Filmler WHERE FilmAd = @filmAd";
                command.Parameters.AddWithValue("@filmAd", textBox1.Text.ToString());
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    dondurFilm = new Film(dr["FilmAd"].ToString(), dr["YonetmenAd"].ToString(), dr["FilmTur"].ToString(), Convert.ToInt32(dr["YayinYili"].ToString()));
                    dondurFilm.resim = dr["FilmResim"].ToString();
                    dondurFilm.degerlendirPuan = Convert.ToInt32(dr["DegerlendirPuan"]);
                    return dondurFilm;

                }
                else
                {
                    return null;
                }
            }
        }

        private void guncelleBtn_Click(object sender, EventArgs e)
        {
            dondurFilm.ad = guncelleAdTxt.Text.ToString();
            dondurFilm.yonetmenAd = guncelleYntmnTxt.Text.ToString();
            dondurFilm.filmTur = guncelTurCombo.Text.ToString();
            dondurFilm.yayinYili = Convert.ToInt32(guncelleYilTxt.Text);
            dondurFilm.resim = guncelResimTxt.Text.ToString();
            dondurFilm.degerlendirPuan = Convert.ToInt32(guncellePuanTxt.Text);
            string[] oyuncular = guncelleOyuncuTxt.Text.Split(new[] { '\n', '\r', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string oyuncu in oyuncular)
            {
                dondurFilm.oyuncuEkle(oyuncu);
            }
            List<string> yeniOyuncuVeri = dondurFilm.oyuncuDondur();
            veriFilmGuncelle(dondurFilm.ad, dondurFilm.yonetmenAd, dondurFilm.filmTur, dondurFilm.yayinYili, dondurFilm.resim, Convert.ToInt32(dondurFilm.degerlendirPuan), textBox1.Text.ToString());


            for (int i = 0; i < yeniOyuncuVeri.Count; i++)
            {
                veriOyuncuGuncelle(dondurFilm.ad, yeniOyuncuVeri[i], textBox1.Text.ToString());

            }

            MessageBox.Show("Film Güncelleme Başarılı");
        }

        public void veriEkle(string filmAdi, string yonetmenAdi, string filmTur, int filmYili, string resim, float degerlendirPuan)
        {
            try
            {
                string sorgu = "INSERT INTO Filmler(FilmAd, YonetmenAd, FilmTur, YayinYili, FilmResim, DegerlendirPuan)VALUES(@FilmAd, @YonetmenAd, @FilmTur, @YayinYili, @FilmResim, @DegerlendirPuan)";

                command = new SqlCommand(sorgu, connection);
                command.Parameters.AddWithValue("@FilmAd", filmAdi);
                command.Parameters.AddWithValue("@YonetmenAd", yonetmenAdi);
                command.Parameters.AddWithValue("@YayinYili", filmYili);
                command.Parameters.AddWithValue("@FilmTur", filmTur);
                command.Parameters.AddWithValue("@FilmResim", resim);
                command.Parameters.AddWithValue("@DegerlendirPuan", degerlendirPuan);
                connection.Open();
                command.ExecuteNonQuery(); ;
                connection.Close();
                MessageBox.Show("Veri tabanına film ekleme işlemi gerçekleşti");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Film Bilgileri Hatalı Formatta Girildi!!!");
            }
        }

        public void veriOyuncuEkle(string oyuncuAd, string filmAd)
        {
            string sorgu2 = "INSERT INTO OyuncularDeneme2(oyuncuAd, filmAd)VALUES(@oyuncuAd, @filmAd)";
            command = new SqlCommand(sorgu2, connection);
            command.Parameters.AddWithValue("@oyuncuAd", oyuncuAd);
            command.Parameters.AddWithValue("@filmAd", filmAd);
            connection.Open();
            command.ExecuteNonQuery(); ;
            connection.Close();
        }

        private void filmResimSecBtn_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            secilenResimTxt.Text = openFileDialog1.FileName;
        }

        private void filmGuncelle_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        public void veriFilmGuncelle(string filmAd, string yonetmenAd, string filmTur, int yayinYili, string filmResim, int degerlendirPuan,string eskiFilmAd)
        {
            try
            {
                string sorgu = "UPDATE Filmler SET FilmAd = @filmAd, YonetmenAd=@yonetmenAd,FilmTur=@filmTur,YayinYili = @yayinYili, FilmResim = @filmResim, DegerlendirPuan = @degerlendirPuan WHERE FilmAd = @eskiFilmAd ";
                command = new SqlCommand(sorgu, connection);
                command.Parameters.AddWithValue("@filmAd", filmAd);
                command.Parameters.AddWithValue("@yonetmenAd", yonetmenAd);
                command.Parameters.AddWithValue("@filmTur", filmTur);
                command.Parameters.AddWithValue("@yayinYili", yayinYili);
                command.Parameters.AddWithValue("@filmResim", filmResim);
                command.Parameters.AddWithValue("@degerlendirPuan", degerlendirPuan);
                command.Parameters.AddWithValue("@eskiFilmAd", eskiFilmAd);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Veri tabanı film güncelleme başarılı");
                connection.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Veri Tabanı Film Güncelleme Hata");
            }
        }

        public void veriOyuncuGuncelle(string filmAd, string oyuncuAd,string eskiFilmAd)
        {
            try
            {
                string sorgu = "UPDATE OyuncularDeneme2 SET oyuncuAd = @oyuncuAd, filmAd=@filmAd WHERE filmAd=@eskiFilmAd";
                command = new SqlCommand(sorgu, connection);
                command.Parameters.AddWithValue("@filmAd", filmAd);
                command.Parameters.AddWithValue("@oyuncuAd", oyuncuAd);
                command.Parameters.AddWithValue("@eskiFilmAd", eskiFilmAd);
                //command.Parameters.AddWithValue("@eskiOyuncuAd", eskiOyuncuAd);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Veri tabanı oyuncu güncelleme hata");
            }

        }

        private void admnCikisYapBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

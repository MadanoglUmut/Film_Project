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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace _220601002_donemOdevi
{
    public partial class UyeOl : Form
    {

        SqlConnection connection = new SqlConnection("server=.; Initial Catalog=nypveri1;Integrated Security=SSPI");
        SqlCommand command;
        SqlDataAdapter da;
        SqlDataReader dr;
        public UyeOl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(uyeAdTxt.Text.Trim().Length == 0||uyeSoyadTxt.Text.Trim().Length == 0||uyeTcTxt.Text.Trim().Length == 0||uyeTarihTxt.Text.Trim().Length == 0||uyeCinsiyetcomboBox.Text == ""||uyeTurcomboBox.Text == "")
                {
                    MessageBox.Show("Tüm Bilgilerinizi Doldurulmalısınız!!!");
                }
                else
                {
                    if(uyeTurcomboBox.Text == "Standart Kullanıcı")
                    {
                        StandartKullanici stKullanici = new StandartKullanici(uyeAdTxt.Text.ToString(), uyeSoyadTxt.Text.ToString(), Convert.ToInt32(uyeTcTxt.Text),uyeTarihTxt.Text.ToString(),uyeCinsiyetcomboBox.Text.ToString(), "Standart Kullanıcı", uyeSifreTxt.Text.ToString());
                        //stKullanici.ucretHesapla();
                        MessageBox.Show("Standart Üyeliğiniz Başarılı Şekilde Gerçekleşti...");
                        veriEkle(stKullanici.tcNo, stKullanici.ad, stKullanici.soyAd, stKullanici.cinsiyet, stKullanici.sifre, stKullanici.dogumTarihi, "Standart Kullanıcı");
                        GirisYapcs viewOne = new GirisYapcs();
                        viewOne.Show();
                        this.Hide();  
                    }
                    else if(uyeTurcomboBox.Text == "Premium Kullanıcı")
                    {
                        PreiumKullanici preKullanici = new PreiumKullanici(uyeAdTxt.Text.ToString(), uyeSoyadTxt.Text.ToString(), Convert.ToInt32(uyeTcTxt.Text), uyeTarihTxt.Text.ToString(), uyeCinsiyetcomboBox.Text.ToString(), "Premium Kullanıcı", uyeSifreTxt.Text.ToString());
                        //preKullanici.ucretHesapla();
                        MessageBox.Show("Premium Üyeliğiniz Başarılı Şekilde Gerçekleşti...");
                        veriEkle(preKullanici.tcNo, preKullanici.ad, preKullanici.soyAd, preKullanici.cinsiyet, preKullanici.sifre, preKullanici.dogumTarihi, "Premium Kullanıcı");
                        GirisYapcs viewOne = new GirisYapcs();
                        viewOne.Show();
                        this.Hide();                        
                    }
                    else
                    {
                        MessageBox.Show("Üyelik Gerçekleştirilemedi!!!");
                    }

                }


            }
            catch(System.FormatException ex)
            {
                MessageBox.Show("Bilgiler Hatalı Formatta Girildi!!!");
            
            }

        }

        public void veriEkle(int tcNo, string ad, string soyAd, string cinsiyet, string sifre, string dogumTarihi,string kullaniciTur)
        {
            try
            {
                string sorgu = "INSERT INTO Kullanıcı(tcNo, Ad, Soyad, Cinsiyet, Sifre, DogumTarihi,KullaniciTur)VALUES(@tcNo,@Ad,@Soyad,@Cinsiyet,@Sifre,@DogumTarihi,@KullaniciTur)";
                command = new SqlCommand(sorgu, connection);
                command.Parameters.AddWithValue("@tcNo", tcNo);
                command.Parameters.AddWithValue("@Ad", ad);
                command.Parameters.AddWithValue("@Soyad", soyAd);
                //command.Parameters.AddWithValue("@Ucret", ucret);
                command.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                command.Parameters.AddWithValue("@Sifre", sifre);
                command.Parameters.AddWithValue("@DogumTarihi", dogumTarihi);
                command.Parameters.AddWithValue("@KullaniciTur", kullaniciTur);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Veri tabanı ekleme işlemi gerçekleşti");
                connection.Close();
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show("Tüm Bilgiler Hatalı Formatta Girildi!!!");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            GirisYapcs viewOne = new GirisYapcs();  
            viewOne.Show();
            this.Hide();
        }
    }
}

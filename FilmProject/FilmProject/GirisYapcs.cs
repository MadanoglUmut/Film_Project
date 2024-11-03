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

namespace _220601002_donemOdevi
{
    public partial class GirisYapcs : Form
    {
        SqlConnection connection = new SqlConnection("server=.; Initial Catalog=nypveri1;Integrated Security=SSPI");
        SqlCommand command;
        SqlDataAdapter da;
        SqlDataReader dr;
        Yonetici admin = new Yonetici();

        public GirisYapcs()
        {
            InitializeComponent();
            admin.kullaniciAdi = "admin";
            admin.sifre = "123";
            admin.yoneticiEkle(admin);            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UyeOl viewOne = new UyeOl();
            viewOne.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                if(girisKullaniciTxt.Text == admin.yoneticiDondur()[0].kullaniciAdi && girisSifreTxt.Text == admin.yoneticiDondur()[0].sifre)
                {
                    Admin viewOne = new Admin();
                    viewOne.Show();
                    this.Hide();
                }

                else
                {
                    string sorgu = "SELECT * FROM Kullanıcı WHERE tcNo = @tcNo AND Sifre = @Sifre";
                    command = new SqlCommand(sorgu, connection);
                    command.Parameters.AddWithValue("@tcNo", int.Parse(girisKullaniciTxt.Text));
                    command.Parameters.AddWithValue("@Sifre", girisSifreTxt.Text);
                    connection.Open();
                    dr = command.ExecuteReader();
                   
                    if (dr.Read())
                    {
                        connection.Close();
                        Form1 viewOne = new Form1();
                        viewOne.tcNo = int.Parse(girisKullaniciTxt.Text);
                        viewOne.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Bilgilerinizi Kontrol Edin");
                    }

                }

            }

            catch(Exception ex) 
            {
                MessageBox.Show("Kullanıcı Bulunamadı!!!");
            
            }



        }
    }
}

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

    public partial class Form1 : Form
    {
        private NotifyIcon notifyIcon;
        SqlConnection connection = new SqlConnection("server=.; Initial Catalog=nypveri1;Integrated Security=SSPI");
        SqlCommand command;
        SqlDataAdapter da;
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
        }

        public int tcNo;
        public Kullanici kullaniciNesnesi;


        public Kullanici KullaniciGetir(int tcNo)
        {
            using(SqlConnection connection2 = new SqlConnection("server=.; Initial Catalog=nypveri1;Integrated Security=SSPI"))
            {
                connection2.Open();
                command = connection2.CreateCommand();
                command.CommandText = "SELECT * FROM Kullanıcı WHERE tcNo = @tcNo";
                command.Parameters.AddWithValue("@tcNo", tcNo);
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["KullaniciTur"].ToString() == "Premium Kullanıcı")
                    {
                        PreiumKullanici preKullanici = new PreiumKullanici(dr["Ad"].ToString(), dr["Soyad"].ToString(), Convert.ToInt32(dr["tcNo"].ToString()), dr["DogumTarihi"].ToString(), dr["Cinsiyet"].ToString(), dr["KullaniciTur"].ToString(), dr["Sifre"].ToString());
                        return preKullanici;
                    }
                    else if (dr["KullaniciTur"].ToString() == "Standart Kullanıcı")
                    {
                        StandartKullanici kullanici = new StandartKullanici(dr["Ad"].ToString(), dr["Soyad"].ToString(), Convert.ToInt32(dr["tcNo"].ToString()), dr["DogumTarihi"].ToString(), dr["Cinsiyet"].ToString(), dr["KullaniciTur"].ToString(), dr["Sifre"].ToString());
                        return kullanici;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

        }

        private void profilBtn_Click(object sender, EventArgs e)
        {     
            tabControl1.SelectTab(1);
            bilgiAdTxt.Text = kullaniciNesnesi.ad;
            bilgiTcTx.Text = kullaniciNesnesi.tcNo.ToString();
            bilgiDogumTxt.Text = kullaniciNesnesi.dogumTarihi;
            bilgiSoyadTxt.Text = kullaniciNesnesi.soyAd;
            bilgiSifreTxt.Text = kullaniciNesnesi.sifre;
            cinsiyetComboBox.Text = kullaniciNesnesi.cinsiyet;
            uyeTurComboBox.Text = kullaniciNesnesi.uyelikTur;
            kullaniciNesnesi.ucretHesapla();
            odenenUcretTxt.Text = kullaniciNesnesi.ucret.ToString();            
        }

        private void bilgiGuncelleBtn_Click(object sender, EventArgs e)
        {
            kullaniciNesnesi.ad = bilgiAdTxt.Text.ToString();
            kullaniciNesnesi.soyAd = bilgiSoyadTxt.Text.ToString();
            kullaniciNesnesi.tcNo = Convert.ToInt32(bilgiTcTx.Text);
            kullaniciNesnesi.dogumTarihi = bilgiDogumTxt.Text.ToString();
            kullaniciNesnesi.cinsiyet = cinsiyetComboBox.Text.ToString();
            kullaniciNesnesi.sifre = bilgiSifreTxt.Text.ToString();
            kullaniciNesnesi.uyelikTur = uyeTurComboBox.Text.ToString();
            kullaniciNesnesi.ucretHesapla();
            veriKullaniciBilgiGuncelle(tcNo, kullaniciNesnesi.tcNo,kullaniciNesnesi.soyAd,kullaniciNesnesi.cinsiyet, kullaniciNesnesi.sifre, kullaniciNesnesi.dogumTarihi,kullaniciNesnesi.uyelikTur ,kullaniciNesnesi.ad);
            veriIzlemeListeGuncelle(kullaniciNesnesi.tcNo, tcNo);
            MessageBox.Show("Bilgiler Başarılı Şekilde Güncellendi");

        }

        private void filmAraBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (filmAraTxt.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Aranacak Filmi Giriniz!!!");
                }
                else
                {
                    string aramaMetni = filmAraTxt.Text.ToString();
                    string sorgu = "SELECT Filmler.FilmAd,Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim,REPLACE(REPLACE(REPLACE(RTRIM(STRING_AGG(NULLIF(OyuncularDeneme2.oyuncuAd, ''), ', ')), ' ', ''), CHAR(9), ''), CHAR(10), '') AS Oyuncular,Filmler.DegerlendirPuan FROM  Filmler INNER JOIN OyuncularDeneme2 ON OyuncularDeneme2.filmAd = Filmler.FilmAd WHERE Filmler.FilmAd = @aramaMetni OR FilmTur= @aramaMetni OR Filmler.YonetmenAd = @aramaMetni GROUP BY Filmler.FilmAd,Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim,Filmler.DegerlendirPuan";
                    connection.Open();
                    da = new SqlDataAdapter(sorgu, connection);
                    da.SelectCommand.Parameters.AddWithValue("@aramaMetni", aramaMetni);
                    DataTable table = new DataTable();
                    da.Fill(table);
                    dataGridView1.DataSource = table;
                    connection.Close();
                    filmTik();
                    filmAraTxt.Clear();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void yorumEkleBtn_Click(object sender, EventArgs e)
        {
            int secilenFilm = dataGridView1.SelectedCells[0].RowIndex;
            Film film = new Film(dataGridView1.Rows[secilenFilm].Cells[0].Value.ToString(), dataGridView1.Rows[secilenFilm].Cells[1].Value.ToString(), dataGridView1.Rows[secilenFilm].Cells[2].Value.ToString(), Convert.ToInt16(dataGridView1.Rows[secilenFilm].Cells[3].Value.ToString()));
            film.yorumEkle(yorumRichTxtBox.Text.ToString());
            veriYorumEkle(tcNo, film.ad, film.yorumDondur());
            MessageBox.Show("Yorum Ekleme İşlemi Başarılı");
            yorumRichTxtBox.Clear();

        }

        private void yorumInceleBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection4 = new SqlConnection("server=.; Initial Catalog=nypveri1;Integrated Security=SSPI"))
            {

                connection4.Open();
                command = connection4.CreateCommand();
                command.CommandText = "SELECT Filmler.FilmAd,Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim,REPLACE(REPLACE(REPLACE(RTRIM(STRING_AGG(NULLIF(Yorumlar.yorum, ''), ', ')), ' ', ''), CHAR(9), ''), CHAR(10), '') AS Yorumlar FROM Filmler INNER JOIN Yorumlar On Yorumlar.filmAd = Filmler.FilmAd GROUP BY Filmler.FilmAd,Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim";
                command.Parameters.AddWithValue("@tcNo", kullaniciNesnesi.tcNo);
                dr = command.ExecuteReader();
                int secilenFilm = dataGridView1.SelectedCells[0].RowIndex;
                while (dr.Read())
                {
                    if (dr["FilmAd"].ToString() == dataGridView1.Rows[secilenFilm].Cells[0].Value.ToString())
                    {
                        Film film1 = new Film(dr["FilmAd"].ToString(), dr["YonetmenAd"].ToString(), dr["FilmTur"].ToString(), Convert.ToInt32(dr["YayinYili"]));
                        film1.yorumEkle(dr["Yorumlar"].ToString());
                        filmBilgiRichTxt.Text += "\n YORUMLAR:";

                        foreach (char film4 in film1.yorumDondur())
                        {
                            filmBilgiRichTxt.Text += $"{film4} ";

                        }
                    }
                }
            }
        }

        private void puanKaydet_Click(object sender, EventArgs e)
        {
            if (kullaniciNesnesi.GetType().Name == "StandartKullanici")
            {
                MessageBox.Show("Değerlendirme Yapabilmek İçin Premium Üye Olmalısınız!!!");
                dgrlendirPuanTxt.Clear();
            }
            else
            {
                if(dgrlendirPuanTxt.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Puan Alanı Boş Bırakılamaz!!");
                }
                else if(!int.TryParse(dgrlendirPuanTxt.Text, out int value))
                {
                    MessageBox.Show("Puan Sadece Rakam Formatında Girilir");
                }
                else
                {
                    int secilenFilm = dataGridView1.SelectedCells[0].RowIndex;
                    double degerlendirmePuani = (Convert.ToInt32(dgrlendirPuanTxt.Text) + Convert.ToDouble(dataGridView1.Rows[secilenFilm].Cells[6].Value.ToString())) / 2;
                    veriPuanGuncelle(degerlendirmePuani, dataGridView1.Rows[secilenFilm].Cells[0].Value.ToString());
                    dgrlendirPuanTxt.Clear();
                }
            }
        }

        private void sonEklnenBtn_Click(object sender, EventArgs e)
        {
            Film sonEklenenFilm = sonEklenenFilmVeri();
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.Visible = true; 
            notifyIcon.BalloonTipTitle = "Son Eklenen Film";
            notifyIcon.BalloonTipText = sonEklenenFilm.ad + " " + sonEklenenFilm.yonetmenAd + " " + sonEklenenFilm.filmTur + " " + sonEklenenFilm.yayinYili;
            notifyIcon.ShowBalloonTip(1000);
        }

        private void hsbSilBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string queryFour = "DELETE Kullanıcı WHERE tcNo = @tcNo";
                command = new SqlCommand(queryFour, connection);
                command.Parameters.AddWithValue("@tcNo", kullaniciNesnesi.tcNo);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Üyeliğiniz Başarılı Şekilde Silinmiştir..");
                connection.Close();
                kullaniciNesnesi = null;
                GC.Collect();
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Üyelik Silinirken Hata");
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            filmTik();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string query = "SELECT Filmler.FilmAd,Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim,REPLACE(REPLACE(REPLACE(RTRIM(STRING_AGG(NULLIF(OyuncularDeneme2.oyuncuAd, ''), ', ')), ' ', ''), CHAR(9), ''), CHAR(10), '') AS Oyuncular,Filmler.DegerlendirPuan FROM  Filmler INNER JOIN OyuncularDeneme2 ON OyuncularDeneme2.filmAd = Filmler.FilmAd GROUP BY Filmler.FilmAd,Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim,Filmler.DegerlendirPuan";
            connection.Open();
            da = new SqlDataAdapter(query, connection);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
            dataGridView1.Rows[0].Selected = true;
            filmTik();
            kullaniciNesnesi = KullaniciGetir(tcNo);
            kullaniciNesnesi.ucretHesapla();
        }


        private void listeEkleBtn_Click(object sender, EventArgs e)
        {
            int secilenFilm = dataGridView1.SelectedCells[0].RowIndex;
            Film film = new Film(dataGridView1.Rows[secilenFilm].Cells[0].Value.ToString(), dataGridView1.Rows[secilenFilm].Cells[1].Value.ToString(), dataGridView1.Rows[secilenFilm].Cells[2].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[secilenFilm].Cells[3].Value.ToString()));
            kullaniciNesnesi.izlemeListeEkle(film);
            veriIzlemeListesiEkle(tcNo, film.ad);
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.Visible = true;
            notifyIcon.BalloonTipTitle = "Bildirim";
            notifyIcon.BalloonTipText = "Film İzleme Listesine Başarılı Şekilde Eklenmiştir";
            notifyIcon.ShowBalloonTip(1000);
        }

        private void listeCikarBtn_Click(object sender, EventArgs e)
        {
            int secilenFilm = dataGridView1.SelectedCells[0].RowIndex;
            Film film = new Film(dataGridView1.Rows[secilenFilm].Cells[0].Value.ToString(), dataGridView1.Rows[secilenFilm].Cells[1].Value.ToString(), dataGridView1.Rows[secilenFilm].Cells[2].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[secilenFilm].Cells[3].Value.ToString()));
            kullaniciNesnesi.izlemeListeCikar(film);
            veriIzlemeListesiCıkar(film.ad, kullaniciNesnesi.tcNo);
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.Visible = true;
            notifyIcon.BalloonTipTitle = "Bildirim";
            notifyIcon.BalloonTipText = "Film İzleme Listesinden Başarılı Şekilde Silinmiştir";
            notifyIcon.ShowBalloonTip(1000);
        }

        private void listemBtn_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl1.SelectTab(0);
                string sorgu = "SELECT Filmler.FilmAd,Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim,REPLACE(REPLACE(REPLACE(RTRIM(STRING_AGG(NULLIF(OyuncularDeneme2.oyuncuAd, ''), ', ')), ' ', ''), CHAR(9), ''), CHAR(10), ''),Filmler.DegerlendirPuan AS Oyuncular FROM Filmler INNER JOIN İzlemeListe ON Filmler.FilmAd = İzlemeListe.filmAd INNER JOIN Kullanıcı ON Kullanıcı.tcNo = İzlemeListe.tcNo INNER JOIN OyuncularDeneme2 ON OyuncularDeneme2.filmAd = Filmler.FilmAd WHERE Kullanıcı.tcNo = @tcNo GROUP BY Filmler.FilmAd,Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim,Filmler.DegerlendirPuan ";
                connection.Open();
                da = new SqlDataAdapter(sorgu, connection);
                da.SelectCommand.Parameters.AddWithValue("@tcNo",tcNo);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
                connection.Close();
                filmTik();
                label4.Text = "İzleme Listem";

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void filmTik()
        {
            int secilenFilm = dataGridView1.SelectedCells[0].RowIndex; //Hücrenin satır indeksini verir.
            filmBilgiRichTxt.Text = $"Film Adı: {dataGridView1.Rows[secilenFilm].Cells[0].Value.ToString()}\n";
            filmBilgiRichTxt.Text += $"Yonetmen Adı:{dataGridView1.Rows[secilenFilm].Cells[1].Value.ToString()}\n";
            filmBilgiRichTxt.Text += $"Film Türü:{dataGridView1.Rows[secilenFilm].Cells[2].Value.ToString()}\n";
            filmBilgiRichTxt.Text += $"Yayın Yılı:{dataGridView1.Rows[secilenFilm].Cells[3].Value.ToString()}\n";
            filmBilgiRichTxt.Text += $"Oyuncular:{dataGridView1.Rows[secilenFilm].Cells[5].Value.ToString()}\n";
            filmBilgiRichTxt.Text += $"Film Değerlendirme Puanı: {dataGridView1.Rows[secilenFilm].Cells[6].Value.ToString()}\n";
            pictureBox1.ImageLocation = dataGridView1.Rows[secilenFilm].Cells[4].Value.ToString();
        }

        public void veriIzlemeListesiEkle(int tcKimlik, string filmAd)
        {
            try
            {
                string sorgu = "INSERT INTO İzlemeListe(tcNo, filmAd)VALUES(@tcNo,@filmAd)";
                command = new SqlCommand(sorgu, connection);
                command.Parameters.AddWithValue("@tcNo", tcKimlik);
                command.Parameters.AddWithValue("@filmAd", filmAd);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Veri Tabanına Ekleme İşlemi Başarılı");
                connection.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ekleme İşlemi Gerçekleşmedi");
            }
        }
        public void veriIzlemeListesiCıkar(string silinecekFilmAd, int silenKullanici)
        {
            try
            {
                string sorgu = "DELETE FROM İzlemeListe WHERE filmAd = @filmAd AND tcNo = @tcNo";
                command = new SqlCommand(sorgu, connection);
                command.Parameters.AddWithValue("@filmAd", silinecekFilmAd);
                command.Parameters.AddWithValue("@tcNo", silenKullanici);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Film Silme Başarılı");
                connection.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void veriKullaniciBilgiGuncelle(int degisenTc,int tcNo, string soyAd, string cinsiyet, string sifre, string dogumTarihi,string kullaniciTur, string ad)
        {
            try
            {

                string sorgu = "UPDATE Kullanıcı SET tcNo = @tcNo, Soyad =@soyAd ,Cinsiyet =@cinsiyet, Sifre =@sifre, DogumTarihi = @dogumTarihi,KullaniciTur=@kullaniciTur ,Ad=@ad  WHERE tcNo =@degisenTc";
                command = new SqlCommand(sorgu, connection);
                command.Parameters.AddWithValue("@tcNo", tcNo);
                command.Parameters.AddWithValue("@soyAd", soyAd);
                command.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                command.Parameters.AddWithValue("@sifre", sifre);
                command.Parameters.AddWithValue("@dogumTarihi", dogumTarihi);
                command.Parameters.AddWithValue("@kullaniciTur", kullaniciTur);
                command.Parameters.AddWithValue("@ad", ad);
                command.Parameters.AddWithValue("@degisenTc", degisenTc);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Veri tabanı güncelleme işlemi gerçekleşti");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void veriYorumEkle(int tcNo, string filmAd, string yorum)
        {

            try
            {
                string sorgu = "INSERT INTO Yorumlar(tcNo,filmAd,yorum)VALUES(@tcNo,@filmAd,@yorum)";
                command = new SqlCommand(sorgu, connection);
                command.Parameters.AddWithValue("@tcNo", tcNo);
                command.Parameters.AddWithValue("@filmAd", filmAd);
                command.Parameters.AddWithValue("@yorum", yorum);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Veri Tabanına Ekleme İşlemi Başarılı");
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Veri Tabanı Yorum Hata");
            }

        }
        public void veriPuanGuncelle(double puan, string filmAd)
        {

            try
            {
                string sorgu = "UPDATE Filmler SET DegerlendirPuan = @puan WHERE FilmAd = @filmAd ";
                command = new SqlCommand(sorgu,connection);
                command.Parameters.AddWithValue("@puan", puan);
                command.Parameters.AddWithValue("@filmAd", filmAd);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Veri tabanı puan güncelleme başarılı");
                connection.Close();
            }

            catch( Exception ex )
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void fimleriGoruntuleBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            string query = "SELECT Filmler.FilmAd,Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim,REPLACE(REPLACE(REPLACE(RTRIM(STRING_AGG(NULLIF(OyuncularDeneme2.oyuncuAd, ''), ', ')), ' ', ''), CHAR(9), ''), CHAR(10), '') AS Oyuncular,Filmler.DegerlendirPuan FROM  Filmler INNER JOIN OyuncularDeneme2 ON OyuncularDeneme2.filmAd = Filmler.FilmAd GROUP BY Filmler.FilmAd,Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim,Filmler.DegerlendirPuan";
            connection.Open();
            da = new SqlDataAdapter(query, connection);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
            dataGridView1.Rows[0].Selected = true;
            filmTik();
            label4.Text = "Tüm Filmler";
        }




        public Film sonEklenenFilmVeri()
        {
            using (SqlConnection connection4 = new SqlConnection("server=.; Initial Catalog=nypveri1;Integrated Security=SSPI"))
            {
                Film sonEklenenFilm;
                connection4.Open();
                command = connection4.CreateCommand();
                command.CommandText = "SELECT TOP 1 * FROM Filmler ORDER BY YayinYili DESC;";
                dr = command.ExecuteReader();
                if(dr.Read()) 
                {
                    sonEklenenFilm = new Film(dr["FilmAd"].ToString() , dr["YonetmenAd"].ToString() , dr["FilmTur"].ToString() , Convert.ToInt32(dr["YayinYili"]));
                    return sonEklenenFilm;
                }
                else
                {
                    return null;
                }
            }
        }

        private void filmSıralaBtn_Click(object sender, EventArgs e)
        {
            FilmRapor viewOne = new FilmRapor();
            viewOne.Show();
        }

        private void kullaniciCikisBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void veriIzlemeListeGuncelle(int tcNo, int eskiTcNo)
        {
            try
            {
                string sorgu = "UPDATE İzlemeListe SET tcNo = @tcNo WHERE tcNo = @eskiTcNo ";
                command = new SqlCommand(sorgu, connection);
                command.Parameters.AddWithValue("@tcNo", tcNo);
                command.Parameters.AddWithValue("@eskiTcNo", eskiTcNo);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Veri tabanı izleme liste güncelleme başarılı");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show ("İzleme Liste tc hata!!!");
            }
        }
    }
}

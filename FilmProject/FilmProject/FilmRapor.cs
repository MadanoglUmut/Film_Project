using System;
using System.Collections;
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
    public partial class FilmRapor : Form
    {
        private NotifyIcon notifyIcon;
        SqlConnection connection = new SqlConnection("server=.; Initial Catalog=nypveri1;Integrated Security=SSPI");
        SqlCommand command;
        SqlDataAdapter da;
        SqlDataReader dr;
        public FilmRapor()
        {
            InitializeComponent();
        }

        private void FilmRapor_Load(object sender, EventArgs e)
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
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            filmTik();
        }

        public void filmTik()
        {
            int secilenFilm = dataGridView1.SelectedCells[0].RowIndex; //Hücrenin satır indeksini verir.
            pictureBox1.ImageLocation = dataGridView1.Rows[secilenFilm].Cells[4].Value.ToString();
        }

        private void puanYkskBtn_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT Filmler.FilmAd, Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim,REPLACE(REPLACE(REPLACE(RTRIM(STRING_AGG(NULLIF(OyuncularDeneme2.oyuncuAd, ''), ', ')), ' ', ''), CHAR(9), ''), CHAR(10), '') AS Oyuncular,Filmler.DegerlendirPuan FROM  Filmler INNER JOIN  OyuncularDeneme2 ON OyuncularDeneme2.filmAd = Filmler.FilmAd  GROUP BY Filmler.FilmAd, Filmler.YonetmenAd, Filmler.FilmTur, Filmler.YayinYili, Filmler.FilmResim, Filmler.DegerlendirPuan ORDER BY Filmler.DegerlendirPuan DESC";
            connection.Open();
            da = new SqlDataAdapter(sorgu, connection);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
            dataGridView1.Rows[0].Selected = true;
            filmTik();
        }

        private void puanDskBtn_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT Filmler.FilmAd, Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim,REPLACE(REPLACE(REPLACE(RTRIM(STRING_AGG(NULLIF(OyuncularDeneme2.oyuncuAd, ''), ', ')), ' ', ''), CHAR(9), ''), CHAR(10), '') AS Oyuncular,Filmler.DegerlendirPuan FROM  Filmler INNER JOIN  OyuncularDeneme2 ON OyuncularDeneme2.filmAd = Filmler.FilmAd  GROUP BY Filmler.FilmAd, Filmler.YonetmenAd, Filmler.FilmTur, Filmler.YayinYili, Filmler.FilmResim, Filmler.DegerlendirPuan ORDER BY Filmler.DegerlendirPuan ASC";
            connection.Open();
            da = new SqlDataAdapter(sorgu, connection);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
            dataGridView1.Rows[0].Selected = true;
            filmTik();
        }

        private void degerlendirilenBtn_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT Filmler.FilmAd, Filmler.YonetmenAd,Filmler.FilmTur,Filmler.YayinYili,Filmler.FilmResim,REPLACE(REPLACE(REPLACE(RTRIM(STRING_AGG(NULLIF(OyuncularDeneme2.oyuncuAd, ''), ', ')), ' ', ''), CHAR(9), ''), CHAR(10), '') AS Oyuncular,Filmler.DegerlendirPuan FROM  Filmler INNER JOIN  OyuncularDeneme2 ON OyuncularDeneme2.filmAd = Filmler.FilmAd INNER JOIN Yorumlar on Yorumlar.filmAd=Filmler.FilmAd  GROUP BY Filmler.FilmAd, Filmler.YonetmenAd, Filmler.FilmTur, Filmler.YayinYili, Filmler.FilmResim, Filmler.DegerlendirPuan,Yorumlar.filmAd ORDER BY Filmler.DegerlendirPuan ASC";
            connection.Open();
            da = new SqlDataAdapter(sorgu, connection);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
            dataGridView1.Rows[0].Selected = true;
            filmTik();
        }
    }
}

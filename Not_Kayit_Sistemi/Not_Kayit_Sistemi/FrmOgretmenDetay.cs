using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Not_Kayit_Sistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-RR2PLUS\SQLEXPRESS;Initial Catalog=DBNotKayit;Integrated Security=True");


        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBNotKayitDataSet.TblDers' table. You can move, or remove it, as needed.
            this.tblDersTableAdapter.Fill(this.dBNotKayitDataSet.TblDers);
            gecen();
            Kalan();
        }

        public void gecen()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select count(*) from TblDers WHERE Durum='True' ;", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblGecen.Text = dr[0].ToString();
            }
            
            baglanti.Close();
        }
        public  void Kalan()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select count(*) from TblDers WHERE Durum='False' ;", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblKalan.Text = dr[0].ToString();
            }

            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TblDers(OgrNumara,OgrAd,OgrSoyad) VALUES (@p1,@p2,@p3)",baglanti);
            komut.Parameters.AddWithValue("@p1",mskNumara.Text);
            komut.Parameters.AddWithValue("@p2",txtAd.Text);
            komut.Parameters.AddWithValue("@p3",txtSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tblDersTableAdapter.Fill(this.dBNotKayitDataSet.TblDers);
            gecen();
            Kalan();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            mskNumara.Text= dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtS1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtS2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtS3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ort, s1, s2, s3;
            s1=Convert.ToDouble(txtS1.Text);
            s2= Convert.ToDouble(txtS2.Text);
            s3= Convert.ToDouble(txtS3.Text);
            bool gec;

            ort = (s1 + s2 + s3) / 3;

            if (ort >= 50)
            {
                gec = true;
            }
            else
            {
                gec = false;
            }
           
            lblOrt.Text=ort.ToString("00.00");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TblDers set OgrS1=@p1,OgrS2=@p2,OgrS3=@p3,Ort=@p4,Durum=@p5 Where OgrNumara=@p6",baglanti);
            komut.Parameters.AddWithValue("@p1",txtS1.Text);
            komut.Parameters.AddWithValue("@p2",txtS2.Text);
            komut.Parameters.AddWithValue("@p3",txtS3.Text);
            komut.Parameters.AddWithValue("@p4",decimal.Parse(lblOrt.Text));
            komut.Parameters.AddWithValue("@p5",gec);
            komut.Parameters.AddWithValue("@p6",mskNumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi");
            this.tblDersTableAdapter.Fill(this.dBNotKayitDataSet.TblDers);
            gecen();
            Kalan();
        }
    }
}

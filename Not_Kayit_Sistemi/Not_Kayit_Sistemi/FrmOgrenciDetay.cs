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
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }
        public string ogrNo;
        SqlConnection baglanti=new SqlConnection(@"Data Source=DESKTOP-RR2PLUS\SQLEXPRESS;Initial Catalog=DBNotKayit;Integrated Security=True");
       
        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {
            lblNumara.Text = ogrNo;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TblDers WHERE OgrNumara=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1",ogrNo);
            SqlDataReader dr=komut.ExecuteReader();
            while(dr.Read())
            {
                lblAdSoyad.Text = dr[2].ToString()+" " + dr[3].ToString();
                lblS1.Text = dr[4].ToString();
                lblS2.Text = dr[5].ToString();
                lblS3.Text = dr[6].ToString();
                lblOrt.Text = dr[7].ToString();
                if (dr[8].ToString().Equals("True"))
                {
                    lblDurum.Text = "Geçti";
                }
                else
                {
                    lblDurum.Text = "Kaldı";
                }
                

            }
        }
    }
}

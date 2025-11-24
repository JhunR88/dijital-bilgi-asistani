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

namespace OTEL_BİLGİ_SİSTEMİ
{
    public partial class Form1 : Form
    {
        string connectionString = "Data Source=JAYHUN;Initial Catalog=OTEL BİLGİ SİSTEMİ; Integrated Security = True; TrustServerCertificate=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            string kelime = txtArama.Text.Trim();
            rtbSonuclar.Clear(); // RichTextBox'ı temizle

            if (kelime.Length < 2)
            {
                rtbSonuclar.Text = "Please select minimum 2 characters!";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sorgu = "SELECT Baslik, Icerik FROM Bilgiler WHERE Baslik LIKE @kelime OR Icerik LIKE @kelime OR Etiketler LIKE @kelime";
                SqlCommand cmd = new SqlCommand(sorgu, conn);
                cmd.Parameters.AddWithValue("@kelime", "%" + kelime + "%");

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    // Başlığı KALIN ve MAVİ yap
                    rtbSonuclar.SelectionFont = new Font(rtbSonuclar.Font, FontStyle.Bold);
                    rtbSonuclar.SelectionColor = Color.Blue;
                    rtbSonuclar.AppendText(dr["Baslik"].ToString() + "\n");

                    // İçeriği normal siyah yazı
                    rtbSonuclar.SelectionFont = new Font(rtbSonuclar.Font, FontStyle.Regular);
                    rtbSonuclar.SelectionColor = Color.Black;
                    rtbSonuclar.AppendText(dr["Icerik"].ToString() + "\n\n");
                }
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("MERHABA! Bilgiye mi ihtiyacın var?");
            
        }
    }
}

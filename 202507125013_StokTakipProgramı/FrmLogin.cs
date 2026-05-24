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

namespace _202507125013_StokTakipProgramı
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {

            InitializeComponent();
            ModernTasarim();
            this.AcceptButton = btnLogin;
        }

        private void ModernTasarim()
        {
            this.Text = "Stok Takip Programı";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(430, 500);
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            stoktakiplabel.Text = "Stok Takip Programı";
            stoktakiplabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            stoktakiplabel.ForeColor = Color.FromArgb(31, 78, 121);
            stoktakiplabel.AutoSize = false;
            stoktakiplabel.TextAlign = ContentAlignment.MiddleCenter;
            stoktakiplabel.Size = new Size(350, 45);
            stoktakiplabel.Location = new Point(30, 70);

            kullaniciAdilbl.Text = "Kullanıcı Adı";
            kullaniciAdilbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            kullaniciAdilbl.Location = new Point(85, 150);

            txtUserName.Font = new Font("Segoe UI", 11F);
            txtUserName.Size = new Size(250, 30);
            txtUserName.Location = new Point(85, 175);

            sifrelbl.Text = "Şifre";
            sifrelbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            sifrelbl.Location = new Point(85, 225);

            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Size = new Size(250, 30);
            txtPassword.Location = new Point(85, 250);

            btnLogin.Text = "Giriş Yap";
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.Size = new Size(250, 45);
            btnLogin.Location = new Point(85, 320);
            btnLogin.BackColor = Color.FromArgb(31, 78, 121);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;

            btnLogin.MouseEnter += (s, e) =>
            {
                btnLogin.BackColor = Color.FromArgb(24, 60, 95);
            };

            btnLogin.MouseLeave += (s, e) =>
            {
                btnLogin.BackColor = Color.FromArgb(31, 78, 121);
            };
        }

        private void girisbutton_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş bırakılamaz.");
                return;
            }

            SqlConnectionClass sql = new SqlConnectionClass();

            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Users WHERE UserName=@p1 AND Password=@p2",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", txtUserName.Text);
            cmd.Parameters.AddWithValue("@p2", txtPassword.Text);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                FrmMain frm = new FrmMain();
                frm.userRole = dr["Role"].ToString();

                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış");
            }

            sql.connection.Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
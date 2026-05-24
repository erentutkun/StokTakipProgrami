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
    public partial class FrmUsers : Form
    {
        public FrmUsers()
        {
            InitializeComponent();
        }
        SqlConnectionClass sql = new SqlConnectionClass();

        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Users", sql.connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmUsers_Load(object sender, EventArgs e)
        {
            ModernTasarim();
            Listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            txtUserName.Text = dataGridView1.Rows[e.RowIndex].Cells["UserName"].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells["Password"].Value.ToString();
            cmbRole.Text = dataGridView1.Rows[e.RowIndex].Cells["Role"].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Users(UserName, Password, Role) VALUES(@p1, @p2, @p3)",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", txtUserName.Text);
            cmd.Parameters.AddWithValue("@p2", txtPassword.Text);
            cmd.Parameters.AddWithValue("@p3", cmbRole.Text);

            cmd.ExecuteNonQuery();
            sql.connection.Close();

            MessageBox.Show("Kullanıcı eklendi.");
            Listele();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "UPDATE Users SET UserName=@p1, Password=@p2, Role=@p3 WHERE Id=@p4",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", txtUserName.Text);
            cmd.Parameters.AddWithValue("@p2", txtPassword.Text);
            cmd.Parameters.AddWithValue("@p3", cmbRole.Text);
            cmd.Parameters.AddWithValue("@p4", txtId.Text);

            cmd.ExecuteNonQuery();
            sql.connection.Close();

            MessageBox.Show("Kullanıcı güncellendi.");
            Listele();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "DELETE FROM Users WHERE Id=@p1",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", txtId.Text);

            cmd.ExecuteNonQuery();
            sql.connection.Close();

            MessageBox.Show("Kullanıcı silindi.");
            Listele();
        }

        private void ButonStil(Button btn, string text, Point location, Color color)
        {
            btn.Text = text;
            btn.Size = new Size(85, 40);
            btn.Location = location;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }
        private void ModernTasarim()
        {
            this.Text = "Kullanıcı Yönetimi";
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblBaslik = new Label();
            lblBaslik.Text = "Kullanıcı Yönetimi";
            lblBaslik.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(24, 35, 56);
            lblBaslik.AutoSize = true;
            lblBaslik.Location = new Point(35, 25);
            this.Controls.Add(lblBaslik);

            Label lblAciklama = new Label();
            lblAciklama.Text = "Sisteme giriş yapacak kullanıcıları buradan yönetin.";
            lblAciklama.Font = new Font("Segoe UI", 11);
            lblAciklama.ForeColor = Color.Gray;
            lblAciklama.AutoSize = true;
            lblAciklama.Location = new Point(38, 70);
            this.Controls.Add(lblAciklama);

            Panel pnlForm = new Panel();
            pnlForm.Size = new Size(330, 420);
            pnlForm.Location = new Point(35, 120);
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlForm);

            Label lblKartBaslik = new Label();
            lblKartBaslik.Text = "Kullanıcı Bilgileri";
            lblKartBaslik.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblKartBaslik.ForeColor = Color.FromArgb(31, 78, 121);
            lblKartBaslik.AutoSize = true;
            lblKartBaslik.Location = new Point(25, 25);
            pnlForm.Controls.Add(lblKartBaslik);

            Kadilbl.Parent = pnlForm;
            Kadilbl.Text = "Kullanıcı Adı";
            Kadilbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            Kadilbl.ForeColor = Color.FromArgb(50, 50, 50);
            Kadilbl.Location = new Point(30, 90);

            txtUserName.Parent = pnlForm;
            txtUserName.Font = new Font("Segoe UI", 11);
            txtUserName.Size = new Size(260, 30);
            txtUserName.Location = new Point(30, 115);

            sifrelbl.Parent = pnlForm;
            sifrelbl.Text = "Şifre";
            sifrelbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            sifrelbl.ForeColor = Color.FromArgb(50, 50, 50);
            sifrelbl.Location = new Point(30, 165);

            txtPassword.Parent = pnlForm;
            txtPassword.Font = new Font("Segoe UI", 11);
            txtPassword.Size = new Size(260, 30);
            txtPassword.Location = new Point(30, 190);
            txtPassword.UseSystemPasswordChar = true;

            rollbl.Parent = pnlForm;
            rollbl.Text = "Rol";
            rollbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            rollbl.ForeColor = Color.FromArgb(50, 50, 50);
            rollbl.Location = new Point(30, 240);

            cmbRole.Parent = pnlForm;
            cmbRole.Font = new Font("Segoe UI", 11);
            cmbRole.Size = new Size(260, 30);
            cmbRole.Location = new Point(30, 265);
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Kullanıcı");
            cmbRole.SelectedIndex = 0;

            btnAdd.Parent = pnlForm;
            btnUpdate.Parent = pnlForm;
            btnDelete.Parent = pnlForm;

            ButonStil(btnAdd, "➕ Ekle", new Point(30, 325), Color.FromArgb(46, 139, 87));
            ButonStil(btnUpdate, "✏️ Güncelle", new Point(115, 325), Color.FromArgb(0, 122, 204));
            ButonStil(btnDelete, "🗑 Sil", new Point(215, 325), Color.FromArgb(180, 50, 50));

            Panel pnlGrid = new Panel();
            pnlGrid.Size = new Size(680, 420);
            pnlGrid.Location = new Point(390, 120);
            pnlGrid.BackColor = Color.White;
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlGrid);

            Label lblListe = new Label();
            lblListe.Text = "Kullanıcı Listesi";
            lblListe.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblListe.ForeColor = Color.FromArgb(31, 78, 121);
            lblListe.AutoSize = true;
            lblListe.Location = new Point(20, 20);
            pnlGrid.Controls.Add(lblListe);

            dataGridView1.Parent = pnlGrid;
            dataGridView1.Location = new Point(20, 65);
            dataGridView1.Size = new Size(640, 330);
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 35, 56);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(31, 78, 121);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.RowTemplate.Height = 32;

            txtId.Visible = false;
        }
    }
}

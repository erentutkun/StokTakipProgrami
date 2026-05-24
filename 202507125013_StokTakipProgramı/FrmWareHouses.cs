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
    public partial class FrmWareHouses : Form
    {
        public FrmWareHouses()
        {
            InitializeComponent();
        }
        SqlConnectionClass sql = new SqlConnectionClass();

        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM WareHouses", sql.connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void FrmWareHouses_Load(object sender, EventArgs e)
        {
            ModernTasarim();
            Listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            txtWarehouseName.Text = dataGridView1.Rows[e.RowIndex].Cells["WarehouseName"].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells["Address"].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO WareHouses(WarehouseName, Address) VALUES(@p1,@p2)",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", txtWarehouseName.Text);
            cmd.Parameters.AddWithValue("@p2", txtAddress.Text);

            cmd.ExecuteNonQuery();
            sql.connection.Close();

            MessageBox.Show("Depo eklendi.");
            Listele();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "UPDATE WareHouses SET WarehouseName=@p1, Address=@p2 WHERE Id=@p3",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", txtWarehouseName.Text);
            cmd.Parameters.AddWithValue("@p2", txtAddress.Text);
            cmd.Parameters.AddWithValue("@p3", txtId.Text);

            cmd.ExecuteNonQuery();
            sql.connection.Close();

            MessageBox.Show("Depo güncellendi.");
            Listele();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "DELETE FROM WareHouses WHERE Id=@p1",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", txtId.Text);

            cmd.ExecuteNonQuery();
            sql.connection.Close();

            MessageBox.Show("Depo silindi.");
            Listele();
        }
        private void ModernTasarim()
        {
            this.Text = "Depo Yönetimi";
            this.Size = new Size(1050, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblBaslik = new Label();
            lblBaslik.Text = "Depo Yönetimi";
            lblBaslik.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(24, 35, 56);
            lblBaslik.AutoSize = true;
            lblBaslik.Location = new Point(35, 25);
            this.Controls.Add(lblBaslik);

            Label lblAciklama = new Label();
            lblAciklama.Text = "Depo bilgilerini ekleyin, güncelleyin ve sistemdeki depo listesini yönetin.";
            lblAciklama.Font = new Font("Segoe UI", 11);
            lblAciklama.ForeColor = Color.Gray;
            lblAciklama.AutoSize = true;
            lblAciklama.Location = new Point(38, 70);
            this.Controls.Add(lblAciklama);

            Panel pnlForm = new Panel();
            pnlForm.Size = new Size(320, 360);
            pnlForm.Location = new Point(35, 130);
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlForm);

            Label lblKartBaslik = new Label();
            lblKartBaslik.Text = "Depo Bilgileri";
            lblKartBaslik.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblKartBaslik.ForeColor = Color.FromArgb(31, 78, 121);
            lblKartBaslik.AutoSize = true;
            lblKartBaslik.Location = new Point(25, 25);
            pnlForm.Controls.Add(lblKartBaslik);

            LabelOlustur(pnlForm, "Depo Adı", new Point(30, 90));
            TextBoxStil(txtWarehouseName, pnlForm, new Point(30, 115));

            LabelOlustur(pnlForm, "Adres", new Point(30, 165));
            TextBoxStil(txtAddress, pnlForm, new Point(30, 190));

            txtId.Visible = false;

            btnAdd.Parent = pnlForm;
            btnUpdate.Parent = pnlForm;
            btnDelete.Parent = pnlForm;

            ButonStil(btnAdd, "➕ Ekle", new Point(30, 270), Color.FromArgb(46, 139, 87));
            ButonStil(btnUpdate, "✏ Güncelle", new Point(115, 270), Color.FromArgb(0, 122, 204));
            ButonStil(btnDelete, "🗑 Sil", new Point(215, 270), Color.FromArgb(180, 50, 50));

            Panel pnlGrid = new Panel();
            pnlGrid.Size = new Size(620, 360);
            pnlGrid.Location = new Point(380, 130);
            pnlGrid.BackColor = Color.White;
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlGrid);

            Label lblListe = new Label();
            lblListe.Text = "Depo Listesi";
            lblListe.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblListe.ForeColor = Color.FromArgb(31, 78, 121);
            lblListe.AutoSize = true;
            lblListe.Location = new Point(20, 20);
            pnlGrid.Controls.Add(lblListe);

            dataGridView1.Parent = pnlGrid;
            dataGridView1.Location = new Point(20, 70);
            dataGridView1.Size = new Size(580, 260);
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
        }
        private void TextBoxStil(TextBox txt, Panel parent, Point location)
        {
            txt.Parent = parent;
            txt.Font = new Font("Segoe UI", 11);
            txt.Size = new Size(260, 30);
            txt.Location = location;
            txt.BorderStyle = BorderStyle.FixedSingle;
        }

        private void LabelOlustur(Panel parent, string text, Point location)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(50, 50, 50);
            lbl.AutoSize = true;
            lbl.Location = location;
            parent.Controls.Add(lbl);
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
    }
 }

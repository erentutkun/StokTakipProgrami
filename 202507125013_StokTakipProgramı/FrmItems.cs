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
    public partial class FrmItems : Form
    {
        public FrmItems()
        {
            InitializeComponent();
        }
            SqlConnectionClass sql = new SqlConnectionClass();

            void Listele()
            {
            SqlDataAdapter da = new SqlDataAdapter(
                   "SELECT Id, ItemCode, ItemName, Category, PurchasePrice, SalePrice FROM Items",
                   sql.connection
               );

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmItems_Load(object sender, EventArgs e)
        {
            ModernTasarim();
            Listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            txtItemCode.Text = dataGridView1.Rows[e.RowIndex].Cells["ItemCode"].Value.ToString();
            txtItemName.Text = dataGridView1.Rows[e.RowIndex].Cells["ItemName"].Value.ToString();
            txtCategory.Text = dataGridView1.Rows[e.RowIndex].Cells["Category"].Value.ToString();
            txtPurchasePrice.Text = dataGridView1.Rows[e.RowIndex].Cells["PurchasePrice"].Value.ToString();
            txtSalePrice.Text = dataGridView1.Rows[e.RowIndex].Cells["SalePrice"].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtItemCode.Text == "" || txtItemName.Text == "" ||
        txtPurchasePrice.Text == "" || txtSalePrice.Text == "")
            {
                MessageBox.Show("Lütfen zorunlu alanları doldurunuz.");
                return;
            }

            decimal purchasePrice, salePrice;

            if (!decimal.TryParse(txtPurchasePrice.Text, out purchasePrice) ||
                !decimal.TryParse(txtSalePrice.Text, out salePrice))
            {
                MessageBox.Show("Alış ve satış fiyatı sayısal olmalıdır.");
                return;
            }

            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Items(ItemCode, ItemName, Category, PurchasePrice, SalePrice) VALUES(@p1,@p2,@p3,@p4,@p5)",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", txtItemCode.Text);
            cmd.Parameters.AddWithValue("@p2", txtItemName.Text);
            cmd.Parameters.AddWithValue("@p3", txtCategory.Text);
            cmd.Parameters.AddWithValue("@p4", purchasePrice);
            cmd.Parameters.AddWithValue("@p5", salePrice);

            cmd.ExecuteNonQuery();
            sql.connection.Close();

            MessageBox.Show("Ürün eklendi.");
            Listele();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Lütfen güncellenecek ürünü seçiniz.");
                return;
            }

            decimal purchasePrice, salePrice;

            if (!decimal.TryParse(txtPurchasePrice.Text, out purchasePrice) ||
                !decimal.TryParse(txtSalePrice.Text, out salePrice))
            {
                MessageBox.Show("Alış ve satış fiyatı sayısal olmalıdır.");
                return;
            }

            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "UPDATE Items SET ItemCode=@p1, ItemName=@p2, Category=@p3, PurchasePrice=@p4, SalePrice=@p5 WHERE Id=@p6",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", txtItemCode.Text);
            cmd.Parameters.AddWithValue("@p2", txtItemName.Text);
            cmd.Parameters.AddWithValue("@p3", txtCategory.Text);
            cmd.Parameters.AddWithValue("@p4", purchasePrice);
            cmd.Parameters.AddWithValue("@p5", salePrice);
            cmd.Parameters.AddWithValue("@p6", txtId.Text);

            cmd.ExecuteNonQuery();
            sql.connection.Close();

            MessageBox.Show("Ürün güncellendi.");
            Listele();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "DELETE FROM Items WHERE Id=@p1",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", txtId.Text);

            cmd.ExecuteNonQuery();
            sql.connection.Close();

            MessageBox.Show("Ürün silindi.");
            Listele();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter(
       "SELECT Id, ItemCode, ItemName, Category, PurchasePrice, SalePrice FROM Items " +
       "WHERE ItemName LIKE @p1 OR ItemCode LIKE @p1 OR Category LIKE @p1",
       sql.connection
   );

            da.SelectCommand.Parameters.AddWithValue("@p1", "%" + txtSearch.Text + "%");

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void ModernTasarim()
        {
            this.Text = "Ürün Yönetimi";
            this.Size = new Size(1150, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblBaslik = new Label();
            lblBaslik.Text = "Ürün Yönetimi";
            lblBaslik.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(24, 35, 56);
            lblBaslik.AutoSize = true;
            lblBaslik.Location = new Point(35, 25);
            this.Controls.Add(lblBaslik);

            Label lblAciklama = new Label();
            lblAciklama.Text = "Ürün kartlarını, fiyat bilgilerini ve kategorileri buradan yönetin.";
            lblAciklama.Font = new Font("Segoe UI", 11);
            lblAciklama.ForeColor = Color.Gray;
            lblAciklama.AutoSize = true;
            lblAciklama.Location = new Point(38, 70);
            this.Controls.Add(lblAciklama);

            Panel pnlForm = new Panel();
            pnlForm.Size = new Size(340, 480);
            pnlForm.Location = new Point(35, 120);
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlForm);

            Label lblKartBaslik = new Label();
            lblKartBaslik.Text = "Ürün Bilgileri";
            lblKartBaslik.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblKartBaslik.ForeColor = Color.FromArgb(31, 78, 121);
            lblKartBaslik.AutoSize = true;
            lblKartBaslik.Location = new Point(25, 20);
            pnlForm.Controls.Add(lblKartBaslik);

            TextBoxStil(txtItemCode, pnlForm, new Point(30, 90));
            LabelOlustur(pnlForm, "Ürün Kodu", new Point(30, 65));

            TextBoxStil(txtItemName, pnlForm, new Point(30, 155));
            LabelOlustur(pnlForm, "Ürün Adı", new Point(30, 130));

            TextBoxStil(txtCategory, pnlForm, new Point(30, 220));
            LabelOlustur(pnlForm, "Kategori", new Point(30, 195));

            TextBoxStil(txtPurchasePrice, pnlForm, new Point(30, 285));
            LabelOlustur(pnlForm, "Alış Fiyatı", new Point(30, 260));

            TextBoxStil(txtSalePrice, pnlForm, new Point(30, 350));
            LabelOlustur(pnlForm, "Satış Fiyatı", new Point(30, 325));

            txtUnitPrice.Visible = false;
            txtId.Visible = false;

            btnAdd.Parent = pnlForm;
            btnUpdate.Parent = pnlForm;
            btnDelete.Parent = pnlForm;

            ButonStil(btnAdd, "➕ Ekle", new Point(30, 415), Color.FromArgb(46, 139, 87));
            ButonStil(btnUpdate, "✏ Güncelle", new Point(125, 415), Color.FromArgb(0, 122, 204));
            ButonStil(btnDelete, "🗑 Sil", new Point(235, 415), Color.FromArgb(180, 50, 50));

            Panel pnlGrid = new Panel();
            pnlGrid.Size = new Size(720, 480);
            pnlGrid.Location = new Point(400, 120);
            pnlGrid.BackColor = Color.White;
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlGrid);

            Label lblListe = new Label();
            lblListe.Text = "Ürün Listesi";
            lblListe.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblListe.ForeColor = Color.FromArgb(31, 78, 121);
            lblListe.AutoSize = true;
            lblListe.Location = new Point(20, 20);
            pnlGrid.Controls.Add(lblListe);

            label1.Parent = pnlGrid;
            label1.Text = "Ürün Ara";
            label1.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(50, 50, 50);
            label1.Location = new Point(420, 28);

            txtSearch.Parent = pnlGrid;
            txtSearch.Font = new Font("Segoe UI", 10);
            txtSearch.Size = new Size(180, 28);
            txtSearch.Location = new Point(500, 25);

            dataGridView1.Parent = pnlGrid;
            dataGridView1.Location = new Point(20, 75);
            dataGridView1.Size = new Size(680, 380);
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

            label2.Visible = false;
            label3.Visible = false;
        }
        private void TextBoxStil(TextBox txt, Panel parent, Point location)
        {
            txt.Parent = parent;
            txt.Font = new Font("Segoe UI", 11);
            txt.Size = new Size(280, 30);
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

using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace _202507125013_StokTakipProgramı
{
    public partial class FrmStockSearch : Form
    {
        SqlConnectionClass sql = new SqlConnectionClass();

        public FrmStockSearch()
        {
            InitializeComponent();
        }

        private void FrmStockSearch_Load(object sender, EventArgs e)
        {
            ModernTasarim();
            StoklariGetir();
        }

        private void ModernTasarim()
        {
            this.Text = "Stok Sorgulama";
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblBaslik = new Label();
            lblBaslik.Text = "Stok Sorgulama";
            lblBaslik.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(24, 35, 56);
            lblBaslik.AutoSize = true;
            lblBaslik.Location = new Point(35, 25);
            this.Controls.Add(lblBaslik);

            Label lblAciklama = new Label();
            lblAciklama.Text = "Ürünlerin hangi depoda ne kadar stoğu olduğunu buradan sorgulayabilirsiniz.";
            lblAciklama.Font = new Font("Segoe UI", 11);
            lblAciklama.ForeColor = Color.Gray;
            lblAciklama.AutoSize = true;
            lblAciklama.Location = new Point(38, 70);
            this.Controls.Add(lblAciklama);

            Panel pnlSearch = new Panel();
            pnlSearch.Size = new Size(1020, 85);
            pnlSearch.Location = new Point(35, 120);
            pnlSearch.BackColor = Color.White;
            pnlSearch.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlSearch);

            label1.Parent = pnlSearch;
            label1.Text = "Ürün / Depo Ara";
            label1.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(50, 50, 50);
            label1.AutoSize = true;
            label1.Location = new Point(25, 30);

            txtSearch.Parent = pnlSearch;
            txtSearch.Font = new Font("Segoe UI", 12);
            txtSearch.Size = new Size(350, 30);
            txtSearch.Location = new Point(165, 27);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.TextChanged += txtSearch_TextChanged;

            Label lblBilgi = new Label();
            lblBilgi.Text = "Ürün adı, ürün kodu, kategori veya depo adı yazabilirsiniz.";
            lblBilgi.Font = new Font("Segoe UI", 10);
            lblBilgi.ForeColor = Color.Gray;
            lblBilgi.AutoSize = true;
            lblBilgi.Location = new Point(540, 31);
            pnlSearch.Controls.Add(lblBilgi);

            Panel pnlGrid = new Panel();
            pnlGrid.Size = new Size(1020, 360);
            pnlGrid.Location = new Point(35, 230);
            pnlGrid.BackColor = Color.White;
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlGrid);

            Label lblListe = new Label();
            lblListe.Text = "Depo Bazlı Stok Listesi";
            lblListe.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblListe.ForeColor = Color.FromArgb(31, 78, 121);
            lblListe.AutoSize = true;
            lblListe.Location = new Point(20, 20);
            pnlGrid.Controls.Add(lblListe);

            dataGridView1.Parent = pnlGrid;
            dataGridView1.Location = new Point(20, 70);
            dataGridView1.Size = new Size(980, 260);
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 35, 56);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(31, 78, 121);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.RowTemplate.Height = 32;
        }

        private void StoklariGetir()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT " +
                "Items.ItemCode AS [Ürün Kodu], " +
                "Items.ItemName AS [Ürün Adı], " +
                "Items.Category AS [Kategori], " +
                "WareHouses.WarehouseName AS [Depo], " +
                "WareHouses.Address AS [Depo Adresi], " +
                "Stocks.Quantity AS [Stok Miktarı] " +
                "FROM Stocks " +
                "INNER JOIN Items ON Stocks.ItemId = Items.Id " +
                "INNER JOIN WareHouses ON Stocks.WarehouseId = WareHouses.Id " +
                "ORDER BY Items.ItemName, WareHouses.WarehouseName",
                sql.connection
            );

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void StokAra()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT " +
                "Items.ItemCode AS [Ürün Kodu], " +
                "Items.ItemName AS [Ürün Adı], " +
                "Items.Category AS [Kategori], " +
                "WareHouses.WarehouseName AS [Depo], " +
                "WareHouses.Address AS [Depo Adresi], " +
                "Stocks.Quantity AS [Stok Miktarı] " +
                "FROM Stocks " +
                "INNER JOIN Items ON Stocks.ItemId = Items.Id " +
                "INNER JOIN WareHouses ON Stocks.WarehouseId = WareHouses.Id " +
                "WHERE Items.ItemName LIKE @search " +
                "OR Items.ItemCode LIKE @search " +
                "OR Items.Category LIKE @search " +
                "OR WareHouses.WarehouseName LIKE @search " +
                "ORDER BY Items.ItemName, WareHouses.WarehouseName",
                sql.connection
            );

            da.SelectCommand.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                StoklariGetir();
            }
            else
            {
                StokAra();
            }
        }
    }
}
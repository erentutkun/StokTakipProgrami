using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace _202507125013_StokTakipProgramı
{
    public partial class FrmReports : Form
    {
        SqlConnectionClass sql = new SqlConnectionClass();

        DataGridView dgvReport = new DataGridView();
        DateTimePicker dtStart = new DateTimePicker();
        DateTimePicker dtEnd = new DateTimePicker();

        public FrmReports()
        {
            InitializeComponent();
        }

        private void FrmReports_Load(object sender, EventArgs e)
        {
            ModernTasarim();
            RaporlariGetir();
            SatisRaporuGetir();
        }

        private void ModernTasarim()
        {
            this.Text = "Raporlar";
            this.Size = new Size(1250, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            foreach (Control c in this.Controls)
                c.Visible = false;

            Label lblBaslik = new Label();
            lblBaslik.Text = "Raporlar";
            lblBaslik.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(24, 35, 56);
            lblBaslik.AutoSize = true;
            lblBaslik.Location = new Point(35, 25);
            this.Controls.Add(lblBaslik);

            Label lblAciklama = new Label();
            lblAciklama.Text = "Satış, stok, müşteri, kâr-zarar ve kritik stok analizlerinizi buradan takip edin.";
            lblAciklama.Font = new Font("Segoe UI", 11);
            lblAciklama.ForeColor = Color.Gray;
            lblAciklama.AutoSize = true;
            lblAciklama.Location = new Point(38, 75);
            this.Controls.Add(lblAciklama);

            Panel pnlCards = new Panel();
            pnlCards.Location = new Point(35, 120);
            pnlCards.Size = new Size(1160, 150);
            pnlCards.BackColor = Color.Transparent;
            this.Controls.Add(pnlCards);

            KartHazirla(lblTotalItems, pnlCards, "Toplam Ürün", "0", "📦", new Point(0, 0), Color.FromArgb(0, 122, 204));
            KartHazirla(lblTotalStock, pnlCards, "Toplam Stok", "0", "🏬", new Point(235, 0), Color.FromArgb(46, 139, 87));
            KartHazirla(lblCriticalStock, pnlCards, "Kritik Stok", "0", "⚠", new Point(470, 0), Color.FromArgb(220, 120, 40));
            KartHazirla(lblTotalInvoice, pnlCards, "Fatura", "0", "🧾", new Point(705, 0), Color.FromArgb(120, 80, 180));
            KartHazirla(lblProfit, pnlCards, "Tahmini Kâr", "0 TL", "💰", new Point(940, 0), Color.FromArgb(24, 35, 56));

            Panel pnlFilter = new Panel();
            pnlFilter.Location = new Point(35, 290);
            pnlFilter.Size = new Size(1160, 80);
            pnlFilter.BackColor = Color.White;
            pnlFilter.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlFilter);

            Label lblTarih = new Label();
            lblTarih.Text = "Tarih Aralığı";
            lblTarih.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblTarih.ForeColor = Color.FromArgb(24, 35, 56);
            lblTarih.Location = new Point(20, 27);
            lblTarih.AutoSize = true;
            pnlFilter.Controls.Add(lblTarih);

            dtStart.Parent = pnlFilter;
            dtStart.Font = new Font("Segoe UI", 10);
            dtStart.Location = new Point(130, 25);
            dtStart.Size = new Size(180, 25);
            dtStart.Value = DateTime.Now.AddMonths(-1);

            dtEnd.Parent = pnlFilter;
            dtEnd.Font = new Font("Segoe UI", 10);
            dtEnd.Location = new Point(330, 25);
            dtEnd.Size = new Size(180, 25);
            dtEnd.Value = DateTime.Now;

            Button btnSatis = RaporButonu("Satış Raporu", new Point(540, 20), Color.FromArgb(0, 122, 204));
            btnSatis.Click += (s, e) => SatisRaporuGetir();
            pnlFilter.Controls.Add(btnSatis);

            Button btnKarZarar = RaporButonu("Kâr-Zarar", new Point(670, 20), Color.FromArgb(46, 139, 87));
            btnKarZarar.Click += (s, e) => KarZararRaporuGetir();
            pnlFilter.Controls.Add(btnKarZarar);

            Button btnMusteri = RaporButonu("Müşteri Raporu", new Point(800, 20), Color.FromArgb(120, 80, 180));
            btnMusteri.Click += (s, e) => MusteriRaporuGetir();
            pnlFilter.Controls.Add(btnMusteri);

            Button btnStok = RaporButonu("Stok Raporu", new Point(930, 20), Color.FromArgb(220, 120, 40));
            btnStok.Click += (s, e) => StokRaporuGetir();
            pnlFilter.Controls.Add(btnStok);

            Button btnKritik = RaporButonu("Kritik Stok", new Point(1040, 20), Color.FromArgb(180, 50, 50));
            btnKritik.Click += (s, e) => KritikStokRaporuGetir();
            pnlFilter.Controls.Add(btnKritik);

            dgvReport.Location = new Point(35, 390);
            dgvReport.Size = new Size(1160, 260);
            dgvReport.BackgroundColor = Color.White;
            dgvReport.BorderStyle = BorderStyle.FixedSingle;
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReport.MultiSelect = false;
            dgvReport.ReadOnly = true;
            dgvReport.RowHeadersVisible = false;
            dgvReport.EnableHeadersVisualStyles = false;
            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 35, 56);
            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvReport.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvReport.DefaultCellStyle.SelectionBackColor = Color.FromArgb(31, 78, 121);
            dgvReport.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvReport.RowTemplate.Height = 32;
            this.Controls.Add(dgvReport);
        }

        private void RaporlariGetir()
        {
            sql.connection.Open();

            lblTotalItems.Text = Convert.ToString(new SqlCommand("SELECT COUNT(*) FROM Items", sql.connection).ExecuteScalar());
            lblTotalStock.Text = Convert.ToString(new SqlCommand("SELECT ISNULL(SUM(Quantity),0) FROM Stocks", sql.connection).ExecuteScalar());
            lblCriticalStock.Text = Convert.ToString(new SqlCommand("SELECT COUNT(*) FROM Stocks WHERE Quantity <= 10", sql.connection).ExecuteScalar());
            lblTotalInvoice.Text = Convert.ToString(new SqlCommand("SELECT COUNT(*) FROM Invoices", sql.connection).ExecuteScalar());

            decimal satis = Convert.ToDecimal(new SqlCommand(
                "SELECT ISNULL(SUM(TotalPrice),0) FROM Invoices WHERE InvoiceType=N'Çıkış'",
                sql.connection).ExecuteScalar());

            decimal maliyet = Convert.ToDecimal(new SqlCommand(
                "SELECT ISNULL(SUM(Invoices.Quantity * Items.PurchasePrice),0) " +
                "FROM Invoices INNER JOIN Items ON Invoices.ItemId = Items.Id " +
                "WHERE Invoices.InvoiceType=N'Çıkış'",
                sql.connection).ExecuteScalar());

            lblProfit.Text = (satis - maliyet).ToString("N2") + " TL";

            sql.connection.Close();
        }

        private void SatisRaporuGetir()
        {
            string query =
                "SELECT Invoices.InvoiceDate AS [Tarih], Invoices.CustomerName AS [Müşteri], " +
                "Items.ItemName AS [Ürün], WareHouses.WarehouseName AS [Depo], " +
                "Invoices.Quantity AS [Adet], Invoices.UnitPrice AS [Birim Fiyat], " +
                "Invoices.TotalPrice AS [Toplam Tutar] " +
                "FROM Invoices " +
                "INNER JOIN Items ON Invoices.ItemId = Items.Id " +
                "INNER JOIN WareHouses ON Invoices.WarehouseId = WareHouses.Id " +
                "WHERE Invoices.InvoiceType=N'Çıkış' AND Invoices.InvoiceDate BETWEEN @start AND @end " +
                "ORDER BY Invoices.InvoiceDate DESC";

            RaporDoldur(query);
        }

        private void KarZararRaporuGetir()
        {
            string query =
                "SELECT Items.ItemName AS [Ürün], " +
                "SUM(Invoices.Quantity) AS [Satılan Adet], " +
                "SUM(Invoices.Quantity * Items.PurchasePrice) AS [Alış Maliyeti], " +
                "SUM(Invoices.TotalPrice) AS [Satış Tutarı], " +
                "SUM(Invoices.TotalPrice) - SUM(Invoices.Quantity * Items.PurchasePrice) AS [Kâr / Zarar] " +
                "FROM Invoices " +
                "INNER JOIN Items ON Invoices.ItemId = Items.Id " +
                "WHERE Invoices.InvoiceType=N'Çıkış' AND Invoices.InvoiceDate BETWEEN @start AND @end " +
                "GROUP BY Items.ItemName " +
                "ORDER BY [Kâr / Zarar] DESC";

            RaporDoldur(query);
        }

        private void MusteriRaporuGetir()
        {
            string query =
                "SELECT CustomerName AS [Müşteri], " +
                "COUNT(*) AS [Fatura Sayısı], " +
                "SUM(Quantity) AS [Toplam Ürün Adedi], " +
                "SUM(TotalPrice) AS [Toplam Harcama] " +
                "FROM Invoices " +
                "WHERE InvoiceType=N'Çıkış' AND InvoiceDate BETWEEN @start AND @end " +
                "GROUP BY CustomerName " +
                "ORDER BY [Toplam Harcama] DESC";

            RaporDoldur(query);
        }

        private void StokRaporuGetir()
        {
            string query =
                "SELECT Items.ItemCode AS [Ürün Kodu], Items.ItemName AS [Ürün], " +
                "Items.Category AS [Kategori], WareHouses.WarehouseName AS [Depo], " +
                "Stocks.Quantity AS [Stok Miktarı], Items.PurchasePrice AS [Alış Fiyatı], " +
                "Items.SalePrice AS [Satış Fiyatı] " +
                "FROM Stocks " +
                "INNER JOIN Items ON Stocks.ItemId = Items.Id " +
                "INNER JOIN WareHouses ON Stocks.WarehouseId = WareHouses.Id " +
                "ORDER BY Stocks.Quantity ASC";

            RaporDoldur(query, false);
        }

        private void KritikStokRaporuGetir()
        {
            string query =
                "SELECT Items.ItemCode AS [Ürün Kodu], Items.ItemName AS [Ürün], " +
                "WareHouses.WarehouseName AS [Depo], Stocks.Quantity AS [Kalan Stok] " +
                "FROM Stocks " +
                "INNER JOIN Items ON Stocks.ItemId = Items.Id " +
                "INNER JOIN WareHouses ON Stocks.WarehouseId = WareHouses.Id " +
                "WHERE Stocks.Quantity <= 10 " +
                "ORDER BY Stocks.Quantity ASC";

            RaporDoldur(query, false);
        }

        private void RaporDoldur(string query, bool tarihli = true)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, sql.connection);

            if (tarihli)
            {
                da.SelectCommand.Parameters.AddWithValue("@start", dtStart.Value.Date);
                da.SelectCommand.Parameters.AddWithValue("@end", dtEnd.Value.Date.AddDays(1).AddSeconds(-1));
            }

            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvReport.DataSource = dt;
        }

        private void KartHazirla(Label label, Panel parent, string baslik, string deger, string ikon, Point location, Color renk)
        {
            Panel kart = new Panel();
            kart.Size = new Size(215, 120);
            kart.Location = location;
            kart.BackColor = renk;
            parent.Controls.Add(kart);

            Label lblIcon = new Label();
            lblIcon.Text = ikon;
            lblIcon.Font = new Font("Segoe UI Emoji", 22, FontStyle.Bold);
            lblIcon.ForeColor = Color.White;
            lblIcon.Location = new Point(15, 15);
            lblIcon.Size = new Size(50, 45);
            kart.Controls.Add(lblIcon);

            Label lblBaslik = new Label();
            lblBaslik.Text = baslik;
            lblBaslik.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblBaslik.ForeColor = Color.WhiteSmoke;
            lblBaslik.Location = new Point(15, 65);
            lblBaslik.Size = new Size(180, 22);
            kart.Controls.Add(lblBaslik);

            label.Parent = kart;
            label.Visible = true;
            label.Text = deger;
            label.Font = new Font("Segoe UI", 17, FontStyle.Bold);
            label.ForeColor = Color.White;
            label.AutoSize = false;
            label.Location = new Point(75, 20);
            label.Size = new Size(125, 35);
            label.TextAlign = ContentAlignment.MiddleRight;
        }

        private Button RaporButonu(string text, Point location, Color color)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(115, 40);
            btn.Location = location;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
            return btn;
        }
    }
}
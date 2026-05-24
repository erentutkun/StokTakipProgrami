using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace _202507125013_StokTakipProgramı
{
    public partial class FrmInvoices : Form
    {
        SqlConnectionClass sql = new SqlConnectionClass();

        public FrmInvoices()
        {
            InitializeComponent();
        }

        private void FrmInvoices_Load(object sender, EventArgs e)
        {
            ModernTasarim();
            DepolariGetir();
            Listele();
        }

        void DepolariGetir()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT Id, WarehouseName FROM WareHouses",
                sql.connection
            );

            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbWarehouse.DataSource = dt;
            cmbWarehouse.DisplayMember = "WarehouseName";
            cmbWarehouse.ValueMember = "Id";
            cmbWarehouse.SelectedIndex = -1;
        }

        void UrunleriGetir()
        {
            if (cmbWarehouse.SelectedValue == null)
                return;

            int warehouseId;

            if (!int.TryParse(cmbWarehouse.SelectedValue.ToString(), out warehouseId))
                return;

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT Items.Id, Items.ItemName, Stocks.Quantity " +
                "FROM Stocks " +
                "INNER JOIN Items ON Stocks.ItemId = Items.Id " +
                "WHERE Stocks.WarehouseId = @warehouseId AND Stocks.Quantity > 0",
                sql.connection
            );

            da.SelectCommand.Parameters.AddWithValue("@warehouseId", warehouseId);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dt.Columns.Add("DisplayText", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                row["DisplayText"] = row["ItemName"] + " - Stok: " + row["Quantity"];
            }

            cmbItem.DataSource = dt;
            cmbItem.DisplayMember = "DisplayText";
            cmbItem.ValueMember = "Id";
            cmbItem.SelectedIndex = -1;
        }

        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT " +
                "Invoices.Id, " +
                "Invoices.CustomerName AS [Müşteri Adı], " +
                "Items.ItemName AS [Ürün], " +
                "WareHouses.WarehouseName AS [Depo], " +
                "Invoices.Quantity AS [Adet], " +
                "Invoices.UnitPrice AS [Birim Fiyat], " +
                "Invoices.TotalPrice AS [Toplam Tutar], " +
                "Invoices.InvoiceDate AS [Tarih] " +
                "FROM Invoices " +
                "INNER JOIN Items ON Invoices.ItemId = Items.Id " +
                "INNER JOIN WareHouses ON Invoices.WarehouseId = WareHouses.Id " +
                "WHERE Invoices.InvoiceType = N'Çıkış' " +
                "ORDER BY Invoices.Id DESC",
                sql.connection
            );

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            UrunleriGetir();
        }

        private void Hesapla(object sender, EventArgs e)
        {
            int quantity;
            decimal unitPrice;

            if (int.TryParse(txtQuantity.Text, out quantity) &&
                decimal.TryParse(txtUnitPrice.Text, out unitPrice))
            {
                decimal total = quantity * unitPrice;
                lblTotalPrice.Text = "Toplam: " + total.ToString("N2") + " ₺";
            }
            else
            {
                lblTotalPrice.Text = "Toplam: 0,00 ₺";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "" ||
                cmbWarehouse.SelectedValue == null ||
                cmbItem.SelectedValue == null ||
                txtQuantity.Text == "" ||
                txtUnitPrice.Text == "")
            {
                MessageBox.Show("Lütfen müşteri, depo, ürün, adet ve fiyat alanlarını doldurunuz.");
                return;
            }

            int itemId = Convert.ToInt32(cmbItem.SelectedValue);
            int warehouseId = Convert.ToInt32(cmbWarehouse.SelectedValue);

            int quantity;
            decimal unitPrice;

            if (!int.TryParse(txtQuantity.Text, out quantity))
            {
                MessageBox.Show("Adet sayısal olmalıdır.");
                return;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out unitPrice))
            {
                MessageBox.Show("Birim fiyat sayısal olmalıdır.");
                return;
            }

            if (quantity <= 0 || unitPrice <= 0)
            {
                MessageBox.Show("Adet ve fiyat 0'dan büyük olmalıdır.");
                return;
            }

            decimal totalPrice = quantity * unitPrice;

            sql.connection.Open();

            SqlCommand stockCmd = new SqlCommand(
                "SELECT Quantity FROM Stocks WHERE ItemId=@itemId AND WarehouseId=@warehouseId",
                sql.connection
            );

            stockCmd.Parameters.AddWithValue("@itemId", itemId);
            stockCmd.Parameters.AddWithValue("@warehouseId", warehouseId);

            object stockResult = stockCmd.ExecuteScalar();

            if (stockResult == null)
            {
                MessageBox.Show("Bu ürün seçilen depoda bulunmamaktadır.");
                sql.connection.Close();
                return;
            }

            int currentStock = Convert.ToInt32(stockResult);

            if (currentStock < quantity)
            {
                MessageBox.Show("Yetersiz stok. Bu depoda sadece " + currentStock + " adet var.");
                sql.connection.Close();
                return;
            }

            SqlCommand updateStockCmd = new SqlCommand(
                "UPDATE Stocks SET Quantity = Quantity - @quantity WHERE ItemId=@itemId AND WarehouseId=@warehouseId",
                sql.connection
            );

            updateStockCmd.Parameters.AddWithValue("@quantity", quantity);
            updateStockCmd.Parameters.AddWithValue("@itemId", itemId);
            updateStockCmd.Parameters.AddWithValue("@warehouseId", warehouseId);

            updateStockCmd.ExecuteNonQuery();

            SqlCommand insertCmd = new SqlCommand(
                "INSERT INTO Invoices(CustomerName, ItemId, WarehouseId, Quantity, InvoiceType, InvoiceDate, UnitPrice, TotalPrice) " +
                "VALUES(@customerName, @itemId, @warehouseId, @quantity, @invoiceType, @invoiceDate, @unitPrice, @totalPrice)",
                sql.connection
            );

            insertCmd.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            insertCmd.Parameters.AddWithValue("@itemId", itemId);
            insertCmd.Parameters.AddWithValue("@warehouseId", warehouseId);
            insertCmd.Parameters.AddWithValue("@quantity", quantity);
            insertCmd.Parameters.AddWithValue("@invoiceType", "Çıkış");
            insertCmd.Parameters.AddWithValue("@invoiceDate", dateTimePicker1.Value);
            insertCmd.Parameters.AddWithValue("@unitPrice", unitPrice);
            insertCmd.Parameters.AddWithValue("@totalPrice", totalPrice);

            insertCmd.ExecuteNonQuery();

            sql.connection.Close();

            MessageBox.Show("Satış faturası oluşturuldu ve stok düşürüldü.");

            Temizle();
            DepolariGetir();
            Listele();
        }

        void Temizle()
        {
            txtCustomerName.Clear();
            txtQuantity.Clear();
            txtUnitPrice.Clear();
            lblTotalPrice.Text = "Toplam: 0,00 ₺";

            cmbWarehouse.SelectedIndex = -1;
            cmbItem.DataSource = null;

            dateTimePicker1.Value = DateTime.Now;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int invoiceId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT Id, CustomerName, ItemId, WarehouseId, Quantity, UnitPrice, InvoiceDate " +
                "FROM Invoices WHERE Id=@id",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@id", invoiceId);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int itemId = Convert.ToInt32(reader["ItemId"]);
                int warehouseId = Convert.ToInt32(reader["WarehouseId"]);

                txtCustomerName.Text = reader["CustomerName"].ToString();
                txtQuantity.Text = reader["Quantity"].ToString();
                txtUnitPrice.Text = reader["UnitPrice"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(reader["InvoiceDate"]);

                reader.Close();
                sql.connection.Close();

                cmbWarehouse.SelectedValue = warehouseId;

                UrunleriGetir();

                cmbItem.SelectedValue = itemId;

                Hesapla(null, null);
            }
            else
            {
                reader.Close();
                sql.connection.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek kaydı seçiniz.");
                return;
            }

            int id = Convert.ToInt32(
                dataGridView1.SelectedRows[0].Cells["Id"].Value
            );

            DialogResult cevap = MessageBox.Show(
                "Bu faturayı silmek istiyor musunuz?",
                "Onay",
                MessageBoxButtons.YesNo
            );

            if (cevap == DialogResult.No)
                return;

            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "DELETE FROM Invoices WHERE Id=@p1",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", id);

            cmd.ExecuteNonQuery();

            sql.connection.Close();

            MessageBox.Show("Fatura silindi.");

            Listele();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellenecek faturayı seçiniz.");
                return;
            }

            if (txtCustomerName.Text == "" ||
                cmbWarehouse.SelectedValue == null ||
                cmbItem.SelectedValue == null ||
                txtQuantity.Text == "" ||
                txtUnitPrice.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }

            int invoiceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
            int newItemId = Convert.ToInt32(cmbItem.SelectedValue);
            int newWarehouseId = Convert.ToInt32(cmbWarehouse.SelectedValue);

            int newQuantity;
            decimal newUnitPrice;

            if (!int.TryParse(txtQuantity.Text, out newQuantity))
            {
                MessageBox.Show("Adet sayısal olmalıdır.");
                return;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out newUnitPrice))
            {
                MessageBox.Show("Birim fiyat sayısal olmalıdır.");
                return;
            }

            decimal newTotalPrice = newQuantity * newUnitPrice;

            sql.connection.Open();

            SqlCommand oldCmd = new SqlCommand(
                "SELECT ItemId, WarehouseId, Quantity FROM Invoices WHERE Id=@id",
                sql.connection
            );

            oldCmd.Parameters.AddWithValue("@id", invoiceId);

            SqlDataReader reader = oldCmd.ExecuteReader();

            if (!reader.Read())
            {
                reader.Close();
                sql.connection.Close();
                MessageBox.Show("Eski fatura kaydı bulunamadı.");
                return;
            }

            int oldItemId = Convert.ToInt32(reader["ItemId"]);
            int oldWarehouseId = Convert.ToInt32(reader["WarehouseId"]);
            int oldQuantity = Convert.ToInt32(reader["Quantity"]);

            reader.Close();

            SqlCommand geriAlCmd = new SqlCommand(
                "UPDATE Stocks SET Quantity = Quantity + @quantity WHERE ItemId=@itemId AND WarehouseId=@warehouseId",
                sql.connection
            );

            geriAlCmd.Parameters.AddWithValue("@quantity", oldQuantity);
            geriAlCmd.Parameters.AddWithValue("@itemId", oldItemId);
            geriAlCmd.Parameters.AddWithValue("@warehouseId", oldWarehouseId);

            geriAlCmd.ExecuteNonQuery();

            SqlCommand stockCmd = new SqlCommand(
                "SELECT Quantity FROM Stocks WHERE ItemId=@itemId AND WarehouseId=@warehouseId",
                sql.connection
            );

            stockCmd.Parameters.AddWithValue("@itemId", newItemId);
            stockCmd.Parameters.AddWithValue("@warehouseId", newWarehouseId);

            object stockResult = stockCmd.ExecuteScalar();

            if (stockResult == null)
            {
                MessageBox.Show("Yeni seçilen ürün bu depoda bulunmamaktadır.");
                sql.connection.Close();
                return;
            }

            int currentStock = Convert.ToInt32(stockResult);

            if (currentStock < newQuantity)
            {
                MessageBox.Show("Yetersiz stok. Bu depoda sadece " + currentStock + " adet var.");
                sql.connection.Close();
                return;
            }

            SqlCommand dusCmd = new SqlCommand(
                "UPDATE Stocks SET Quantity = Quantity - @quantity WHERE ItemId=@itemId AND WarehouseId=@warehouseId",
                sql.connection
            );

            dusCmd.Parameters.AddWithValue("@quantity", newQuantity);
            dusCmd.Parameters.AddWithValue("@itemId", newItemId);
            dusCmd.Parameters.AddWithValue("@warehouseId", newWarehouseId);

            dusCmd.ExecuteNonQuery();

            SqlCommand updateCmd = new SqlCommand(
                "UPDATE Invoices SET CustomerName=@customerName, ItemId=@itemId, WarehouseId=@warehouseId, Quantity=@quantity, UnitPrice=@unitPrice, TotalPrice=@totalPrice, InvoiceDate=@invoiceDate WHERE Id=@id",
                sql.connection
            );

            updateCmd.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            updateCmd.Parameters.AddWithValue("@itemId", newItemId);
            updateCmd.Parameters.AddWithValue("@warehouseId", newWarehouseId);
            updateCmd.Parameters.AddWithValue("@quantity", newQuantity);
            updateCmd.Parameters.AddWithValue("@unitPrice", newUnitPrice);
            updateCmd.Parameters.AddWithValue("@totalPrice", newTotalPrice);
            updateCmd.Parameters.AddWithValue("@invoiceDate", dateTimePicker1.Value);
            updateCmd.Parameters.AddWithValue("@id", invoiceId);

            updateCmd.ExecuteNonQuery();

            sql.connection.Close();

            MessageBox.Show("Fatura güncellendi ve stok yeniden düzenlendi.");

            Temizle();
            DepolariGetir();
            Listele();
        }
        private void ModernTasarim()
        {

            this.Text = "Satış Faturası";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblBaslik = new Label();
            lblBaslik.Text = "Satış Faturası";
            lblBaslik.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(24, 35, 56);
            lblBaslik.AutoSize = true;
            lblBaslik.Location = new Point(35, 25);
            this.Controls.Add(lblBaslik);

            Label lblAciklama = new Label();
            lblAciklama.Text = "Müşteri, depo ve ürün seçerek satış faturası oluşturun.";
            lblAciklama.Font = new Font("Segoe UI", 11);
            lblAciklama.ForeColor = Color.Gray;
            lblAciklama.AutoSize = true;
            lblAciklama.Location = new Point(38, 70);
            this.Controls.Add(lblAciklama);

            Panel pnlForm = new Panel();
            pnlForm.Size = new Size(360, 500);
            pnlForm.Location = new Point(35, 120);
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlForm);

            Label lblKart = new Label();
            lblKart.Text = "Fatura Bilgileri";
            lblKart.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblKart.ForeColor = Color.FromArgb(31, 78, 121);
            lblKart.AutoSize = true;
            lblKart.Location = new Point(25, 20);
            pnlForm.Controls.Add(lblKart);

            LabelOlustur(pnlForm, "Müşteri Adı", new Point(30, 75));
            TextBoxStil(txtCustomerName, pnlForm, new Point(30, 100));

            LabelOlustur(pnlForm, "Depo", new Point(30, 140));
            ComboBoxStil(cmbWarehouse, pnlForm, new Point(30, 165));

            LabelOlustur(pnlForm, "Ürün", new Point(30, 205));
            ComboBoxStil(cmbItem, pnlForm, new Point(30, 230));

            LabelOlustur(pnlForm, "Adet", new Point(30, 270));
            TextBoxStil(txtQuantity, pnlForm, new Point(30, 295));

            LabelOlustur(pnlForm, "Birim Fiyat", new Point(30, 335));
            TextBoxStil(txtUnitPrice, pnlForm, new Point(30, 360));

            dateTimePicker1.Parent = pnlForm;
            dateTimePicker1.Font = new Font("Segoe UI", 10);
            dateTimePicker1.Size = new Size(280, 30);
            dateTimePicker1.Location = new Point(30, 405);

            lblTotalPrice.Parent = pnlForm;
            lblTotalPrice.Text = "Toplam: 0,00 ₺";
            lblTotalPrice.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTotalPrice.ForeColor = Color.FromArgb(46, 139, 87);
            lblTotalPrice.AutoSize = true;
            lblTotalPrice.Location = new Point(30, 445);

            btnAdd.Parent = pnlForm;
            btnUpdate.Parent = pnlForm;
            btnDelete.Parent = pnlForm;

            ButonStil(btnAdd, "🧾 Kes", new Point(30, 465), Color.FromArgb(46, 139, 87));
            ButonStil(btnUpdate, "✏ Güncelle", new Point(125, 465), Color.FromArgb(0, 122, 204));
            ButonStil(btnDelete, "🗑 Sil", new Point(240, 465), Color.FromArgb(180, 50, 50));

            Panel pnlGrid = new Panel();
            pnlGrid.Size = new Size(730, 500);
            pnlGrid.Location = new Point(420, 120);
            pnlGrid.BackColor = Color.White;
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlGrid);

            Label lblListe = new Label();
            lblListe.Text = "Kesilen Faturalar";
            lblListe.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblListe.ForeColor = Color.FromArgb(31, 78, 121);
            lblListe.AutoSize = true;
            lblListe.Location = new Point(20, 20);
            pnlGrid.Controls.Add(lblListe);

            dataGridView1.Parent = pnlGrid;
            dataGridView1.Location = new Point(20, 70);
            dataGridView1.Size = new Size(690, 400);
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

            txtQuantity.TextChanged += Hesapla;
            txtUnitPrice.TextChanged += Hesapla;
            cmbWarehouse.SelectedIndexChanged -= cmbWarehouse_SelectedIndexChanged;
            cmbWarehouse.SelectedIndexChanged += cmbWarehouse_SelectedIndexChanged;
        }
        private void TextBoxStil(TextBox txt, Panel parent, Point location)
        {
            txt.Parent = parent;
            txt.Font = new Font("Segoe UI", 11);
            txt.Size = new Size(280, 30);
            txt.Location = location;
            txt.BorderStyle = BorderStyle.FixedSingle;
        }

        private void ComboBoxStil(ComboBox cmb, Panel parent, Point location)
        {
            cmb.Parent = parent;
            cmb.Font = new Font("Segoe UI", 11);
            cmb.Size = new Size(280, 30);
            cmb.Location = location;
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
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
            btn.Size = new Size(95, 38);
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
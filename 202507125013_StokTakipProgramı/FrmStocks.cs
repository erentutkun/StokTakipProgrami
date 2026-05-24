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
    public partial class FrmStocks : Form
    {
        public FrmStocks()
        {
            InitializeComponent();
        }
        SqlConnectionClass sql = new SqlConnectionClass();

        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT Stocks.Id, Items.ItemName, WareHouses.WarehouseName, Stocks.Quantity " +
                "FROM Stocks " +
                "INNER JOIN Items ON Stocks.ItemId = Items.Id " +
                "INNER JOIN WareHouses ON Stocks.WarehouseId = WareHouses.Id",
                sql.connection
            );

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void FrmStocks_Load(object sender, EventArgs e)
        {
            ModernTasarim();
            UrunleriGetir();
            DepolariGetir();
            Listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int stockId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT Id, ItemId, WarehouseId, Quantity FROM Stocks WHERE Id=@id",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@id", stockId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txtId.Text = dr["Id"].ToString();
                txtQuantity.Text = dr["Quantity"].ToString();

                int itemId = Convert.ToInt32(dr["ItemId"]);
                int warehouseId = Convert.ToInt32(dr["WarehouseId"]);

                dr.Close();
                sql.connection.Close();

                cmbItem.SelectedValue = itemId;
                cmbWarehouse.SelectedValue = warehouseId;
            }
            else
            {
                dr.Close();
                sql.connection.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbItem.SelectedValue == null ||
                cmbWarehouse.SelectedValue == null ||
                txtQuantity.Text == "")
            {
                MessageBox.Show("Lütfen ürün, depo ve stok miktarı alanlarını doldurunuz.");
                return;
            }

            int itemId = Convert.ToInt32(cmbItem.SelectedValue);
            int warehouseId = Convert.ToInt32(cmbWarehouse.SelectedValue);

            int quantity;

            if (!int.TryParse(txtQuantity.Text, out quantity))
            {
                MessageBox.Show("Stok miktarı sayısal olmalıdır.");
                return;
            }

            if (quantity <= 0)
            {
                MessageBox.Show("Stok miktarı 0'dan büyük olmalıdır.");
                return;
            }

            sql.connection.Open();

            SqlCommand checkCmd = new SqlCommand(
                "SELECT COUNT(*) FROM Stocks WHERE ItemId=@p1 AND WarehouseId=@p2",
                sql.connection
            );

            checkCmd.Parameters.AddWithValue("@p1", itemId);
            checkCmd.Parameters.AddWithValue("@p2", warehouseId);

            int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (existingCount > 0)
            {
                SqlCommand updateCmd = new SqlCommand(
                    "UPDATE Stocks SET Quantity = Quantity + @p3 WHERE ItemId=@p1 AND WarehouseId=@p2",
                    sql.connection
                );

                updateCmd.Parameters.AddWithValue("@p1", itemId);
                updateCmd.Parameters.AddWithValue("@p2", warehouseId);
                updateCmd.Parameters.AddWithValue("@p3", quantity);

                updateCmd.ExecuteNonQuery();

                MessageBox.Show("Bu ürün bu depoda zaten vardı. Stok miktarı üzerine eklendi.");
            }
            else
            {
                SqlCommand insertCmd = new SqlCommand(
                    "INSERT INTO Stocks(ItemId, WarehouseId, Quantity) VALUES(@p1,@p2,@p3)",
                    sql.connection
                );

                insertCmd.Parameters.AddWithValue("@p1", itemId);
                insertCmd.Parameters.AddWithValue("@p2", warehouseId);
                insertCmd.Parameters.AddWithValue("@p3", quantity);

                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Yeni stok kaydı eklendi.");
            }

            sql.connection.Close();

            txtQuantity.Clear();
            cmbItem.SelectedIndex = -1;
            cmbWarehouse.SelectedIndex = -1;

            Listele();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "UPDATE Stocks SET ItemId=@p1, WarehouseId=@p2, Quantity=@p3 WHERE Id=@p4",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(cmbItem.SelectedValue));
            cmd.Parameters.AddWithValue("@p2", Convert.ToInt32(cmbWarehouse.SelectedValue));
            cmd.Parameters.AddWithValue("@p3", int.Parse(txtQuantity.Text));
            cmd.Parameters.AddWithValue("@p4", txtId.Text);

            cmd.ExecuteNonQuery();
            sql.connection.Close();

            MessageBox.Show("Stok güncellendi.");
            Listele();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sql.connection.Open();

            SqlCommand cmd = new SqlCommand(
                "DELETE FROM Stocks WHERE Id=@p1",
                sql.connection
            );

            cmd.Parameters.AddWithValue("@p1", txtId.Text);

            cmd.ExecuteNonQuery();
            sql.connection.Close();

            MessageBox.Show("Stok silindi.");
            Listele();
        }

        private void btnCriticalStock_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter(
       "SELECT Stocks.Id, Items.ItemName, WareHouses.WarehouseName, Stocks.Quantity " +
       "FROM Stocks " +
       "INNER JOIN Items ON Stocks.ItemId = Items.Id " +
       "INNER JOIN WareHouses ON Stocks.WarehouseId = WareHouses.Id " +
       "WHERE Stocks.Quantity <= 10",
       sql.connection
   );

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            Listele();
        }
        private void UrunleriGetir()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT Id, ItemName FROM Items",
                sql.connection
            );

            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbItem.DataSource = dt;
            cmbItem.DisplayMember = "ItemName";
            cmbItem.ValueMember = "Id";
            cmbItem.SelectedIndex = -1;
        }

        private void DepolariGetir()
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

        private void ModernTasarim()
        {

            txtItemId.Visible = false;
            txtWarehouseId.Visible = false;
            this.Text = "Stok Yönetimi";
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblBaslik = new Label();
            lblBaslik.Text = "Stok Yönetimi";
            lblBaslik.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(24, 35, 56);
            lblBaslik.AutoSize = true;
            lblBaslik.Location = new Point(35, 25);
            this.Controls.Add(lblBaslik);

            Label lblAciklama = new Label();
            lblAciklama.Text = "Ürünlerin depolardaki stok miktarlarını buradan yönetin.";
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
            lblKartBaslik.Text = "Stok Bilgileri";
            lblKartBaslik.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblKartBaslik.ForeColor = Color.FromArgb(31, 78, 121);
            lblKartBaslik.AutoSize = true;
            lblKartBaslik.Location = new Point(25, 25);
            pnlForm.Controls.Add(lblKartBaslik);

            LabelOlustur(pnlForm, "Ürün ID", new Point(30, 90));
            TextBoxStil(txtItemId, pnlForm, new Point(30, 115));

            LabelOlustur(pnlForm, "Depo ID", new Point(30, 160));
            TextBoxStil(txtWarehouseId, pnlForm, new Point(30, 185));

            LabelOlustur(pnlForm, "Stok Miktarı", new Point(30, 230));
            TextBoxStil(txtQuantity, pnlForm, new Point(30, 255));
            LabelOlustur(pnlForm, "Ürün", new Point(30, 90));

            cmbItem.Parent = pnlForm;
            cmbItem.Font = new Font("Segoe UI", 11);
            cmbItem.Size = new Size(260, 30);
            cmbItem.Location = new Point(30, 115);
            cmbItem.DropDownStyle = ComboBoxStyle.DropDownList;

            LabelOlustur(pnlForm, "Depo", new Point(30, 160));

            cmbWarehouse.Parent = pnlForm;
            cmbWarehouse.Font = new Font("Segoe UI", 11);
            cmbWarehouse.Size = new Size(260, 30);
            cmbWarehouse.Location = new Point(30, 185);
            cmbWarehouse.DropDownStyle = ComboBoxStyle.DropDownList;
            txtId.Visible = false;

            btnAdd.Parent = pnlForm;
            btnUpdate.Parent = pnlForm;
            btnDelete.Parent = pnlForm;

            ButonStil(btnAdd, "➕ Ekle", new Point(30, 330), Color.FromArgb(46, 139, 87));
            ButonStil(btnUpdate, "✏ Güncelle", new Point(115, 330), Color.FromArgb(0, 122, 204));
            ButonStil(btnDelete, "🗑 Sil", new Point(215, 330), Color.FromArgb(180, 50, 50));

            Panel pnlGrid = new Panel();
            pnlGrid.Size = new Size(680, 420);
            pnlGrid.Location = new Point(390, 120);
            pnlGrid.BackColor = Color.White;
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pnlGrid);

            Label lblListe = new Label();
            lblListe.Text = "Stok Listesi";
            lblListe.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblListe.ForeColor = Color.FromArgb(31, 78, 121);
            lblListe.AutoSize = true;
            lblListe.Location = new Point(20, 20);
            pnlGrid.Controls.Add(lblListe);

            btnList.Parent = pnlGrid;
            ButonStil(btnList, "📋 Tümünü Listele", new Point(370, 18), Color.FromArgb(31, 78, 121));
            btnList.Size = new Size(135, 40);

            btnCriticalStock.Parent = pnlGrid;
            ButonStil(btnCriticalStock, "⚠ Kritik Stoklar", new Point(515, 18), Color.FromArgb(220, 120, 40));
            btnCriticalStock.Size = new Size(135, 40);

            dataGridView1.Parent = pnlGrid;
            dataGridView1.Location = new Point(20, 75);
            dataGridView1.Size = new Size(640, 320);
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

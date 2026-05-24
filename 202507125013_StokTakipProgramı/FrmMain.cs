using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _202507125013_StokTakipProgramı
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        public string userRole;

        private void btnUsers_Click(object sender, EventArgs e)
        {
            FrmUsers frm = new FrmUsers();
            frm.Show();
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            FrmItems frm = new FrmItems();
            frm.Show();
        }

        private void btnWarehouse_Click(object sender, EventArgs e)
        {
            FrmWareHouses frm = new FrmWareHouses();
            frm.Show();
        }

        private void btnStocks_Click(object sender, EventArgs e)
        {
            FrmStocks frm = new FrmStocks();
            frm.Show();
        }

        private void btnInvoices_Click(object sender, EventArgs e)
        {
            FrmInvoices frm = new FrmInvoices();
            frm.Show();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            FrmReports frm = new FrmReports();
            frm.Show();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            AnaMenuTasarim();

            if (userRole != "Admin")
            {
                btnUsers.Visible = false;

                btnReports.Visible = false;
            }
        }
        private void AnaMenuTasarim()
        {
            this.Text = "Stok Takip Sistemi";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(245, 247, 250);

            Panel solMenu = new Panel();
            solMenu.Size = new Size(260, this.Height);
            solMenu.Dock = DockStyle.Left;
            solMenu.BackColor = Color.FromArgb(24, 35, 56);
            this.Controls.Add(solMenu);

            Label lblLogo = new Label();
            lblLogo.Text = "STOK TAKİP";
            lblLogo.ForeColor = Color.White;
            lblLogo.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            lblLogo.Size = new Size(260, 90);
            lblLogo.Location = new Point(0, 20);
            solMenu.Controls.Add(lblLogo);

            Label lblAlt = new Label();
            lblAlt.Text = "Yönetim Paneli";
            lblAlt.ForeColor = Color.FromArgb(170, 180, 200);
            lblAlt.Font = new Font("Segoe UI", 10);
            lblAlt.TextAlign = ContentAlignment.MiddleCenter;
            lblAlt.Size = new Size(260, 25);
            lblAlt.Location = new Point(0, 85);
            solMenu.Controls.Add(lblAlt);

            List<Button> menuButonlari = new List<Button>();

            if (userRole == "Admin")
            {
                menuButonlari.Add(btnUsers);
            }

            menuButonlari.Add(btnItems);
            menuButonlari.Add(btnWarehouse);
            menuButonlari.Add(btnStocks);
            menuButonlari.Add(btnInvoices);

            if (userRole == "Admin")
            {
                menuButonlari.Add(btnReports);
            }
            Button btnStockSearch = new Button();

            btnStockSearch.Text = "🔎  Stok Sorgula";
            btnStockSearch.Size = new Size(220, 50);
            btnStockSearch.Location = new Point(20, 150 + (menuButonlari.Count * 62));
            btnStockSearch.FlatStyle = FlatStyle.Flat;
            btnStockSearch.FlatAppearance.BorderSize = 0;
            btnStockSearch.BackColor = Color.FromArgb(34, 48, 74);
            btnStockSearch.ForeColor = Color.White;
            btnStockSearch.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnStockSearch.TextAlign = ContentAlignment.MiddleLeft;
            btnStockSearch.Cursor = Cursors.Hand;
            btnStockSearch.UseVisualStyleBackColor = false;

            btnStockSearch.MouseEnter += (s, e) =>
            {
                btnStockSearch.BackColor = Color.FromArgb(0, 122, 204);
            };

            btnStockSearch.MouseLeave += (s, e) =>
            {
                btnStockSearch.BackColor = Color.FromArgb(34, 48, 74);
            };

            btnStockSearch.Click += (s, e) =>
            {
                FrmStockSearch frm = new FrmStockSearch();
                frm.ShowDialog();
            };

            solMenu.Controls.Add(btnStockSearch);
            Button btnLogout = new Button();

            btnLogout.Text = "🚪  Çıkış Yap";
            btnLogout.Size = new Size(220, 50);
            btnLogout.Location = new Point(20, 150 + ((menuButonlari.Count + 1) * 62));
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.BackColor = Color.FromArgb(180, 50, 50);
            btnLogout.ForeColor = Color.White;
            btnLogout.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.UseVisualStyleBackColor = false;

            btnLogout.MouseEnter += (s, e) =>
            {
                btnLogout.BackColor = Color.FromArgb(220, 40, 40);
            };

            btnLogout.MouseLeave += (s, e) =>
            {
                btnLogout.BackColor = Color.FromArgb(180, 50, 50);
            };

            btnLogout.Click += BtnLogout_Click;

            solMenu.Controls.Add(btnLogout);

            List<string> butonYazilari = new List<string>();

            if (userRole == "Admin")
                butonYazilari.Add("👤 Kullanıcılar");

            butonYazilari.Add("📦 Ürünler");
            butonYazilari.Add("🏬 Depolar");
            butonYazilari.Add("📊 Stoklar");
            butonYazilari.Add("🧾 Faturalar");

            if (userRole == "Admin")
                butonYazilari.Add("📈 Raporlar");

            for (int i = 0; i < menuButonlari.Count; i++)
            {
                Button btn = menuButonlari[i];

                btn.Parent = solMenu;
                btn.Text = butonYazilari[i];
                btn.Size = new Size(220, 50);
                btn.Location = new Point(20, 150 + (i * 62));
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(34, 48, 74);
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Cursor = Cursors.Hand;
                btn.UseVisualStyleBackColor = false;

                btn.MouseEnter += (s, e) =>
                {
                    ((Button)s).BackColor = Color.FromArgb(0, 122, 204);
                };

                btn.MouseLeave += (s, e) =>
                {
                    ((Button)s).BackColor = Color.FromArgb(34, 48, 74);
                };
            }

            Panel ustPanel = new Panel();
            ustPanel.Dock = DockStyle.Top;
            ustPanel.Height = 90;
            ustPanel.BackColor = Color.White;
            this.Controls.Add(ustPanel);
            ustPanel.BringToFront();

            Label lblBaslik = new Label();
            lblBaslik.Text = "Stok Takip Sistemi";
            lblBaslik.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(24, 35, 56);
            lblBaslik.AutoSize = true;
            lblBaslik.Location = new Point(300, 25);
            ustPanel.Controls.Add(lblBaslik);

            Label lblTarih = new Label();
            lblTarih.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            lblTarih.Font = new Font("Segoe UI", 11);
            lblTarih.ForeColor = Color.Gray;
            lblTarih.AutoSize = true;
            lblTarih.Location = new Point(720, 35);
            ustPanel.Controls.Add(lblTarih);

            Panel dashboard = new Panel();
            dashboard.Location = new Point(300, 130);
            dashboard.Size = new Size(900, 430);
            dashboard.BackColor = Color.White;
            dashboard.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(dashboard);

            Label lblHosgeldin = new Label();
            lblHosgeldin.Text = "Hoş geldiniz 👋";
            lblHosgeldin.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblHosgeldin.ForeColor = Color.FromArgb(31, 78, 121);
            lblHosgeldin.AutoSize = true;
            lblHosgeldin.Location = new Point(40, 35);
            dashboard.Controls.Add(lblHosgeldin);

            Label lblAciklama = new Label();
            lblAciklama.Text = "Ürün, depo, stok, fatura ve rapor işlemlerinizi bu panelden yönetebilirsiniz.";
            lblAciklama.Font = new Font("Segoe UI", 12);
            lblAciklama.ForeColor = Color.Gray;
            lblAciklama.AutoSize = true;
            lblAciklama.Location = new Point(43, 85);
            dashboard.Controls.Add(lblAciklama);

            KartOlustur(dashboard, "Ürün Yönetimi", "Ürün kartlarını ekle, güncelle ve listele.", 45, 150, Color.FromArgb(0, 122, 204));
            KartOlustur(dashboard, "Depo Yönetimi", "Depoları ve stok konumlarını kontrol et.", 330, 150, Color.FromArgb(46, 139, 87));
            KartOlustur(dashboard, "Satış Faturası", "Depodan ürün satışı yap ve stok düş.", 615, 150, Color.FromArgb(220, 120, 40));
            KartOlustur(dashboard, "Raporlama", "Stok ve satış raporlarını görüntüle.", 45, 285, Color.FromArgb(120, 80, 180));
        }
        private void KartOlustur(Panel parent, string baslik, string aciklama, int x, int y, Color renk)
        {
            Panel kart = new Panel();
            kart.Size = new Size(240, 100);
            kart.Location = new Point(x, y);
            kart.BackColor = renk;
            parent.Controls.Add(kart);

            Label lblBaslik = new Label();
            lblBaslik.Text = baslik;
            lblBaslik.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblBaslik.ForeColor = Color.White;
            lblBaslik.AutoSize = false;
            lblBaslik.Size = new Size(220, 30);
            lblBaslik.Location = new Point(15, 15);
            kart.Controls.Add(lblBaslik);

            Label lblAciklama = new Label();
            lblAciklama.Text = aciklama;
            lblAciklama.Font = new Font("Segoe UI", 9);
            lblAciklama.ForeColor = Color.WhiteSmoke;
            lblAciklama.AutoSize = false;
            lblAciklama.Size = new Size(210, 45);
            lblAciklama.Location = new Point(15, 48);
            kart.Controls.Add(lblAciklama);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show(
                "Çıkış yapmak istiyor musunuz?",
                "Çıkış",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (cevap == DialogResult.Yes)
            {
                FrmLogin frm = new FrmLogin();

                frm.Show();

                this.Close();
            }
        }
    }
}

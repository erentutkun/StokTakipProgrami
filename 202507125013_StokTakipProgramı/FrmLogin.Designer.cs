using System;
using System.Drawing;
using System.Windows.Forms;

namespace _202507125013_StokTakipProgramı
{
    partial class FrmLogin
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            this.btnLogin = new System.Windows.Forms.Button();
            this.kullaniciAdilbl = new System.Windows.Forms.Label();
            this.sifrelbl = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.stoktakiplabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(55, 187);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(246, 37);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Giriş Yap";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.girisbutton_Click);
            // 
            // kullaniciAdilbl
            // 
            this.kullaniciAdilbl.AutoSize = true;
            this.kullaniciAdilbl.Location = new System.Drawing.Point(52, 135);
            this.kullaniciAdilbl.Name = "kullaniciAdilbl";
            this.kullaniciAdilbl.Size = new System.Drawing.Size(70, 13);
            this.kullaniciAdilbl.TabIndex = 1;
            this.kullaniciAdilbl.Text = "Kullanıcı Adı :";
            // 
            // sifrelbl
            // 
            this.sifrelbl.AutoSize = true;
            this.sifrelbl.Location = new System.Drawing.Point(88, 164);
            this.sifrelbl.Name = "sifrelbl";
            this.sifrelbl.Size = new System.Drawing.Size(34, 13);
            this.sifrelbl.TabIndex = 2;
            this.sifrelbl.Text = "Şifre :";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(128, 135);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(164, 20);
            this.txtUserName.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(128, 161);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(164, 20);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // stoktakiplabel
            // 
            this.stoktakiplabel.AutoSize = true;
            this.stoktakiplabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.stoktakiplabel.Location = new System.Drawing.Point(100, 93);
            this.stoktakiplabel.Name = "stoktakiplabel";
            this.stoktakiplabel.Size = new System.Drawing.Size(170, 20);
            this.stoktakiplabel.TabIndex = 5;
            this.stoktakiplabel.Text = "Stok Takip Programı";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(361, 315);
            this.Controls.Add(this.stoktakiplabel);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.sifrelbl);
            this.Controls.Add(this.kullaniciAdilbl);
            this.Controls.Add(this.btnLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.Text = "Stok Takip Pogramı";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label kullaniciAdilbl;
        private System.Windows.Forms.Label sifrelbl;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label stoktakiplabel;
    }
}


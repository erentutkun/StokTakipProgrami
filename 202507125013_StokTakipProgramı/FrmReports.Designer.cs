namespace _202507125013_StokTakipProgramı
{
    partial class FrmReports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTotalItems = new System.Windows.Forms.Label();
            this.lblTotalStock = new System.Windows.Forms.Label();
            this.lblCriticalStock = new System.Windows.Forms.Label();
            this.lblTotalInvoice = new System.Windows.Forms.Label();
            this.lblTotalSalesQuantity = new System.Windows.Forms.Label();
            this.lblTotalPurchaseCost = new System.Windows.Forms.Label();
            this.lblTotalSalesAmount = new System.Windows.Forms.Label();
            this.lblProfit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTotalItems
            // 
            this.lblTotalItems.AutoSize = true;
            this.lblTotalItems.Location = new System.Drawing.Point(72, 57);
            this.lblTotalItems.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(88, 16);
            this.lblTotalItems.TabIndex = 0;
            this.lblTotalItems.Text = "Toplam Ürün:";
            // 
            // lblTotalStock
            // 
            this.lblTotalStock.AutoSize = true;
            this.lblTotalStock.Location = new System.Drawing.Point(73, 84);
            this.lblTotalStock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalStock.Name = "lblTotalStock";
            this.lblTotalStock.Size = new System.Drawing.Size(87, 16);
            this.lblTotalStock.TabIndex = 1;
            this.lblTotalStock.Text = "Toplam Stok:";
            // 
            // lblCriticalStock
            // 
            this.lblCriticalStock.AutoSize = true;
            this.lblCriticalStock.Location = new System.Drawing.Point(89, 117);
            this.lblCriticalStock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCriticalStock.Name = "lblCriticalStock";
            this.lblCriticalStock.Size = new System.Drawing.Size(68, 16);
            this.lblCriticalStock.TabIndex = 2;
            this.lblCriticalStock.Text = "Kritik Stok:";
            // 
            // lblTotalInvoice
            // 
            this.lblTotalInvoice.AutoSize = true;
            this.lblTotalInvoice.Location = new System.Drawing.Point(63, 150);
            this.lblTotalInvoice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalInvoice.Name = "lblTotalInvoice";
            this.lblTotalInvoice.Size = new System.Drawing.Size(98, 16);
            this.lblTotalInvoice.TabIndex = 3;
            this.lblTotalInvoice.Text = "Toplam Fatura:";
            // 
            // lblTotalSalesQuantity
            // 
            this.lblTotalSalesQuantity.AutoSize = true;
            this.lblTotalSalesQuantity.Location = new System.Drawing.Point(619, 57);
            this.lblTotalSalesQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalSalesQuantity.Name = "lblTotalSalesQuantity";
            this.lblTotalSalesQuantity.Size = new System.Drawing.Size(129, 16);
            this.lblTotalSalesQuantity.TabIndex = 4;
            this.lblTotalSalesQuantity.Text = "Toplam Satış Adedi:";
            // 
            // lblTotalPurchaseCost
            // 
            this.lblTotalPurchaseCost.AutoSize = true;
            this.lblTotalPurchaseCost.Location = new System.Drawing.Point(619, 84);
            this.lblTotalPurchaseCost.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPurchaseCost.Name = "lblTotalPurchaseCost";
            this.lblTotalPurchaseCost.Size = new System.Drawing.Size(131, 16);
            this.lblTotalPurchaseCost.TabIndex = 5;
            this.lblTotalPurchaseCost.Text = "Toplam Alış Maliyeti:";
            // 
            // lblTotalSalesAmount
            // 
            this.lblTotalSalesAmount.AutoSize = true;
            this.lblTotalSalesAmount.Location = new System.Drawing.Point(619, 117);
            this.lblTotalSalesAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalSalesAmount.Name = "lblTotalSalesAmount";
            this.lblTotalSalesAmount.Size = new System.Drawing.Size(127, 16);
            this.lblTotalSalesAmount.TabIndex = 6;
            this.lblTotalSalesAmount.Text = "Toplam Satış Tutarı:";
            // 
            // lblProfit
            // 
            this.lblProfit.AutoSize = true;
            this.lblProfit.Location = new System.Drawing.Point(619, 150);
            this.lblProfit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProfit.Name = "lblProfit";
            this.lblProfit.Size = new System.Drawing.Size(81, 16);
            this.lblProfit.TabIndex = 7;
            this.lblProfit.Text = "Tahmini Kâr:";
            // 
            // FrmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 253);
            this.Controls.Add(this.lblProfit);
            this.Controls.Add(this.lblTotalSalesAmount);
            this.Controls.Add(this.lblTotalPurchaseCost);
            this.Controls.Add(this.lblTotalSalesQuantity);
            this.Controls.Add(this.lblTotalInvoice);
            this.Controls.Add(this.lblCriticalStock);
            this.Controls.Add(this.lblTotalStock);
            this.Controls.Add(this.lblTotalItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReports";
            this.Load += new System.EventHandler(this.FrmReports_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotalItems;
        private System.Windows.Forms.Label lblTotalStock;
        private System.Windows.Forms.Label lblCriticalStock;
        private System.Windows.Forms.Label lblTotalInvoice;
        private System.Windows.Forms.Label lblTotalSalesQuantity;
        private System.Windows.Forms.Label lblTotalPurchaseCost;
        private System.Windows.Forms.Label lblTotalSalesAmount;
        private System.Windows.Forms.Label lblProfit;
    }
}

namespace Future2
{
    partial class FormMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaster));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAccount = new System.Windows.Forms.ToolStripButton();
            this.btnCommQuote = new System.Windows.Forms.ToolStripButton();
            this.btnEntrustTest = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAccount,
            this.btnCommQuote,
            this.btnEntrustTest});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAccount
            // 
            this.btnAccount.Image = ((System.Drawing.Image)(resources.GetObject("btnAccount.Image")));
            this.btnAccount.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(75, 22);
            this.btnAccount.Text = "帳號設定";
            this.btnAccount.Click += new System.EventHandler(this.btnAccount_Click);
            // 
            // btnCommQuote
            // 
            this.btnCommQuote.Image = ((System.Drawing.Image)(resources.GetObject("btnCommQuote.Image")));
            this.btnCommQuote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCommQuote.Name = "btnCommQuote";
            this.btnCommQuote.Size = new System.Drawing.Size(75, 22);
            this.btnCommQuote.Text = "商品報價";
            this.btnCommQuote.Click += new System.EventHandler(this.btnCommQuote_Click);
            // 
            // btnEntrustTest
            // 
            this.btnEntrustTest.Image = ((System.Drawing.Image)(resources.GetObject("btnEntrustTest.Image")));
            this.btnEntrustTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEntrustTest.Name = "btnEntrustTest";
            this.btnEntrustTest.Size = new System.Drawing.Size(87, 22);
            this.btnEntrustTest.Text = "委託單測試";
            this.btnEntrustTest.Click += new System.EventHandler(this.btnEntrustTest_Click);
            // 
            // FormMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Name = "FormMaster";
            this.Text = "期貨自動交易";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMaster_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAccount;
        private System.Windows.Forms.ToolStripButton btnCommQuote;
        private System.Windows.Forms.ToolStripButton btnEntrustTest;
    }
}
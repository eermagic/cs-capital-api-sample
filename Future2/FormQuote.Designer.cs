
namespace Future2
{
    partial class FormQuote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQuote));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnStartQuote = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboCommID = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.gvQuote = new System.Windows.Forms.DataGridView();
            this.QuoteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuoteValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvBest5Merge = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTwSignal = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gvTick = new System.Windows.Forms.DataGridView();
            this.TickName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TickValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Best5BidQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Best5BidPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Best5AskPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Best5AskQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuote)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvBest5Merge)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTick)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStartQuote});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(751, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnStartQuote
            // 
            this.btnStartQuote.Image = ((System.Drawing.Image)(resources.GetObject("btnStartQuote.Image")));
            this.btnStartQuote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartQuote.Name = "btnStartQuote";
            this.btnStartQuote.Size = new System.Drawing.Size(75, 22);
            this.btnStartQuote.Text = "開始報價";
            this.btnStartQuote.Click += new System.EventHandler(this.btnStartQuote_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboCommID);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(725, 57);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "交易商品";
            // 
            // cboCommID
            // 
            this.cboCommID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCommID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCommID.FormattingEnabled = true;
            this.cboCommID.Location = new System.Drawing.Point(6, 21);
            this.cboCommID.Name = "cboCommID";
            this.cboCommID.Size = new System.Drawing.Size(713, 20);
            this.cboCommID.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtMessage);
            this.groupBox5.Location = new System.Drawing.Point(252, 314);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(493, 138);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "訊息";
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Location = new System.Drawing.Point(6, 21);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(481, 111);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.Text = "";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.gvQuote);
            this.groupBox6.Location = new System.Drawing.Point(12, 91);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(240, 361);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "最新報價";
            // 
            // gvQuote
            // 
            this.gvQuote.AllowUserToAddRows = false;
            this.gvQuote.AllowUserToDeleteRows = false;
            this.gvQuote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvQuote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvQuote.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QuoteName,
            this.QuoteValue});
            this.gvQuote.Location = new System.Drawing.Point(3, 18);
            this.gvQuote.Name = "gvQuote";
            this.gvQuote.RowHeadersWidth = 22;
            this.gvQuote.RowTemplate.Height = 24;
            this.gvQuote.Size = new System.Drawing.Size(231, 337);
            this.gvQuote.TabIndex = 0;
            // 
            // QuoteName
            // 
            this.QuoteName.DataPropertyName = "QuoteName";
            this.QuoteName.HeaderText = "欄位";
            this.QuoteName.Name = "QuoteName";
            // 
            // QuoteValue
            // 
            this.QuoteValue.DataPropertyName = "QuoteValue";
            this.QuoteValue.HeaderText = "值";
            this.QuoteValue.Name = "QuoteValue";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gvBest5Merge);
            this.groupBox2.Location = new System.Drawing.Point(504, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 217);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "最佳 5 檔";
            // 
            // gvBest5Merge
            // 
            this.gvBest5Merge.AllowUserToAddRows = false;
            this.gvBest5Merge.AllowUserToDeleteRows = false;
            this.gvBest5Merge.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvBest5Merge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvBest5Merge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Best5BidQty,
            this.Best5BidPrice,
            this.Best5AskPrice,
            this.Best5AskQty});
            this.gvBest5Merge.Location = new System.Drawing.Point(6, 21);
            this.gvBest5Merge.Name = "gvBest5Merge";
            this.gvBest5Merge.RowHeadersWidth = 22;
            this.gvBest5Merge.RowTemplate.Height = 24;
            this.gvBest5Merge.Size = new System.Drawing.Size(227, 190);
            this.gvBest5Merge.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTwSignal});
            this.statusStrip1.Location = new System.Drawing.Point(0, 461);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(751, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblTwSignal
            // 
            this.lblTwSignal.Name = "lblTwSignal";
            this.lblTwSignal.Size = new System.Drawing.Size(55, 17);
            this.lblTwSignal.Text = "連線狀態";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gvTick);
            this.groupBox3.Location = new System.Drawing.Point(258, 91);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(240, 217);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "最新 Tick";
            // 
            // gvTick
            // 
            this.gvTick.AllowUserToAddRows = false;
            this.gvTick.AllowUserToDeleteRows = false;
            this.gvTick.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvTick.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTick.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TickName,
            this.TickValue});
            this.gvTick.Location = new System.Drawing.Point(3, 18);
            this.gvTick.Name = "gvTick";
            this.gvTick.RowHeadersWidth = 22;
            this.gvTick.RowTemplate.Height = 24;
            this.gvTick.Size = new System.Drawing.Size(231, 193);
            this.gvTick.TabIndex = 0;
            // 
            // TickName
            // 
            this.TickName.DataPropertyName = "TickName";
            this.TickName.HeaderText = "欄位";
            this.TickName.Name = "TickName";
            // 
            // TickValue
            // 
            this.TickValue.DataPropertyName = "TickValue";
            this.TickValue.HeaderText = "值";
            this.TickValue.Name = "TickValue";
            // 
            // Best5BidQty
            // 
            this.Best5BidQty.DataPropertyName = "Best5BidQty";
            this.Best5BidQty.HeaderText = "量";
            this.Best5BidQty.Name = "Best5BidQty";
            this.Best5BidQty.Width = 40;
            // 
            // Best5BidPrice
            // 
            this.Best5BidPrice.DataPropertyName = "Best5BidPrice";
            this.Best5BidPrice.HeaderText = "委買";
            this.Best5BidPrice.Name = "Best5BidPrice";
            this.Best5BidPrice.Width = 60;
            // 
            // Best5AskPrice
            // 
            this.Best5AskPrice.DataPropertyName = "Best5AskPrice";
            this.Best5AskPrice.HeaderText = "委賣";
            this.Best5AskPrice.Name = "Best5AskPrice";
            this.Best5AskPrice.Width = 60;
            // 
            // Best5AskQty
            // 
            this.Best5AskQty.DataPropertyName = "Best5AskQty";
            this.Best5AskQty.HeaderText = "量";
            this.Best5AskQty.Name = "Best5AskQty";
            this.Best5AskQty.Width = 40;
            // 
            // FormQuote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 483);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormQuote";
            this.Text = "商品報價";
            this.Load += new System.EventHandler(this.FormQuote_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvQuote)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvBest5Merge)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvTick)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnStartQuote;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboCommID;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView gvQuote;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuoteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuoteValue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gvBest5Merge;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTwSignal;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView gvTick;
        private System.Windows.Forms.DataGridViewTextBoxColumn TickName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TickValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Best5BidQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Best5BidPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Best5AskPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Best5AskQty;
    }
}
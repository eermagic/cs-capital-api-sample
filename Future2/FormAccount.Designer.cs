
namespace Future2
{
    partial class FormAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAccount));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboFutureAccount = new System.Windows.Forms.ComboBox();
            this.btnCapitalLoginTest = new System.Windows.Forms.Button();
            this.txtCapitalPwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCapitalAcct = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboFutureAccount);
            this.groupBox1.Controls.Add(this.btnCapitalLoginTest);
            this.groupBox1.Controls.Add(this.txtCapitalPwd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCapitalAcct);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "群益帳號";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(6, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "期貨帳號：";
            // 
            // cboFutureAccount
            // 
            this.cboFutureAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFutureAccount.FormattingEnabled = true;
            this.cboFutureAccount.Location = new System.Drawing.Point(100, 50);
            this.cboFutureAccount.Name = "cboFutureAccount";
            this.cboFutureAccount.Size = new System.Drawing.Size(236, 20);
            this.cboFutureAccount.TabIndex = 5;
            // 
            // btnCapitalLoginTest
            // 
            this.btnCapitalLoginTest.Location = new System.Drawing.Point(353, 22);
            this.btnCapitalLoginTest.Name = "btnCapitalLoginTest";
            this.btnCapitalLoginTest.Size = new System.Drawing.Size(71, 23);
            this.btnCapitalLoginTest.TabIndex = 4;
            this.btnCapitalLoginTest.Text = "登入測試";
            this.btnCapitalLoginTest.UseVisualStyleBackColor = true;
            this.btnCapitalLoginTest.Click += new System.EventHandler(this.btnCapitalLoginTest_Click);
            // 
            // txtCapitalPwd
            // 
            this.txtCapitalPwd.Location = new System.Drawing.Point(236, 22);
            this.txtCapitalPwd.Name = "txtCapitalPwd";
            this.txtCapitalPwd.PasswordChar = '*';
            this.txtCapitalPwd.Size = new System.Drawing.Size(100, 22);
            this.txtCapitalPwd.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(174, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "密碼：";
            // 
            // txtCapitalAcct
            // 
            this.txtCapitalAcct.Location = new System.Drawing.Point(68, 22);
            this.txtCapitalAcct.Name = "txtCapitalAcct";
            this.txtCapitalAcct.Size = new System.Drawing.Size(100, 22);
            this.txtCapitalAcct.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "帳號：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(460, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(51, 22);
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMessage);
            this.groupBox2.Location = new System.Drawing.Point(12, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(439, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "訊息";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(6, 21);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(427, 73);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.Text = "";
            // 
            // FormAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 262);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormAccount";
            this.Text = "帳號設定";
            this.Load += new System.EventHandler(this.FormAccount_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCapitalLoginTest;
        private System.Windows.Forms.TextBox txtCapitalPwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCapitalAcct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboFutureAccount;
    }
}
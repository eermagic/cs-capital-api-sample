using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Future2
{
    public partial class FormMaster : Form
    {
        #region 屬性

        #endregion

        #region 建構子
        public FormMaster()
        {
            InitializeComponent();
        }

        private void FormMaster_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region 動作
        #region ToolStrip 按鈕
        private void btnAccount_Click(object sender, EventArgs e)
        {
            bool isFind = false;
            foreach (Form form in this.MdiChildren)
            {
                if (form.Name == "FormAccount")
                {
                    isFind = true;
                    form.MdiParent = this;
                    form.Focus();
                }
            }
            if (isFind == false)
            {
                FormAccount childForm = new FormAccount();
                childForm.MdiParent = this;
                childForm.Show();
            }
        }

        /// <summary>
        /// 商品報價
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommQuote_Click(object sender, EventArgs e)
        {
            FormQuote childForm = new FormQuote();
            childForm.MdiParent = this;
            childForm.Show();
        }

        /// <summary>
        /// 委託單測試
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEntrustTest_Click(object sender, EventArgs e)
        {
            bool isFind = false;
            foreach (Form form in this.MdiChildren)
            {
                if (form.Name == "FormEntrustTest")
                {
                    isFind = true;
                    form.MdiParent = this;
                    form.Focus();
                }
            }
            if (isFind == false)
            {
                FormEntrustTest childForm = new FormEntrustTest();
                childForm.MdiParent = this;
                childForm.Show();
            }
        }
    }
    #endregion

    #endregion

    #region 方法

    #endregion

    #region 事件

    #endregion

}

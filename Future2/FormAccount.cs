using Future2.Classes;
using SKCOMLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Future2
{
    public partial class FormAccount : Form
    {
        #region 屬性
        SKCenterLib m_pSKCenter = null;//登入&環境物件
        SKOrderLib m_pSKOrder = null;//下單物件
        SKReplyLib m_pSKReply = null;//回應物件

        string futAcct;//期貨帳號
        string osFutAcct;//海期帳號
        int nCode;

        bool isFirst = true;
        #endregion

        #region 建構子
        public FormAccount()
        {
            InitializeComponent();
        }

        private void FormAccount_Load(object sender, EventArgs e)
        {
            // 初始化物件
            m_pSKCenter = new SKCenterLib();
            m_pSKReply = new SKReplyLib();
            m_pSKOrder = new SKOrderLib();

            // // 取出序列化使用者
            string path = ConfigurationManager.AppSettings["UserSavePath"];
            if (File.Exists(path + "\\" + "UserProfile"))
            {
                FileStream fs = new FileStream(path + "\\" + "UserProfile", FileMode.Open);
                IFormatter formatter = new BinaryFormatter();
                UserProfile user = (UserProfile)formatter.Deserialize(fs);
                fs.Close();

                txtCapitalAcct.Text = user.CapitalUserId;
                txtCapitalPwd.Text = user.CapitalUserPwd;
            }
        }
        #endregion

        #region 動作
        private void btnCapitalLoginTest_Click(object sender, EventArgs e)
        {
            if (txtCapitalAcct.Text.Trim() == "")
            {
                MessageBox.Show("請輸入帳號");
                return;
            }
            if (txtCapitalPwd.Text.Trim() == "")
            {
                MessageBox.Show("請輸入密碼");
                return;
            }
            cboFutureAccount.Items.Clear();

            // 註冊事件
            if (isFirst == true)
            {
                // 註冊公告事件
                m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(this.m_pSKReply_OnAnnouncement);

                // 註冊登入帳號事件
                m_pSKOrder.OnAccount += new _ISKOrderLibEvents_OnAccountEventHandler(m_OrderObj_OnAccount);
                isFirst = false;
            }

            // 不用 SGX DMA
            m_pSKCenter.SKCenterLib_SetAuthority(1);

            // 呼叫群益帳號登入
            nCode = m_pSKCenter.SKCenterLib_Login(txtCapitalAcct.Text.Trim().ToUpper(), txtCapitalPwd.Text.Trim());

            // 取得回傳訊息
            string msg = GetMessage("登入", nCode);
            txtMessage.AppendText(msg + "\n");

            if (nCode != 0 && nCode != 2003)
            {
                // 失敗
                return;
            }

            // 下單物件初始化
            nCode = m_pSKOrder.SKOrderLib_Initialize();
            msg = GetMessage("下單物件初始化", nCode);
            txtMessage.AppendText(msg + "\n");

            // 讀取憑證
            nCode = m_pSKOrder.ReadCertByID(txtCapitalAcct.Text);
            msg = GetMessage("讀取憑證", nCode);
            txtMessage.AppendText(msg + "\n");
            if (nCode != 0)
            {
                // 失敗
                return;
            }

            //取得下單帳號
            nCode = m_pSKOrder.GetUserAccount();
            msg = GetMessage("取得下單帳號", nCode);
            txtMessage.AppendText(msg + "\n");

            if (nCode != 0)
                return;

            MessageBox.Show("測試登入正常");
        }

        /// <summary>
        /// 儲存帳號資訊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCapitalAcct.Text.Trim() == "")
            {
                MessageBox.Show("請輸入帳號");
                return;
            }
            if (txtCapitalPwd.Text.Trim() == "")
            {
                MessageBox.Show("請輸入密碼");
                return;
            }

            UserProfile user = new UserProfile();
            user.CapitalUserId = txtCapitalAcct.Text.Trim();
            user.CapitalUserPwd = txtCapitalPwd.Text.Trim();
            user.FutureAccount = ComboUtil.GetItem(cboFutureAccount).Value;

            // 將物件序列化儲存
            string path = ConfigurationManager.AppSettings["UserSavePath"];
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // 序列化
            FileStream fs = new FileStream(path + "\\" + "UserProfile", FileMode.Create);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, user);
            fs.Close();

            MessageBox.Show("已儲存");
        }
        #endregion

        #region 方法
        /// <summary>
        /// 取得群益api回傳訊息說明
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="nCode"></param>
        /// <param name="strMessage"></param>
        private string GetMessage(string strType, int nCode)
        {
            string strInfo = "";

            if (nCode != 0)
                strInfo = "【" + m_pSKCenter.SKCenterLib_GetLastLogInfo() + "】";

            string message = "【" + strType + "】【" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + "】" + strInfo;
            return message;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 取得下單帳號回傳事件
        /// </summary>
        /// <param name="bstrLogInID"></param>
        /// <param name="bstrAccountData"></param>
        void m_OrderObj_OnAccount(string bstrLogInID, string bstrAccountData)
        {
            string[] strValues;
            strValues = bstrAccountData.Split(',');

            if (strValues[0] == "TF")
            {
                // 取得期貨帳號
                futAcct = strValues[1] + strValues[3];

                cboFutureAccount.Items.Add(new ComboboxItem(futAcct, futAcct + "-" + strValues[5]));
                cboFutureAccount.SelectedIndex = 0;
            }

        }

        /// <summary>
        /// 公告
        /// </summary>
        void m_pSKReply_OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
        {
            nConfirmCode = -1;
        }

        #endregion

    }
}

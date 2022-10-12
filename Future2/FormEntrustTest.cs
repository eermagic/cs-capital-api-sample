using Future2.Classes;
using Future2.Properties;
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
    public partial class FormEntrustTest : Form
    {
        #region 屬性
        SKCenterLib m_pSKCenter = new SKCenterLib();//登入&環境物件
        SKOrderLib m_pSKOrder = new SKOrderLib();//下單物件
        SKReplyLib m_pSKReply = new SKReplyLib();//回應物件
        UserProfile user = null; // 使用者檔案

        int nCode;
        bool isLogined = false;
        #endregion

        #region 建構子
        public FormEntrustTest()
        {
            InitializeComponent();
        }

        private void FormEntrustTest_Load(object sender, EventArgs e)
        {
            // 加入商品下拉
            cboSymbolCode.Items.Add(new ComboboxItem("TX00", "大台指期近月"));
            cboSymbolCode.Items.Add(new ComboboxItem("MTX00", "小台指期近月"));
            cboSymbolCode.Items.Add(new ComboboxItem("TE00", "電子期近月"));
            cboSymbolCode.Items.Add(new ComboboxItem("ZE0000", "小型電子期近月"));
            cboSymbolCode.SelectedIndex = 0;

            // 加入方向下拉
            cboBuySell.Items.Add(new ComboboxItem("0", "BUY"));
            cboBuySell.Items.Add(new ComboboxItem("1", "SELL"));
            cboBuySell.SelectedIndex = 0;

            //加入方式下拉
            cboTradeType.Items.Add(new ComboboxItem("0", "ROD"));
            cboTradeType.Items.Add(new ComboboxItem("1", "IOC"));
            cboTradeType.Items.Add(new ComboboxItem("2", "FOK"));
            cboTradeType.SelectedIndex = 0;

            // 加入倉別下拉
            cboNewClose.Items.Add(new ComboboxItem("0", "新倉"));
            cboNewClose.Items.Add(new ComboboxItem("1", "平倉"));
            cboNewClose.Items.Add(new ComboboxItem("2", "自動"));
            cboNewClose.SelectedIndex = 0;

            // 註冊群益 API 事件
            m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(this.m_pSKReply_OnAnnouncement); //公告
            m_pSKReply.OnNewData += new _ISKReplyLibEvents_OnNewDataEventHandler(this.m_SKReplyLib_OnNewData);//交易回報事件

            // 取出序列化使用者
            string path = ConfigurationManager.AppSettings["UserSavePath"];
            if (File.Exists(path + "\\" + "UserProfile"))
            {
                FileStream fs = new FileStream(path + "\\" + "UserProfile", FileMode.Open);
                IFormatter formatter = new BinaryFormatter();
                user = (UserProfile)formatter.Deserialize(fs);
                fs.Close();

                if (user.FutureAccount == "")
                {
                    MessageBox.Show("尚未設定期貨下單帳號");
                    return;
                }
            }
            else
            {
                MessageBox.Show("請先儲存帳號設定");
                return;
            }
        }
        #endregion

        #region 動作
        /// <summary>
        /// 送出委託單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendEntrust_Click(object sender, EventArgs e)
        {
            if (cboSymbolCode.Text == "")
            {
                MessageBox.Show("請輸入 [商品]");
                return;
            }
            if (txtPrice.Text == "")
            {
                MessageBox.Show("請輸入 [價格]");
                return;
            }
            if (cboBuySell.Text == "")
            {
                MessageBox.Show("請輸入 [方向]");
                return;
            }
            if (cboTradeType.Text == "")
            {
                MessageBox.Show("請輸入 [方式]");
                return;
            }
            if (cboNewClose.Text == "")
            {
                MessageBox.Show("請輸入 [倉別]");
                return;
            }
            if (txtQty.Text == "")
            {
                MessageBox.Show("請輸入 [口數]");
                return;
            }


            if (isLogined == false)
            {
                // 群益登入
                m_pSKCenter.SKCenterLib_SetAuthority(1);

                // 呼叫群益帳號登入
                nCode = m_pSKCenter.SKCenterLib_Login(user.CapitalUserId, user.CapitalUserPwd);

                // 取得回傳訊息
                string msg = GetMessage("登入", nCode);
                if (nCode != 0 && nCode != 2003)
                {
                    // 失敗
                    txtMessage.AppendText(msg + "\n");
                    return;
                }

                // 下單物件初始化
                nCode = m_pSKOrder.SKOrderLib_Initialize();
                msg = GetMessage("下單物件初始化", nCode);
                txtMessage.AppendText(msg + "\n");

                // 讀取憑證
                nCode = m_pSKOrder.ReadCertByID(user.CapitalUserId);

                if (nCode != 0 && nCode != 2005)
                {
                    // 失敗
                    msg = GetMessage("讀取憑證", nCode);
                    txtMessage.AppendText(msg + "\n");
                    return;
                }
                isLogined = true;
            }


            //建立期權下單物件
            FUTUREORDER pFutureOrder = new FUTUREORDER();
            pFutureOrder.bstrFullAccount = user.FutureAccount;//期貨帳號，分公司代碼＋帳號7碼
            pFutureOrder.bstrPrice = txtPrice.Text;//委託價格，「M」表示市價，「P」表示範圍市價
            pFutureOrder.bstrStockNo = ComboUtil.GetItem(cboSymbolCode).Value;//委託期權代號
            pFutureOrder.nQty = Convert.ToInt32(txtQty.Text);//交易口數
            pFutureOrder.sBuySell = Convert.ToInt16(ComboUtil.GetItem(cboBuySell).Value);//0:買進 1:賣出
            pFutureOrder.sDayTrade = 0;// 當沖0:否 1:是，可當沖商品請參考交易所規定。
            pFutureOrder.sTradeType = Convert.ToInt16(ComboUtil.GetItem(cboTradeType).Value); //方式；0:ROD  1:IOC  2:FOK
            pFutureOrder.sNewClose = Convert.ToInt16(ComboUtil.GetItem(cboNewClose).Value);//新平倉，0:新倉 1:平倉 2:自動{新期貨、選擇權使用}
            pFutureOrder.sReserved = 0;//盤別，0:盤中(T盤及T+1盤)；1:T盤預約

            string orderNo = "";
            nCode = m_pSKOrder.SendFutureOrder(user.CapitalUserId, false, pFutureOrder, out orderNo); //採用同步呼叫
            txtMessage.AppendText(GetMessage("期貨委託", nCode) + "\n");

            if (nCode == 0)
            {
                txtMessage.AppendText("委託成功！委託序號：" + orderNo);
            }
            else
            {
                txtMessage.AppendText("委託失敗！原因：" + orderNo);
            }
            txtMessage.ScrollToCaret();
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
        /// 公告
        /// </summary>
        void m_pSKReply_OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
        {
            nConfirmCode = -1;
        }

        /// <summary>
        /// 交易回報事件
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="strData"></param>
        void m_SKReplyLib_OnNewData(string strUserID, string strData)
        {
            try
            {
                txtMessage.AppendText("交易回報資料：" + strData);
                string[] datas = strData.Split(',');

                // 委託序號
                string entrustNo = datas[0];
                if (entrustNo == "")
                {
                    entrustNo = datas[47];
                }

                // 交易狀態
                string OrderStatus = "";
                string date = "";
                double? TradePrice = null;
                if (datas[2] == "N")
                {
                    OrderStatus = "委託";
                }
                else if (datas[2] == "D")
                {
                    OrderStatus = "成交";

                    //成交時間
                    date = datas[23];
                    if (date.Length == 8)
                    {
                        date = date.Substring(0, 4) + "/" + date.Substring(4, 2) + "/" + date.Substring(6, 2);
                    }
                    // 成交價格
                    TradePrice = Convert.ToDouble(datas[11]);
                }
                else if (datas[2] == "C")
                {
                    OrderStatus = "取消";
                }

                // 交易訊息
                string OrderMsg = "";
                if (datas[3] == "Y")
                {
                    OrderMsg = "失敗";
                }
                else if (datas[3] == "T")
                {
                    OrderMsg = "逾時";
                }

                txtMessage.AppendText($"交易結果：單號={entrustNo} 狀態={OrderStatus} 成交時間={date} 成交價格={TradePrice}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtMessage.AppendText(ex.Message + "\n" + ex.StackTrace + "\n");
            }
        }
        #endregion

    }
}

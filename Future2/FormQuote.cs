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
    public partial class FormQuote : Form
    {
        #region 屬性
        int nCode = 0;
        SKCenterLib m_pSKCenter = null;// 登入&環境物件
        SKQuoteLib m_SKQuoteLib = null;// 國內報價物件
        SKReplyLib m_pSKReply = null;// 回應物件

        UserProfile user = null;// 使用者物件

        DataTable dtQuote = null;// 報價資料
        DataTable dtTick = null;// Tick 資料
        DataTable dtBest5 = null;// Best5 資料

        short sPage = -1; // 報價頁編號
        double dDigitNum = 0.000; // 小數位
        #endregion

        #region 建構子
        public FormQuote()
        {
            InitializeComponent();
        }

        private void FormQuote_Load(object sender, EventArgs e)
        {
            // 初始設定
            gvBest5Merge.AutoGenerateColumns = false;

            // 初始化物件
            m_pSKCenter = new SKCenterLib();
            m_pSKReply = new SKReplyLib();
            m_SKQuoteLib = new SKQuoteLib();

            // 註冊公告事件
            m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(this.m_pSKReply_OnAnnouncement);

            // 國內報價連線狀態事件
            m_SKQuoteLib.OnConnection += new _ISKQuoteLibEvents_OnConnectionEventHandler(m_SKQuoteLib_OnConnection);

            // 國內報價事件
            m_SKQuoteLib.OnNotifyQuoteLONG += new _ISKQuoteLibEvents_OnNotifyQuoteLONGEventHandler(m_SKQuoteLib_OnNotifyQuoteLONG);

            // 國內 Tick 回傳事件
            m_SKQuoteLib.OnNotifyTicksLONG += new _ISKQuoteLibEvents_OnNotifyTicksLONGEventHandler(m_SKQuoteLib_OnNotifyTicks);

            // 國內 Best5 回傳事件
            m_SKQuoteLib.OnNotifyBest5LONG += new _ISKQuoteLibEvents_OnNotifyBest5LONGEventHandler(m_SKQuoteLib_OnNotifyBest5);

            // 加入商品
            cboCommID.Items.Add(new ComboboxItem("TX00", "大台指期近月"));
            cboCommID.Items.Add(new ComboboxItem("MTX00", "小台指期近月"));
            cboCommID.Items.Add(new ComboboxItem("TE00", "電子期近月"));
            cboCommID.Items.Add(new ComboboxItem("ZE0000", "小型電子期近月"));
            cboCommID.SelectedIndex = 0;
        }
        #endregion

        #region 動作
        /// <summary>
        /// 開始報價
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartQuote_Click(object sender, EventArgs e)
        {
            // 取出序列化使用者
            string path = ConfigurationManager.AppSettings["UserSavePath"];
            if (File.Exists(path + "\\" + "UserProfile"))
            {
                FileStream fs = new FileStream(path + "\\" + "UserProfile", FileMode.Open);
                IFormatter formatter = new BinaryFormatter();
                user = (UserProfile)formatter.Deserialize(fs);
                fs.Close();
            }
            else
            {
                MessageBox.Show("請先完成帳號設定儲存");
                return;
            }

            // 不用 SGX DMA
            m_pSKCenter.SKCenterLib_SetAuthority(1);

            // 登入群益帳戶
            nCode = m_pSKCenter.SKCenterLib_Login(user.CapitalUserId, user.CapitalUserPwd);
            if (nCode != 0 && nCode != 2003)
            {
                txtMessage.AppendText(GetMessage("登入", nCode) + "\n");
                return;
            }

            // 國內報價連線
            nCode = m_SKQuoteLib.SKQuoteLib_EnterMonitorLONG();
            txtMessage.AppendText(GetMessage("國內報價連線", nCode) + "\n");
            if (nCode != 0)
            {
                return;
            }

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

        /// <summary>
        /// 訂閱最新報價
        /// </summary>
        private void RequestQuote()
        {
            // 取回商品報價的相關資訊
            SKSTOCKLONG pSKStockLONG = new SKSTOCKLONG();
            nCode = m_SKQuoteLib.SKQuoteLib_GetStockByNoLONG(ComboUtil.GetItem(cboCommID).Value, ref pSKStockLONG);
            txtMessage.AppendText(GetMessage("取回商品報價的相關資訊", nCode) + "\n");

            // 將報價資訊物件輸出在 DataGridView
            onUpdateQuote(pSKStockLONG);

            if (nCode != 0)
            {
                // 發生錯誤
                return;
            }

            // 更新價格小數位
            dDigitNum = (Math.Pow(10, pSKStockLONG.sDecimal));

            //訂閱商品即時報價，訂閱後等待 OnNotifyQuoteLONG 事件回報
            nCode = m_SKQuoteLib.SKQuoteLib_RequestStocks(ref sPage, ComboUtil.GetItem(cboCommID).Value);
            txtMessage.AppendText(GetMessage("訂閱商品即時報價", nCode) + "\n");
        }

        /// <summary>
        /// 更新最新報價
        /// </summary>
        private void onUpdateQuote(SKSTOCKLONG pSKStockLONG)
        {
            if (dtQuote == null)
            {
                // 報價物件寫入 Datatable
                dtQuote = new DataTable();
                dtQuote.Columns.Add("QuoteName");
                dtQuote.Columns.Add("QuoteValue");
                DataRow drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "代碼";
                drNew["QuoteValue"] = pSKStockLONG.bstrStockNo;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "名稱";
                drNew["QuoteValue"] = pSKStockLONG.bstrStockName;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "開盤價";
                drNew["QuoteValue"] = pSKStockLONG.nOpen / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "最高價";
                drNew["QuoteValue"] = pSKStockLONG.nHigh / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "最低價";
                drNew["QuoteValue"] = pSKStockLONG.nLow / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "成交價";
                drNew["QuoteValue"] = pSKStockLONG.nClose / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "單量";
                drNew["QuoteValue"] = pSKStockLONG.nTickQty;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "昨收價";
                drNew["QuoteValue"] = pSKStockLONG.nRef / (Math.Pow(10, pSKStockLONG.sDecimal));
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "買價";
                drNew["QuoteValue"] = (pSKStockLONG.nBid / (Math.Pow(10, pSKStockLONG.sDecimal))).ToString();
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "買量";
                drNew["QuoteValue"] = pSKStockLONG.nBc;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "賣價";
                drNew["QuoteValue"] = (pSKStockLONG.nAsk / (Math.Pow(10, pSKStockLONG.sDecimal))).ToString();
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "賣量";
                drNew["QuoteValue"] = pSKStockLONG.nAc;
                dtQuote.Rows.Add(drNew);

                drNew = dtQuote.NewRow();
                drNew["QuoteName"] = "總量";
                drNew["QuoteValue"] = pSKStockLONG.nTQty;
                dtQuote.Rows.Add(drNew);

                //輸出 GridView
                gvQuote.DataSource = dtQuote;
            }
            else
            {
                // 報價物件更新 Datatable
                DataRow dr = dtQuote.Select("QuoteName='開盤價'")[0];
                dr["QuoteValue"] = pSKStockLONG.nOpen / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='最高價'")[0];
                dr["QuoteValue"] = pSKStockLONG.nHigh / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='最低價'")[0];
                dr["QuoteValue"] = pSKStockLONG.nLow / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='成交價'")[0];
                dr["QuoteValue"] = pSKStockLONG.nClose / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='單量'")[0];
                dr["QuoteValue"] = pSKStockLONG.nTickQty;

                dr = dtQuote.Select("QuoteName='昨收價'")[0];
                dr["QuoteValue"] = pSKStockLONG.nRef / (Math.Pow(10, pSKStockLONG.sDecimal));

                dr = dtQuote.Select("QuoteName='買價'")[0];
                dr["QuoteValue"] = (pSKStockLONG.nBid / (Math.Pow(10, pSKStockLONG.sDecimal))).ToString();

                dr = dtQuote.Select("QuoteName='買量'")[0];
                dr["QuoteValue"] = pSKStockLONG.nBc;

                dr = dtQuote.Select("QuoteName='賣價'")[0];
                dr["QuoteValue"] = (pSKStockLONG.nAsk / (Math.Pow(10, pSKStockLONG.sDecimal))).ToString();

                dr = dtQuote.Select("QuoteName='賣量'")[0];
                dr["QuoteValue"] = pSKStockLONG.nAc;

                dr = dtQuote.Select("QuoteName='總量'")[0];
                dr["QuoteValue"] = pSKStockLONG.nTQty;
            }
        }

        /// <summary>
        /// 訂閱 Tick & Best5
        /// </summary>
        private void RequestTickBest5()
        {
            //訂閱 Tick & Best5，訂閱後等待 OnNotifyTicks 及 OnNotifyBest5 事件回報
            nCode = m_SKQuoteLib.SKQuoteLib_RequestTicks(ref sPage, ComboUtil.GetItem(cboCommID).Value);
            txtMessage.AppendText(GetMessage("訂閱 Tick & Best5", nCode) + "\n");
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
        /// 國內報價連線回應事件
        /// </summary>
        /// <param name="nKind"></param>
        /// <param name="nCode"></param>
        void m_SKQuoteLib_OnConnection(int nKind, int nCode)
        {
            if (nKind == 3001)
            {
                if (nCode == 0)
                {
                    // 連線中
                    lblTwSignal.ForeColor = Color.Blue;
                    lblTwSignal.Text = "連線狀態：連線中";
                }
            }
            else if (nKind == 3002)
            {
                // 連線中斷
                lblTwSignal.ForeColor = Color.Red;
                lblTwSignal.Text = "連線狀態：中斷";

                btnStartQuote.Enabled = true;
            }
            else if (nKind == 3003)
            {
                // 連線成功
                lblTwSignal.ForeColor = Color.Green;
                lblTwSignal.Text = "連線狀態：正常";

                // 訂閱最新報價
                RequestQuote();

                // 訂閱 Tick & Best5
                RequestTickBest5();

                btnStartQuote.Enabled = false;
            }
            else if (nKind == 3021)
            {
                //網路斷線
                lblTwSignal.ForeColor = Color.DarkRed;
                lblTwSignal.Text = "連線狀態：網路斷線";
            }
        }

        /// <summary>
        /// 國內報價回應事件
        /// </summary>
        /// <param name="sMarketNo"></param>
        /// <param name="nStockIdx"></param>
        void m_SKQuoteLib_OnNotifyQuoteLONG(short sMarketNo, int nStockIdx)
        {
            // 報價資訊物件
            SKSTOCKLONG pSKStockLONG = new SKSTOCKLONG();

            // 取得最新報價寫入報價資訊物件
            m_SKQuoteLib.SKQuoteLib_GetStockByIndexLONG(sMarketNo, nStockIdx, ref pSKStockLONG);

            // 將報價資訊物件輸出在 DataGridView
            onUpdateQuote(pSKStockLONG);
        }

        /// <summary>
        /// 國內 Tick 回傳事件
        /// </summary>
        void m_SKQuoteLib_OnNotifyTicks(short sMarketNo, int nStockIdx, int nPtr, int nDate, int lTimehms, int lTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate)
        {
            DataRow dr = null;

            // 轉化時間格式為 yyyy/MM/dd HH:mm:ss.sss
            string date = (nDate.ToString().Substring(0, 4) + "/" + nDate.ToString().Substring(4, 2) + "/" + nDate.ToString().Substring(6));
            string time = lTimehms.ToString("000000").Substring(0, 2) + ":" + lTimehms.ToString("000000").Substring(2, 2) + ":" + lTimehms.ToString("000000").Substring(4) + "." + lTimemillismicros.ToString("000000").Substring(0, 3);

            if (dtTick == null)
            {
                // 報價物件寫入 Datatable
                dtTick = new DataTable();
                dtTick.Columns.Add("TickName");
                dtTick.Columns.Add("TickValue");
                dr = dtTick.NewRow();
                dr["TickName"] = "日期";
                dr["TickValue"] = date;
                dtTick.Rows.Add(dr);

                dr = dtTick.NewRow();
                dr["TickName"] = "時間";
                dr["TickValue"] = time;
                dtTick.Rows.Add(dr);

                dr = dtTick.NewRow();
                dr["TickName"] = "委買價";
                dr["TickValue"] = nBid / dDigitNum;
                dtTick.Rows.Add(dr);

                dr = dtTick.NewRow();
                dr["TickName"] = "委賣價";
                dr["TickValue"] = nAsk / dDigitNum;
                dtTick.Rows.Add(dr);

                dr = dtTick.NewRow();
                dr["TickName"] = "成交價";
                dr["TickValue"] = nClose / dDigitNum;
                dtTick.Rows.Add(dr);

                dr = dtTick.NewRow();
                dr["TickName"] = "數量";
                dr["TickValue"] = nQty;
                dtTick.Rows.Add(dr);

                //輸出 GridView
                gvTick.DataSource = dtTick;
            }
            else
            {
                // 報價物件更新 Datatable
                dr = dtTick.Select("TickName='日期'")[0];
                dr["TickValue"] = date;

                dr = dtTick.Select("TickName='時間'")[0];
                dr["TickValue"] = time;

                dr = dtTick.Select("TickName='委買價'")[0];
                dr["TickValue"] = nBid / dDigitNum;

                dr = dtTick.Select("TickName='委賣價'")[0];
                dr["TickValue"] = nAsk / dDigitNum;

                dr = dtTick.Select("TickName='成交價'")[0];
                dr["TickValue"] = nClose / dDigitNum;

                dr = dtTick.Select("TickName='數量'")[0];
                dr["TickValue"] = nQty;
            }
        }

        /// <summary>
        /// 國內 Best5 回傳事件
        /// </summary>
        void m_SKQuoteLib_OnNotifyBest5(short sMarketNo, int nStockIdx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nExtendBid, int nExtendBidQty, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5, int nExtendAsk, int nExtendAskQty, int nSimulate)
        {
            DataRow dr = null;

            if (dtBest5 == null)
            {
                // 報價物件寫入 Datatable
                dtBest5 = new DataTable();
                dtBest5.Columns.Add("Seq");
                dtBest5.Columns.Add("Best5BidQty");
                dtBest5.Columns.Add("Best5BidPrice");
                dtBest5.Columns.Add("Best5AskPrice");
                dtBest5.Columns.Add("Best5AskQty");

                // 價格除以 dDigitNum，是因為來的資料裡面會多 2 個 0，而預設 dDigitNum 是 100，所以要除掉 2 個 0

                dr = dtBest5.NewRow();
                dr["Seq"] = "1";
                dr["Best5BidQty"] = nBestBidQty1;
                dr["Best5BidPrice"] = nBestBid1 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty1;
                dr["Best5AskPrice"] = nBestAsk1 / dDigitNum;
                dtBest5.Rows.Add(dr);

                dr = dtBest5.NewRow();
                dr["Seq"] = "2";
                dr["Best5BidQty"] = nBestBidQty2;
                dr["Best5BidPrice"] = nBestBid2 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty2;
                dr["Best5AskPrice"] = nBestAsk2 / dDigitNum;
                dtBest5.Rows.Add(dr);

                dr = dtBest5.NewRow();
                dr["Seq"] = "3";
                dr["Best5BidQty"] = nBestBidQty3;
                dr["Best5BidPrice"] = nBestBid3 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty3;
                dr["Best5AskPrice"] = nBestAsk3 / dDigitNum;
                dtBest5.Rows.Add(dr);

                dr = dtBest5.NewRow();
                dr["Seq"] = "4";
                dr["Best5BidQty"] = nBestBidQty4;
                dr["Best5BidPrice"] = nBestBid4 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty4;
                dr["Best5AskPrice"] = nBestAsk4 / dDigitNum;
                dtBest5.Rows.Add(dr);

                dr = dtBest5.NewRow();
                dr["Seq"] = "5";
                dr["Best5BidQty"] = nBestBidQty5;
                dr["Best5BidPrice"] = nBestBid5 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty5;
                dr["Best5AskPrice"] = nBestAsk5 / dDigitNum;
                dtBest5.Rows.Add(dr);

                //輸出 GridView
                gvBest5Merge.DataSource = dtBest5;
            }
            else
            {
                // 報價物件更新 Datatable
                dr = dtBest5.Select("Seq='1'")[0];
                dr["Best5BidQty"] = nBestBidQty1;
                dr["Best5BidPrice"] = nBestBid1 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty1;
                dr["Best5AskPrice"] = nBestAsk1 / dDigitNum;

                dr = dtBest5.Select("Seq='2'")[0];
                dr["Best5BidQty"] = nBestBidQty2;
                dr["Best5BidPrice"] = nBestBid2 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty2;
                dr["Best5AskPrice"] = nBestAsk2 / dDigitNum;

                dr = dtBest5.Select("Seq='3'")[0];
                dr["Best5BidQty"] = nBestBidQty3;
                dr["Best5BidPrice"] = nBestBid3 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty3;
                dr["Best5AskPrice"] = nBestAsk3 / dDigitNum;

                dr = dtBest5.Select("Seq='4'")[0];
                dr["Best5BidQty"] = nBestBidQty4;
                dr["Best5BidPrice"] = nBestBid4 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty4;
                dr["Best5AskPrice"] = nBestAsk4 / dDigitNum;

                dr = dtBest5.Select("Seq='5'")[0];
                dr["Best5BidQty"] = nBestBidQty5;
                dr["Best5BidPrice"] = nBestBid5 / dDigitNum;
                dr["Best5AskQty"] = nBestAskQty5;
                dr["Best5AskPrice"] = nBestAsk5 / dDigitNum;
            }
        }
        #endregion


    }
}

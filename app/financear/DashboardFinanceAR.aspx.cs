using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.FINANCEAR
{ 

public partial class DashboardFinanceAR : System.Web.UI.Page
{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack) Act.ProjectList(project);
            init();
            fillChartBarFin();
        }
        private void init()
        {
            Cf.BindTahun(tahun3);
        }
        void fillChartBarFin()
        {
            if (tahun3.SelectedIndex != 0)
            {
                string data = "";
                for (int i = 1; i <= 12; i++)
                {
                    decimal tts = Db.SingleDecimal("SELECT ISNULL (SUM(Total),0) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE Status = 'POST' AND MONTH(TglTTS) = '" + i + "'AND YEAR(TglTTS) = '" + tahun3.SelectedValue + "' AND Project = '"+project.SelectedValue+"'");
                    decimal totaltts = Db.SingleDecimal("SELECT ISNULL (SUM(Total),0) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE Status = 'POST' AND YEAR(TglTTS) = '" + tahun3.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");
                    decimal totalmemo = Db.SingleDecimal("SELECT ISNULL (SUM(Total),0) FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO WHERE Status = 'POST' AND YEAR(TglMEMO) = '" + tahun3.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");
                    decimal memo = Db.SingleDecimal("SELECT ISNULL (SUM(Total),0) FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO WHERE Status = 'POST' AND MONTH(TglMEMO) = '" + i + "'AND YEAR(TglMEMO) = '" + tahun3.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");
                    decimal total = tts + memo;
                    decimal total2 = totaltts + totalmemo;
                    labelthn3.InnerHtml = tahun3.SelectedValue;

                    data += "{\"periode\": \"" + Category[i - 1] + "\", "
                    + "    \"memo\": " + memo + ", "
                    + "    \"kuitansi\": " + tts + ", "
                    + "    \"total\": " + total + ""
                    + " },";
                    ttslegend.InnerHtml = Cf.Num(totaltts);
                    memolegend.InnerHtml = Cf.Num(totalmemo);
                    totallegend.InnerHtml = Cf.Num(total2);
                }
                //data = data.Remove(data.Length-1,1);
                string script = "<script>var chartBarFin = [ " + data + " ]; </script>";
                Response.Write(script);
            }
            else
            {
                tahun3.SelectedValue = DateTime.Today.Year.ToString();
                string data = "";
                for (int i = 1; i <= 12; i++)
                {
                    decimal tts = Db.SingleDecimal("SELECT ISNULL (SUM(Total),0) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE Status = 'POST' AND MONTH(TglTTS) = '" + i + "'AND YEAR(TglTTS) = '" + DateTime.Now.Year + "' AND Project = '" + project.SelectedValue + "'");
                    decimal totaltts = Db.SingleDecimal("SELECT ISNULL (SUM(Total),0) FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE Status = 'POST' AND YEAR(TglTTS) = '" + DateTime.Now.Year + "' AND Project = '" + project.SelectedValue + "'");
                    decimal totalmemo = Db.SingleDecimal("SELECT ISNULL (SUM(Total),0) FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO WHERE Status = 'POST' AND YEAR(TglMEMO) = '" + DateTime.Now.Year + "' AND Project = '" + project.SelectedValue + "'");
                    decimal memo = Db.SingleDecimal("SELECT ISNULL (SUM(Total),0) FROM " + Mi.DbPrefix + "FINANCEAR..MS_MEMO WHERE Status = 'POST' AND MONTH(TglMEMO) = '" + i + "'AND YEAR(TglMEMO) = '" + DateTime.Now.Year + "' AND Project = '" + project.SelectedValue + "'");
                    decimal total = tts + memo;
                    decimal total2 = totaltts + totalmemo;
                    labelthn3.InnerHtml = DateTime.Today.Year.ToString();

                    data += "{\"periode\": \"" + Category[i - 1] + "\", "
                    + "    \"memo\": " + memo + ", "
                    + "    \"kuitansi\": " + tts + ", "
                    + "    \"total\": " + total + ""
                    + " },";
                    ttslegend.InnerHtml = Cf.Num(totaltts);
                    memolegend.InnerHtml = Cf.Num(totalmemo);
                    totallegend.InnerHtml = Cf.Num(total2);
                }
                //data = data.Remove(data.Length-1,1);
                string script = "<script>var chartBarFin = [ " + data + " ]; </script>";
                Response.Write(script);
            }
        }
        string[] FontColor
        {
            get
            {
                string[] x = {
                    "1DC7EA",
                    "FB404B",
                    "FFA534",
                    "9368E9",
                    "87CB16",
                    "1F77D0",
                    "5e5e5e",
                    "dd4b39",
                    "35465c",
                    "e52d27",
                    "55acee",
                    "cc2127",
                    "1769ff",
                    "6188e2",
                    "a748ca",
                    "ababab",
                    "00ffea",
                    "00ff84",
                    "ffcc00",
                    "a98700",
                    "119902",
                    "c1ebbd",
                    "6361ab",
                    "ed0097",
                    "9f4980",
                    "cd0000",
                    "ff00fc",
                    "ff9037",
                    "ba5300",
                    "ffca65",
                    "00f6ff",
                    "6cadaf",
                    "9e0af3","","","","",
                    "","","","","","","","","","","","","","","","","","","","","","",
                    "","","","","","","","","","","","","","","","","","","","","","",
                    "","","","","","","","","","","","","","","","","","","","","","",
                    "","","","","","","","","","","","","","","","","","","","","",""
                };
                return x;
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected string[] Category
        {
            get
            {
                string[] x = { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };
                return x;
            }
        }

        protected string[] CategoryColl
        {
            get
            {
                string[] x = { "Piutang", "Pelunasan", "Sisa" };
                return x;
            }
        }
        protected string[] CategoryPenjualan
        {
            get
            {
                string[] x = { "Penjualan Bersih", "Batal", "Penjualan" };
                return x;
            }
        }

        void FillLegend(string[] ListColor, string[] Text, Label Legend)
        {
            Legend.Text += "<center><div style='border:solid 1px #C4C4C4;'>";
            for (int i = 0; i < Text.Length - 1; i++)
            {
                Legend.Text += "<div style='margin:5px;float:left;padding:2px;'>"
                            + "<div style='width:12px;height:12px;margin-right:10px;background:#" + ListColor[i] + "'></div>" + Text[i]
                            + ""
                            + "</div>";
            }
            Legend.Text += "</div></center>";
        }
    }
}
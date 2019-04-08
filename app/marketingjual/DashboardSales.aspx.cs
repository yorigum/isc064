using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.IO;

namespace ISC064.MARKETINGJUAL
{

    public partial class DashboardSales : System.Web.UI.Page
{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Act.ProjectList(project2);
            }
            init();
            fillChartBarPenjualan();
            fillChartLine();
            fillChartPieSetupSales();
            fillChartLinePenjualan();
            fillChartCaraBayar();
        }
        private void init()
        {
            Cf.BindTahun(tahun);
            Cf.BindTahun(tahun2);
        }
        void fillChartLine()
        {
            if (tahun.SelectedIndex != 0)
            {
                string data = "";
                for (int i = 1; i <= 12; i++)
                {
                    int penjualan = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK WHERE Status = 'A' AND MONTH(TglKontrak) = '" + i + "' AND YEAR(TglKontrak) = '" + tahun.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");
                    int penjualan2 = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK WHERE Status = 'A' AND YEAR(TglKontrak) = '" + tahun.SelectedValue + "' AND Project = '"+project.SelectedValue+"'");
                    labelthn.InnerHtml = tahun.SelectedValue;

                    data += "{\"month\": \"" + Category[i - 1] + "\", "
                    + "    \"penjualan\": " + penjualan + ""
                    + " },";
                    penjualanl.InnerHtml = Cf.Num(penjualan2);
                }
                //data = data.Remove(data.Length-1,1);
                string script = "<script>var chartz22 = [ " + data + " ]; </script>";
                Response.Write(script);
            }
            else
            {
                tahun.SelectedValue = DateTime.Today.Year.ToString();
                string data = "";
                for (int i = 1; i <= 12; i++)
                {
                    int penjualan = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK WHERE Status = 'A' AND MONTH(TglKontrak) = '" + i + "' AND YEAR(TglKontrak) = '" + DateTime.Now.Year + "' AND Project = '" + project.SelectedValue + "'");
                    int penjualan2 = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK WHERE Status = 'A' AND YEAR(TglKontrak) = '" + DateTime.Now.Year + "'  AND Project = '" + project.SelectedValue + "'");
                    labelthn.InnerHtml = DateTime.Today.Year.ToString();

                    data += "{\"month\": \"" + Category[i - 1] + "\", "
                    + "    \"penjualan\": " + penjualan + ""
                    + " },";
                    penjualanl.InnerHtml = Cf.Num(penjualan2);
                }
                //data = data.Remove(data.Length-1,1);
                string script = "<script>var chartz22 = [ " + data + " ]; </script>";
                Response.Write(script);
            }

        }
        void fillChartLinePenjualan()
        {
            if (tahun2.SelectedIndex != 0)
            {
                string data = "";
                for (int i = 1; i <= 12; i++)
                {
                    decimal penjualan = Db.SingleDecimal("SELECT ISNULL (SUM(NilaiKontrak),0) FROM MS_KONTRAK WHERE Status = 'A' AND MONTH(TglKontrak) = '" + i + "' AND YEAR(TglKontrak) = '" + tahun2.SelectedValue + "' AND Project = '" + project2.SelectedValue + "'");
                    decimal penjualan2 = Db.SingleDecimal("SELECT ISNULL (SUM(NilaiKontrak),0) FROM MS_KONTRAK WHERE Status = 'A' AND YEAR(TglKontrak) = '" + tahun2.SelectedValue + "' AND Project = '" + project2.SelectedValue + "'");
                    omsetpenjualanl.InnerHtml = Cf.Num(penjualan2);
                    labelthn2.InnerHtml = tahun2.SelectedValue;

                    data += "{\"month\": \"" + Category[i - 1] + "\", "
                    + "    \"penjualan\": " + penjualan + ""
                    + " },";
                }
                //data = data.Remove(data.Length-1,1);
                string script = "<script>var chartLinePenjualan = [ " + data + " ]; </script>";
                Response.Write(script);
            }
            else
            {
                tahun2.SelectedValue = DateTime.Today.Year.ToString();
                string data = "";
                for (int i = 1; i <= 12; i++)
                {
                    decimal penjualan = Db.SingleDecimal("SELECT ISNULL (SUM(NilaiKontrak),0) FROM MS_KONTRAK WHERE Status = 'A' AND MONTH(TglKontrak) = '" + i + "' AND YEAR(TglKontrak) = '" + DateTime.Now.Year + "' AND Project = '" + project2.SelectedValue + "'");
                    decimal penjualan2 = Db.SingleDecimal("SELECT ISNULL (SUM(NilaiKontrak),0) FROM MS_KONTRAK WHERE Status = 'A' AND YEAR(TglKontrak) = '" + DateTime.Now.Year + "' AND Project = '" + project2.SelectedValue + "'");
                    omsetpenjualanl.InnerHtml = Cf.Num(penjualan2);
                    labelthn2.InnerHtml = DateTime.Today.Year.ToString();

                    data += "{\"month\": \"" + Category[i - 1] + "\", "
                    + "    \"penjualan\": " + penjualan + ""
                    + " },";
                }
                //data = data.Remove(data.Length-1,1);
                string script = "<script>var chartLinePenjualan = [ " + data + " ]; </script>";
                Response.Write(script);
            }

        }
        void fillChartPieSetupSales()
        {
            int available = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE Status = 'A' AND Project = '" + project2.SelectedValue + "' ");
            int sold = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE Status = 'B' AND Project = '" + project2.SelectedValue + "' ");
            int hold = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE Status = 'H' AND Project = '" + project2.SelectedValue + "' ");
            int totalsold = sold + hold;
            int total = available + sold + hold;

            string Script = "";

            Script += " <script type='text/javascript'>";
            Script += "var chartPieSetupSales =[{";
            Script += "      'title': 'Available',";
            Script += "      'color': '#1DC7EA',";
            Script += "      'value': " + available + "";
            Script += "   }, {";
            Script += "      'title': 'Sold',";
            Script += "      'color': '#FB404B',";
            Script += "       'value': " + totalsold + "";
            Script += "   }, {";
            Script += "      'title': 'Hold',";
            Script += "      'color': '#00e600',";
            Script += "       'value': " + hold + "";
            Script += "   }];";

            Script += "</script>";
            Response.Write(Script);
        }
        void fillChartBarPenjualan()
        {
            int penjualanb = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK WHERE Status = 'A' AND Project = '" + project2.SelectedValue + "' ");
            int batal = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK WHERE Status = 'B' AND Project = '" + project2.SelectedValue + "' ");
            int penjualan = penjualanb + batal;
            string Script = "";

            Script += " <script type='text/javascript'>";
            Script += "var chartz31 =[{";
            Script += "      'title': 'Penjualan',";
            Script += "      'color': '#1DC7EA',";
            Script += "      'periode': 'Penjualan',";
            Script += "      'penjualan': " + penjualan + "";
            Script += "   }, {";
            Script += "      'title': 'Batal',";
            Script += "      'color': '#FB404B',";
            Script += "      'periode': 'Batal',";
            Script += "       'batal': " + (batal) + "";
            Script += "   }, {";
            Script += "      'title': 'Penjualan Bersih',";
            Script += "      'color': '#FB404B',";
            Script += "      'periode': 'Penjualan Bersih',";
            Script += "       'penjualanb': " + (penjualanb) + "";
            Script += "   }];";

            Script += "</script>";
            Response.Write(Script);
        }
        void fillChartCaraBayar()
        {
            int kpa = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK WHERE Status = 'A' AND CaraBayar = 'KPA' AND Project = '" + project2.SelectedValue + "' ");
            int cash = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK WHERE Status = 'A' AND CaraBayar = 'CASH KERAS' AND Project = '" + project2.SelectedValue + "' ");
            int bertahap = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM MS_KONTRAK WHERE Status = 'A' AND CaraBayar = 'CASH BERTAHAP' AND Project = '" + project2.SelectedValue + "' ");

            string Script = "";

            Script += " <script type='text/javascript'>";
            Script += "var chartCaraBayar =[{";
            Script += "      'title': 'KPA',";
            Script += "      'color': '#1DC7EA',";
            Script += "      'value': " + kpa + "";
            Script += "   }, {";
            Script += "      'title': 'Cash Keras',";
            Script += "      'color': '#FB404B',";
            Script += "       'value': " + cash + "";
            Script += "   }, {";
            Script += "      'title': 'Cash Bertahap',";
            Script += "      'color': '#00e600',";
            Script += "       'value': " + bertahap + "";
            Script += "   }];";

            Script += "</script>";
            Response.Write(Script);
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
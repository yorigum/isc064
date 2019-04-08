using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.ADMINJUAL
{


    public partial class DashboardSetupSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if(!Page.IsPostBack) Act.ProjectList(project);

            fillChartPieSetupSales();
        }

        void fillChartPieSetupSales()
        {
            int available = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE Status = 'A' AND Project = '" + project.SelectedValue + "' ");
            int sold = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE Status = 'B' AND Project = '" + project.SelectedValue + "' ");
            int hold = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_UNIT WHERE Status = 'H' AND Project = '" + project.SelectedValue + "' ");
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
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

namespace ISC064.COLLECTION
{

    public partial class DashboardCollection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack) Act.ProjectList(project);
            fillChartPieColl();
            fillChartBarColl();
        }


        void fillChartBarColl()
        {
            decimal piutang = Db.SingleDecimal("SELECT ISNULL (SUM(NilaiTagihan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN A INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK B ON A.NoKontrak = B.NoKontrak  WHERE B.Status = 'A' AND B.Project = '" + project.SelectedValue + "' ");
            decimal pelunasan = Db.SingleDecimal("SELECT ISNULL (SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN A INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK B ON A.NoKontrak = B.NoKontrak WHERE Status = 'A' AND B.Project = '" + project.SelectedValue + "' ");
            decimal sisa = piutang - pelunasan;
            string Script = "";

            Script += " <script type='text/javascript'>";
            Script += "var chartz30 =[{";
            Script += "      'title': 'Piutang',";
            Script += "      'color': '#1DC7EA',";
            Script += "      'periode': 'Piutang',";
            Script += "      'piutang': " + piutang + "";
            Script += "   }, {";
            Script += "      'title': 'Pelunasan',";
            Script += "      'color': '#FB404B',";
            Script += "      'periode': 'Pelunasan',";
            Script += "       'pelunasan': " + (pelunasan) + "";
            Script += "   }, {";
            Script += "      'title': 'Sisa',";
            Script += "      'color': '#FB404B',";
            Script += "      'periode': 'Sisa',";
            Script += "       'sisa': " + (sisa) + "";
            Script += "   }];";

            Script += "</script>";
            Response.Write(Script);

        }
        void fillChartPieColl()
        {
            decimal piutang = Db.SingleDecimal("SELECT ISNULL (SUM(NilaiTagihan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_TAGIHAN A INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK B ON A.NoKontrak = B.NoKontrak  WHERE B.Status = 'A' AND B.Project = '" + project.SelectedValue + "' ");
            decimal pelunasan = Db.SingleDecimal("SELECT ISNULL (SUM(NilaiPelunasan),0) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN A INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK B ON A.NoKontrak = B.NoKontrak WHERE Status = 'A' AND B.Project = '" + project.SelectedValue + "' ");
            decimal sisa = piutang - pelunasan;

            string Script = "";

            Script += " <script type='text/javascript'>";
            Script += "var PieChartColl =[{";
            Script += "      'title': 'Piutang',";
            Script += "      'color': '#1DC7EA',";
            Script += "      'value': " + piutang + "";
            Script += "   }, {";
            Script += "      'title': 'Pelunasan',";
            Script += "      'color': '#FB404B',";
            Script += "       'value': " + (pelunasan) + "";
            Script += "   }, {";
            Script += "      'title': 'Sisa',";
            Script += "      'color': '#00e600',";
            Script += "       'value': " + (sisa) + "";
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
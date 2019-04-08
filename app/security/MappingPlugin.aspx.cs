using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
    public partial class MappingPlugin : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                BindModul(); //daftar modul
                Js.Focus(this, display);
            }

            FeedBack();
        }

        private void BindModul()
        {
            DataTable rsList = Db.Rs("SELECT DISTINCT Modul FROM PLUGIN ORDER BY Modul");
            for (int i = 0; i < rsList.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;
                modul.Items.Add(new ListItem(rsList.Rows[i][0].ToString()));
            }
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Map Ulang Berhasil...";
            }
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            string addq = "";
            if (modul.SelectedIndex != 0)
                addq = " AND Modul = '" + modul.SelectedValue + "'";

            DataTable rs = Db.Rs("SELECT * FROM PLUGIN WHERE 1=1 " + addq + " ORDER BY Modul, Nama");
            Rpt.NoData(rpt, rs, "Mapping program belum dijalankan.");

            string namamodul = "";
            System.Text.StringBuilder filebaru = new System.Text.StringBuilder();
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r;
                TableCell c;

                if (namamodul != rs.Rows[i]["Modul"].ToString())
                {
                    r = new TableRow();

                    c = new TableCell();
                    c.Text = "<br>" + rs.Rows[i]["Modul"].ToString();
                    c.Font.Bold = true;
                    c.Font.Size = 10;
                    c.ColumnSpan = 3;
                    r.Cells.Add(c);

                    Rpt.Border(r);
                    rpt.Rows.Add(r);

                    namamodul = rs.Rows[i]["Modul"].ToString();
                }

                r = new TableRow();

                string namaFile = rs.Rows[i]["Halaman"].ToString();
                int index = namaFile.LastIndexOf("\\");
                if (index != -1)
                {
                    namaFile = namaFile.Substring(index + 1, namaFile.Length - index - 1);
                }

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<a style='font:8pt' "
                    + "href=\"javascript:popPlugin('" + rs.Rows[i]["Halaman"].ToString().Replace("\\", "\\\\") + "')\">"
                    + namaFile + "</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = GetSecLevel(rs.Rows[i]["Halaman"].ToString());
                c.Font.Size = 8;
                r.Cells.Add(c);

                Rpt.Border(r);
                r.Cells[0].Attributes["style"] = r.Cells[0].Attributes["style"]
                    + ";padding-left:30px";
                rpt.Rows.Add(r);
            }
        }

        private string GetSecLevel(string Halaman)
        {
            System.Text.StringBuilder all = new System.Text.StringBuilder();
            DataTable rsAll = Db.Rs("SELECT Kode FROM SECLEVEL ORDER BY Kode");
            for (int i = 0; i < rsAll.Rows.Count; i++)
            {
                if (all.Length != 0) all.Append(", ");
                all.Append(rsAll.Rows[i][0]);
            }

            System.Text.StringBuilder x = new System.Text.StringBuilder();
            DataTable rsSl = Db.Rs("SELECT Kode FROM PLUGINSEC WHERE Halaman = '" + Halaman + "' ORDER BY Kode");
            for (int i = 0; i < rsSl.Rows.Count; i++)
            {
                if (x.Length != 0) x.Append(", ");
                x.Append(rsSl.Rows[i][0]);
            }

            if (x.ToString() == all.ToString())
                return "*"; //all security level
            else
                return x.ToString();
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
    }
}

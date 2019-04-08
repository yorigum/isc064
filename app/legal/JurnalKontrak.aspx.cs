using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class JurnalKontrak : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Fill();
            FeedBack();

            Js.Confirm(this, "Simpan aktivitas baru ke dalam jurnal?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Simpan Jurnal Berhasil...";
            }
        }

        private void Fill()
        {
            string strSql = "SELECT *"
                + ",(SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = MS_KONTRAK_JURNAL.UserID) AS Nama"
                + " FROM MS_KONTRAK_JURNAL"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                + " ORDER BY JurnalID";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Jurnal untuk kontrak tersebut masih kosong.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Time(rs.Rows[i]["Tgl"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["UserID"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Ket"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                if (System.IO.File.Exists(Request.PhysicalApplicationPath
                    + "JurnalKontrak\\" + rs.Rows[i]["JurnalID"] + ".jpg"))
                    c.Text = "<a href=\"javascript:popGambar('JurnalKontrak/" + rs.Rows[i]["JurnalID"] + ".jpg')\">View</a>";
                else
                    c.Text = "&nbsp;";
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (file.PostedFile.FileName.Length != 0
                && !file.PostedFile.FileName.EndsWith(".jpg"))
            {
                Js.Alert(
                    this
                    , "Proses Upload Gagal.\\n"
                    + "File yang boleh di-upload adalah file dengan extension .jpg saja."
                    , ""
                    );
            }
            else
            {
                string Ket = Cf.Str(akt.SelectedValue) + "<br>" + Cf.Str(baru.Text);

                Db.Execute("EXEC spJurnalKontrak "
                    + " '" + Act.UserID + "'"
                    + ",'" + NoKontrak + "'"
                    + ",'" + Ket + "'"
                    );

                if (akt.SelectedValue.ToString() == "COMPLAIN")
                {
                    long JurnalID = Db.SingleLong("SELECT TOP 1 JurnalID FROM MS_KONTRAK_JURNAL ORDER BY JurnalID DESC");
                    Db.Execute("UPDATE MS_KONTRAK_JURNAL SET Complain='1' WHERE JurnalID='" + JurnalID + "'");
                }
                if (file.PostedFile.FileName.Length != 0)
                {
                    long JurnalID = Db.SingleLong("SELECT TOP 1 JurnalID FROM MS_KONTRAK_JURNAL ORDER BY JurnalID DESC");
                    string path = Request.PhysicalApplicationPath
                        + "JurnalKontrak\\" + JurnalID + ".jpg";
                    Dfc.UploadFile(".jpg", path, file);
                }

                Response.Redirect("JurnalKontrak.aspx?done=1&NoKontrak=" + NoKontrak);
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
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

    }
}

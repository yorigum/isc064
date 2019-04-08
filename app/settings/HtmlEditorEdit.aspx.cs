using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{
    public partial class HtmlEditorEdit : System.Web.UI.Page
    {
        protected string Halaman { get { return Request.QueryString["id"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                fillParam();
                fill();
            }

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Berhasil...";
            }
        }

        protected void fillParam()
        {
            DataTable rs = Db.Rs("SELECT * FROM HTMLEDITORPARAMETER WHERE Halaman = '" + Halaman + "' AND Project = '" + Project + "'");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                HtmlTableRow tr = new HtmlTableRow();
                list.Controls.Add(tr);

                HtmlTableCell c;

                c = new HtmlTableCell();
                c.InnerHtml = "@@" + rs.Rows[i]["Parameter"];
                tr.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["Keterangan"].ToString();
                tr.Cells.Add(c);
            }
        }

        protected void fill()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=HTMLEDITOR_LOG&Pk=" + Halaman + "'";

            DataTable rs = Db.Rs("SELECT * FROM HtmlEditor WHERE Halaman = '" + Halaman + "' AND Project = '" + Project + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");

            halaman.Text = rs.Rows[0]["Nama"].ToString();
            project.Text = rs.Rows[0]["Project"].ToString();
            modul.Text = rs.Rows[0]["Modul"].ToString();
            html.Text = rs.Rows[0]["Html"].ToString();
        }

        protected void save_Click(object sender, EventArgs e)
        {
            //Save();
            if (Save())
                Js.Close(this);
            //Response.Redirect("HtmlEditorEdit.aspx?done=1&id=" + Halaman);
        }

        protected bool Save()
        {
            DataTable rs = Db.Rs("SELECT * FROM HtmlEditor WHERE Halaman = '" + Halaman + "' AND Project = '" + Project + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");

            DataTable rsBef = Capture;

            string isi = HttpUtility.HtmlDecode(html.Text);

            // Update data
            Db.Execute("UPDATE HtmlEditor SET Html = '" + isi + "'"
                + " WHERE Halaman = '" + Halaman + "'  AND Project = '" + Project + "'"
                );

            DataTable rsAft = Capture;

            //Log File
            string Ket = "Halaman : " + Halaman + "<br />"
                + Cf.LogCompare(rsBef, rsAft);

            Db.Execute("EXEC spLogHtmlEditor"
                + " 'EDIT'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + Halaman + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM HTMLEDITOR_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE HTMLEDITOR_LOG SET Project = '" + project.Text + "' WHERE LogID  = " + LogID);

            return true;
        }

        protected DataTable Capture
        {//untuk log file
            get
            {
                return
                    Db.Rs
                    ("SELECT "
                    + " Halaman"
                    + ",Modul"
                    + ",Html"
                    + " FROM HtmlEditor"
                    + " WHERE Halaman = '" + Halaman + "' AND Project = '" + Project + "'"
                    );
            }
        }
        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
            }
        }
    }
}
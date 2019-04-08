using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.COLLECTION
{
    public partial class SKLEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack)
            {
                Fill();
            }

            FeedBack();
        }
        protected void Fill()
        {
            btnlog.Attributes["onclick"] = "location.href='LogSKL.aspx?Tb=MS_SKL_LOG&Pk=" + NoSKL + "'";            
			
            string strsql = "SELECT * FROM MS_SKL WHERE NOSKL='"+ NoSKL +"'";
            DataTable rs = Db.Rs(strsql);

            if (rs.Rows.Count > 0)
            {
                tgl.Text = Cf.Day(rs.Rows[0]["TglSKL"]);
                nosys.Text = rs.Rows[0]["NoSKL"].ToString();
                nom.Text = rs.Rows[0]["NoSKLManual"].ToString();
                nod.SelectedValue = rs.Rows[0]["Used"].ToString();

                printSKL.InnerHtml = printSKL.InnerHtml + " (" + rs.Rows[0]["PrintSKL"] + ")";
            }
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[0]["Ref"] + "'");
            printSKL.HRef = "PrintSuratLunas.aspx?NoSKL=" + NoSKL + "&project=" + Project;
        }
        protected void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Berhasil...";
            }
        }
        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("SKLEdit.aspx?done=1&NoSKL=" + NoSKL);
        }
        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

           

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }
        private bool Save()
        {
            if (valid())
            {
                DataTable rs = Db.Rs("SELECT "
                          + " CONVERT(varchar, TglSKL, 106) AS [Tanggal]"
                          + ",NoSKL"
                          + ",Ref AS [Ref.]"
                          + ",NoSKLManual"
                          + ",Used AS [No.YgDigunakan]"
                          + " FROM MS_SKL WHERE NoSKL = '" + NoSKL + "'");
                DataTable rsBef = Db.Rs("SELECT "
                          + " CONVERT(varchar, TglSKL, 106) AS [Tanggal]"
                          + ",NoSKL"
                          + ",Ref AS [Ref.]"
                          + ",NoSKLManual"
                          + ",Used AS [No.YgDigunakan]"
                          + " FROM MS_SKL WHERE NoSKL = '" + NoSKL + "'");

                DateTime Tgl = Convert.ToDateTime(tgl.Text);
                Db.Execute("EXEC spSKLEdit"
                    + " '" + NoSKL + "'"
                    + ",'" + Tgl + "'"
                    + ",'" + nom.Text + "'"
                    + "," + nod.SelectedValue
                    );

                DataTable rsAft = Db.Rs("SELECT "
                          + " CONVERT(varchar, TglSKL, 106) AS [Tanggal]"
                          + ",NoSKL"
                          + ",Ref AS [Ref.]"
                          + ",NoSKLManual"
                          + ",Used AS [No.YgDigunakan]"
                          + " FROM MS_SKL WHERE NoSKL = '" + NoSKL + "'");

                string ketlog = Cf.LogCapture(rs)
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogSKL "
                        + " '" + DateTime.Now + "'"
                        + ",'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + NoSKL + "'"
                        + ",'" + ketlog + "'"
                        );

                return true;
            }
            else
            {
                return false;
            }
        }
        private string NoSKL
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoSKL"]);
            }
        }
    }
}

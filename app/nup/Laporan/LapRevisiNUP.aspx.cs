using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.NUP.Laporan
{
    public partial class LapRevisiNUP : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.PlaceHolder list;

        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();

            if(!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this,scr);
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;

                tglawal.Text = Cf.Day(DateTime.Now);
                tglakhir.Text = Cf.Day(DateTime.Now);
            }
        }

        private bool Valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tglawal) || !Cf.isTgl(tglakhir))
            {
                x = false;
                tglc.Text = "Format salah";
            }
            else
                tglc.Text = "";

            if (Cf.isEmpty(tglawal) || Cf.isEmpty(tglakhir))
            {
                x = false;
                tglc.Text = "Kosong";
            }
            else
                tglc.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }
        protected void scr_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                Report();
            }
        }
        protected void xls_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                Report();
                Rpt.ToExcel(this, rpt);
            }
        }
        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            lblPT.Text = Mi.Pt;
            lblHeader.Text = "Laporan Ganti Nama NUP";

            System.Text.StringBuilder x = new System.Text.StringBuilder();

            x.Append("<br /><span style='font-weight: normal;'>Laporan dihasilkan pada hari : " + Cf.IndoWeek(DateTime.Today)
                + ", " + Cf.Date(DateTime.Now)
                + " dari workstation : " + Act.IP
                + " dan username : " + Act.UserID
                + "</span>"
                );

            lblSubHeader.Text = x.ToString();
            Fill();
        }

        private void Fill()
        {
            DateTime Tanggal1 = Convert.ToDateTime(tglawal.Text);
            DateTime Tanggal2 = Convert.ToDateTime(tglakhir.Text);

            string strSql = "SELECT * FROM MS_NUP WHERE 1=1"
                    + " AND CONVERT(DATETIME,TglDaftar,112) BETWEEN '" + Cf.Tgl112(Tanggal1) + "' AND '" + Cf.Tgl112(Tanggal2) + "'"
                    + " AND Revisi != 0"
                    + " ORDER BY NoNUP ASC";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ada data NUP yang terdaftar.");

            int index = 1;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = (index + i).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglDaftar"].ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoNUP"].ToString().PadLeft(4,'0');
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglEdit"]); ;
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCUSTOMER = " + rs.Rows[i]["NoCUstomerBfr"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCUSTOMER = " + rs.Rows[i]["NoCUstomer"]);
                c.Wrap = false;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }
        }
    }
}

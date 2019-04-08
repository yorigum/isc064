using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.NUP.Laporan
{
    public partial class NUPBelumBayar : System.Web.UI.Page
    {
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Admin { get { return (Request.QueryString["admin"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Report();
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + Act.UserID + "'");

            return a;
        }

        private void Report()
        {
            rpt.Visible = true;
            Header();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            //Rpt.Judul(x, comp, judul);
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID=" + AttachmentID + "");

            Rpt.SubJudul(x
                , "As Of" + " : " + Cf.Day(Sampai)
                );

            Rpt.SubJudul(x
                , "Project : " + Project
                );

            Rpt.SubJudul(x
                , "Admin : " + Admin
                );

            //Rpt.Header(rpt, x);
            string legend = "";
            Rpt.HeaderReport(headReport, legend, x);
        }


        private void Fill()
        {
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            //string nProject = "";
            //if (Project != "SEMUA") nProject = " AND A.Project = '" + Project + "'";

            //string addSql = "";

            //if (admin.SelectedIndex > 0)
            //    addSql = " AND UserInputID = '" + admin.SelectedValue + "'";

            //string Project = " AND " + Mi.DbPrefix + "MARKETINGJUAL..MS_NUP.Project IN (" + Act.ProjectListSql + ")";
            //if (project.SelectedValue != "0") Project = " AND " + Mi.DbPrefix + "MARKETINGJUAL..MS_NUP.Project = '" + project.SelectedValue + "'";

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND MS_NUP.Project = '" + Project + "'";
            string nAdmin = "";
            if (Admin != "Semua Admin") nAdmin = " AND MS_NUP.UserInputID = '" + Admin + "'";

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_NUP WHERE 1=1"
                    + " AND NoNUP NOT IN (SELECT DISTINCT(NoNUP) FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_NUP_PELUNASAN WHERE Tipe = MS_NUP.Tipe AND PROJECT = MS_NUP.PROJECT)"
                    + " AND CONVERT(DATETIME,TglDaftar,112) <= '" + Cf.Tgl112(Sampai) + "' "
                    + nProject
                    + nAdmin;

            DataTable dtNUP = Db.Rs(strSql);
            Rpt.NoData(rpt, dtNUP, "Tidak ada data NUP yang terdaftar.");

            int no = 0;
            for (int i = 0; i < dtNUP.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = (no + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Wrap = false;
                r.Cells.Add(c);

                //Tgl NUP
                c = new TableCell();
                c.Text = Cf.Day(dtNUP.Rows[i]["TglDaftar"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //NoNUP
                c = new TableCell();
                string cetakn = dtNUP.Rows[i]["NoNUP"].ToString();

                if (Convert.ToInt32(dtNUP.Rows[i]["Revisi"].ToString()) > 0)
                    cetakn = cetakn + "R";
                c.Text = Cf.Str(cetakn);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string namaCS = "";
                string noKTP = "";
                string alamatCS = "";
                string telpCS = "";
                string bank = "", nrek = "";
                DataTable dtCs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer=" + Convert.ToInt32(dtNUP.Rows[i]["NoCustomer"]));

                if (dtCs.Rows.Count > 0)
                {
                    namaCS = dtCs.Rows[0]["Nama"].ToString();
                    noKTP = dtCs.Rows[0]["NoKTP"].ToString();
                    telpCS = dtCs.Rows[0]["NoTelp"].ToString();
                    bank = dtCs.Rows[0]["RekBank"].ToString();
                    nrek = dtCs.Rows[0]["RekNo"].ToString();
                    alamatCS = dtCs.Rows[0]["KTP1"].ToString() + " " + dtCs.Rows[0]["KTP1"].ToString() + " " + dtCs.Rows[0]["KTP2"].ToString() + " " + dtCs.Rows[0]["KTP3"].ToString() + " " + dtCs.Rows[0]["KTP4"].ToString();
                }

                //Unit Customer
                string unit = "";
                DataTable rskontrak = Db.Rs("SELECT a.NoUnit FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_NUP b ON a.NUP=b.NoNUP WHERE b.NoNUP='" + dtNUP.Rows[i]["NoNUP"].ToString() + "'");
                if (rskontrak.Rows.Count > 0)
                {
                    unit = rskontrak.Rows[0]["NoUnit"].ToString();
                }
                c = new TableCell();
                c.Text = Cf.Str(unit);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Nama Customer
                c = new TableCell();
                c.Text = Cf.Str(namaCS);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Alamat Customer
                c = new TableCell();
                c.Text = Cf.Str(alamatCS);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Telpon Customer
                c = new TableCell();
                c.Text = Cf.Str(telpCS);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                string namaAG = "";
                string telpAG = "";
                DataTable dtAG = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_AGENT WHERE NoAgent=" + Convert.ToInt32(dtNUP.Rows[i]["NoAgent"]));

                if (dtAG.Rows.Count > 0)
                {
                    namaAG = dtAG.Rows[0]["Nama"].ToString();
                    telpAG = dtAG.Rows[0]["Kontak"].ToString();
                }

                //Agent
                c = new TableCell();
                c.Text = Cf.Str(namaAG);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //telp Agent
                c = new TableCell();
                c.Text = Cf.Str(telpAG);
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //Tipe
                c = new TableCell();
                c.Text = Cf.Str(dtNUP.Rows[i]["Tipe"].ToString());
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                //telp Agent
                c = new TableCell();
                c.Text = bank + " " + nrek;
                c.HorizontalAlign = HorizontalAlign.Left;
                c.Wrap = false;
                r.Cells.Add(c);

                rpt.Rows.Add(r);

                no++;
            }
        }
    }
}
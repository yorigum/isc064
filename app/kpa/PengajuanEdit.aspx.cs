using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;

namespace ISC064.KPA
{
    public partial class PengajuanEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("id");

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                Fill();
            }
            FillDetil();
            FeedBack();

            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=ISC064_FINANCEAR..MS_PENGAJUAN_KPA_LOG&Pk=" + NoPengajuan + "'";
        }
        private string NoPengajuan
        {
            get
            {
                return Cf.Pk(Request.QueryString["id"]);
            }
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
        protected void Fill()
        {
            string strSql = "SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan=" + NoPengajuan;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                printPengajuan.HRef = "PrintPengajuan.aspx?NoPengajuan=" + NoPengajuan;
                printPengajuan.InnerHtml = printPengajuan.InnerHtml + " (" + rs.Rows[0]["PrintPengajuan"] + ")";
                tglform.Text = Cf.Day(rs.Rows[0]["TglFormulir"]);
                tglcair.Text = Cf.Day(rs.Rows[0]["TglRencanaCair"]);
                ket.Text = rs.Rows[0]["Keterangan"].ToString();
                nilai.Text = Cf.Num(rs.Rows[0]["Total"]);
                status.Text = rs.Rows[0]["Status"].ToString();
                nosurat.Text = rs.Rows[0]["NoSurat"].ToString();
                if (rs.Rows[0]["Status"].ToString() == "POST")
                {
                    batal.Enabled = false;
                }
                else if (rs.Rows[0]["Status"].ToString() == "BATAL")
                {
                    batal.Enabled = false;
                    status.ForeColor = Color.Red;
                }


            }
        }
        protected void FillDetil()
        {
            DataTable rs2 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL WHERE NoPengajuan=" + NoPengajuan);

            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                TableCell c;
                TableRow r = new TableRow();

                c = new TableCell();
                c.Text = rs2.Rows[i]["NoKontrak"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs2.Rows[i]["Nama"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs2.Rows[i]["NoUnit"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.CssClass = "txt_num";
                c.Text = Cf.Num(rs2.Rows[i]["Nilai"]);
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.CssClass = "txt_num";
                decimal NilaiRetensi = Db.SingleDecimal("SELECT Total FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA WHERE NoPengajuan = '" + rs2.Rows[i]["NoPengajuan"] + "'");
                c.Text = Cf.Num(NilaiRetensi);
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.CssClass = "txt_num";
                string NoReal = Db.SingleString("SELECT FORMAT(NoReal,'000000#') FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA WHERE NoPengajuan = '" + rs2.Rows[i]["NoPengajuan"] + "'");
                //int SifatPPN = (Kategori.ToUpper() == "") ? 1 : 0;
                c.Text = NoReal;
                c.Wrap = false;
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);


            }
        }
        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("PengajuanEdit.aspx?done=1&id=" + NoPengajuan);
        }
        private bool valid()
        {
            bool x = true;

            if (tglform.Text == "")
            {
                x = false;
                tglformc.Text = "Kosong";
            }
            else if (!Cf.isTgl(tglform))
            {
                x = false;
                tglformc.Text = "Format Tanggal";
            }
            else
            {
                tglformc.Text = "";
            }

            if (tglcair.Text == "")
            {
                x = false;
                tglcairc.Text = "Kosong";
            }
            else if (!Cf.isTgl(tglcair))
            {
                x = false;
                tglcairc.Text = "Format Tanggal";
            }
            else
            {
                tglcairc.Text = "";
            }

            return x;
        }
        private bool Save()
        {
            if (valid())
            {
                DataTable rs = Db.Rs("SELECT "
                  + " CONVERT(varchar, TglInput, 106) AS [Tanggal]"
                  + ",NoPengajuan"
                  + ",CONVERT(varchar, TglFormulir, 106) AS [Tanggal Formulir]"
                  + ",CONVERT(varchar, TglRencanaCair, 106) AS [Tanggal Rencana Cair]"
                  + ",Total"
                  + ",Keterangan"
                  + ",Project"
                  + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = " + NoPengajuan);

                DataTable rsBef = Db.Rs("SELECT "
                  + " CONVERT(varchar, TglInput, 106) AS [Tanggal]"
                  + ",NoPengajuan"
                  + ",NoSurat"
                  + ",CONVERT(varchar, TglFormulir, 106) AS [Tanggal Formulir]"
                  + ",CONVERT(varchar, TglRencanaCair, 106) AS [Tanggal Rencana Cair]"
                  + ",Total"
                  + ",Keterangan"
                  + ",Project"
                  + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = " + NoPengajuan);

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spPengajuanEdit"
                      + " " + NoPengajuan
                      + ",'" + Convert.ToDateTime(tglform.Text) + "'"
                      + ",'" + Convert.ToDateTime(tglcair.Text) + "'"
                      + ",'" + ket.Text + "'"
                  );

                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA SET NoSurat='" + nosurat.Text + "' WHERE NoPengajuan = " + NoPengajuan);

                DataTable rsAft = Db.Rs("SELECT "
                  + " CONVERT(varchar, TglInput, 106) AS [Tanggal]"
                  + ",NoPengajuan"
                  + ",NoSurat"
                  + ",CONVERT(varchar, TglFormulir, 106) AS [Tanggal Formulir]"
                  + ",CONVERT(varchar, TglRencanaCair, 106) AS [Tanggal Rencana Cair]"
                  + ",Total"
                  + ",Keterangan"
                  + ",Project"
                  + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = " + NoPengajuan);


                string KetLog = Cf.LogCapture(rs)
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogPengajuanKPA"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + NoPengajuan + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = '" + NoPengajuan + "'");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                return true;
            }
            else
            {
                return false;
            }

        }
        protected void batal_Click(object sender, EventArgs e)
        {
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA SET Status='BATAL' WHERE NoPengajuan = " + NoPengajuan);

            DataTable rs2 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL WHERE NoPengajuan=" + NoPengajuan);

            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                Db.Execute("UPDATE MS_TAGIHAN_KPA SET FlagPengajuanKPA=0 WHERE NoKontrak='" + rs2.Rows[i]["NoKontrak"].ToString() + "' AND NoUrut='" + rs2.Rows[i]["NoTagihan"].ToString() + "'");
            }

            DataTable rs = Db.Rs("SELECT "
                  + " CONVERT(varchar, TglInput, 106) AS [Tanggal]"
                  + ",NoPengajuan"
                  + ",CONVERT(varchar, TglFormulir, 106) AS [Tanggal Formulir]"
                  + ",CONVERT(varchar, TglRencanaCair, 106) AS [Tanggal Rencana Cair]"
                  + ",Total"
                  + ",Keterangan"
                  + ",Project"
                  + " FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = " + NoPengajuan);

            string KetLog = Cf.LogCapture(rs);

            Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogPengajuanKPA"
                + " 'BATAL'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + KetLog + "'"
                + ",'" + NoPengajuan + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = '" + NoPengajuan + "'");
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Response.Redirect("PengajuanEdit.aspx?id=" + NoPengajuan);
        }
    }
}
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
    public partial class RealisasiEdit : System.Web.UI.Page
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

            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=ISC064_FINANCEAR..MS_REAL_KPA_LOG&Pk=" + NoReal + "'";
        }
        private string NoReal
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
            string strSql = "SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA a INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN_KPA b ON a.NoReal = b.NoRealKPA"
                        + " INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_TTS c ON b.NoTTS = c.NoTTS WHERE NoReal=" + NoReal;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {

                printrealisasi.InnerHtml = printrealisasi.InnerHtml + " (" + rs.Rows[0]["PrintReal"] + ")";
                printrealisasi.HRef = "PrintRealisasi.aspx?NoReal=" + NoReal;
                tglform.Text = Cf.Day(rs.Rows[0]["TglReal"]);
                ket.Text = rs.Rows[0]["Ket"].ToString();
                nilai.Text = Cf.Num(rs.Rows[0]["Total"]);
                status.Text = rs.Rows[0]["Status"].ToString();
                if (rs.Rows[0]["Status"].ToString() == "")
                {
                    btnvoid.Enabled = true;
                }
                else if (rs.Rows[0]["Status"].ToString() == "VOID")
                {
                    btnvoid.Enabled = false;
                    status.ForeColor = Color.Red;
                }
            }
        }
        protected void FillDetil()
        {
            DataTable rs2 = Db.Rs("SELECT * FROM MS_PELUNASAN_KPA WHERE NoRealKPA=" + NoReal);

            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                TableCell c;
                TableRow r = new TableRow();

                DataTable tts = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS=" + rs2.Rows[i]["NoTTS"]);

                c = new TableCell();
                c.Text = tts.Rows[0]["NoTTS2"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<b>" + tts.Rows[0]["Status"].ToString() + "</b>";
                if (tts.Rows[0]["Status"].ToString() == "VOID")
                {
                    c.ForeColor = Color.Red;
                }
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs2.Rows[i]["NoKontrak"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = tts.Rows[0]["Customer"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = tts.Rows[0]["Unit"].ToString();
                c.Wrap = false;
                r.Cells.Add(c);

                c = new TableCell();
                c.CssClass = "txt_num";
                c.Text = Cf.Num(rs2.Rows[i]["NilaiPelunasan"]);
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
            if (Save()) Response.Redirect("RealisasiEdit.aspx?done=1&id=" + NoReal);
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


            return x;
        }
        private bool Save()
        {
            if (valid())
            {
                DataTable rs = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglInput, 106) AS [Tanggal]"
                    + ",NoPengajuan"
                    + ",CONVERT(varchar, TglReal, 106) AS [Tanggal Realisasi]"
                    + ",Total"
                    + ",Ket"
                    + " FROM ISC064_FINANCEAR..MS_REAL_KPA WHERE NoReal = " + NoReal);

                DataTable rsBef = Db.Rs("SELECT "
                    + " CONVERT(varchar, TglInput, 106) AS [Tanggal]"
                    + ",NoPengajuan"
                    + ",CONVERT(varchar, TglReal, 106) AS [Tanggal Realisasi]"
                    + ",Total"
                    + ",Ket"
                    + " FROM ISC064_FINANCEAR..MS_REAL_KPA WHERE NoReal = " + NoReal);


                Db.Execute("EXEC ISC064_FINANCEAR..spRealisasiKPAEdit"
                      + " " + NoReal
                      + ",'" + Convert.ToDateTime(tglform.Text) + "'"
                      + ",'" + ket.Text + "'"
                  );



                DataTable rsAft = Db.Rs("SELECT "
                + " CONVERT(varchar, TglInput, 106) AS [Tanggal]"
                + ",NoPengajuan"
                + ",CONVERT(varchar, TglReal, 106) AS [Tanggal Realisasi]"
                + ",Total"
                + ",Ket"
                + " FROM ISC064_FINANCEAR..MS_REAL_KPA WHERE NoReal = " + NoReal);



                string KetLog = Cf.LogCapture(rs)
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC ISC064_FINANCEAR..spLogRealKPA"
                            + " 'EDIT'"
                            + ",'" + Act.UserID + "'"
                            + ",'" + Act.IP + "'"
                            + ",'" + KetLog + "'"
                            + ",'" + NoReal + "'"
                            );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = (SELECT NoPengajuan FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA WHERE NoReal = '" + NoReal + "')");
                Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);




                return true;
            }
            else
            {
                return false;
            }

        }

        protected void btnvoid_Click(object sender, EventArgs e)
        {
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA SET Status='VOID' WHERE NoReal = " + NoReal);
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA SET Total = 0 WHERE NoReal = " + NoReal);
            Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_PELUNASAN_KPA SET STATUS='VOID', NilaiPelunasan = 0 WHERE NoRealKPA =" + NoReal);
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA SET STATUS='VOID' WHERE NoReal =" + NoReal);
            string StatusBaru = Db.SingleString(
                "SELECT Status FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA WHERE NoReal = " + NoReal);
            //DataTable rs2 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL WHERE NoPengajuan=" + NoPengajuan);

            //for (int i = 0; i < rs2.Rows.Count; i++)
            //{
            //    Db.Execute("UPDATE MS_TAGIHAN_KPA SET FlagPengajuanKPA=0 WHERE NoKontrak='" + rs2.Rows[i]["NoKontrak"].ToString() + "' AND NoUrut='" + rs2.Rows[i]["NoTagihan"].ToString() + "'");
            //}

            DataTable rs = Db.Rs("SELECT "
                  + " CONVERT(varchar, TglReal, 106) AS [Tanggal]"
                  + ",NoReal"
                  + ",NoPengajuan"
                  + ",CONVERT(varchar, TglInput, 106) AS [Tanggal Input]"
                  + ",Total"
                  + ",Status"
                  + " FROM ISC064_FINANCEAR..MS_REAL_KPA WHERE NoReal = " + NoReal);

            string KetLog = Cf.LogCapture(rs);

            Db.Execute("EXEC ISC064_FINANCEAR..spLogRealKPA"
                        + " 'VOID'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + NoReal + "'"
                        );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA_LOG ORDER BY LogID DESC");
            string Project = Db.SingleString("SELECT Project FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan = (SELECT NoPengajuan FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA WHERE NoReal = '" + NoReal + "')");
            Db.Execute("UPDATE " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

            Response.Redirect("RealisasiEdit.aspx?id=" + NoReal);
        }
    }
}

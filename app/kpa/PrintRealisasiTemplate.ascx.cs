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
    public partial class PrintRealisasiTemplate : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Fill();
        }
        public string nomor;
        public string NoRealisasi
        {
            set { nomor = value; }
        }
        protected void Fill()
        {
            string strSql = "SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA WHERE NoReal=" + nomor;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                tgl.Text = Cf.Day(rs.Rows[0]["TglReal"]);
                ket.Text = rs.Rows[0]["Ket"].ToString();
                nilai.Text = Cf.Num(rs.Rows[0]["Total"]);
                no.Text = nomor;
                FillDetil();
            }
        }
        protected void FillDetil()
        {
            DataTable rs2 = Db.Rs("SELECT * FROM MS_PELUNASAN_KPA WHERE NoRealKPA=" + nomor);

            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                TableCell c;
                TableRow r = new TableRow();

                DataTable tts = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_TTS WHERE NoTTS=" + rs2.Rows[i]["NoTTS"]);

                c = new TableCell();
                c.Text = tts.Rows[0]["NoTTS2"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "<b>" + tts.Rows[0]["Status"].ToString() + "</b>";
                if (tts.Rows[0]["Status"].ToString() == "VOID")
                {
                    c.ForeColor = Color.Red;
                }
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs2.Rows[i]["NoKontrak"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = tts.Rows[0]["Customer"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = tts.Rows[0]["Unit"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.CssClass = "txt_num";
                c.Text = Cf.Num(rs2.Rows[i]["NilaiPelunasan"]);
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);


            }
        }
    }
}

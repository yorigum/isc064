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

namespace ISC064.KPA
{
    public partial class PrintPengajuanTemplate : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Fill();
        }
        public string nomor;
        public string NoPengajuan
        {
            set { nomor = value; }
        }
        private void Fill()
        {
            string strSql = "SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA WHERE NoPengajuan=" + nomor;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                tgl.Text = Cf.Day(rs.Rows[0]["TglFormulir"]);
                tglcair.Text = Cf.Day(rs.Rows[0]["TglRencanaCair"]);
                ket.Text = rs.Rows[0]["Keterangan"].ToString();
                nilai.Text = Cf.Num(rs.Rows[0]["Total"]);
                status.Text = rs.Rows[0]["Status"].ToString();
                no.Text = nomor;
                nosurat.Text = rs.Rows[0]["NoSurat"].ToString();
                FillDetil();
            }
        }
        private void FillDetil()
        {
            DataTable rs2 = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL WHERE NoPengajuan=" + nomor);

            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                TableCell c;
                TableRow r = new TableRow();

                c = new TableCell();
                c.Text = rs2.Rows[i]["NoKontrak"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs2.Rows[i]["Nama"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs2.Rows[i]["NoUnit"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.CssClass = "txt_num";
                c.Text = Cf.Num(rs2.Rows[i]["Nilai"]);
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);

            }
        }
    }
}

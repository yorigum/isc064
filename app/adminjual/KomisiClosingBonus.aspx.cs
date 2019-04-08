using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ISC064.ADMINJUAL
{
    public partial class KomisiClosingBonus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Bind();
            }

        }
        protected void Bind()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_KOMISI_CF");

            if (rs != null)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    TableCell c;
                    TableRow r = new TableRow();

                    c = new TableCell();
                    c.Text = "<b>" + rs.Rows[i]["Keterangan"].ToString() + "</b>";
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiBawah"].ToString()));
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiAtas"].ToString()));
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiGM"].ToString()));
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiSM"].ToString()));
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiM"].ToString()));
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = "<a show-modal='#ModalPopUp' modal-title='Komisi Edit' modal-url='KomisiCFEdit.aspx?Lvl=" + rs.Rows[i]["Lvl"].ToString() + "'>Edit</a>";
                    r.Cells.Add(c);

                    rpt.Rows.Add(r);
                }
            }
        }
    }
}
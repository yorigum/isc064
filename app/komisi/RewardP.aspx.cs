using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ISC064.KOMISI
{
    public partial class RewardP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                init();
            }

            Js.Focus(this, search);
        }

        private void init()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);
            Act.ProjectList(project);
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(dari))
            {
                daric.Text = "Tanggal";
                if (s == "") s = dari.ID;
                x = false;
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                sampaic.Text = "Tanggal";
                if (s == "") s = sampai.ID;
                x = false;
            }
            else
                sampaic.Text = "";

            if (!x)
                RegisterStartupScript("err"
                    , "<script language='javascript'>document.getElementById('" + s + "').select()</script>");

            return x;
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Fill();
            }
        }

        private void Fill()
        {
            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }
            string strSql = "SELECT * FROM MS_KOMISI_REWARD_P"
                          + " WHERE CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                          + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                          + " AND Project IN (" + Act.ProjectListSql + ")"
                          + " ORDER BY NoRP";
            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ada pengajuan reward dengan kriteria seperti tersebut diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;


                c = new TableCell();
                c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoRP"] + "')\">"
                    + rs.Rows[i]["NoRP"].ToString() + "</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Ket"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Project"].ToString();
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ISC064.KOMISI
{
    public partial class CFP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                init();
                Act.ProjectList(project);
            }

            Js.Focus(this, search);
        }

        private void init()
        {
            dari.Text = Cf.Day(Cf.AwalBulan(DateTime.Now.Month, DateTime.Now.Year));
            sampai.Text = Cf.Day(Cf.AkhirBulan(DateTime.Now.Month, DateTime.Now.Year));

            tipesales.Items.Add(new ListItem { Text = "Tipe Marketing :", Value = "0" });
            LibMkt.ListTipeSales(tipesales, project.SelectedValue);
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (project.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = project.ID;
                alertc.Text = " &nbsp; Project Belum Dipilih";
            }
            else
            {
                alertc.Text = "";
            }

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

            string w = "";
            if (tipesales.SelectedIndex != 0)
            {
                w = " AND SalesTipe = '" + tipesales.SelectedValue + "'";
            }

            string strSql = "SELECT * FROM MS_KOMISI_CFP"
                        + " WHERE CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                        + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                        + " AND Project = '" + project.SelectedValue + "'"
                        + w
                        + " ORDER BY NoCFP";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ada pengajuan closing fee dengan kriteria seperti tersebut diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;
                
                c = new TableCell();
                c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoCFP"] + "','" + rs.Rows[i]["Project"] + "')\">"
                    + rs.Rows[i]["NoCFP"].ToString() + "</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("select Tipe from REF_AGENT_TIPE where ID = '" + rs.Rows[i]["SalesTipe"] + "'");
                r.Cells.Add(c);

                string Sales = Db.SingleString("SELECT TOP 1 STUFF((SELECT distinct ', ' + NamaAgent FROM MS_KOMISI_CFP_DETAIL AS T1"
                    + " where NoCFP = '" + rs.Rows[i]["NoCFP"].ToString() + "'"
                    + " FOR XML PATH('')), 1, 1, '') As Nama "
                    + " FROM MS_KOMISI_CFP_DETAIL AS T2 where NoCFP = '" + rs.Rows[i]["NoCFP"].ToString() + "'"
                );

                c = new TableCell();
                c.Text = Sales;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }

        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipesales.Items.Clear();
            init();
        }

    }
}

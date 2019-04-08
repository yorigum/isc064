using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ISC064.KOMISI
{
    public partial class CF : System.Web.UI.Page
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
            string v = "";
            if(tipesales.SelectedIndex != 0)
            {
                w = " AND SalesTipe = '" + tipesales.SelectedValue + "'";
            }

            if(sales.SelectedIndex != 0)
            {
                v = " AND NoAgent = '" + sales.SelectedValue + "'";
            }

            string strSql = "SELECT * FROM MS_KOMISI_CF"
                          + " WHERE CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                          + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                          + w
                          + v
                          + " AND Project = '" + project.SelectedValue + "'"
                          + " ORDER BY NoCF";
            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Tidak ada closing fee dengan kriteria seperti tersebut diatas.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;


                c = new TableCell();
                c.Text = "<a href=\"javascript:call('" + rs.Rows[i]["NoCF"] + "','" + rs.Rows[i]["Project"] + "')\">"
                    + rs.Rows[i]["NoCF"].ToString() + "</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["Tgl"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaAgent"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaCust"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaSkema"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                string Nama = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project ='" + rs.Rows[i]["Project"].ToString() + "'");
                c.Text = Nama;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
            }

        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipesales.Items.Clear();
            init();
        }

        protected void tipesales_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSales();
        }

        void BindTipeSales()
        {
            tipesales.Items.Clear();
            DataTable rs = Db.Rs("SELECT * FROM REF_AGENT_TIPE WHERE Project = '" + project.SelectedValue + "'");
            tipesales.Items.Add(new ListItem { Text = "Tipe Marketing :", Value = "0" });

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["ID"].ToString();
                string t = rs.Rows[i]["Tipe"].ToString();
                tipesales.Items.Add(new ListItem(t, v));
            }
        }

        void BindSales()
        {
            sales.Items.Clear();

            string strSql = "SELECT * FROM MS_AGENT WHERE SalesTipe ='" + tipesales.SelectedValue + "' AND Project = '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs(strSql);
            sales.Items.Add(new ListItem { Text = "Nama :", Value = "0" });

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                sales.Items.Add(new ListItem(t, v));
            }
        }


    }
}

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace ISC064.ADMINJUAL
{
    public partial class AgentJadwalKomisi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("NoAgent");

            if (!Page.IsPostBack)
            {
                init();
                FillTb(NoAgent);
            }
        }

        protected void ddlPeriode_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (Page.IsPostBack)
            {              
                FillTb(NoAgent);
            }
        }

        private void init()
        {
            DataTable rs = Db.Rs("SELECT DISTINCT PeriodeKomisi FROM MS_KOMISI WHERE NoAgent='"+NoAgent+"'");
            for (int i = 0; i < rs.Rows.Count; i++)
                ddlPeriode.Items.Add(new ListItem(rs.Rows[i][0].ToString()));
            if (rs.Rows.Count == 0)
            {
                ddlPeriode.SelectedIndex = 0;
            }
            else
            {
                ddlPeriode.SelectedIndex =1;
            }
            
        }

        private void FillTb(string NoAgent)
        {
            
            string periodeKomisi = "";
            if (ddlPeriode.SelectedIndex != 0)
            {
                periodeKomisi = " AND PeriodeKomisi = '" + ddlPeriode.SelectedValue + "'";
            }
            string strSql = "SELECT *"
                + " FROM MS_KOMISI"
                + " INNER JOIN MS_KONTRAK ON MS_KOMISI.NoKontrak = MS_KONTRAK.NoKontrak"
                + " WHERE MS_KOMISI.NoAgent = '" + NoAgent + "'"
                + periodeKomisi
                ;


            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Daftar komisi untuk agen tersebut masih kosong.");

            decimal t = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableRow s = new TableRow();
                TableRow u = new TableRow();
                TableCell c;
                TableCell d;
                TableCell e;
                Button btn;
                Button btn2;
                Button btn3;

                decimal nilaiKomisi = Convert.ToDecimal(rs.Rows[i]["NilaiKomisi"]);
                decimal komisi40 = (decimal)0.4 * nilaiKomisi;
                decimal komisi60 = (decimal)0.6 * nilaiKomisi;
                t = t + nilaiKomisi;

                c = new TableCell();
                //c.Text = "<a href=\"javascript:call('" + Cf.Str(rs.Rows[i]["NoAgent"]) + "')\">"+ rs.Rows[i]["NoKontrak"].ToString() + "</a><br>";
                c.Text = Cf.Str(rs.Rows[i]["NoKontrak"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["PeriodeKomisi"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "40 %";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(komisi40.ToString());
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                decimal persen40 = 0;
                if (Convert.ToDecimal(rs.Rows[i]["Komisi40"]) != 0)
                {
                    persen40 = (Convert.ToDecimal(rs.Rows[i]["NilaiBayarKomisi40"]) / Convert.ToDecimal(rs.Rows[i]["Komisi40"])) * 100;
                }
                c = new TableCell();
                if (persen40 == 0)
                {
                    c.Text = "";
                }
                else
                {
                    c.Text = Math.Round(persen40, 2).ToString()+ " %";
                }
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                btn = new Button();
                btn.Text = "Cair";

                string strSql2 = " SELECT * from MS_PELUNASAN"
                    + " INNER JOIN MS_KOMISI ON MS_PELUNASAN.NoKontrak = MS_KOMISI.NoKontrak"
                    + " WHERE MS_PELUNASAN.NoTagihan=1"
                    + " AND MS_PELUNASAN.SudahCair = 1";

                DataTable rs2 = Db.Rs(strSql2);


                btn.ID = "cair40_" + i;
                btn.Attributes["onclick"] = "popKomisiBayar('" + rs.Rows[i]["NoKontrak"].ToString() + "','" + NoAgent + "', '" + btn.ID + "');";
                btn.Style.Add("Width", "80px");
                btn.Style.Add("Border", "1px solid black");
                c = new TableCell();
                c.Text = "a";
                r.Cells.Add(c);
                c.Controls.Add(btn);

                bool sudahcair = Convert.ToBoolean(rs2.Rows[i]["SudahCair"]);
                bool sudahbayar = Convert.ToBoolean(rs.Rows[i]["SudahBayar"]);
                bool FlagKomisi40 = Convert.ToBoolean(rs.Rows[i]["FlagKomisi40"]);
                bool FlagKomisi60 = Convert.ToBoolean(rs.Rows[i]["FlagKomisi60"]);
                bool FlagCF = Convert.ToBoolean(rs.Rows[i]["FlagCF"]);
                if (sudahcair == true)
                {
                    btn.Visible = true;
                }

                if (FlagKomisi40 == true)
                {
                    btn.Enabled = false;
                }


                Rpt.Border(r);
                rpt.Rows.Add(r);

                //second Row
                d = new TableCell();
                d.Text ="";
                s.Cells.Add(d);

                d = new TableCell();
                d.Text = "";
                s.Cells.Add(d);

                d = new TableCell();
                d.Text = "";
                s.Cells.Add(d);

                d = new TableCell();
                d.Text = "60 %";
                s.Cells.Add(d);

                d = new TableCell();
                d.Text = Cf.Num(komisi60.ToString());
                d.HorizontalAlign = HorizontalAlign.Right;
                s.Cells.Add(d);

                decimal persen60 = 0;
                if (Convert.ToDecimal(rs.Rows[i]["Komisi60"]) != 0)
                {
                    persen60 = (Convert.ToDecimal(rs.Rows[i]["NilaiBayarKomisi60"]) / Convert.ToDecimal(rs.Rows[i]["Komisi60"])) * 100;
                }
                d = new TableCell();
                if (persen60 == 0)
                {
                    d.Text = "";
                }
                else
                {
                    d.Text = Math.Round(persen60, 2).ToString() + " %";
                }
                d.HorizontalAlign = HorizontalAlign.Right;
                s.Cells.Add(d);

                btn2 = new Button();
                btn2.Text = "Cair";
                btn2.Style.Add("Width", "80px");
                btn2.Style.Add("Border", "1px solid black");
                btn2.ID = "cair60_" + i;
                btn2.Attributes["onclick"] = "popKomisiBayar('" + rs.Rows[i]["NoKontrak"].ToString() + "','" + NoAgent + "', '" + btn2.ID + "');";
                d = new TableCell();
                d.Text = "a";
                s.Cells.Add(d);
                d.Controls.Add(btn2);
                
                decimal persenlunas = Db.SingleDecimal("SELECT PersenLunas FROM MS_KONTRAK WHERE NoAgent='" + NoAgent + "' AND NoKontrak='" + rs.Rows[i]["NoKontrak"] + "' ");
                if (persenlunas < 30)
                {
                    btn2.Visible = false;
                }

                if (FlagKomisi60==true)
                {
                    btn2.Enabled = false;
                }

                Rpt.Border(s);
                rpt.Rows.Add(s);

                //Third row
                e = new TableCell();
                e.Text = "";
                u.Cells.Add(e);

                e = new TableCell();
                e.Text = "";
                u.Cells.Add(e);

                e = new TableCell();
                e.Text = "";
                u.Cells.Add(e);

                e = new TableCell();
                e.Text = "Closing Fee";
                u.Cells.Add(e);

                e = new TableCell();
                e.Text = Cf.Num(rs.Rows[i]["ClosingFee"].ToString());
                e.HorizontalAlign = HorizontalAlign.Right;
                u.Cells.Add(e);

                decimal persenCF = 0;
                if (Convert.ToDecimal(rs.Rows[i]["ClosingFee"]) != 0)
                {
                    persenCF = (Convert.ToDecimal(rs.Rows[i]["NilaiBayarCF"]) / Convert.ToDecimal(rs.Rows[i]["ClosingFee"])) * 100;
                }
                e = new TableCell();
                if (persenCF == 0)
                {
                    e.Text = "";
                }
                else
                {
                    e.Text = Math.Round(persenCF, 2).ToString() + " %";
                }
                e.HorizontalAlign = HorizontalAlign.Right;
                u.Cells.Add(e);

                btn3 = new Button();
                btn3.Text = "Cair";
                btn3.Style.Add("Width", "80px");
                btn3.Style.Add("Border", "1px solid black");
                btn3.ID = "cairCF_" + i;
                btn3.Attributes["onclick"] = "popKomisiBayar('" + rs.Rows[i]["NoKontrak"].ToString() + "','" + NoAgent + "', '" + btn3.ID + "');";
                e = new TableCell();
                e.Text = "a";
                u.Cells.Add(e);
                e.Controls.Add(btn3);

                if (FlagCF == true)
                {
                    btn3.Enabled = false;
                }

                Rpt.Border(u);
                rpt.Rows.Add(u);

                if (i == rs.Rows.Count - 1)
                    SubTotal(t);
            }
        }

        private void SubTotal(decimal t)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.ColumnSpan = 4;
            c.Text = "<b>GRAND TOTAL</b>";
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(t);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private string NoAgent
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoAgent"]);
            }
        }

        //public string Periode
        //{
        //    get
        //    {
        //        return Cf.Pk(Request.QueryString[ddlPeriode.SelectedValue]);
        //    }
        //}
 }
}

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.ADMINJUAL.Laporan
{
    public partial class PriceList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            if (!Page.IsPostBack)
            {
                comp.InnerHtml = Mi.Pt;
                rpt.Visible = false;
                Js.Focus(this, scr);
                init();
                if (!Act.Sec("DownloadExcel")) xls.Enabled = false;
            }
        }

        private void init()
        {
            DataTable rs;

            rs = Db.Rs("SELECT DISTINCT YEAR(TglInput), MONTH(TglInput) FROM MS_UNIT "
                + " ORDER BY YEAR(TglInput), MONTH(TglInput)");
            for (int i = 0; i < rs.Rows.Count; i++)
                periode.Items.Add(new ListItem(
                    Cf.Monthname((int)rs.Rows[i][1]) + " " + rs.Rows[i][0].ToString()
                    , rs.Rows[i][0] + "," + rs.Rows[i][1]
                    ));

            rs = Db.Rs("SELECT * FROM REF_JENIS ORDER BY SN");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Jenis"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                jenis.Items.Add(new ListItem(t, v));
                jenis.Items[i].Selected = true;
            }

            rs = Db.Rs("SELECT DISTINCT Lokasi FROM MS_UNIT ORDER BY Lokasi");
            for (int i = 0; i < rs.Rows.Count; i++)
                lokasi.Items.Add(new ListItem(rs.Rows[i][0].ToString()));

            lokasi.SelectedIndex = 0;
            periode.SelectedIndex = 0;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isPilih(jenis))
            {
                x = false;
                jenisc.Text = " Pilih Minimum Satu";
            }
            else
                jenisc.Text = "";

            if (!x && s != "")
            {
                RegisterStartupScript("err"
                    , "<script type='text/javascript'>document.getElementById('" + s + "').select()</script>");
            }

            return x;
        }

        protected void scr_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
            }
        }

        protected void xls_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Report();
                Rpt.ToExcel(this, rpt);
            }
        }

        private void Report()
        {
            param.Visible = false;
            rpt.Visible = true;

            Header();
            FillColumn();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            Rpt.Judul(x, comp, judul);

            if (statusA.Checked)
                Rpt.SubJudul(x, "Status : " + statusA.Text);
            else if (statusB.Checked)
                Rpt.SubJudul(x, "Status : " + statusB.Text);
            else
                Rpt.SubJudul(x, "Status : " + statusS.Text);

            Rpt.SubJudul(x
                , "Periode : " + periode.SelectedItem.Text
                );

            Rpt.SubJudul(x
                , "Jenis : " + Rpt.inSql(jenis).Replace("'", "")
                );

            Rpt.SubJudul(x
                , "Lokasi : " + lokasi.SelectedItem.Text
                );

            Rpt.Header(rpt, x);
        }

        protected void FillColumn()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_SKEMA WHERE Status = 'A'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableHeaderCell c = new TableHeaderCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                c.Width = 100;
                c.HorizontalAlign = HorizontalAlign.Right;
                c.Attributes["style"] = "background-color:Gray;color:White;";

                rpt.Rows[2].Cells.Add(c);
            }
            
        }

        private void Fill()
        {
            string Status = "";
            if (statusA.Checked) Status = " AND Status = 'A'";
            if (statusB.Checked) Status = " AND Status = 'B'";
            if (statusC.Checked) Status = " AND Status = 'H'";

            string Periode = "";
            if (periode.SelectedIndex != 0)
            {
                string[] z = periode.SelectedValue.Split(',');
                Periode = " AND YEAR(TglInput) = " + z[0]
                    + " AND MONTH(TglInput) = " + z[1];
            }

            string Lokasi = "";
            if (lokasi.SelectedIndex != 0)
            {
                Lokasi = " AND MS_UNIT.Lokasi = '" + Cf.Str(lokasi.SelectedValue) + "'";
            }

            decimal t1 = 0, t2 = 0, t3 = 0;

            string strSql = "SELECT "
                + "	NoStock"
                + ",Jenis"
                + ",Lokasi"
                + ",NoUnit"
                + ",Luas"
                + ",LuasSG"
                + ",LuasNETT"
                + ",PriceList"
                + ",PriceListMin"
                + ",TglInput"
                + ",Status"
                + " FROM MS_UNIT"
                + " WHERE Jenis IN (" + Rpt.inSql(jenis) + ")"
                + Status
                + Lokasi
                + Periode
                + " ORDER BY NoStock";

            DataTable rsa = Db.Rs("SELECT * FROM REF_SKEMA WHERE Status = 'A'");
            decimal[] total = new decimal[rsa.Rows.Count];
               
            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;
                r.Attributes["ondblclick"] = "popEditUnit('" + rs.Rows[i]["NoStock"] + "')";

                DateTime p = Convert.ToDateTime(rs.Rows[i]["TglInput"]);

                c = new TableCell();
                c.Text = (i + 1).ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT NoKontrak FROM MS_KONTRAK WHERE NoStock = '" + rs.Rows[i]["NoStock"].ToString() + "'");
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglInput"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Lokasi"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Jenis"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LuasSG"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["LuasNett"]);
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                decimal PP = 0;
                

                for (int k = 0; k < rsa.Rows.Count; k++)
                {
                    
                    c = new TableCell();
                    PP = Db.SingleDecimal("SELECT ISNULL(Pricelist,0) from MS_PRICELIST WHERE NoStock = '" + rs.Rows[i]["NoStock"].ToString() + "' AND NoSkema = " + rsa.Rows[k]["Nomor"]);
                    c.Text = Cf.Num(PP);
                    c.HorizontalAlign = HorizontalAlign.Right;
                    r.Cells.Add(c);  
                                  
                    if (total[k] == null)
                    {
                        total[k] = PP;
                    }
                    else
                    {
                        total[k] += PP;
                    }
                }

                rpt.Rows.Add(r);
                t3 += PP;
                if (i == rs.Rows.Count - 1)
                    SubTotal("GRAND TOTAL",total, t1, t2, t3);


            }
        }

        private void SubTotal(string txt,decimal[] total, decimal t1, decimal t2, decimal t3)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 9;
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            for (int k = 0; k < total.Length; k++)
            {
                c = new TableCell();
                c.Text = Cf.Num(total[k]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);
            }

            rpt.Rows.Add(r);
        }

        protected void jenisCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < jenis.Items.Count; i++)
            {
                jenis.Items[i].Selected = jenisCheck.Checked;
            }

            Js.Focus(this, jenisCheck);
            jenisc.Text = "";
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}

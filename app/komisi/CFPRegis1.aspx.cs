using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class CFPRegis1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Bind();
                BindTipeSales();
                Act.ProjectList(project);
            }

            if (project.SelectedIndex != 0 && tipesales.SelectedIndex != 0) //utk menghindari kegagalan postback
            {
                Fill();
            }

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                {
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                    + "<a href=\"javascript:popEditCFP('" + Request.QueryString["done"] + "')\">"
                    + "Regis Pengajuan Closing Fee Telah Berhasil..."
                    + "</a>";
                }
            }
        }

        private void Bind()
        {
            dari.Text = Cf.Day(Cf.AwalBulan(DateTime.Now.Month, DateTime.Now.Year));
            sampai.Text = Cf.Day(Cf.AkhirBulan(DateTime.Now.Month, DateTime.Now.Year));

            //bind tipe marketing 
            tipesales.Items.Add(new ListItem { Text = "Tipe Marketing :", Value = "0" });
            LibMkt.ListTipeSales(tipesales, project.SelectedValue);
        }

        protected void Fill()
        {
            list.Controls.Clear();

            DateTime Dari = Convert.ToDateTime(dari.Text);
            DateTime Sampai = Convert.ToDateTime(sampai.Text);
            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            //fill tittle
            headperiode.Text = Cf.DayIndo(Dari) + " s/d " + Cf.DayIndo(Sampai);

            string w = "";
            if (tipesales.SelectedIndex != 0)
            {
                trTipeSales.Visible = true;
                headtipe.Text = Db.SingleString("select Tipe from REF_AGENT_TIPE where ID = '" + tipesales.SelectedValue + "'");
                w = " AND b.SalesTipe = '" + tipesales.SelectedValue + "'";
            }
            else
            {
                trTipeSales.Visible = false;
            }

            string v = "";
            if (sales.SelectedIndex != 0)
            {
                trNama.Visible = true;
                headnama.Text = Db.SingleString("select Nama from MS_AGENT where NoAgent = '" + sales.SelectedValue + "'");
                v = " AND a.NoAgent = '" + sales.SelectedValue + "'";
            }
            else
            {
                trNama.Visible = false;
            }

            string strSql = "SELECT a.NoCF, a.SN, a.Nilai, b.NoKontrak, b.NoUnit, a.NamaAgent, b.NamaCust"
                + " FROM MS_KOMISI_CF_DETAIL a"
                + " INNER JOIN MS_KOMISI_CF b ON a.NoCF = b.NoCF"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,b.Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,b.Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND (SELECT COUNT(*) FROM MS_KOMISI_CFP_DETAIL WHERE NoCF = a.NoCF AND SN_NoCF = a.SN) = 0"
                + w
                + v
                + " AND b.Project = '" + project.SelectedValue + "'"
                + " ORDER BY b.NoCF";
            
            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Tidak terdapat data dengan kriteria seperti tersebut diatas.");

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow r = new HtmlTableRow();
                HtmlTableCell c;
                CheckBox cb;

                cb = new CheckBox();
                cb.ID = "cb_" + index;
                cb.Attributes["title"] = rs.Rows[i]["NoCF"] + ";" + rs.Rows[i]["SN"];

                c = new HtmlTableCell();
                c.Controls.Add(cb);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoCF"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoKontrak"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NamaCust"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NamaAgent"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["Nilai"]));
                c.Attributes["class"] = "right";
                r.Cells.Add(c);

                list.Controls.Add(r);

                index++;
            }
        }
        private bool valid()
        {

            string s = "";
            bool x = true;

            //Tanggal Pengajuan
            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

            //dropdown project
            if (project.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = project.ID;
                projectc.Text = " &nbsp; Project / Marketing Belum Dipilih";
            }
            else
            {
                projectc.Text = "";
            }

            if (tipesales.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = tipesales.ID;
                projectc.Text = " &nbsp; Project / Tipe Marketing Belum Dipilih";
            }
            else
            {
                projectc.Text = "";
            }

            if (!x)
            {
                this.RegisterStartupScript(
                    "focusScript"
                    , "<script language='javascript'>"
                    + " document.getElementById('" + s + "').focus();"
                    + " document.getElementById('" + s + "').select();"
                    + "</script>"
                    );
            }
            return x;
        }
        protected void display_Click(object sender, System.EventArgs e)
        {
            Fill();
        }
        protected void save_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                DateTime Tgl = Convert.ToDateTime(tgl.Text);
                string CFPID = LibKom.CFPID(Tgl.Month, Tgl.Year, project.SelectedValue);
                string Project = Cf.Str(project.SelectedValue);

                Db.Execute("EXEC spKomisiCFPDaftar"
                    + " '" + CFPID + "'"
                    + ",'" + Tgl + "'"
                    + ",'" + Cf.Str(ket.Text) + "'"
                    );

                Db.Execute("UPDATE MS_KOMISI_CFP SET Project = '" + Project + "', SalesTipe = '" + tipesales.SelectedValue + "'"
                    + " WHERE NoCFP = '" + CFPID + "'");

                int index = 0;
                foreach (Control tr in list.Controls)
                {
                    CheckBox cb = (CheckBox)list.FindControl("cb_" + index);
                    
                    string[] aa = cb.Attributes["title"].Split(';');

                    if (cb.Checked)
                    {
                        DataTable dd = Db.Rs("SELECT * FROM MS_KOMISI_CF_DETAIL WHERE NoCF = '" + aa[0] + "' AND SN = " + Convert.ToInt32(aa[1]));
                        if (dd != null)
                        {
                            Db.Execute("EXEC spKomisiCFPDetil"
                                + " '" + CFPID + "'"
                                + ",'" + dd.Rows[0]["NoCF"].ToString() + "'"
                                + ", " + Convert.ToInt32(dd.Rows[0]["SN"])
                                + ", " + Convert.ToDecimal(dd.Rows[0]["Nilai"])
                                + ", " + Convert.ToInt32(dd.Rows[0]["NoAgent"])
                                + ", '" + dd.Rows[0]["NamaAgent"].ToString() + "'"
                                );
                        }
                    }

                    index++;
                }

                DataTable rsHeader = Db.Rs("SELECT "
                    + " NoCFP"
                    + ",CONVERT(varchar,Tgl,106) AS [Tgl. Pengajuan]"
                    + ",Ket AS [Keterangan]"
                    + ",(select Tipe from ref_agent_tipe where ref_agent_tipe.ID = MS_KOMISI_CFP.SalesTipe) AS [Tipe]"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_CFP "
                    + " WHERE NoCFP = '" + CFPID + "'");

                DataTable rsDetail = Db.Rs("SELECT "
                    + " CONVERT(VARCHAR, SN) "
                    + " + '.  ' + (SELECT NamaAgent FROM MS_KOMISI_CF WHERE NoCF = a.NoCF AND SN = a.SN_NoCF)"
                    + " + '  ' + CONVERT(VARCHAR, Nilai, 1) "
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_CFP_DETAIL a WHERE NoCFP = '" + CFPID + "'");

                string Ket = Cf.LogCapture(rsHeader)
                    + Cf.LogList(rsDetail, "DETAIL");

                Db.Execute("EXEC spLogKomisiCFP"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + CFPID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_CFP_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_KOMISI_CFP_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("CFPRegis1.aspx?done="+CFPID);
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipesales.Items.Clear();
            Bind();
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class CFDel : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                BindTipeSales();
                Bind();
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
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Clear False Closing Fee Berhasil...";
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

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(dari))
            {
                x = false;
                if (s == "") s = dari.ID;
                daric.Text = "Tanggal";
            }
            else
                daric.Text = "";

            if (!Cf.isTgl(sampai))
            {
                x = false;
                if (s == "") s = sampai.ID;
                sampaic.Text = "Tanggal";
            }
            else
                sampaic.Text = "";

            if (project.SelectedIndex == 0)
            {
                x = false;
                if (s == "") s = project.ID;
                projectc.Text = " &nbsp; Project / Tipe Marketing Belum Dipilih";
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

            return x;
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Fill();
            }
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

            string strSql = "SELECT a.*, b.Nama AS NamaAgent, b.SalesTipe, c.Nama AS NamaCust"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_AGENT b ON a.NoAgent = b.NoAgent"
                + " INNER JOIN MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,a.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,a.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Status = 'A' AND a.CFID != ''"
                + w
                + v
                + " AND a.Project = '" + project.SelectedValue + "'"
                + " ORDER BY NoKontrak";

            DataTable rs = Db.Rs(strSql);
            del.Enabled = false;
            Rpt.NoData(list, rs, "Tidak terdapat kontrak dengan kriteria seperti tersebut diatas.");

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                DateTime TglKontrak = Convert.ToDateTime(rs.Rows[i]["TglKontrak"]);

                bool tampil = true;
                int cfp = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_CFP_DETAIL WHERE NoCF = '" + rs.Rows[i]["CFID"].ToString() + "'");
                if (cfp != 0)
                {
                    tampil = false;
                }

                if (tampil)
                {
                    TableRow r = new TableRow();
                    TableCell c;

                    r.VerticalAlign = VerticalAlign.Top;

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NamaAgent"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoKontrak"].ToString();
                    c.ID = "cfid_" + index;
                    c.Attributes["title"] = rs.Rows[i]["CFID"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);
                    //Response.Write(rs.Rows[i]["CFID"].ToString());

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoUnit"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NamaCust"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    //nge-fill ref_skema_closing fee
                    string w2 = "";
                    if (tipesales.SelectedIndex != 0)
                    {
                        w2 = " AND SalesTipe = '" + tipesales.SelectedValue + "'";
                    }

                    string Skema = "", DasarHitung = "";
                    DataTable rsSkema = Db.Rs("SELECT * FROM REF_SKOM_CF WHERE " + Cf.Tgl112(TglKontrak) + " >= CONVERT(VARCHAR,Dari,112) AND " + Cf.Tgl112(TglKontrak) + " <= CONVERT(VARCHAR,Sampai,112)"
                        + " AND Inaktif = 0"
                        + w2
                        );
                    if (rsSkema.Rows.Count > 0)
                    {
                        Skema = rsSkema.Rows[0]["Nama"].ToString();
                        DasarHitung = rsSkema.Rows[0]["DasarHitung"].ToString();
                    }
                    else
                    {
                        del.Enabled = false;
                        alert.Text = "Skema belum Tersedia";
                    }

                    c = new TableCell();
                    c.Text = Skema;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    decimal Nilai = 0;
                    if (DasarHitung != "")
                    {
                        Nilai = DasarHitung == "DPP" ? Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]) : Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                    }

                    c = new TableCell();
                    c.Text = Cf.Num(Nilai);
                    c.CssClass = "right";
                    r.Cells.Add(c);

                    Rpt.Border(r);
                    list.Controls.Add(r);

                    index++;
                    del.Enabled = true;
                }
            }
        }

        protected void del_Click(object sender, EventArgs e)
        {
            int index = 0;
            foreach (Control tr in list.Controls)
            {
                TableCell cfid = (TableCell)list.FindControl("cfid_" + index);

                int cfp = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_CFP_DETAIL WHERE NoCF = '" + cfid.Attributes["title"] + "'");
                if (cfp == 0)
                {
                    DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_CF WHERE NoCF = '" + cfid.Attributes["title"] + "'");
                    if (rs.Rows.Count == 0)
                        Response.Redirect("/CustomError/Deleted.html");
                    else
                    {
                        string Ket = "***Alasan Delete :<br>" + Cf.Str(alasan.Text)
                            + "<br><br>***Data Sebelum Delete :<br>"
                            + Cf.LogCapture(rs);

                        Db.Execute("EXEC spKomisiCFDel '" + rs.Rows[0]["NoCF"].ToString() + "'");

                        int c = Db.SingleInteger(
                            "SELECT COUNT(*) FROM MS_KOMISI_CF WHERE NoCF = '" + rs.Rows[0]["NoCF"].ToString() + "'");

                        if (c == 0)
                        {
                            //Log
                            Db.Execute("EXEC spLogKomisiCF "
                                + " 'DELETE'"
                                + ",'" + Act.UserID + "'"
                                + ",'" + Act.IP + "'"
                                + ",'" + Ket + "'"
                                + ",'" + rs.Rows[0]["NoCF"].ToString() + "'"
                                );
                        }
                    }
                }

                index++;
            }

            Response.Redirect("CFDel.aspx?done=1");
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

        protected void skema_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skema.SelectedIndex != 0)
            {
                string strSql = "SELECT * FROM REF_SKOM_CF WHERE NoSkema = '" + skema.SelectedValue + "'";
                DataTable rs = Db.Rs(strSql);
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    dari.Text = Cf.Day(rs.Rows[i]["Dari"]);
                    sampai.Text = Cf.Day(rs.Rows[i]["Sampai"]);
                }
            }
            else
            {
                dari.Text = Cf.Day(Cf.AwalBulan(DateTime.Now.Month, DateTime.Now.Year));
                sampai.Text = Cf.Day(Cf.AkhirBulan(DateTime.Now.Month, DateTime.Now.Year));
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
            BindSkema();
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

        void BindSkema()
        {
            skema.Items.Clear();

            string strSql = "SELECT * FROM REF_SKOM_CF WHERE SalesTipe ='" + tipesales.SelectedValue + "' AND Project = '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs(strSql);
            skema.Items.Add(new ListItem { Text = "Skema :", Value = "0" });

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoSkema"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                skema.Items.Add(new ListItem(t, v));
            }
        }
    }
}

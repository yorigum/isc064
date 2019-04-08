using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class CFPDel : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                BindTipeSales();
                init();
            }

            if (IsPostBack)
            {
                if(tipesales.SelectedIndex != 0)
                {
                    Fill();
                }
            }

            del.Enabled = false;

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Clear False Pengajuan Closing Fee Berhasil...";
            }
        }

        private void Bind()
        {
            //bind tipe marketing 
            tipesales.Items.Add(new ListItem { Text = "Tipe Marketing :", Value = "0" });
            LibMkt.ListTipeSales(tipesales, project.SelectedValue);
        }

        private void init()
        {
            Act.ProjectList(project);

            dari.Text = Cf.Day(Cf.AwalBulan(DateTime.Now.Month, DateTime.Now.Year));
            sampai.Text = Cf.Day(Cf.AkhirBulan(DateTime.Now.Month, DateTime.Now.Year));
        }

        private bool valid()
        {

            string s = "";
            bool x = true;

            //Tanggal Pengajuan
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

            return x;
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            if(valid())
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

            string w = "";
            if (tipesales.SelectedIndex != 0)
            {
                w = " AND SalesTipe = '" + tipesales.SelectedValue + "'";
            }

            string strSql = "SELECT * FROM MS_KOMISI_CFP"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND (SELECT COUNT(*) FROM MS_KOMISI_CFR WHERE NoCFP = MS_KOMISI_CFP.NoCFP) = 0" //jika sudah realisasi..gak nongol
                + w
                + " AND Project = '" + project.SelectedValue + "'"
                + " ORDER BY NoCFP";

            //string strSql = "SELECT a.NoCFP, a.SN, a.Nilai, c.NoKontrak, c.NoUnit, (SELECT NamaAgent From MS_KOMISI_CF_DETAIL WHERE NoCF = a.NoCF and SN = a.SN) AS NamaAgent, c.NamaCust"
            //    + " FROM MS_KOMISI_CFP_DETAIL a"
            //    + " INNER JOIN MS_KOMISI_CFP b ON a.NoCFP = b.NoCFP"
            //    + " INNER JOIN MS_KOMISI_CF c ON a.NoCF = c.NoCF"
            //    + " WHERE 1=1 "
            //    + " AND CONVERT(varchar,b.Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
            //    + " AND CONVERT(varchar,b.Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
            //    + " AND (SELECT COUNT(*) FROM MS_KOMISI_CFP_DETAIL WHERE NoCF = a.NoCF AND SN_NoCF = a.SN) > 0"
            //    + " AND c.Project = '" + project.SelectedValue + "'"
            //    + " ORDER BY a.NoCF";

            DataTable rs = Db.Rs(strSql);
            del.Enabled = false;
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
                cb.Attributes["title"] = rs.Rows[i]["NoCFP"].ToString();
                if(tipesales.SelectedIndex == 0)
                {
                    cb.Enabled = false;
                }

                c = new HtmlTableCell();
                c.Controls.Add(cb);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoCFP"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Day(rs.Rows[i]["Tgl"]);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Db.SingleString("select Tipe from REF_AGENT_TIPE where ID = '" + rs.Rows[i]["SalesTipe"] + "'");
                r.Cells.Add(c);

                string Sales = Db.SingleString("SELECT TOP 1 STUFF((SELECT distinct ', ' + NamaAgent FROM MS_KOMISI_CFP_DETAIL AS T1"
                    + " where NoCFP = '" + rs.Rows[i]["NoCFP"].ToString() + "'"
                    + " FOR XML PATH('')), 1, 1, '') As Nama "
                    + " FROM MS_KOMISI_CFP_DETAIL AS T2 where NoCFP = '" + rs.Rows[i]["NoCFP"].ToString() + "'"
                );

                c = new HtmlTableCell();
                c.InnerHtml = Sales;
                r.Cells.Add(c);

                list.Controls.Add(r);

                index++;
                del.Enabled = true;
            }
        }

        protected void delbtn_Click(object sender, EventArgs e)
        {
            int index = 0;
            foreach (Control tr in list.Controls)
            {
                CheckBox cb = (CheckBox)list.FindControl("cb_" + index);
                HtmlTableCell cfid = (HtmlTableCell)list.FindControl("cfid_" + index);
                if (cb.Checked)
                {
                    Response.Write(cb.Attributes["title"]);
                    int cfp = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_CFP_DETAIL WHERE NoCFP = '" + cb.Attributes["title"] + "'");
                    if (cfp > 0)
                    {
                        DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_CFP WHERE NoCFP = '" + cb.Attributes["title"] + "'");
                        if (rs.Rows.Count == 0)
                            Response.Redirect("/CustomError/Deleted.html");
                        else
                        {
                            string Ket = "***Alasan Delete :<br>" + Cf.Str(alasan.Text)
                                + "<br><br>***Data Sebelum Delete :<br>"
                                + Cf.LogCapture(rs);

                            Db.Execute("EXEC spKomisiCFPDel '" + rs.Rows[0]["NoCFP"].ToString() + "'");

                            int c = Db.SingleInteger(
                                "SELECT COUNT(*) FROM MS_KOMISI_CFP WHERE NoCFP = '" + rs.Rows[0]["NoCFP"].ToString() + "'");

                            if (c > 0)
                            {
                                //Log
                                Db.Execute("EXEC spLogKomisiCFP "
                                    + " 'DELETE'"
                                    + ",'" + Act.UserID + "'"
                                    + ",'" + Act.IP + "'"
                                    + ",'" + Ket + "'"
                                    + ",'" + rs.Rows[0]["NoCFP"].ToString() + "'"
                                    );

                                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_CFP_LOG ORDER BY LogID DESC");
                                string Project = Db.SingleString("SELECT Project FROM MS_KOMISI_CFP WHERE NoCFP = " + rs.Rows[0]["NoCFP"].ToString());
                                Db.Execute("UPDATE MS_KOMISI_CFP_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                            }
                        }
                    }
                }

                index++;
            }
            Response.Redirect("CFPDel.aspx?done=1");
        }

        private string NoCFP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoCFP"]);
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipesales.Items.Clear();
            BindTipeSales();
        }

        protected void tipesales_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind();
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

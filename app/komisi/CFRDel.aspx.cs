using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class CFRDel : System.Web.UI.Page
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
                fill();
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
                        + "Clear False Realisasi Closing Fee Berhasil...";
            }
        }

        private void Bind()
        {
            dari.Text = Cf.Day(Cf.AwalBulan(DateTime.Now.Month, DateTime.Now.Year));
            sampai.Text = Cf.Day(Cf.AkhirBulan(DateTime.Now.Month, DateTime.Now.Year));

            string strSql = "SELECT * FROM REF_AGENT_TIPE WHERE Project= '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs(strSql);
            tipesales.Items.Add(new ListItem { Text = "Tipe Marketing:", Value = "0" });

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["ID"].ToString();
                string t = rs.Rows[i]["Tipe"].ToString();
                tipesales.Items.Add(new ListItem(t, v));
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

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

            //if (!x)
            //    Js.Alert(
            //        this
            //        , "Input Tidak Valid.\\n\\n"
            //        + "Aturan Proses :\\n"
            //        + s
            //        + "1. Project harus dipilih.\\n"
            //        , "document.getElementById('" + s + "').focus();"
            //        + "document.getElementById('" + s + "').select();"
            //        );

            return x;
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                fill();
            }
        }

        protected void fill()
        {
            list.Controls.Clear();

            //mengaktifkan tittle
            tbHead.Visible = true;

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
                w = " AND SalesTipe = '" + tipesales.SelectedValue + "'";
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
                v = " AND (select count(*) from MS_KOMISI_CFP_DETAIL where NoAgent = '" + sales.SelectedValue + "') != 0";
            }
            else
            {
                trNama.Visible = false;
            }

            string strSql = "SELECT * FROM MS_KOMISI_CFR"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                //+ " AND (SELECT COUNT(*) FROM MS_KOMISI_CFR WHERE NoCFP = MS_KOMISI_CFP.NoCFP) = 0" //jika sudah realisasi..gak nongol
                + w
                + " AND Project IN (" + Act.ProjectListSql + ")"
                + " ORDER BY NoCFR";
            
            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Tidak terdapat data dengan kriteria seperti tersebut diatas.");

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                //fill tittle
                headperiode.Text = Cf.DayIndo(Dari) + " s/d " + Cf.DayIndo(Sampai);
                headtipe.Text = Db.SingleString("select Tipe from REF_AGENT_TIPE where ID = '" + tipesales.SelectedValue + "'");
                if (sales.SelectedIndex != 0)
                {
                    headnama.Text = rs.Rows[i]["NamaAgent"].ToString();
                }

                HtmlTableRow r = new HtmlTableRow();
                HtmlTableCell c;
                CheckBox cb;

                cb = new CheckBox();
                cb.ID = "cb_" + index;
                cb.Attributes["title"] = rs.Rows[i]["NoCFR"].ToString();

                c = new HtmlTableCell();
                c.Controls.Add(cb);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoCFR"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Day(rs.Rows[i]["Tgl"]);
                r.Cells.Add(c);

                string Sales = Db.SingleString("SELECT TOP 1 STUFF((SELECT distinct ', ' + NamaAgent FROM MS_KOMISI_CFR_DETAIL AS T1"
                    + " where NoCFR = '" + rs.Rows[i]["NoCFR"].ToString() + "'"
                    + " FOR XML PATH('')), 1, 1, '') As Nama "
                    + " FROM MS_KOMISI_CFR_DETAIL AS T2 where NoCFR = '" + rs.Rows[i]["NoCFR"].ToString() + "'"
                );

                c = new HtmlTableCell();
                c.InnerHtml = Sales;
                r.Cells.Add(c);

                list.Controls.Add(r);

                index++;
            }
        }

        protected void delbtn_Click(object sender, EventArgs e)
        {
            int index = 0;
            foreach (Control tr in list.Controls)
            {
                //TableCell cfpid = (TableCell)list.FindControl("cfpid_" + index);
                CheckBox cb = (CheckBox)list.FindControl("cb_" + index);

                //Response.Write(cfpid.Attributes["title"]);
                if (cb.Checked)
                {
                    int cfp = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_CFR_DETAIL WHERE NoCFR = '" + cb.Attributes["title"] + "'");
                    if (cfp > 0)
                    {
                        DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_CFR WHERE NoCFR = '" + cb.Attributes["title"] + "'");
                        if (rs.Rows.Count == 0)
                            Response.Redirect("/CustomError/Deleted.html");
                        else
                        {
                            string Ket = "***Alasan Delete :<br>" + Cf.Str(alasan.Text)
                                + "<br><br>***Data Sebelum Delete :<br>"
                                + Cf.LogCapture(rs);

                            //Db.Execute("UPDATE MS_KOMISI_CFP SET Realisasi = 0 WHERE NoCFP = '" + rs.Rows[0]["NoCFP"].ToString() + "' AND Realisasi = 1");
                            Db.Execute("EXEC spKomisiCFRDel '" + rs.Rows[0]["NoCFR"].ToString() + "'");

                            int c = Db.SingleInteger(
                                "SELECT COUNT(*) FROM MS_KOMISI_CFR WHERE NoCFR = '" + rs.Rows[0]["NoCFR"].ToString() + "'");

                            if (c > 0)
                            {
                                //Log
                                Db.Execute("EXEC spLogKomisiCFR "
                                    + " 'DELETE'"
                                    + ",'" + Act.UserID + "'"
                                    + ",'" + Act.IP + "'"
                                    + ",'" + Ket + "'"
                                    + ",'" + rs.Rows[0]["NoCFR"].ToString() + "'"
                                    );
                            }
                        }
                    }
                }

                index++;
            }

            Response.Redirect("CFRDel.aspx?done=1");
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

        private string NoCFR
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoCFR"]);
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

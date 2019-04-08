using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KomisiGen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                backbtn.Visible = false;
                nokontrak.Attributes["ondblclick"] = "popDaftarKontrak('a&kom=1');";

                if (Request.QueryString["NoKontrak"] != null)
                {
                    nokontrak.Text = Request.QueryString["NoKontrak"];

                    if (Request.QueryString["kom"] != null)
                    {
                        dariReminder.Checked = true;
                        cancel.Attributes["onclick"] = "location.href='ReminderKom.aspx'";
                    }
                    else
                    {
                        dariDaftar.Checked = true;
                        cancel.Attributes["onclick"] = "location.href='KontrakDaftar3.aspx?NoKontrak=" + NoKontrak + "'";
                    }

                    LoadKontrak();
                }
                else
                {
                    Js.Focus(this, nokontrak);
                    frm.Visible = false;
                }
            }

            FeedBack();
            if (frm.Visible) Js.Confirm(this, "Lanjutkan proses generate jadwal komisi?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popJadwalKomisi('" + Request.QueryString["done"] + "')\">"
                        + "Generate Komisi Berhasil..."
                        + "</a>";
            }
        }

        private bool valid()
        {
            bool x = true;

            int c = Db.SingleInteger(
                "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "' AND Status = 'A' AND FlagKomisi = 0");

            if (c == 0)
                x = false;

            if (!x)
                Js.Alert(
                    this
                    , "Kontrak Tidak Valid.\\n\\n"
                    + "Kemungkinan Sebab :\\n"
                    + "1. Kontrak tersebut tidak terdaftar.\\n"
                    + "2. Kontrak tersebut sudah dibatalkan.\\n"
                    + "3. Jadwal komisi sudah pernah dilakukan."
                    , "document.getElementById('nokontrak').focus();"
                    + "document.getElementById('nokontrak').select();"
                    );

            return x;
        }

        private void LoadKontrak()
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                InitForm();
                Fill();

                Js.Confirm(this, "Lanjutkan proses generate jadwal komisi?");
            }
            else
            {
                backbtn.Visible = true;
                Js.Focus(this, nokontrak);
                frm.Visible = false;
            }
        }

        protected void next_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                pilih.Visible = false;
                frm.Visible = true;

                InitForm();
                Fill();

                Js.Confirm(this, "Lanjutkan proses generate jadwal komisi?");
            }
        }

        private void InitForm()
        {
            DataTable rs = Db.Rs("SELECT Nomor,Nama FROM REF_SKOM ORDER BY Nama");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Nomor"].ToString();
                string t = rs.Rows[i]["Nama"] + " (" + v.PadLeft(3, '0') + ")";
                daftarskema.Items.Add(new ListItem(t, v));
            }
        }

        private void Fill()
        {
            Func.KontrakHeader(NoKontrak, nokontrakl, unit, customer, agent);

            string strSql = "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                principal.Text = Db.SingleString(
                    "SELECT Principal FROM MS_AGENT WHERE NoAgent = " + rs.Rows[0]["NoAgent"]);

                netto.Text = Cf.Num(rs.Rows[0]["NilaiKontrak"]);

                FillAgent(Convert.ToInt32(rs.Rows[0]["NoAgent"]));
                FillTb(Convert.ToInt32(rs.Rows[0]["NoAgent"]));
            }
        }

        private void FillAgent(int NoAgent)
        {
            string strSql = "SELECT * FROM MS_AGENT WHERE NoAgent = " + NoAgent;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                //Target Marketing
                int Skema = (int)rs.Rows[0]["Skema0"];

                decimal TotalKontrak = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiKontrak),0)"
                    + " FROM MS_KONTRAK WHERE Status = 'A'"
                    + " AND NoAgent = " + NoAgent);

                kumulatif.Text = Cf.Num(TotalKontrak);

                skemaid.Text = Skema.ToString();
                skema.Text = daftarskema.Items.FindByValue(skema.ToString()).Text;
            }
        }

        private void FillTb(int NoAgent)
        {
            string strSql = "SELECT "
                + " NoKontrak"
                + ",NoUnit"
                + ",NilaiKontrak"
                + ",TglKontrak"
                + ",Nama AS Cs"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " WHERE NoAgent = " + NoAgent
                + " AND MS_KONTRAK.Status  = 'A'"
                + " ORDER BY NoKontrak";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Agen ini belum pernah melakukan penjualan.");

            decimal k = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<a href=\"javascript:popEditKontrak('" + rs.Rows[i]["NoKontrak"] + "')\">"
                    + rs.Rows[i]["NoKontrak"].ToString()
                    + "</a>";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Day(rs.Rows[i]["TglKontrak"]);
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Cs"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(rs.Rows[i]["NilaiKontrak"]);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                //nilai kumulatif
                k = k + (decimal)rs.Rows[i]["NilaiKontrak"];
                c = new TableCell();
                c.Text = Cf.Num(k);
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            //generate komisi
            Func.GenerateKomisi(NoKontrak, special);

            DataTable rs = Db.Rs("SELECT "
                + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                + ",MS_KONTRAK.NoUnit AS [Unit]"
                + ",MS_CUSTOMER.Nama AS [Customer]"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS [Agent]"
                + ",MS_KONTRAK.SkemaKomisi AS [Skema Komisi]"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                + " WHERE NoKontrak = '" + NoKontrak + "'"
                );

            DataTable rsKomisi = Db.Rs("SELECT "
                + "CONVERT(VARCHAR,NoUrut) + '.  ' + NamaKomisi + ' ('+Tipe+')   CAIR:' + CONVERT(VARCHAR,TermCair,1) + '% (' + Jadwal + ')  ' + CONVERT(VARCHAR,NilaiKomisi,1) "
                + "FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "' ORDER BY NoUrut");

            string Ket = Cf.LogCapture(rs)
                + Cf.LogList(rsKomisi, "JADWAL KOMISI");

            Db.Execute("EXEC spLogKontrak"
                + " 'KOMISI'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + NoKontrak + "'"
                );

            if (dariReminder.Checked)
                Response.Redirect("ReminderKom.aspx?done=" + NoKontrak);
            else if (dariDaftar.Checked)
                Response.Redirect("KontrakDaftar3.aspx?done=1&NoKontrak=" + NoKontrak);
            else
                Response.Redirect("KomisiGen.aspx?done=" + NoKontrak);
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(nokontrak.Text);
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
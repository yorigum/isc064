using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class KomisiRRegis2 : System.Web.UI.Page
    {
        protected string Project { get { return Request.QueryString["Project"]; } }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Fill();
        }
        protected void Fill()
        {
            kode.Text = Request.QueryString["id"];
            //tgl.Text = Cf.Day(DateTime.Today);

            list.Controls.Clear();

            string v = "";
            if (Project != "")
                v = "AND b.Project = '" + Project + "'";

            string strSql = "SELECT a.NoKomisi, a.SN, a.SN_KomisiTermin, a.Nilai, b.NoKontrak, b.NoUnit, b.NamaCust"
                + " FROM MS_KOMISIP_DETAIL a"
                + " INNER JOIN MS_KOMISI b ON a.NoKomisi = b.NoKomisi"
                + " WHERE 1=1 "
                + " AND (SELECT COUNT(*) FROM MS_KOMISIR_DETAIL WHERE NoKomisi = a.NoKomisi AND SN_KomisiTermin = a.SN_KomisiTermin) = 0"
                + v
                + " AND NoKomisiP = '" + Request.QueryString["id"] + "'";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Tidak terdapat data dengan kriteria seperti tersebut diatas.");

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow r = new HtmlTableRow();
                HtmlTableCell c;
                CheckBox cb;

                string NamaAgent = Db.SingleString("SELECT ISNULL(NamaAgent,'') FROM MS_KOMISI_TERM WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"].ToString() + "' AND SN = " + Convert.ToInt32(rs.Rows[i]["SN_KomisiTermin"]));
                int NoAgent = Db.SingleInteger("SELECT ISNULL(NoAgent,0) FROM MS_KOMISI_TERM WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"].ToString() + "' AND SN = " + Convert.ToInt32(rs.Rows[i]["SN_KomisiTermin"]));
                string Termin = Db.SingleString("SELECT ISNULL(Nama,'') FROM MS_KOMISI_TERM WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"].ToString() + "' AND SN = " + Convert.ToInt32(rs.Rows[i]["SN_KomisiTermin"]));

                cb = new CheckBox();
                cb.ID = "cb_" + index;
                cb.Attributes["title"] = rs.Rows[i]["NoKomisi"] + ";" + rs.Rows[i]["SN_KomisiTermin"] + ";" + NoAgent;

                c = new HtmlTableCell();
                c.Controls.Add(cb);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = NamaAgent;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NamaCust"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoKontrak"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Termin;
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

            //Tanggal Realisasi
            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

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
            else
            {
            }

            return x;
        }
        protected void save_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                DateTime Tgl = Convert.ToDateTime(tgl.Text);
                string KRID = LibKom.KRID(Tgl.Month, Tgl.Year);

                Db.Execute("EXEC spKomisiRDaftar"
                    + " '" + KRID + "'"
                    + ",'" + Tgl + "'"
                    + ",'" + Request.QueryString["id"] + "'"
                    + ",'" + Cf.Str(ket.Text) + "'"
                    );

                Db.Execute("UPDATE MS_KOMISIR SET Project = '" + Project + "'"
                    + " WHERE NoKomisiR = '" + KRID + "'");

                int index = 0;
                foreach (Control tr in list.Controls)
                {
                    CheckBox cb = (CheckBox)list.FindControl("cb_" + index);

                    string[] aa = cb.Attributes["title"].Split(';');

                    if (cb.Checked)
                    {
                        DataTable dd = Db.Rs("SELECT * FROM MS_KOMISIP_DETAIL WHERE NoKomisi = '" + aa[0] + "' AND SN_KomisiTermin = " + Convert.ToInt32(aa[1]));
                        if (dd != null)
                        {
                            Db.Execute("EXEC spKomisiRDetil"
                                + " '" + KRID + "'"
                                + ",'" + dd.Rows[0]["NoKomisi"].ToString() + "'"
                                + ", " + Convert.ToInt32(dd.Rows[0]["SN_KomisiTermin"])
                                + ", " + Convert.ToDecimal(dd.Rows[0]["Nilai"])
                                );

                            Db.Execute("UPDATE MS_KOMISIR_DETAIL SET NoAgent = '" + Convert.ToInt32(aa[2]) + "' WHERE NoKomisiR = '" + KRID + "' AND SN_KomisiTermin = " + Convert.ToInt32(dd.Rows[0]["SN_KomisiTermin"]));
                        }
                    }

                    index++;
                }

                DataTable rs = Db.Rs("SELECT * FROM MS_KOMISIP_DETAIL a"
                    + " INNER JOIN MS_KOMISI b ON a.NoKomisi = b.NoKomisi"
                    + " WHERE (SELECT COUNT(*) FROM MS_KOMISIR_DETAIL WHERE NoKomisi = a.NoKomisi AND SN_KomisiTermin = a.SN_KomisiTermin) = 0"
                    + " AND a.NoKomisiP = '" + Request.QueryString["id"] + "'"
                    );

                if (rs.Rows.Count == 0)
                {
                    Db.Execute("UPDATE MS_KOMISIP SET Realisasi = 1 WHERE NoKomisiP = '" + Request.QueryString["id"] + "'");
                }

                DataTable rsHeader = Db.Rs("SELECT "
                    + " NoKomisiR"
                    + ",CONVERT(varchar,Tgl,106) AS [Tgl. Realisasi]"
                    + ",Ket AS [Keterangan]"
                    + ",NoKomisiP AS [Kode Pengajuan]"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISIR "
                    + " WHERE NoKomisiR = '" + KRID + "'");

                DataTable rsDetail = Db.Rs("SELECT "
                    + " CONVERT(VARCHAR, SN) "
                    + " + '.  ' + (SELECT NamaAgent FROM MS_KOMISI_DETAIL WHERE NoKomisi = a.NoKomisi AND SN = a.SN_KomisiTermin)"
                    + " + '  ' + CONVERT(VARCHAR, Nilai, 1) "
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISIR_DETAIL a WHERE NoKomisiR = '" + KRID + "'");

                string Ket = Cf.LogCapture(rsHeader)
                    + Cf.LogList(rsDetail, "DETAIL");

                Db.Execute("EXEC spLogKomisiR"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + KRID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISIR_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_KOMISIR_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("KomisiRRegis1.aspx?id=" + KRID);
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

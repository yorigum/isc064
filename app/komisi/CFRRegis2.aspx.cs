using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class CFRRegis2 : System.Web.UI.Page
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

            string strSql = "SELECT a.NoCF, a.SN, a.SN_NoCF, a.Nilai, b.NoKontrak, b.NoUnit, b.NamaCust, b.NoAgent, b.NamaSkema"
                + " FROM MS_KOMISI_CFP_DETAIL a"
                + " INNER JOIN MS_KOMISI_CF b ON a.NoCF = b.NoCF"
                + " WHERE 1=1 "
                + " AND (SELECT COUNT(*) FROM MS_KOMISI_CFR_DETAIL WHERE NoCF = a.NoCF AND SN_NoCF = a.SN_NoCF) = 0"
                + " AND b.Project = '" + Project + "'"
                + " AND NoCFP = '" + Request.QueryString["id"] + "'";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Tidak terdapat data dengan kriteria seperti tersebut diatas.");

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow r = new HtmlTableRow();
                HtmlTableCell c;
                CheckBox cb;
                TextBox tb6;

                string NamaPenerima = Db.SingleString("SELECT ISNULL(NamaAgent,'') FROM MS_KOMISI_CF_DETAIL WHERE NoCF = '" + rs.Rows[i]["NoCF"].ToString() + "' AND SN = " + Convert.ToInt32(rs.Rows[i]["SN_NoCF"]));
                int NoAgent = Db.SingleInteger("SELECT ISNULL(NoAgent,0) FROM MS_KOMISI_CF_DETAIL WHERE NoCF = '" + rs.Rows[i]["NoCF"].ToString() + "' AND SN = " + Convert.ToInt32(rs.Rows[i]["SN_NoCF"]));

                cb = new CheckBox();
                cb.ID = "cb_" + index;
                cb.Attributes["title"] = rs.Rows[i]["NoCF"] + ";" + rs.Rows[i]["SN_NoCF"] + ";" + NoAgent;

                c = new HtmlTableCell();
                c.Controls.Add(cb);
                r.Cells.Add(c);                

                c = new HtmlTableCell();
                c.InnerHtml = NamaPenerima;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoKontrak"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Db.SingleString("SELECT ISNULL(Nama,'') FROM MS_AGENT WHERE NoAgent = '" + NoAgent + "'");
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NamaSkema"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["Nilai"]));
                c.Attributes["class"] = "right";
                r.Cells.Add(c);

                tb6 = new TextBox();
                tb6.ID = "pph_" + i;
                tb6.Text = "0";
                tb6.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                tb6.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                //tb6.Attributes["onblur"] = "CalcBlur(this);hitungaja('" + i + "');";
                tb6.Width = 100;
                tb6.CssClass = "txt_num";
                Js.NumberFormat(tb6);

                c = new HtmlTableCell();
                c.Controls.Add(tb6);
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
                string NoCFP = Request.QueryString["id"].ToString(); //No. Pengajuan
                DateTime Tgl = Convert.ToDateTime(tgl.Text);
                string CFRID = LibKom.CFRID(Tgl.Month, Tgl.Year, Project);
                int SalesTipe = Db.SingleInteger("select SalesTipe from MS_KOMISI_CFP where NoCFP = '" + NoCFP + "'");

                Db.Execute("EXEC spKomisiCFRDaftar"
                    + " '" + CFRID + "'"
                    + ",'" + Tgl + "'"
                    + ",'" + Request.QueryString["id"] + "'"
                    + ",'" + Cf.Str(ket.Text) + "'"
                    );

                Db.Execute("UPDATE MS_KOMISI_CFR SET Project = '" + Project + "', SalesTipe = '" + SalesTipe + "'"
                    + " WHERE NoCFR = '" + CFRID + "'");

                int index = 0;
                foreach (Control tr in list.Controls)
                {
                    CheckBox cb = (CheckBox)list.FindControl("cb_" + index);

                    string[] aa = cb.Attributes["title"].Split(';');

                    if (cb.Checked)
                    {
                        DataTable dd = Db.Rs("SELECT * FROM MS_KOMISI_CFP_DETAIL WHERE NoCF = '" + aa[0] + "' AND SN_NoCF = " + Convert.ToInt32(aa[1]));
                        if (dd != null)
                        {
                            TextBox pph = (TextBox)list.FindControl("pph_" + index);
                            string NilaiPPH = Cf.Str(pph.Text);

                            Db.Execute("EXEC spKomisiCFRDetil"
                                + " '" + CFRID + "'"
                                + ",'" + dd.Rows[0]["NoCF"].ToString() + "'"
                                + ", " + Convert.ToInt32(dd.Rows[0]["SN_NoCF"])
                                + ", " + Convert.ToDecimal(dd.Rows[0]["Nilai"])
                                + ", '" + Convert.ToInt32(dd.Rows[0]["NoAgent"]) + "'"
                                + ", '" + Convert.ToString(dd.Rows[0]["NamaAgent"]) + "'"
                                + ", '" + NoCFP + "'"
                                + ", '" + Convert.ToDecimal(NilaiPPH) + "'"
                                );

                            Db.Execute("UPDATE MS_KOMISI_CFR_DETAIL SET NoAgent = '" + Convert.ToInt32(aa[2]) + "' WHERE NoCFR = '" + CFRID + "' AND SN_NoCF = " + Convert.ToInt32(dd.Rows[0]["SN_NoCF"]));
                        }
                    }

                    index++;
                }

                //ini untuk update ke Pengajuannya.
                int countCFP = Db.SingleInteger("select count(*) from MS_KOMISI_CFP_DETAIL where NoCFP = '" + NoCFP + "'");
                int countCFR = Db.SingleInteger("select count(*) from MS_KOMISI_CFR_DETAIL where NoCFP = '" + NoCFP + "'");

                if(countCFR == countCFP)
                {
                    Db.Execute("UPDATE MS_KOMISI_CFP SET Realisasi = 1 WHERE NoCFP = '" + NoCFP + "'");
                }

                DataTable rsHeader = Db.Rs("SELECT "
                    + " NoCFR"
                    + ",CONVERT(varchar,Tgl,106) AS [Tgl. Realisasi]"
                    + ",(select Tipe from REF_AGENT_TIPE where REF_AGENT_TIPE.ID = MS_KOMISI_CFR.SalesTipe) AS [Tipe]"
                    + ",Ket AS [Keterangan]"
                    + ",NoCFP AS [Kode Pengajuan]"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_CFR "
                    + " WHERE NoCFR = '" + CFRID + "'");

                DataTable rsDetail = Db.Rs("SELECT "
                    + " CONVERT(VARCHAR, SN) "
                    + " + '.  ' + (SELECT NamaAgent FROM MS_KOMISI_CF_DETAIL WHERE NoCF = a.NoCF AND SN = a.SN_NoCF)"
                    + " + '  ' + CONVERT(VARCHAR, Nilai, 1) "
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_CFR_DETAIL a WHERE NoCFR = '" + CFRID + "'");

                string Ket = Cf.LogCapture(rsHeader)
                    + Cf.LogList(rsDetail, "DETAIL");

                Db.Execute("EXEC spLogKomisiCFR"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + CFRID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_CFR_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_KOMISI_CFR_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("CFRRegis1.aspx?id="+CFRID);
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

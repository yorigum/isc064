using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KomisiEdit : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();
            }

            FeedBack();

            FillTable();
            Js.Confirm(this, "Lanjutkan proses edit komisi?");
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit Jadwal Komisi Berhasil...";
            }
        }

        private void Fill()
        {
            //barunilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            //barunilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            //barunilai.Attributes["onblur"] = "CalcBlur(this);";

            cancel.Attributes["onclick"] = "location.href='KontrakJadwalKomisi.aspx?NoKontrak=" + NoKontrak + "'";

            string strSql = "SELECT "
                + " MS_KONTRAK.*"
                + ",MS_KOMISI.NamaSkema"
                + ",MS_CUSTOMER.Nama AS Cs"
                + ",MS_AGENT.Nama + ' ' + MS_AGENT.Principal AS Ag"
                + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer INNER JOIN MS_KOMISI ON MS_KONTRAK.NoKontrak = MS_KOMISI.NoKontrak"
                + " INNER JOIN MS_AGENT ON MS_KONTRAK.NoAgent = MS_AGENT.NoAgent"
                + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'";

            DataTable rsHeader = Db.Rs(strSql);

            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nokontrak.Text = rsHeader.Rows[0]["NoKontrak"].ToString();
                unit.Text = rsHeader.Rows[0]["NoUnit"].ToString();
                customer.Text = rsHeader.Rows[0]["Cs"].ToString();
                agent.Text = rsHeader.Rows[0]["Ag"].ToString();
                nilai.Text = Cf.Num(rsHeader.Rows[0]["NilaiDPP"]);
                skema.Text = rsHeader.Rows[0]["NamaSkema"].ToString();
                persenlunas.Text = Cf.Num(rsHeader.Rows[0]["PersenLunas"]) + "%";
            }
        }

        private void FillTable()
        {
            list.Controls.Clear();

            decimal persenlunas = Db.SingleDecimal("SELECT PersenLunas FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            decimal persenbf = Db.SingleDecimal("select (NilaiTagihan / NilaiPelunasan * 100) as a from ms_tagihan a join ms_pelunasan b on a.nokontrak = b.nokontrak where a.Tipe = 'BF' and a.nokontrak = '" + NoKontrak + "'");
            decimal persendp = Db.SingleDecimal("select (NilaiTagihan / NilaiPelunasan * 100) as a from ms_tagihan a join ms_pelunasan b on a.nokontrak = b.nokontrak where a.Tipe = 'DP' and a.nokontrak = '" + NoKontrak + "'");
            decimal persenang = Db.SingleDecimal("select (NilaiTagihan / NilaiPelunasan * 100) as a from ms_tagihan a join ms_pelunasan b on a.nokontrak = b.nokontrak where a.Tipe = 'ANG' and a.nokontrak = '" + NoKontrak + "'");
            string ppjb = Db.SingleString("SELECT PPJB FROM MS_PPJB WHERE NoKontrak = '" + NoKontrak + "'");
            string ajb = Db.SingleString("SELECT AJB FROM MS_AJB WHERE NoKontrak = '" + NoKontrak + "'");
            string akad = Db.SingleString("SELECT NoAkad FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            rs = Db.Rs("SELECT * FROM MS_KOMISI_TERM WHERE NoKomisi = '" + NoKomisi + "'");
            Rpt.NoData(list, rs, "Tidak ada komisi untuk kontrak tersebut.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                //No
                Label l;
                TextBox bx;
                RadioButton rb;

                l = new Label();
                l.Text = "<tr>"
                    + "<td>" + rs.Rows[i]["Nama"] + ".</td>";
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                //tipe
                l = new Label();
                l.ID = "tipe_" + i;
                string Tipe = Db.SingleString("SELECT Tipe FROM REF_AGENT_TIPE WHERE ID = (SELECT SalesTipe FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoAgent"] + "')");
                l.Text = Tipe;
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                //tipe
                l = new Label();
                l.ID = "namaag_" + i;
                l.Text = rs.Rows[i]["NamaAgent"].ToString();
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                //Nilai Penerima

                bx = new TextBox();
                bx.ID = "nilaibayar_" + i;
                bx.Width = 140;
                bx.CssClass = "txt";
                bx.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiCair"])));
                int b = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISIR_DETAIL WHERE NoKomisi = '" + NoKomisi + "' AND SN_KomisiTermin = '" + rs.Rows[i]["SN"] + "'");
                bx.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                bx.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                bx.Attributes["onblur"] = "CalcBlur(this);";
                if (b > 0) bx.Enabled = false;
                bx.MaxLength = 50;
                bx.Font.Size = 8;
                list.Controls.Add(bx);

                //Nilai Bayar
                l = new Label();
                l.Text = "</td><td>";
                list.Controls.Add(l);

                l = new Label();

                decimal NilaiBayar = 0;
                int count = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISIR_DETAIL WHERE NoKomisi = '" + NoKomisi + "' AND SN_KomisiTermin = '" + rs.Rows[i]["SN"] + "'");
                if (count > 0)
                {
                    NilaiBayar = Db.SingleDecimal("SELECT ISNULL(Nilai,0) FROM MS_KOMISIR_DETAIL a WHERE a.NoKomisi = '" + NoKomisi + "' AND a.NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                }

                if (rs.Rows[i]["TipeCair"].ToString() == "0")
                {
                    if ((persenlunas >= Convert.ToDecimal(rs.Rows[i]["PersenLunas"]) && Convert.ToDecimal(rs.Rows[i]["Lunas"]) == 1) && (persenbf >= Convert.ToDecimal(rs.Rows[i]["PersenBF"]) && Convert.ToDecimal(rs.Rows[i]["BF"]) == 1) && (persendp >= Convert.ToDecimal(rs.Rows[i]["PersenDP"]) && Convert.ToDecimal(rs.Rows[i]["DP"]) == 1) && (persenang >= Convert.ToDecimal(rs.Rows[i]["PersenANG"]) && Convert.ToDecimal(rs.Rows[i]["ANG"]) == 1) && (ppjb != "B" && Convert.ToDecimal(rs.Rows[i]["PPJB"]) == 1) && (ajb != "B" && Convert.ToDecimal(rs.Rows[i]["AJB"]) == 1) && (akad != "" && Convert.ToDecimal(rs.Rows[i]["AKAD"]) == 1))
                        l.Text = "Rp. " + Cf.Num(NilaiBayar);
                    else
                        l.Text = "Rp. 0";
                }
                else
                {
                    if ((persenlunas >= Convert.ToDecimal(rs.Rows[i]["PersenLunas"]) && Convert.ToDecimal(rs.Rows[i]["Lunas"]) == 1) || (persenbf >= Convert.ToDecimal(rs.Rows[i]["PersenBF"]) && Convert.ToDecimal(rs.Rows[i]["BF"]) == 1) || (persendp >= Convert.ToDecimal(rs.Rows[i]["PersenDP"]) && Convert.ToDecimal(rs.Rows[i]["DP"]) == 1) || (persenang >= Convert.ToDecimal(rs.Rows[i]["PersenANG"]) && Convert.ToDecimal(rs.Rows[i]["ANG"]) == 1) || (ppjb != "B" && Convert.ToDecimal(rs.Rows[i]["PPJB"]) == 1) || (ajb != "B" && Convert.ToDecimal(rs.Rows[i]["AJB"]) == 1) || (akad != "" && Convert.ToDecimal(rs.Rows[i]["AKAD"]) == 1))
                        l.Text = "Rp. " + Cf.Num(NilaiBayar);
                    else
                        l.Text = "Rp. 0";
                }
                list.Controls.Add(l);

                l = new Label();
                l.Text = "</td><td>";
                l.ID = "err_ " + i;

                list.Controls.Add(l);

            }
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TextBox nilaibayar = (TextBox)list.FindControl("nilaibayar_" + i);
                Label err = (Label)list.FindControl("err_" + i);
                if (Cf.isEmpty(nilaibayar))
                {
                    x = false;
                    if (s == "") s = nilaibayar.ID;
                }

                if (!Cf.isMoney(nilaibayar))
                {
                    x = false;
                    if (s == "") s = nilaibayar.ID;
                }

                decimal komisi = Db.SingleDecimal("SELECT NilaiCair From MS_KOMISI_TERM WHERE NoKomisi = '" + NoKomisi + "' AND SN = '" + rs.Rows[i]["SN"] + "'");
                if (Convert.ToDecimal(nilaibayar.Text) > Convert.ToDecimal(Math.Round(komisi)))
                {
                    x = false;
                }
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "1. Nilai harus diisi.\\n"
                    + "2. Nilai Tidak Boleh Melebihi Batas Komisi.\\n"
                    , " document.getElementById('" + s + "').focus();"
                    + " document.getElementById('" + s + "').select();"
                    );
            }

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                DataTable rsBef = Db.Rs("SELECT * "
                    //+ "CONVERT(VARCHAR,Baris) + '.  ('+Tipe+') ' + CONVERT(VARCHAR,NilaiBayar,1) "
                    + "FROM MS_KOMISI_TERM WHERE NoKomisi ='" + NoKomisi + "'");

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    TextBox nilaibayar = (TextBox)list.FindControl("nilaibayar_" + i);
                    decimal NB1 = Convert.ToDecimal(nilaibayar.Text);

                    //Db.Execute("EXEC spKomisiEditDetail "
                    //    + " '" + NoKontrak + "'"
                    //    + ", " + NB1
                    //    );
                    Db.Execute("UPDATE MS_KOMISI_TERM SET NilaiCair = '" + NB1 + "' WHERE NoKomisi = '" + NoKomisi + "' AND SN = '" + rs.Rows[i]["SN"] + "'");

                    decimal total = Db.SingleDecimal("SELECT SUM(ISNULL(NilaiCair,0)) FROM MS_KOMISI_TERM WHERE NoKomisi = '" + NoKomisi + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");

                    Db.Execute("UPDATE MS_KOMISI_DETAIL SET Nilai = '" + total + "' WHERE NoKomisi = '" + NoKomisi + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                }

                DataTable rsAft = Db.Rs("SELECT * "
                    //+ "CONVERT(VARCHAR,Baris) + '.  ('+Tipe+') ' + CONVERT(VARCHAR,NilaiBayar,1) "
                    + "FROM MS_KOMISI_DETAIL WHERE NoKomisi = '" + NoKomisi + "'");

                DataTable rsDetail = Db.Rs("SELECT"
                    + " MS_KONTRAK.NoKontrak AS [No. Kontrak]"
                    + ",MS_KONTRAK.NoUnit AS [Unit]"
                    + ",MS_CUSTOMER.Nama AS [Customer]"
                    + ",MS_KONTRAK.NilaiKontrak AS [Nilai Kontrak]"
                    + ",MS_KONTRAK.SkemaKomisi AS [Skema Komisi]"
                    + " FROM MS_KONTRAK INNER JOIN MS_CUSTOMER"
                    + " ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer"
                    + " WHERE MS_KONTRAK.NoKontrak = '" + NoKontrak + "'");

                string Ket = Cf.LogCapture(rsDetail)
                    + "<br>---EDIT KOMISI---<br>"
                    + Cf.LogList(rsBef, rsAft, "KOMISI");

                Db.Execute("EXEC spLogKontrak"
                    + " 'EJK'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoKontrak + "'"
                    );

                return true;
            }
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("KontrakJadwalKomisi.aspx?NoKontrak=" + NoKontrak + "&done=1");
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("KomisiEdit.aspx?NoKontrak=" + NoKontrak + "&done=1");
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }

        private string NoKomisi
        {
            get
            {
                return Db.SingleString("SELECT NoKomisi FROM MS_KOMISI WHERE NoKontrak = '" + NoKontrak + "'");
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

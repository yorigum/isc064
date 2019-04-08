using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.MARKETINGJUAL
{
    public partial class KontrakJadwalKomisi : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            int No = Db.SingleInteger("Select ISNULL(Count(NoKontrak),0) From MS_KOMISI where NoKontrak ='" + NoKontrak + "'");
            int Nocf = Db.SingleInteger("Select ISNULL(Count(NoKontrak),0) From MS_KOMISI_CF where NoKontrak ='" + NoKontrak + "'");
            if (No > 0 || Nocf > 0)
            {
                Fill();
            }
            else
            {
                NoData.Visible = true;
                datas.Visible = false;
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
                    if (Request.QueryString["done"] == "1")
                    {
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                            + "Edit Berhasil...";
                    }
                    else if (Request.QueryString["done"] == "2")
                    {
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                            + "Pembayaran Komisi Berhasil";
                    }
                    else if (Request.QueryString["done"] == "3")
                    {
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                            + "Solve Komisi Berhasil";
                    }
                }
            }
        }


        private void Fill()
        {
            edit.Attributes["onclick"] = "location.href='KomisiEdit.aspx?NoKontrak=" + NoKontrak + "'";
            data.Attributes["onclick"] = "location.href='KomisiEditDataOver.aspx?NoKontrak=" + NoKontrak + "'";
            solve.Attributes["onclick"] = "if(confirm('Lanjutkan proses solve komisi?'))"
                + "location.href='KomisiSolve.aspx?NoKontrak=" + NoKontrak + "'"
                ;

            string strSql = "SELECT "
                + " (SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = MS_KONTRAK.NoKontrak) AS TotalTagihan"
                + ",NilaiKontrak"
                + ",OutBalance"
                + ",Skema"
                + ",PersenLunas"
                + ",FlagKomisi"
                + ",PersenLunas"
                + ",NilaiDPP"
                + ",TglKontrak"
                + " FROM MS_KONTRAK"
                + " WHERE NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                nilai.Text = Cf.Num(rs.Rows[0]["NilaiKontrak"]);
                nilaidpp.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[0]["NilaiDPP"])));
                skema.Text = rs.Rows[0]["Skema"].ToString();
                persenlunas.Text = Cf.Num(rs.Rows[0]["PersenLunas"]) + "%";

                if (rs.Rows[0]["FlagKomisi"].ToString() != "2")
                    solve.Disabled = true;

                FillTop();
                FillTb();

                MidTB();
                FillCF(NoKontrak);
            }
        }


        protected void FillTop()
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = "Termin";
            c.Attributes["style"] = "Background:skyblue;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Jenis Termin";
            c.Attributes["style"] = "Background:skyblue;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Tipe";
            c.Attributes["style"] = "Background:skyblue;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Nama";
            c.Attributes["style"] = "Background:skyblue;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "NPWP";
            c.Attributes["style"] = "Background:skyblue;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Bank Account";
            c.Attributes["style"] = "Background:skyblue;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Gross Komisi (Rp)";
            c.Attributes["style"] = "Background:skyblue;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Nilai Bayar";
            c.Attributes["style"] = "Background:skyblue;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Status Komisi";
            c.Attributes["style"] = "Background:skyblue;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Tanggal Cair";
            c.Attributes["style"] = "Background:skyblue;";
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        protected void MidTB()
        {
            TableCell c;
            TableRow tr, tr2;

            tr = new TableRow();
            tr2 = new TableRow();

            c = new TableCell();
            c.Attributes["style"] = "Background:lightblue;text-align:center;";
            c.ColumnSpan = 5;
            c.Text = "Closing Fee";
            tr2.Cells.Add(c);

            c = new TableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.Text = "Nama";
            tr.Cells.Add(c);

            c = new TableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.Text = "Tipe";
            tr.Cells.Add(c);

            c = new TableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.Text = "Nilai Closing Fee";
            tr.Cells.Add(c);

            c = new TableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.Text = "Nilai Bayar";
            tr.Cells.Add(c);

            c = new TableCell();
            c.Attributes["style"] = "Background:lightblue;";
            c.Text = "Tanggal Cair";
            tr.Cells.Add(c);

            rpt2.Rows.Add(tr2);
            rpt2.Rows.Add(tr);
        }

        protected void FillCF(string NoKontrak)
        {
            string strSql1 = "select*,b.NoAgent as Ag from ms_komisi_cf a inner join ms_komisi_cf_detail b on a.NoCF = b.NoCF where b.Nilai > 0 and a.project = '" + Project + "' AND NoKontrak = '" + NoKontrak + "'";
            DataTable rs = Db.Rs(strSql1);
            Rpt.NoData(rpt2, rs, "Daftar komisi untuk kontrak tersebut masih kosong.");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                string Nama = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["Ag"].ToString() + "'");
                c.Text = Nama;
                r.Cells.Add(c);

                c = new TableCell();
                string TipeAgent = Db.SingleString("SELECT Tipe FROM REF_AGENT_TIPE WHERE ID = (SELECT SalesTipe FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoAgent"].ToString() + "')");
                c.Text = TipeAgent;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["Nilai"])));
                r.Cells.Add(c);

                int b = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_CFR WHERE NoCFP IN (SELECT NoCFP FROM MS_KOMISI_CFP_DETAIL WHERE NoCF = '" + rs.Rows[i]["NoCF"] + "' AND SN_NoCF = '" + rs.Rows[i]["SN"] + "')");

                c = new TableCell();
                if (b > 0)
                {
                    decimal Nilai = Db.SingleDecimal("SELECT Nilai FROM MS_KOMISI_CFR_DETAIL WHERE NoCF  = '" + rs.Rows[i]["NoCF"] + "' AND NoAgent = '" + rs.Rows[i]["Ag"] + "'");
                    c.Text = Cf.Num(Nilai);
                }
                r.Cells.Add(c);

                DateTime TglKontrak = Db.SingleTime("Select TglKontrak From MS_KONTRAK Where NoKontrak = '" + NoKontrak + "'");

                c = new TableCell();
                c.Text = "";
                if (b > 0)
                {
                    DateTime tgl = Db.SingleTime("SELECT Tgl FROM MS_KOMISI_CFR WHERE NoCFP IN (SELECT NoCFP FROM MS_KOMISI_CFP_DETAIL WHERE NoCF = '" + rs.Rows[i]["NoCF"] + "' AND SN_NoCF = '" + rs.Rows[i]["SN"] + "')");
                    c.Text = Cf.Day(tgl.ToString());
                }
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt2.Rows.Add(r);
            }
        }

        private void FillTb()
        {
            int noagent = Db.SingleInteger("Select NoAgent from ms_kontrak where nokontrak='" + NoKontrak + "'");
            string TipeAgent = Db.SingleString("SELECT Tipe FROM REF_AGENT_TIPE WHERE ID = (SELECT SalesTipe FROM MS_AGENT WHERE NoAgent = '" + noagent + "')");


            decimal persenlunas = Db.SingleDecimal("SELECT PersenLunas FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
            decimal persenbf = Db.SingleDecimal("select (NilaiTagihan / NilaiPelunasan * 100) as a from ms_tagihan a join ms_pelunasan b on a.nokontrak = b.nokontrak where a.Tipe = 'BF' and a.nokontrak = '" + NoKontrak + "'");
            decimal persendp = Db.SingleDecimal("select (NilaiTagihan / NilaiPelunasan * 100) as a from ms_tagihan a join ms_pelunasan b on a.nokontrak = b.nokontrak where a.Tipe = 'DP' and a.nokontrak = '" + NoKontrak + "'");
            decimal persenang = Db.SingleDecimal("select (NilaiTagihan / NilaiPelunasan * 100) as a from ms_tagihan a join ms_pelunasan b on a.nokontrak = b.nokontrak where a.Tipe = 'ANG' and a.nokontrak = '" + NoKontrak + "'");
            int ppjb1 = Db.SingleInteger("SELECT COUNT(*) FROM MS_PPJB WHERE NoKontrak = '" + NoKontrak + "'");
            int ajb1 = Db.SingleInteger("SELECT COUNT(*) FROM MS_AJB WHERE NoKontrak = '" + NoKontrak + "'");
            string ppjb = (ppjb1 > 0) ? Db.SingleString("SELECT PPJB FROM MS_PPJB WHERE NoKontrak = '" + NoKontrak + "'") : "B";
            string ajb = (ajb1 > 0) ? Db.SingleString("SELECT AJB FROM MS_AJB WHERE NoKontrak = '" + NoKontrak + "'") : "B";
            string akad = Db.SingleString("SELECT NoAkad FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");            

            //string strSql = "select *,b.NoAgent as Ag from ms_komisi a inner join ms_komisi_detail b on a.NoKomisi = b.NoKomisi where b.Nilai > 0 and a.project = '" + Project + "' AND NoKontrak = '" + NoKontrak + "'";
            string strSql = "SELECT *,a.NoAgent AS Ag,a.SN AS sn FROM MS_KOMISI_TERM a JOIN MS_KOMISI b ON a.NoKomisi = b.NoKomisi WHERE b.NoKontrak = '" + NoKontrak + "' AND b.Project = '" + Project + "'";
            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(rpt, rs, "Daftar komisi untuk kontrak tersebut masih kosong.");

            decimal t = 0, t2 = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaTermin"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Nama"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = TipeAgent;
                r.Cells.Add(c);

                string n = Db.SingleString("SELECT NPWP FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                string an = Db.SingleString("SELECT AtasNama FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                string bank = Db.SingleString("SELECT Rekening FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");
                string rek = Db.SingleString("SELECT RekBank FROM MS_AGENT WHERE NoAgent = '" + rs.Rows[i]["NoAgent"] + "'");

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaAgent"].ToString();
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = n;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = bank + " / " + an + " / " + rek;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Cf.Num(Math.Round(Convert.ToDecimal(rs.Rows[i]["NilaiCair"])));
                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                decimal NilaiBayar = 0;
                int count = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISIR_DETAIL WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"] + "' AND SN_KomisiTermin = '" + rs.Rows[i]["sn"] + "'");
                if (count > 0)
                {
                    NilaiBayar = Db.SingleDecimal("SELECT ISNULL(Nilai,0) FROM MS_KOMISIR_DETAIL a WHERE a.NoKomisi = '" + rs.Rows[i]["NoKomisi"] + "' AND NoAgent = '" + rs.Rows[i]["Ag"] + "'");
                }

                if (rs.Rows[i]["TipeCair"].ToString() == "0")
                {
                    if ((persenlunas >= Convert.ToDecimal(rs.Rows[i]["PersenLunas"]) && Convert.ToDecimal(rs.Rows[i]["Lunas"]) == 1) && (persenbf >= Convert.ToDecimal(rs.Rows[i]["PersenBF"]) && Convert.ToDecimal(rs.Rows[i]["BF"]) == 1) && (persendp >= Convert.ToDecimal(rs.Rows[i]["PersenDP"]) && Convert.ToDecimal(rs.Rows[i]["DP"]) == 1) && (persenang >= Convert.ToDecimal(rs.Rows[i]["PersenANG"]) && Convert.ToDecimal(rs.Rows[i]["ANG"]) == 1) && (ppjb != "B" && Convert.ToDecimal(rs.Rows[i]["PPJB"]) == 1) && (ajb != "B" && Convert.ToDecimal(rs.Rows[i]["AJB"]) == 1) && (akad != "" && Convert.ToDecimal(rs.Rows[i]["AKAD"]) == 1))
                        c.Text = Cf.Num(NilaiBayar);
                    else
                        c.Text = "0";
                }
                else
                {
                    if ((persenlunas >= Convert.ToDecimal(rs.Rows[i]["PersenLunas"]) && Convert.ToDecimal(rs.Rows[i]["Lunas"]) == 1) || (persenbf >= Convert.ToDecimal(rs.Rows[i]["PersenBF"]) && Convert.ToDecimal(rs.Rows[i]["BF"]) == 1) || (persendp >= Convert.ToDecimal(rs.Rows[i]["PersenDP"]) && Convert.ToDecimal(rs.Rows[i]["DP"]) == 1) || (persenang >= Convert.ToDecimal(rs.Rows[i]["PersenANG"]) && Convert.ToDecimal(rs.Rows[i]["ANG"]) == 1) || (ppjb != "B" && Convert.ToDecimal(rs.Rows[i]["PPJB"]) == 1) || (ajb != "B" && Convert.ToDecimal(rs.Rows[i]["AJB"]) == 1) || (akad != "" && Convert.ToDecimal(rs.Rows[i]["AKAD"]) == 1))
                        c.Text = Cf.Num(NilaiBayar);
                    else
                        c.Text = "0";
                }

                c.HorizontalAlign = HorizontalAlign.Right;
                r.Cells.Add(c);

                c = new TableCell();
                int a = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISIP_DETAIL WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"] + "' AND SN_KomisiTermin = '" + rs.Rows[i]["sn"] + "'");
                int b = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISIR_DETAIL WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"] + "' AND SN_KomisiTermin = '" + rs.Rows[i]["sn"] + "'");
                if (a == 0 && b == 0)
                    c.Text = "Belum Pengajuan";
                else if (a > 0 && b == 0)
                    c.Text = "Pengajuan";
                else if (a > 0 && b > 0)
                    c.Text = "Realisasi";

                r.Cells.Add(c);

                c = new TableCell();
                c.Text = "";
                if (b > 0)
                {
                    DateTime tgl = Db.SingleTime("SELECT Tgl FROM MS_KOMISIR WHERE NoKomisiR IN (SELECT NoKomisiR FROM MS_KOMISIR_DETAIL WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"] + "' AND NoAgent = '" + rs.Rows[i]["Ag"] + "')");
                    c.Text = Cf.Day(tgl);
                }
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
            }
        }

        private void SubTotal(decimal t, decimal t2)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.ColumnSpan = 4;
            c.Text = "<b>GRAND TOTAL</b>";
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.ColumnSpan = 2;
            c.Text = Cf.Num(t);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = new TableCell();
            c.Font.Bold = true;
            c.Text = Cf.Num(t2);
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
            }
        }

        private string NoUrut
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoUrut"]);
            }
        }

        private string Baris
        {
            get
            {
                return Cf.Pk(Request.QueryString["Baris"]);
            }
        }

        private string Project
        {
            get
            {
                return Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
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

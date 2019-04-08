using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KOMISI.Laporan
{

    public partial class MasterKomisiDetail : System.Web.UI.Page
{
        private string StatusS { get { return (Request.QueryString["status_s"]); } }
        private string StatusA { get { return (Request.QueryString["status_a"]); } }
        private string StatusB { get { return (Request.QueryString["status_b"]); } }
        private string UserID { get { return (Request.QueryString["userid"]); } }
        private string Project { get { return (Request.QueryString["project"]); } }
        private string Perusahaan { get { return (Request.QueryString["pers"]); } }
        private string AttachmentID { get { return (Request.QueryString["id"]); } }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Report();
        }

        protected int UserAgent()
        {
            int a = Db.SingleInteger("SELECT NoAgent FROM " + Mi.DbPrefix + "SECURITY..USERNAME WHERE UserID = '" + UserID + "'");

            return a;
        }

        private void Report()
        {
            rpt.Visible = true;

            Header();
            MenuAtas();
            Fill();
        }

        private void Header()
        {
            System.Text.StringBuilder x = new System.Text.StringBuilder();

            //Rpt.Judul(x, comp, judul);

            if (StatusA != "")
                Rpt.SubJudul(x, "Status : " + StatusA);
            else if (StatusB != "")
                Rpt.SubJudul(x, "Status : " + StatusB);
            else
                Rpt.SubJudul(x, "Status : " + StatusS);

            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID=" + AttachmentID + "");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID=" + AttachmentID + "");

            Rpt.SubJudul(x
                , "Tanggal Kontrak" + " : " + Cf.Day(Dari) + " s/d " + Cf.Day(Sampai)
                );

            Rpt.SubJudul(x
                , "Project : " + Project
                );

            string pers = (Perusahaan == "SEMUA") ? "SEMUA" : Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + Project + "'");
            Rpt.SubJudul(x
                , "Perusahaan : " + pers
                );

            string legend = "Status: A = Aktif / B = Batal.";
            Rpt.HeaderReport(headReport, legend, x);
        }

        private void MenuAtas()
        {
            TableRow r = new TableRow();
            TableRow r2 = new TableRow();
            TableRow r3 = new TableRow();
            TableRow r4 = new TableRow();
            TableCell c = new TableCell();
            TableCell c2 = new TableCell();
            TableCell c3 = new TableCell();
            TableCell c4 = new TableCell();

            //c = new TableCell();
            //c.Text = "No";
            //c.ForeColor = Color.White;
            //c.Attributes["style"] = "background-color:#1E90FF";
            //c.HorizontalAlign = HorizontalAlign.Left;
            ////c.RowSpan = 3;
            //r.Cells.Add(c);

            c = new TableCell();
            c.Text = "No Kontrak";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            //c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "No Unit";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            //c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Agent";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            //c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Termin";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Nilai";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            //c.RowSpan = 3;
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "Status";
            c.ForeColor = Color.White;
            c.Attributes["style"] = "background-color:#1E90FF";
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        private void Fill()
        {
            string Status = "";
            if (StatusA != "") Status = " AND A.Status = 'A'";
            if (StatusB != "") Status = " AND A.Status = 'B'";

            string tgl = "";
            string order = "";

            tgl = "A.TglKontrak";
            order = ",A.NoKontrak";


            DateTime Dari = Db.SingleTime("SELECT FilterDari FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");
            DateTime Sampai = Db.SingleTime("SELECT FilterSampai FROM LapPDF WHERE AttachmentID='" + AttachmentID + "'");

            string nProject = "";
            if (Project != "SEMUA") nProject = " AND A.Project = '" + Project + "'";
            string nPerusahaan = "";
            if (Perusahaan != "SEMUA") nPerusahaan = " AND A.Pers = '" + Perusahaan + "'";

            int index = 1;

            int no = 1;
            decimal t1 = 0;

            string strSql = "SELECT b.*,d.* "
                + ",A.NoKontrak"
                + ",A.NoUnit"
                + ",C.Nilai"
                + ",B.NamaAgent"
                + ",A.Status"
                + ",A.PersenLunas"
                //+ ",C.Nama as Customer"
                + ",A.NoAgent"
                + " FROM MS_KONTRAK A INNER JOIN MS_KOMISI B ON A.NoKontrak = B.NoKontrak"
                + " INNER JOIN MS_KOMISI_DETAIL C ON B.NoKomisi = C.NoKomisi"
                + " INNER JOIN MS_KOMISI_TERM D ON C.NoKomisi = D.NoKomisi"
                //+ " WHERE A.NoAgent= '" + sr.Rows[g]["NoAgent"] + "'"
                //+ " AND A.FlagKomisi = '1'"
                + Status
                + " AND CONVERT(varchar,A.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,A.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'"
                //+ Agent
                + nProject
                + nPerusahaan
                + " ORDER BY B.NamaAgent"
                + order;

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;
                TableRow r2a;
                TableHeaderCell th2;
                Table tb;

                r.VerticalAlign = VerticalAlign.Top;
                //r.Attributes["ondblclick"] = "popJadwalKomisi('" + rs.Rows[i]["NoKontrak"] + "')";

                //nambah no default
                //c = new TableCell();
                //c.Text = (no).ToString();
                ////c.RowSpan = 4;
                //c.HorizontalAlign = HorizontalAlign.Left;
                //r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                string NoKontrak = rs.Rows[i]["NoKontrak"].ToString();
                //c.RowSpan = 4;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                //c.RowSpan = 4;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaAgent"].ToString();
                //c.RowSpan = 4;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                //SubTotal("GRAND TOTAL", t1);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaTermin"].ToString();
                //c.RowSpan = 4;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                //c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["Nilai"]));
                decimal NKom = (Convert.ToDecimal(rs.Rows[i]["Nilai"])); //Db.SingleDecimal("Select Nilai From MS_KOMISI_DETAIL Where NoKontrak ='" + rs.Rows[i]["NoKontrak"].ToString() + "'");
                c.Text = Cf.Num(Math.Round(NKom));
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                //cek syarat cair=================================================================
                string bf = "SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' AND Tipe = 'BF'";
                decimal NilaiBF = Db.SingleDecimal(bf);

                string bbf = "SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN a INNER JOIN MS_TAGIHAN b ON a.NoKontrak = b.NoKontrak WHERE a.NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' AND a.NoTagihan = b.NoUrut AND b.Tipe = 'BF'";
                decimal BayarBF = Db.SingleDecimal(bbf);
                decimal PersenBF = NilaiBF != 0 ? BayarBF / NilaiBF * 100 : 0;

                string dp = "SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' AND Tipe = 'DP'";
                decimal NilaiDP = Db.SingleDecimal(dp);

                string bdp = "SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN a INNER JOIN MS_TAGIHAN b ON a.NoKontrak = b.NoKontrak WHERE a.NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' AND a.NoTagihan = b.NoUrut AND b.Tipe = 'DP'";
                decimal BayarDP = Db.SingleDecimal(bdp);
                decimal PersenDP = NilaiDP != 0 ? BayarDP / NilaiDP * 100 : 0;

                string ang = "SELECT ISNULL(SUM(NilaiTagihan),0) FROM MS_TAGIHAN WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' AND Tipe = 'ANG'";
                decimal NilaiANG = Db.SingleDecimal(ang);

                string bang = "SELECT ISNULL(SUM(NilaiPelunasan),0) FROM MS_PELUNASAN a INNER JOIN MS_TAGIHAN b ON a.NoKontrak = b.NoKontrak WHERE a.NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "' AND a.NoTagihan = b.NoUrut AND b.Tipe = 'ANG'";
                decimal BayarANG = Db.SingleDecimal(bang);
                decimal PersenANG = NilaiANG != 0 ? BayarANG / NilaiANG * 100 : 0;

                decimal PersenLunas = 0;
                bool PPJB = false, AJB = false, AKAD = false;

                string kon = "SELECT PersenLunas, PPJB, AJB, StatusAkad FROM MS_KONTRAK WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "'";
                DataTable rkon = Db.Rs(kon);
                if (kon != null)
                {
                    PersenLunas = Convert.ToDecimal(rkon.Rows[0]["PersenLunas"]);
                    PPJB = rkon.Rows[0]["PPJB"].ToString() != "B" ? true : false;
                    AJB = rkon.Rows[0]["AJB"].ToString() == "D" ? true : false;
                    AKAD = rkon.Rows[0]["StatusAkad"].ToString() == "SELESAI" ? true : false;
                }

                bool pengajuan = false;
                bool Lunas = Convert.ToBoolean(rs.Rows[i]["Lunas"]);
                bool BF = Convert.ToBoolean(rs.Rows[i]["BF"]);
                bool DP = Convert.ToBoolean(rs.Rows[i]["DP"]);
                bool ANG = Convert.ToBoolean(rs.Rows[i]["ANG"]);
                bool PPJB_ = Convert.ToBoolean(rs.Rows[i]["PPJB"]);
                bool AJB_ = Convert.ToBoolean(rs.Rows[i]["AJB"]);
                bool AKAD_ = Convert.ToBoolean(rs.Rows[i]["AKAD"]);
                int a = 0, b = 0;
                if (!Lunas && !BF && !DP && !ANG && !PPJB_ && !AJB_ && !AKAD_)
                {
                    pengajuan = true;
                }
                else
                {
                    //Salah satu
                    if (Convert.ToInt32(rs.Rows[i]["TipeCair"]) == 1)
                    {
                        if ((Lunas && PersenLunas >= Convert.ToDecimal(rs.Rows[i]["PersenLunas"])) || (BF && PersenBF >= Convert.ToDecimal(rs.Rows[i]["PersenBF"])) || (DP && PersenDP >= Convert.ToDecimal(rs.Rows[i]["PersenDP"])) || (ANG && PersenANG >= Convert.ToDecimal(rs.Rows[i]["PersenANG"])) || (PPJB_ && PPJB) || (AJB_ && AJB) || (AKAD_ && AKAD))
                        {
                            pengajuan = true;
                        }
                    }
                    //Semua
                    else
                    {
                        if (Lunas)
                        {
                            a++;
                            if (PersenLunas >= Convert.ToDecimal(rs.Rows[i]["PersenLunas"]))
                            {
                                b++;
                            }
                        }
                        if (BF)
                        {
                            a++;
                            if (PersenBF >= Convert.ToDecimal(rs.Rows[i]["PersenBF"]))
                            {
                                b++;
                            }
                        }
                        if (DP)
                        {
                            a++;
                            if (PersenDP >= Convert.ToDecimal(rs.Rows[i]["PersenDP"]))
                            {
                                b++;
                            }
                        }
                        if (ANG)
                        {
                            a++;
                            if (PersenANG >= Convert.ToDecimal(rs.Rows[i]["PersenANG"]))
                            {
                                b++;
                            }
                        }
                        if (PPJB_)
                        {
                            a++;
                            if (PPJB)
                            {
                                b++;
                            }
                        }
                        if (AJB_)
                        {
                            a++;
                            if (AJB)
                            {
                                b++;
                            }
                        }
                        if (AKAD_)
                        {
                            a++;
                            if (AKAD)
                            {
                                b++;
                            }
                        }

                        if (a == b)
                        {
                            pengajuan = true;
                        }
                    }
                }
                //======================================================================================

                string StatusKom = "<label style='color:red;'>Belum Bisa Pengajuan</label>", NoRef = "";
                if (pengajuan)
                {
                    StatusKom = "<label style='color:yellow;'>Siap Cair</label>";
                }
                DataTable kp = Db.Rs("SELECT * FROM MS_KOMISIP_DETAIL WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"].ToString() + "' AND SN_KomisiTermin = " + Convert.ToInt32(rs.Rows[i]["SN"]));
                if (kp.Rows.Count > 0)
                {
                    NoRef = kp.Rows[0]["NoKomisiP"].ToString();
                    StatusKom = "<label style='color:green;'>Pengajuan</label>";

                    DataTable kr = Db.Rs("SELECT * FROM MS_KOMISIR_DETAIL WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"].ToString() + "' AND SN_KomisiTermin = " + Convert.ToInt32(rs.Rows[i]["SN"]));
                    if (kr.Rows.Count > 0)
                    {
                        NoRef = kr.Rows[0]["NoKomisiR"].ToString();
                        StatusKom = "<label style='color:blue;'>Cair</label>";
                    }
                }

                c = new TableCell();
                c.Text = StatusKom;
                r.Cells.Add(c);

                rpt.Rows.Add(r);
                no++;
                t1 += NKom;
                //termin(NoKontrak);
            }

            SubTotal("GRAND TOTAL", t1);
        }
        private void SubTotal(string txt, decimal t1)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = Rpt.Foot();
            c.Text = txt;
            c.ColumnSpan = 4;
            c.HorizontalAlign = HorizontalAlign.Right;
            r.Cells.Add(c);

            c = Rpt.Foot();
            c.Text = Cf.Num(t1);
            c.HorizontalAlign = HorizontalAlign.Left;
            r.Cells.Add(c);

            c = Rpt.Foot();
            r.Cells.Add(c);

            rpt.Rows.Add(r);
        }

        protected string TipeReff(string NoKontrak, string RefEm, string RefCust)
        {
            string Tipe = "";
            if (RefEm != "" && RefCust == "")
            {
                Tipe = "EMPLOYEE";
            }
            else if (RefEm == "" && RefCust != "")
            {
                Tipe = "BUYER";
            }

            return Tipe;
        }

        protected string NamaReff(string RefEm, string RefCust)
        {
            string Nama = "";
            if (RefEm != "" && RefCust == "")
            {
                Nama = Db.SingleString("SELECT Nama FROM MS_AGENT WHERE NoAgent = '" + RefEm + "'");
            }
            else if (RefEm == "" && RefCust != "")
            {
                Nama = Db.SingleString("SELECT Nama FROM MS_CUSTOMER WHERE NoCustomer = '" + RefCust + "'");
            }

            return Nama;
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
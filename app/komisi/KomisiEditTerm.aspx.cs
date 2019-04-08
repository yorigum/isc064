using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ISC064.KOMISI
{
    public partial class KomisiEditTerm : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                FillTable();
            }
        }
        private void FillTable()
        {
            DataTable rs = Db.Rs("SELECT a.*, b.NoKontrak, b.NoUnit, b.NamaAgent, b.NamaCust FROM MS_KOMISI_TERM a"
                + " INNER JOIN MS_KOMISI b ON a.NoKomisi = b.NoKomisi"
                + " WHERE a.NoKomisi = '" + Nomor + "'"
                + " ORDER BY a.NoAgent, a.SN"
                );
            if (rs.Rows.Count > 0)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    TableCell c;
                    TableRow tr;

                    tr = new TableRow();

                    c = new TableCell();
                    c.Text = (i + 1).ToString() + ".";
                    tr.Cells.Add(c);

                    string NamaAgent = Db.SingleString("SELECT ISNULL(NamaAgent,'') FROM MS_KOMISI_DETAIL WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"].ToString() + "' AND NoAgent = '" + rs.Rows[i]["NoAgent"].ToString() + "'");

                    c = new TableCell();
                    c.Text = NamaAgent;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Nama"].ToString();
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiCair"]));
                    c.CssClass = "num";
                    tr.Cells.Add(c);

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

                    string Status = "<label style='color:red;'>Belum Bisa Pengajuan</label>", NoRef = "";
                    if (pengajuan)
                    {
                        Status = "<label style='color:yellow;'>Siap Cair</label>";
                    }
                    DataTable kp = Db.Rs("SELECT * FROM MS_KOMISIP_DETAIL WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"].ToString() + "' AND SN_KomisiTermin = " + Convert.ToInt32(rs.Rows[i]["SN"]));
                    if (kp.Rows.Count > 0)
                    {
                        NoRef = kp.Rows[0]["NoKomisiP"].ToString();
                        Status = "<label style='color:green;'>Pengajuan</label>";

                        DataTable kr = Db.Rs("SELECT * FROM MS_KOMISIR_DETAIL WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"].ToString() + "' AND SN_KomisiTermin = " + Convert.ToInt32(rs.Rows[i]["SN"]));
                        if (kr.Rows.Count > 0)
                        {
                            NoRef = kr.Rows[0]["NoKomisiR"].ToString();
                            Status = "<label style='color:blue;'>Cair</label>";
                        }
                    }

                    c = new TableCell();
                    c.Text = Status;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = NoRef;
                    tr.Cells.Add(c);

                    tb.Rows.Add(tr);
                }
            }
        }
        private string Nomor
        {
            get
            {
                return Cf.Pk(Request.QueryString["Nomor"]);
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

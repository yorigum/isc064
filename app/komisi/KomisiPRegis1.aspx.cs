using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class KomisiPRegis1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Bind();
                Act.ProjectList(project);
            }

            if(IsPostBack)
                Fill();

        }

        private void Bind()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);
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

            string strSql = "SELECT a.*, b.NoKontrak, b.NoUnit, b.NamaCust"
                + " FROM MS_KOMISI_TERM a"
                + " INNER JOIN MS_KOMISI b ON a.NoKomisi = b.NoKomisi"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,b.Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,b.Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND (SELECT COUNT(*) FROM MS_KOMISIP_DETAIL WHERE NoKomisi = a.NoKomisi AND SN_KomisiTermin = a.SN) = 0"
                + " AND b.Project = '" + project.SelectedValue + "'"
                + " ORDER BY b.NoKomisi";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Tidak terdapat data dengan kriteria seperti tersebut diatas.");

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                //cek syarat cair
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

                        string CaraBayar = Db.SingleString("select CaraBayar from ms_kontrak where NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' and Project = '" + project.SelectedValue + "'");
                        if (CaraBayar == "KPR") //landed
                        {
                            if (AKAD_)
                            {
                                a++;
                                if (AKAD)
                                {
                                    b++;
                                }
                            }
                        }
                        else if (CaraBayar == "KPA") //apartmen
                        {
                            if (AKAD_)
                            {
                                a++;
                                if (AKAD)
                                {
                                    b++;
                                }
                            }
                        }

                        if (a == b)
                        {
                            pengajuan = true;
                        }
                    }
                }

                HtmlTableRow r = new HtmlTableRow();
                HtmlTableCell c;
                CheckBox cb;

                cb = new CheckBox();
                cb.ID = "cb_" + index;
                cb.Attributes["title"] = rs.Rows[i]["NoKomisi"] + ";" + rs.Rows[i]["SN"];
                cb.Enabled = pengajuan ? true : false;

                c = new HtmlTableCell();
                c.Controls.Add(cb);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoKomisi"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoKontrak"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NamaCust"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NamaAgent"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["NilaiCair"]));
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

            //Tanggal Pengajuan
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
        protected void display_Click(object sender, System.EventArgs e)
        {
            Fill();
        }
        protected void save_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                DateTime Tgl = Convert.ToDateTime(tgl.Text);
                string KPID = LibKom.KPID(Tgl.Month, Tgl.Year);
                string Project = Cf.Str(project.SelectedValue);

                Db.Execute("EXEC spKomisiPDaftar"
                    + " '" + KPID + "'"
                    + ",'" + Tgl + "'"
                    + ",'" + Cf.Str(ket.Text) + "'"
                    );

                Db.Execute("UPDATE MS_KOMISIP SET Project = '" + Project + "'"
                    + " WHERE NoKomisiP = '" + KPID + "'");

                int index = 0;
                foreach (Control tr in list.Controls)
                {
                    CheckBox cb = (CheckBox)list.FindControl("cb_" + index);

                    string[] aa = cb.Attributes["title"].Split(';');

                    if (cb.Checked)
                    {
                        DataTable dd = Db.Rs("SELECT * FROM MS_KOMISI_TERM WHERE NoKomisi = '" + aa[0] + "' AND SN = " + Convert.ToInt32(aa[1]));
                        if (dd != null)
                        {
                            Db.Execute("EXEC spKomisiPDetil"
                                + " '" + KPID + "'"
                                + ",'" + dd.Rows[0]["NoKomisi"].ToString() + "'"
                                + ", " + Convert.ToInt32(dd.Rows[0]["SN"])
                                + ", " + Convert.ToDecimal(dd.Rows[0]["NilaiCair"])
                                );
                        }
                    }

                    index++;
                }

                DataTable rsHeader = Db.Rs("SELECT "
                    + " NoKomisiP"
                    + ",CONVERT(varchar,Tgl,106) AS [Tgl. Pengajuan]"
                    + ",Ket AS [Keterangan]"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISIP "
                    + " WHERE NoKomisiP = '" + KPID + "'");

                DataTable rsDetail = Db.Rs("SELECT "
                    + " CONVERT(VARCHAR, SN) "
                    + " + '.  ' + (SELECT NamaAgent FROM MS_KOMISI_TERM WHERE NoKomisi = a.NoKomisi AND SN = a.SN_KomisiTermin)"
                    + " + '  ' + (SELECT Nama FROM MS_KOMISI_TERM WHERE NoKomisi = a.NoKomisi AND SN = a.SN_KomisiTermin)"
                    + " + '  ' + CONVERT(VARCHAR, Nilai, 1) "
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISIP_DETAIL a WHERE NoKomisiP = '" + KPID + "'");

                string Ket = Cf.LogCapture(rsHeader)
                    + Cf.LogList(rsDetail, "DETAIL");

                Db.Execute("EXEC spLogKomisiP"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + KPID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISIP_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_KOMISIP_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("KomisiPRegis1.aspx?id=" + KPID);
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class KomisiRegis2 : System.Web.UI.Page
    {
        protected string SalesID { get { return Request.QueryString["Sales"]; } }
        protected string Project { get { return Request.QueryString["Project"]; } }
        protected string TglDari { get { return Request.QueryString["Dari"]; } }
        protected string TglSampai { get { return Request.QueryString["Sampai"]; } }
        public string xx = "";
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            fill();
        }

        protected void fill()
        {
            DateTime Dari = DateTime.Today;
            DateTime Sampai = DateTime.Today;
            if (Cf.isTgl(TglDari))
            {
                Dari = Convert.ToDateTime(TglDari);
            }
            if (Cf.isTgl(TglSampai))
            {
                Sampai = Convert.ToDateTime(TglSampai);
            }

            if (Dari > Sampai)
            {
                DateTime x = Sampai;
                Sampai = Dari;
                Dari = x;
            }

            string w = "";
            if (SalesID != "SEMUA")
                w = " AND a.NoAgent = '" + SalesID + "'";

            string strSql = "SELECT a.*, b.SalesTipe, c.Nama AS NamaCust"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_AGENT b ON a.NoAgent = b.NoAgent"
                + " INNER JOIN MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,a.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,a.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Status = 'A' AND a.KomisiID = '' AND a.CFID != ''"
                + w
                + " AND a.Project ='" + Project + "'"
                + " ORDER BY NoKontrak";

            DataTable rs = Db.Rs(strSql);

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                DateTime TglKontrak = Convert.ToDateTime(rs.Rows[i]["TglKontrak"]);

                TableRow r = new TableRow();
                TableCell c;

                r.VerticalAlign = VerticalAlign.Top;

                c = new TableCell();
                c.Text = rs.Rows[i]["NoKontrak"].ToString();
                c.ID = "nokontrak_" + index;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NoUnit"].ToString();
                c.ID = "nounit_" + index;
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["NamaCust"].ToString();
                c.ID = "cust_" + index;
                c.Attributes["title"] = rs.Rows[i]["NoCustomer"] + ";" + rs.Rows[i]["NamaCust"];
                c.HorizontalAlign = HorizontalAlign.Left;
                r.Cells.Add(c);

                string TerminID = "", NamaTermin = "", CaraBayar = "", SalesTipe = "";
                DataTable ter = Db.Rs("SELECT * FROM REF_SKOM_TERM WHERE CaraBayar = '" + rs.Rows[i]["CaraBayar"].ToString() + "' AND SalesTipe = '" + rs.Rows[i]["SalesTipe"].ToString() + "' AND Inaktif = 0");// tambah kondisi AND tipe marketingnya apa?
                if (ter.Rows.Count > 0)
                {
                    TerminID = ter.Rows[0]["NoTermin"].ToString();
                    NamaTermin = ter.Rows[0]["Nama"].ToString();
                    CaraBayar = ter.Rows[0]["CaraBayar"].ToString();
                    SalesTipe = ter.Rows[0]["SalesTipe"].ToString();
                }
                else
                {
                    DataTable all = Db.Rs("SELECT * FROM REF_SKOM_TERM WHERE CaraBayar = 'ALL' AND SalesTipe = '" + rs.Rows[i]["SalesTipe"].ToString() + "' AND Inaktif = 0"); // tambah kondisi AND tipe marketingnya apa?
                    if (all.Rows.Count > 0)
                    {
                        TerminID = all.Rows[0]["NoTermin"].ToString();
                        NamaTermin = all.Rows[0]["Nama"].ToString();
                        CaraBayar = all.Rows[0]["CaraBayar"].ToString();
                        SalesTipe = all.Rows[0]["SalesTipe"].ToString();
                    }
                }

                string kontrakawal = Db.SingleString("SELECT TOP 1 NoKOntrak FROM MS_KONTRAK");
                string strSql_u = "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoAgent = '" + rs.Rows[i]["NoAgent"] + "' AND Status = 'A' AND CONVERT(VARCHAR,TglKontrak,112) <= '" + Cf.Tgl112(TglKontrak) + "' AND NoKontrak BETWEEN '" + kontrakawal + "' AND '" + rs.Rows[i]["NoKontrak"].ToString() + "'";
                int unit = Db.SingleInteger(strSql_u);

                string strSql_n = "SELECT ISNULL(SUM(NilaiKontrak),0) FROM MS_KONTRAK WHERE NoAgent = '" + rs.Rows[i]["NoAgent"] + "' AND Status = 'A' AND CONVERT(VARCHAR,TglKontrak,112) <= '" + Cf.Tgl112(TglKontrak) + "'";
                decimal nilaikontrak = Db.SingleDecimal(strSql_n);

                string SkemaID = "", SkemaNama = "";

                DataTable rsSkema = Db.Rs("SELECT * FROM REF_SKOM WHERE " + Cf.Tgl112(TglKontrak) + " >= CONVERT(VARCHAR,Dari,112) AND " + Cf.Tgl112(TglKontrak) + " <= CONVERT(VARCHAR,Sampai,112) AND SalesTipe = " + Convert.ToInt32(rs.Rows[i]["SalesTipe"]) + " AND Inaktif = 0");
                if (rsSkema.Rows.Count > 0)
                {
                    decimal NilaiDasar = 0;
                    if (rsSkema.Rows[0]["DasarHitung"].ToString() != "")
                    {
                        SkemaID = rsSkema.Rows[0]["NoSkema"].ToString();
                        SkemaNama = rsSkema.Rows[0]["Nama"].ToString();
                        NilaiDasar = rsSkema.Rows[0]["DasarHitung"].ToString() == "DPP" ? Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]) : Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                    }

                    string strSql2 = "SELECT a.*, b.Nama FROM MS_KONTRAK_AGENT a"
                            + " INNER JOIN MS_AGENT b ON a.NoAgent = b.NoAgent"
                            + " WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "'";
                    DataTable rsa = Db.Rs(strSql2);
                    for (int j = 0; j < rsa.Rows.Count; j++)
                    {
                        decimal NilaiKomisi = 0, PotongKomisi = 0;

                        if (rsSkema.Rows[0]["Rumus"].ToString() == "UNIT")
                        {
                            DataTable rsDetil = Db.Rs("SELECT * FROM REF_SKOM_DETAIL WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]));
                            if (rsDetil.Rows.Count > 0)
                            {
                                if (rsDetil.Rows[0]["TipeTarif"].ToString() == "RP")
                                {
                                    NilaiKomisi = Convert.ToDecimal(rsDetil.Rows[0]["Nilai"]);
                                }
                                else
                                {
                                    NilaiKomisi = Math.Round((Convert.ToDecimal(rsDetil.Rows[0]["Nilai"]) / 100) * NilaiDasar);
                                }
                            }
                        }
                        else if (rsSkema.Rows[0]["Rumus"].ToString() == "KUMULATIF")
                        {
                            //Kumulatif
                            //unit
                            decimal a = Db.SingleDecimal("SELECT TargetAtas FROM REF_SKOM_DETAIL2 WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]) + " AND TipeTarget = 'UNIT' ORDER BY SN DESC");
                            string b = (a > 0) ? "AND " + unit + " <= TargetAtas" : "";                            
                            DataTable rsDetil = Db.Rs("SELECT * FROM REF_SKOM_DETAIL2 WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]) + " AND " + unit + " >= TargetBawah " + b + " AND TipeTarget = 'UNIT' ORDER BY SN DESC");
                            if (rsDetil.Rows.Count > 0)
                            {
                                if (rsDetil.Rows[0]["TipeTarif"].ToString() == "RP")
                                {
                                    NilaiKomisi = Convert.ToDecimal(rsDetil.Rows[0]["Nilai"]);
                                }
                                else
                                {
                                    NilaiKomisi = Math.Round((Convert.ToDecimal(rsDetil.Rows[0]["Nilai"]) / 100) * NilaiDasar);
                                }
                            }

                            decimal d = Db.SingleDecimal("SELECT TargetAtas FROM REF_SKOM_DETAIL2 WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]) + " AND TipeTarget = 'UNIT' ORDER BY SN DESC");
                            string e = (d > 0) ? "AND " + nilaikontrak + " <= TargetAtas" : "";
                            //nilai kontrak
                            DataTable rsDetil2 = Db.Rs("SELECT * FROM REF_SKOM_DETAIL2 WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]) + " AND " + nilaikontrak + " >= TargetBawah " + e + " AND TipeTarget = 'NILAI' ORDER BY SN DESC");
                            if (rsDetil2.Rows.Count > 0)
                            {
                                if (rsDetil2.Rows[0]["TipeTarif"].ToString() == "RP")
                                {
                                    NilaiKomisi = Convert.ToDecimal(rsDetil2.Rows[0]["Nilai"]);
                                }
                                else
                                {
                                    NilaiKomisi = Math.Round((Convert.ToDecimal(rsDetil2.Rows[0]["Nilai"]) / 100) * NilaiDasar);
                                }
                            }
                        }
                        else
                        {
                            //Progresif
                            //unit                            
                            DataTable a = Db.Rs("SELECT TargetAtas FROM REF_SKOM_DETAIL2 WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]) + " AND TipeTarget = 'UNIT'");
                            for (int aa = 0; aa < a.Rows.Count; aa++)
                            {
                                string b = (Convert.ToInt16(a.Rows[aa]["TargetAtas"]) > 0 && unit > Convert.ToInt16(a.Rows[aa]["TargetAtas"])) ? "AND " + Convert.ToInt16(a.Rows[aa]["TargetAtas"]) + " <= TargetAtas" : "";
                                DataTable rsDetil = Db.Rs("SELECT * FROM REF_SKOM_DETAIL2 WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]) + " AND " + unit + " >= TargetBawah " + b + " AND TipeTarget = 'UNIT' ORDER BY TargetBawah");
                                for (int k = 0; k < rsDetil.Rows.Count; k++)
                                {
                                    if (unit > 0)
                                    {
                                        if (unit >= Convert.ToInt32(rsDetil.Rows[k]["TargetBawah"]))
                                        {
                                            if (rsDetil.Rows[k]["TipeTarif"].ToString() == "RP")
                                            {
                                                NilaiKomisi = Convert.ToDecimal(rsDetil.Rows[k]["Nilai"]);
                                            }
                                            else
                                            {
                                                NilaiKomisi = Math.Round((Convert.ToDecimal(rsDetil.Rows[k]["Nilai"]) / 100) * NilaiDasar);
                                            }                                            
                                        }
                                        //else
                                        //{
                                        //    break;
                                        //}
                                    }
                                }
                            }

                            //nilai kontrak
                            decimal d = Db.SingleDecimal("SELECT TargetAtas FROM REF_SKOM_DETAIL2 WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]) + " AND TipeTarget = 'UNIT' ORDER BY SN DESC");
                            string e = (d > 0) ? "AND " + nilaikontrak + " <= TargetAtas" : "";
                            DataTable rsDetil2 = Db.Rs("SELECT * FROM REF_SKOM_DETAIL2 WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]) + " AND " + nilaikontrak + " >= TargetBawah " + e + " AND TipeTarget = 'NILAI'");
                            for (int k = 0; k < rsDetil2.Rows.Count; k++)
                            {
                                if (Convert.ToDecimal(rsDetil2.Rows[k]["TargetAtas"]) >= nilaikontrak)
                                {
                                    if (rsDetil2.Rows[k]["TipeTarif"].ToString() == "RP")
                                    {
                                        NilaiKomisi = Convert.ToDecimal(rsDetil2.Rows[k]["Nilai"]);
                                    }
                                    else
                                    {
                                        NilaiKomisi = Math.Round((Convert.ToDecimal(rsDetil2.Rows[k]["Nilai"]) / 100) * NilaiDasar);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        //Potong CF dari Komisi
                        DataTable dd = Db.Rs("SELECT * FROM MS_KOMISI_CF_DETAIL WHERE NoCF = '" + rs.Rows[i]["CFID"].ToString() + "' AND NoAgent = '" + rsa.Rows[j]["NoAgent"].ToString() + "'");
                        if (dd.Rows.Count > 0)
                        {
                            PotongKomisi = Convert.ToDecimal(dd.Rows[0]["Nilai"]);

                            if (Convert.ToBoolean(dd.Rows[0]["PotongKomisi"]) && NilaiKomisi >= Convert.ToDecimal(dd.Rows[0]["Nilai"]))
                            {
                                NilaiKomisi -= PotongKomisi;
                            }
                        }

                        if (j > 0)
                        {
                            r = new TableRow();

                            r.VerticalAlign = VerticalAlign.Top;

                            c = new TableCell();
                            c.ID = "nokontrak_" + index;
                            c.HorizontalAlign = HorizontalAlign.Left;
                            r.Cells.Add(c);

                            c = new TableCell();
                            c.ID = "nounit_" + index;
                            c.HorizontalAlign = HorizontalAlign.Left;
                            r.Cells.Add(c);

                            c = new TableCell();
                            c.ID = "cust_" + index;
                            c.Attributes["title"] = "-";
                            c.HorizontalAlign = HorizontalAlign.Left;
                            r.Cells.Add(c);
                        }

                        bool terakhir = false;
                        if (j == rsa.Rows.Count - 1)
                            terakhir = true;

                        c = new TableCell();
                        c.Text = rsa.Rows[j]["Nama"].ToString();
                        c.Attributes["title"] = rsa.Rows[j]["NoAgent"] + ";" + rsa.Rows[j]["Nama"] + ";" + SkemaID + ";" + SkemaNama + ";" + terakhir + ";" + TerminID + ";" + NamaTermin + ";" + CaraBayar + ";" + rsa.Rows[j]["SalesLevel"];
                        c.ID = "skema_agent_" + index;
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = Cf.Num(NilaiKomisi);
                        c.ID = "nilaikomisi_" + index;
                        c.CssClass = "right";
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = Cf.Num(PotongKomisi);
                        c.ID = "potongkomisi_" + index;
                        c.CssClass = "num";
                        r.Cells.Add(c);

                        Rpt.Border(r);
                        list.Controls.Add(r);

                        index++;
                    }
                }
                else
                {
                    //Rpt.Border(r);
                    //list.Controls.Add(r);
                }
            }
        }
        protected void save_Click(object sender, EventArgs e)
        {
            DateTime Tgl = DateTime.Today;

            int index = 0;
            foreach (Control tr in list.Controls)
            {
                TableCell nokontrak = (TableCell)list.FindControl("nokontrak_" + index);
                TableCell skema_agent = (TableCell)list.FindControl("skema_agent_" + index);
                TableCell nounit = (TableCell)list.FindControl("nounit_" + index);
                TableCell cust = (TableCell)list.FindControl("cust_" + index);
                TableCell nilaikomisi = (TableCell)list.FindControl("nilaikomisi_" + index);
                TableCell potongkomisi = (TableCell)list.FindControl("potongkomisi_" + index);

                string[] aa = skema_agent.Attributes["title"].Split(';');

                if (cust.Attributes["title"] != "-")
                {
                    string[] bb = cust.Attributes["title"].Split(';');

                    string KomisiID = LibKom.KomisiID(Tgl.Month, Tgl.Year, Project);

                    Db.Execute("EXEC spKomisiDaftar"
                        + " '" + KomisiID + "'"
                        + ",'" + Tgl + "'"
                        + ", " + Convert.ToInt32(aa[2])
                        + ",'" + aa[3] + "'"
                        + ",'" + aa[5] + "'"
                        + ",'" + aa[6] + "'"
                        + ",'" + aa[7] + "'"
                        + ",'" + nokontrak.Text + "'"
                        + ",'" + aa[0] + "'"
                        + ",'" + aa[1] + "'"
                        + ",'" + bb[0] + "'"
                        + ",'" + bb[1] + "'"
                        + ",'" + nounit.Text + "'"
                        );

                    xx = KomisiID;

                    Db.Execute("UPDATE MS_KOMISI SET "
                    + " Project = '" + Project + "'"
                    + " WHERE NoKontrak = '" + nokontrak.Text + "'");

                    string strSql = "UPDATE MS_KONTRAK SET KomisiID = '" + KomisiID + "' WHERE NoKontrak = '" + nokontrak.Text + "'";
                    Db.Execute(strSql);
                }

                SaveDetil(xx, aa[0], aa[1], Convert.ToDecimal(nilaikomisi.Text), Convert.ToBoolean(aa[4]), aa[5], aa[8]);

                index++;
            }

            Response.Redirect("CFRegis1.aspx");
        }
        protected void SaveDetil(string KomisiID, string NoAgent, string NamaAgent, decimal NilaiKomisi, bool Terakhir, string TerminID, string SalesLevel)
        {
            if (NilaiKomisi > 0)
            {
                Db.Execute("EXEC spKomisiDetil"
                    + " '" + KomisiID + "'"
                    + ",'" + NoAgent + "'"
                    + ",'" + NamaAgent + "'"
                    + ", " + NilaiKomisi
                    );

                //Termin
                if (TerminID != "")
                {
                    DataTable rs = Db.Rs("SELECT * FROM REF_SKOM_TERM_DETAIL WHERE NoTermin = '" + TerminID + "' AND SalesLevel = '" + SalesLevel + "'");
                    for (int i = 0; i < rs.Rows.Count; i++)
                    {
                        decimal NilaiCair = Math.Round((Convert.ToDecimal(rs.Rows[i]["PersenCair"]) / 100) * NilaiKomisi);
                        if (i == rs.Rows.Count - 1)
                        {
                            decimal Total = Db.SingleDecimal("SELECT ISNULL(SUM(NilaiCair),0) FROM MS_KOMISI_TERM WHERE NoKomisi = '" + KomisiID + "' AND NoAgent = '" + NoAgent + "'");
                            NilaiCair = NilaiKomisi - Total;
                        }

                        Db.Execute("EXEC spKomisiTerm"
                            + " '" + KomisiID + "'"
                            + ",'" + NoAgent + "'"
                            + ",'" + NamaAgent + "'"
                            + ",'" + rs.Rows[i]["Nama"].ToString() + "'"
                            + ", " + Convert.ToDecimal(rs.Rows[i]["PersenCair"])
                            + ", " + NilaiCair
                            + ",'" + Convert.ToBoolean(rs.Rows[i]["Lunas"]) + "'"
                            + ", " + Convert.ToDecimal(rs.Rows[i]["PersenLunas"])
                            + ",'" + Convert.ToBoolean(rs.Rows[i]["BF"]) + "'"
                            + ", " + Convert.ToDecimal(rs.Rows[i]["PersenBF"])
                            + ",'" + Convert.ToBoolean(rs.Rows[i]["DP"]) + "'"
                            + ", " + Convert.ToDecimal(rs.Rows[i]["PersenDP"])
                            + ",'" + Convert.ToBoolean(rs.Rows[i]["ANG"]) + "'"
                            + ", " + Convert.ToDecimal(rs.Rows[i]["PersenANG"])
                            + ",'" + Convert.ToBoolean(rs.Rows[i]["PPJB"]) + "'"
                            + ",'" + Convert.ToBoolean(rs.Rows[i]["AJB"]) + "'"
                            + ",'" + Convert.ToBoolean(rs.Rows[i]["AKAD"]) + "'"
                            + ", " + Convert.ToInt32(rs.Rows[i]["TipeCair"])
                            );
                    }
                }
            }

            if (Terakhir)
            {
                DataTable rsHeader = Db.Rs("SELECT "
                    + " NoKomisi"
                    + ",CONVERT(varchar,Tgl,106) AS [Tgl. Generate]"
                    + ",NoSkema AS [No. Skema]"
                    + ",NamaSkema AS [Nama Skema]"
                    + ",NoTermin AS [No. Termin]"
                    + ",NamaTermin AS [Nama Termin]"
                    + ",CaraBayar AS [Cara Bayar]"
                    + ",NoKontrak AS [No. Kontrak]"
                    + ",NoAgent AS [No. Agent]"
                    + ",NamaAgent [Nama Agent]"
                    + ",NoCust AS [No. Customer]"
                    + ",NamaCust [Nama Customer]"
                    + ",NoUnit AS [No. Unit]"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI "
                    + " WHERE NoKomisi = '" + KomisiID + "'");

                DataTable rsDetail = Db.Rs("SELECT "
                    + " CONVERT(VARCHAR, SN) "
                    + " + '.  ' + NamaAgent  + ' (' + CONVERT(VARCHAR, NoAgent) + ')  '"
                    + " + '  ' + CONVERT(VARCHAR, Nilai, 1) "
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_DETAIL WHERE NoKomisi = '" + KomisiID + "'");

                DataTable rsTerm = Db.Rs("SELECT "
                    + " CONVERT(VARCHAR, SN) "
                    + " + '.  ' + NamaAgent  + ' (' + CONVERT(VARCHAR, NoAgent) + ')  '"
                    + " + '  ' + Nama "
                    + " + '  ' + CONVERT(VARCHAR, NilaiCair, 1) + ' (' + CONVERT(VARCHAR, PersenCair, 1) + '%)  '"
                    + " + '  BF (' + CONVERT(VARCHAR, PersenBF, 1) + '%)  '"
                    + " + '  DP (' + CONVERT(VARCHAR, PersenDP, 1) + '%)  '"
                    + " + '  ANG (' + CONVERT(VARCHAR, PersenANG, 1) + '%)  '"
                    + " + '  Syarat Cair (' + CONVERT(VARCHAR, TipeCair) + ')  ' "
                    + " + '  PPJB (' + CONVERT(VARCHAR, PPJB, 1) + ')  ' "
                    + " + '  AJB (' + CONVERT(VARCHAR, AJB, 1) + ')  ' "
                    + " + '  AKAD (' + CONVERT(VARCHAR, AKAD, 1) + ')  ' "
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_TERM WHERE NoKomisi = '" + KomisiID + "'");

                string Ket = Cf.LogCapture(rsHeader)
                    + Cf.LogList(rsDetail, "DETAIL")
                    + Cf.LogList(rsTerm, "TERMIN");

                Db.Execute("EXEC spLogKomisi"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + KomisiID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_KOMISI_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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

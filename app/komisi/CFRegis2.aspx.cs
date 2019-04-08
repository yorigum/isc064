using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class CFRegis2 : System.Web.UI.Page
    {
        protected string SalesID { get { return Request.QueryString["Sales"]; } }
        protected string SalesTipe { get { return Request.QueryString["SalesTipe"]; } }
        protected string Project { get { return Request.QueryString["Project"]; } }
        protected string Skema { get { return Request.QueryString["Skema"]; } }
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
            tgl.Text = Cf.Day(DateTime.Today);

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

            //fill judul dan query search
            project_txt.Text = Project.ToString();
            periode_txt.Text = Cf.DayIndo(Dari) + " s/d " + Cf.DayIndo(Sampai);

            if(Skema != "0")
            {
                trskema.Visible = true;
                skema_txt.Text = Db.SingleString("select Nama from REF_SKOM_CF where NoSkema = '" + Skema + "' and Project = '" + Project + "'");
            }

            string w = "";
            if (SalesID != "0")
            {
                trsales.Visible = true;
                sales_txt.Text = Db.SingleString("select Nama from MS_AGENT where NoAgent = '" + SalesID + "' and Project = '" + Project + "'");
                w = " AND a.NoAgent = '" + SalesID + "'";
            }

            string w2 = "";
            if (SalesTipe != "0")
            {
                trtipe.Visible = true;
                tipesales_txt.Text = Db.SingleString("select Tipe from REF_AGENT_TIPE where ID = '" + SalesTipe + "' and Project = '" + Project + "'");
                w2 = " AND b.SalesTipe = '" + SalesTipe + "'";
            }

            string strSql = "SELECT a.*, b.SalesTipe, c.Nama AS NamaCust"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_AGENT b ON a.NoAgent = b.NoAgent"
                + " INNER JOIN MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,a.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,a.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Status = 'A' AND a.CFID = ''"
                + w
                + w2
                + " AND a.Project = '" + Project + "'"
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

                string strSql_u = "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoAgent = '" + rs.Rows[i]["NoAgent"] + "' AND Status = 'A' AND CONVERT(VARCHAR,TglKontrak,112) <= '" + Cf.Tgl112(TglKontrak) + "' and Project = '" + Project + "'";
                int unit = Db.SingleInteger(strSql_u);

                string strSql_n = "SELECT ISNULL(SUM(NilaiKontrak),0) FROM MS_KONTRAK WHERE NoAgent = '" + rs.Rows[i]["NoAgent"] + "' AND Status = 'A' AND CONVERT(VARCHAR,TglKontrak,112) <= '" + Cf.Tgl112(TglKontrak) + "' and Project = '" + Project + "'";
                decimal nilaikontrak = Db.SingleDecimal(strSql_n);

                string w3 = "", SkemaID = "", SkemaNama = "";
                if (SalesTipe != "0")
                {
                    w3 = " AND SalesTipe = '" + SalesTipe + "'";
                }

                DataTable rsSkema = Db.Rs("SELECT * FROM REF_SKOM_CF WHERE " + Cf.Tgl112(TglKontrak) + " >= CONVERT(VARCHAR,Dari,112) AND " + Cf.Tgl112(TglKontrak) + " <= CONVERT(VARCHAR,Sampai,112)"
                    + w3
                    + " AND Inaktif = 0 AND Project = '" + rs.Rows[i]["Project"] + "'");

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
                            + " WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"] + "' and Project = '" + Project + "'";

                    DataTable rsa = Db.Rs(strSql2);
                    for (int j = 0; j < rsa.Rows.Count; j++)
                    {
                        decimal NilaiKomisi = 0;
                        string PotongKomisi = "";

                        if (rsSkema.Rows[0]["Rumus"].ToString() == "UNIT")
                        {
                            DataTable rsDetil = Db.Rs("SELECT * FROM REF_SKOM_CF_DETAIL WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]));
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

                                PotongKomisi = Convert.ToBoolean(rsDetil.Rows[0]["PotongKomisi"]) ? "YA" : "TIDAK";
                            }
                        }
                        else
                        {
                            //Kumulatif
                            //unit
                            decimal a = Db.SingleDecimal("SELECT TargetAtas FROM REF_SKOM_CF_DETAIL2 WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]) + " AND TipeTarget = 'UNIT' ORDER BY SN DESC");
                            string b = (a > 0) ? "AND " + unit + " <= TargetAtas" : "";
                            DataTable rsDetil = Db.Rs("SELECT * FROM REF_SKOM_CF_DETAIL2 WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]) + " AND " + unit + " >= TargetBawah " + b + " AND TipeTarget = 'UNIT' ORDER BY SN DESC");
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

                                PotongKomisi = Convert.ToBoolean(rsDetil.Rows[0]["PotongKomisi"]) ? "YA" : "TIDAK";
                            }

                            //nilai kontrak
                            decimal d = Db.SingleDecimal("SELECT TargetAtas FROM REF_SKOM_CF_DETAIL2 WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]) + " AND TipeTarget = 'KONTRAK' ORDER BY SN DESC");
                            string e = (d > 0) ? "AND " + nilaikontrak + " <= TargetAtas" : "";

                            DataTable rsDetil2 = Db.Rs("SELECT * FROM REF_SKOM_CF_DETAIL2 WHERE NoSkema = '" + rsSkema.Rows[0]["NoSkema"].ToString() + "' AND SalesLevel = " + Convert.ToInt32(rsa.Rows[j]["SalesLevel"]) + " AND " + nilaikontrak + " >= TargetBawah " + e + " AND TipeTarget = 'KONTRAK' ORDER BY SN DESC");                            
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

                                PotongKomisi = Convert.ToBoolean(rsDetil2.Rows[0]["PotongKomisi"]) ? "YA" : "TIDAK";
                            }
                        }

                        if (j > 0)
                        {
                            //index++;
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
                        c.Attributes["title"] = rsa.Rows[j]["NoAgent"] + ";" + rsa.Rows[j]["Nama"] + ";" + SkemaID + ";" + SkemaNama + ";" + terakhir;
                        c.ID = "skema_agent_" + index;
                        c.HorizontalAlign = HorizontalAlign.Left;
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = Cf.Num(NilaiKomisi);
                        c.ID = "nilaikomisi_" + index;
                        c.CssClass = "right";
                        r.Cells.Add(c);

                        c = new TableCell();
                        c.Text = PotongKomisi;
                        c.ID = "potongkomisi_" + index;
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
                    //save.Enabled = false;
                }
            }
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (!Cf.isTgl(tgl))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }
            else
                tglc.Text = "";

            return x;
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                DateTime Tgl = Convert.ToDateTime(tgl.Text);

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

                        string CFID = LibKom.CFID(Tgl.Month, Tgl.Year, Project);

                        Db.Execute("EXEC spKomisiCFDaftar"
                            + " '" + CFID + "'"
                            + ",'" + Tgl + "'"
                            + ", " + Convert.ToInt32(aa[2])
                            + ",'" + aa[3] + "'"
                            + ",'" + nokontrak.Text + "'"
                            + ",'" + aa[0] + "'"
                            + ",'" + aa[1] + "'"
                            + ",'" + bb[0] + "'"
                            + ",'" + bb[1] + "'"
                            + ",'" + nounit.Text + "'"
                            );

                        xx = CFID;

                        Db.Execute("UPDATE MS_KOMISI_CF SET "
                        + " Project = '" + Project + "'"
                        + " ,SalesTipe = '" + SalesTipe + "'"
                        + " WHERE NoKontrak = '" + nokontrak.Text + "'");

                        string strSql = "UPDATE MS_KONTRAK SET CFID = '" + CFID + "' WHERE NoKontrak = '" + nokontrak.Text + "'";
                        Db.Execute(strSql);
                    }

                    SaveDetil(xx, aa[0], aa[1], potongkomisi.Text, Convert.ToDecimal(nilaikomisi.Text), Convert.ToBoolean(aa[4]));

                    index++;
                }

                Response.Redirect("CFRegis1.aspx?done=1");
            }
        }
        protected void SaveDetil(string CFID, string NoAgent, string NamaAgent, string PotongKomisi, decimal NilaiKomisi, bool Terakhir)
        {
            if (NilaiKomisi > 0)
            {
                bool potong = PotongKomisi == "YA" ? true : false;

                Db.Execute("EXEC spKomisiCFDetil"
                    + " '" + CFID + "'"
                    + ",'" + NoAgent + "'"
                    + ",'" + NamaAgent + "'"
                    + ",'" + potong + "'"
                    + ", " + NilaiKomisi
                    );
            }

            if (Terakhir)
            {
                DataTable rsHeader = Db.Rs("SELECT "
                    + " NoCF"
                    + ",CONVERT(varchar,Tgl,106) AS [Tgl. Generate]"
                    + ",NoSkema AS [No. Skema CF]"
                    + ",NamaSkema AS [Nama Skema CF]"
                    + ",SalesTipe AS [Tipe Sales]"
                    + ",NoKontrak AS [No. Kontrak]"
                    + ",NoAgent AS [No. Agent]"
                    + ",NamaAgent AS [Nama Agent]"
                    + ",NoCustomer AS [No. Customer]"
                    + ",NamaCust AS [Nama Customer]"
                    + ",NoUnit AS [No. Unit]"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_CF "
                    + " WHERE NoCF = '" + CFID + "'");

                DataTable rsDetail = Db.Rs("SELECT "
                    + " CONVERT(VARCHAR, SN) "
                    + " + '.  ' + NamaAgent  + ' (' + CONVERT(VARCHAR, NoAgent) + ')  '"
                    + " + '  ' + CONVERT(VARCHAR, Nilai, 1) "
                    + " + '  ' + CONVERT(VARCHAR,PotongKomisi)"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_CF_DETAIL WHERE NoCF = '" + CFID + "'");

                string Ket = Cf.LogCapture(rsHeader)
                    + Cf.LogList(rsDetail, "DETAIL");

                Db.Execute("EXEC spLogKomisiCF"
                    + " 'DAFTAR'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + CFID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_CF_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE MS_KOMISI_CF_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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

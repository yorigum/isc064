using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class RewardRegis1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Bind();
            }

            if (IsPostBack)
            {
                Fill();
            }
        }

        private void Bind()
        {
            DataTable rs;
            string strSql;

            strSql = "SELECT * FROM REF_SKOM_REWARD WHERE Inaktif = 0 AND Project = '" + project.SelectedValue + "'ORDER BY Dari";
            rs = Db.Rs(strSql);
            if (rs.Rows.Count > 0)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string v = rs.Rows[i]["NoSkema"].ToString();
                    string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                    skema.Items.Add(new ListItem(t, v));
                }
            }
            else
            {
                periode.InnerText = "Belum ada Skema Reward untuk project tersebut.";
            }
        }

        protected void Fill()
        {
            list.Controls.Clear();
            if (skema.Items.Count > 0)
            {

                DataTable sk = Db.Rs("SELECT * FROM REF_SKOM_REWARD WHERE NoSkema = " + Convert.ToInt32(skema.SelectedValue));

                if (sk != null)
                {
                    DateTime Dari = Convert.ToDateTime(sk.Rows[0]["Dari"]);
                    DateTime Sampai = Convert.ToDateTime(sk.Rows[0]["Sampai"]);

                    string w = " AND CONVERT(VARCHAR, a.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "' AND CONVERT(VARCHAR, a.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'";

                    periode.InnerText = "Periode " + Cf.Tgl(Dari) + " s/d " + Cf.Tgl(Sampai);

                    string strSql = "";
                    //Satuan per unit
                    if (sk.Rows[0]["Rumus"].ToString() == "UNIT")
                    {
                        strSql = "SELECT a.NoAgent, a.NoKontrak, b.Nama, b.SalesLevel, 1 AS JumUnit, a.NilaiKontrak AS Total FROM MS_KONTRAK a"
                                + " INNER JOIN MS_AGENT b ON a.NoAgent = b.NoAgent"
                                + " WHERE a.Status = 'A'"
                                + " AND b.SalesTipe = " + Convert.ToInt32(sk.Rows[0]["SalesTipe"])
                                + " AND RewardID = ''"
                                + " AND a.Project = '" + project.SelectedValue + "'"
                                + w;
                    }
                    //Kumulatif
                    else
                    {
                        strSql = "SELECT a.NoAgent, b.Nama, b.SalesLevel, COUNT(a.NoKontrak) AS JumUnit, SUM(a.NilaiKontrak) AS Total FROM MS_KONTRAK a"
                                + " INNER JOIN MS_AGENT b ON a.NoAgent = b.NoAgent"
                                + " WHERE a.Status = 'A'"
                                + " AND b.SalesTipe = " + Convert.ToInt32(sk.Rows[0]["SalesTipe"])
                                + " AND RewardID = ''"
                                + " AND a.Project = '" + project.SelectedValue + "'"
                                + w
                                + " GROUP BY a.NoAgent, b.Nama, b.SalesLevel";
                    }

                    DataTable rs = Db.Rs(strSql);

                    int index = 0;
                    for (int i = 0; i < rs.Rows.Count; i++)
                    {
                        if (!Response.IsClientConnected) break;

                        int Unit = Convert.ToInt32(rs.Rows[i]["JumUnit"]);
                        decimal TotalKontrak = Convert.ToDecimal(rs.Rows[i]["Total"]);

                        string reward = "";
                        if (sk.Rows[0]["Rumus"].ToString() == "UNIT")
                        {
                            DataTable d2 = Db.Rs("SELECT * FROM REF_SKOM_REWARD_DETAIL WHERE NoSkema = " + Convert.ToInt32(sk.Rows[0]["NoSkema"]) + " AND SalesLevel = " + Convert.ToInt32(rs.Rows[i]["SalesLevel"]) + " AND " + TotalKontrak + " >= Penjualan");
                            if (d2.Rows.Count > 0)
                            {
                                reward = d2.Rows[0]["Reward"].ToString();
                            }
                        }
                        else
                        {
                            //Kumulatif
                            //unit
                            decimal a = Db.SingleDecimal("SELECT TargetAtas FROM REF_SKOM_REWARD_DETAIL2 WHERE NoSkema = '" + Convert.ToInt32(sk.Rows[0]["NoSkema"]) + "' AND SalesLevel = " + Convert.ToInt32(rs.Rows[i]["SalesLevel"]) + " AND TipeTarget = 'UNIT' ORDER BY SN DESC");
                            string b = (a > 0) ? " AND " + Unit + " <= TargetAtas" : "";

                            DataTable d2 = Db.Rs("SELECT * FROM REF_SKOM_REWARD_DETAIL2 WHERE NoSkema = " + Convert.ToInt32(sk.Rows[0]["NoSkema"]) + " AND SalesLevel = " + Convert.ToInt32(rs.Rows[i]["SalesLevel"]) + " AND " + Unit + " >= TargetBawah " + b + " AND TipeTarget = 'UNIT' ORDER BY SN DESC");
                            if (d2.Rows.Count > 0)
                            {
                                reward = d2.Rows[0]["Reward"].ToString();
                            }

                            decimal d = Db.SingleDecimal("SELECT TargetAtas FROM REF_SKOM_REWARD_DETAIL2 WHERE NoSkema = '" + Convert.ToInt32(sk.Rows[0]["NoSkema"]) + "' AND SalesLevel = " + Convert.ToInt32(rs.Rows[i]["SalesLevel"]) + " AND TipeTarget = 'NILAI' ORDER BY SN DESC");
                            string e = (d > 0) ? " AND " + TotalKontrak + " <= TargetAtas" : "";
                            //nilai kontrak
                            DataTable d3 = Db.Rs("SELECT * FROM REF_SKOM_REWARD_DETAIL2 WHERE NoSkema = " + Convert.ToInt32(sk.Rows[0]["NoSkema"]) + " AND SalesLevel = " + Convert.ToInt32(rs.Rows[i]["SalesLevel"]) + " AND " + TotalKontrak + " >= TargetBawah " + e + " AND TipeTarget = 'NILAI' ORDER BY SN DESC");                            
                            if (d3.Rows.Count > 0)
                            {
                                reward = d3.Rows[0]["Reward"].ToString();
                            }
                        }

                        bool tampil = reward != "" ? true : false;

                        //cek existing periode reward (tidak bisa generate reward lagi jika kumulatif)
                        int cek = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISI_REWARD"
                            + " WHERE NoAgent = " + Convert.ToInt32(rs.Rows[i]["NoAgent"])
                            + " AND ("
                            + "     (CONVERT(VARCHAR,PeriodeDari,112) >= '" + Cf.Tgl112(Dari) + "' AND CONVERT(VARCHAR,PeriodeSampai,112) <= '" + Cf.Tgl112(Sampai) + "') OR"
                            + "     (CONVERT(VARCHAR,PeriodeDari,112) >= '" + Cf.Tgl112(Dari) + "' AND CONVERT(VARCHAR,PeriodeDari,112) <= '" + Cf.Tgl112(Sampai) + "') OR"
                            + "     (CONVERT(VARCHAR,PeriodeSampai,112) >= '" + Cf.Tgl112(Dari) + "' AND CONVERT(VARCHAR,PeriodeSampai,112) <= '" + Cf.Tgl112(Sampai) + "') OR"
                            + "     ('" + Cf.Tgl112(Dari) + "' >= CONVERT(VARCHAR,PeriodeDari,112) AND '" + Cf.Tgl112(Dari) + "' <= CONVERT(VARCHAR,PeriodeSampai,112)) OR"
                            + "     ('" + Cf.Tgl112(Sampai) + "' >= CONVERT(VARCHAR,PeriodeDari,112) AND '" + Cf.Tgl112(Sampai) + "' <= CONVERT(VARCHAR,PeriodeSampai,112))"
                            + " )"
                            + " AND Project = '" + project.SelectedValue + "'"
                            );

                        bool gen = cek > 0 && sk.Rows[0]["Rumus"].ToString() != "UNIT" ? false : true;

                        if (tampil)
                        {
                            TableRow r = new TableRow();
                            TableCell c;

                            r.VerticalAlign = VerticalAlign.Top;
                            if (cek > 0 && sk.Rows[0]["Rumus"].ToString() != "UNIT")
                                r.ForeColor = Color.Red;

                            c = new TableCell();
                            c.Text = rs.Rows[i]["Nama"].ToString();
                            c.Attributes["title"] = rs.Rows[i]["NoAgent"] + ";" + gen;
                            c.ID = "pk_" + index;
                            r.Cells.Add(c);

                            c = new TableCell();
                            c.Text = Cf.Num(Unit);
                            if (sk.Rows[0]["Rumus"].ToString() == "UNIT")
                            {
                                c.Attributes["title"] = rs.Rows[i]["NoKontrak"].ToString();
                            }
                            c.ID = "nokontrak_" + index;
                            c.CssClass = "num";
                            r.Cells.Add(c);

                            c = new TableCell();                            
                            c.Text = Cf.Num(TotalKontrak);
                            c.CssClass = "num";
                            r.Cells.Add(c);

                            c = new TableCell();
                            c.Text = reward;
                            c.ID = "reward_" + index;
                            r.Cells.Add(c);

                            Rpt.Border(r);
                            list.Controls.Add(r);

                            index++;
                        }
                    }
                }
            }
        }
        protected void display_Click(object sender, System.EventArgs e)
        {
            Fill();
        }
        protected void save_Click(object sender, EventArgs e)
        {
            DateTime Tgl = DateTime.Today;
            if (skema.Items.Count > 0)
            {

                DataTable sk = Db.Rs("SELECT * FROM REF_SKOM_REWARD WHERE NoSkema = " + Convert.ToInt32(skema.SelectedValue));
                if (sk.Rows.Count > 0)
                {
                    int index = 0;
                    foreach (Control tr in list.Controls)
                    {
                        TableCell pk = (TableCell)list.FindControl("pk_" + index);
                        TableCell reward = (TableCell)list.FindControl("reward_" + index);
                        TableCell nokontrak = (TableCell)list.FindControl("nokontrak_" + index);

                        string[] aa = pk.Attributes["title"].Split(';');
                        string RewardID = LibKom.RewardID(Tgl.Month, Tgl.Year);

                        if (Convert.ToBoolean(aa[1]))
                        {
                            Db.Execute("EXEC spKomisiRewardDaftar"
                                + " '" + RewardID + "'"
                                + ",'" + Tgl + "'"
                                + ", " + Convert.ToInt32(aa[0])
                                + ",'" + pk.Text + "'"
                                + ", " + Convert.ToInt32(sk.Rows[0]["NoSkema"])
                                + ",'" + sk.Rows[0]["Nama"].ToString() + "'"
                                + ",'" + sk.Rows[0]["Rumus"].ToString() + "'"
                                + ",'" + Convert.ToDateTime(sk.Rows[0]["Dari"]) + "'"
                                + ",'" + Convert.ToDateTime(sk.Rows[0]["Sampai"]) + "'"
                                + ",'" + reward.Text + "'"
                                );

                            Db.Execute("UPDATE MS_KOMISI_REWARD SET "
                            + " Project = '" + project.SelectedValue + "'"
                            + " WHERE NoReward = '" + RewardID + "'");

                            SaveDetil(RewardID, aa[0], sk.Rows[0]["Rumus"].ToString(), Convert.ToDateTime(sk.Rows[0]["Dari"]), Convert.ToDateTime(sk.Rows[0]["Sampai"]), nokontrak.Attributes["title"]);
                        }
                        index++;
                    }
                }
            }

            Response.Redirect("RewardRegis1.aspx");
        }
        protected void SaveDetil(string RewardID, string NoAgent, string Rumus, DateTime Dari, DateTime Sampai, string NoKontrak)
        {
            string strSql = "";
            if (Rumus == "UNIT")
            {
                strSql = "SELECT a.*, b.Nama FROM MS_KONTRAK a"
                    + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                    + " WHERE a.Status = 'A'"
                    + " AND a.NoKontrak = '" + NoKontrak + "'"
                    + " AND a.RewardID = ''"
                    + " AND CONVERT(VARCHAR, a.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "' AND CONVERT(VARCHAR, a.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'";
            }
            //Kumulatif
            else
            {
                strSql = "SELECT a.*, b.Nama FROM MS_KONTRAK a"
                    + " INNER JOIN MS_CUSTOMER b ON a.NoCustomer = b.NoCustomer"
                    + " WHERE a.Status = 'A'"
                    + " AND a.NoAgent = '" + NoAgent + "'"
                    + " AND CONVERT(VARCHAR, a.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "' AND CONVERT(VARCHAR, a.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'";
            }

            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                Db.Execute("EXEC spKomisiRewardDetil"
                    + " '" + RewardID + "'"
                    + ",'" + rs.Rows[i]["NoKontrak"].ToString() + "'"
                    + ",'" + rs.Rows[i]["NoUnit"].ToString() + "'"
                    + ", " + Convert.ToInt32(rs.Rows[i]["NoCustomer"])
                    + ",'" + rs.Rows[i]["Nama"].ToString() + "'"
                    );

                string strSql2 = "UPDATE MS_KONTRAK SET RewardID = '" + RewardID + "' WHERE NoKontrak = '" + rs.Rows[i]["NoKontrak"].ToString() + "'";
                Db.Execute(strSql2);
            }

            DataTable rsHeader = Db.Rs("SELECT "
                    + " NoReward"
                    + ",CONVERT(varchar,Tgl,106) AS [Tgl. Generate]"
                    + ",NoAgent AS [No. Agent]"
                    + ",NamaAgent AS [Nama Agent]"
                    + ",NoSkema AS [No. Skema CF]"
                    + ",NamaSkema AS [Nama Skema CF]"
                    + ",Rumus"
                    + ",CONVERT(varchar,PeriodeDari,106) AS [Periode Dari]"
                    + ",CONVERT(varchar,PeriodeSampai,106) AS [Periode Sampai]"
                    + ",Reward"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_REWARD "
                    + " WHERE NoReward = '" + RewardID + "'");

            DataTable rsDetail = Db.Rs("SELECT "
                + " CONVERT(VARCHAR, SN) "
                + " + '.  ' + NoKontrak "
                + " + '  ' + NoUnit "
                + " + '  ' + NamaCust  + ' (' + CONVERT(VARCHAR, NoCustomer) + ')  '"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_KOMISI_REWARD_DETAIL WHERE NoReward = '" + RewardID + "'");

            string Ket = Cf.LogCapture(rsHeader)
                + Cf.LogList(rsDetail, "DETAIL");

            Db.Execute("EXEC spLogKomisiReward"
                + " 'DAFTAR'"
                + ",'" + Act.UserID + "'"
                + ",'" + Act.IP + "'"
                + ",'" + Ket + "'"
                + ",'" + RewardID + "'"
                );

            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_REWARD_LOG ORDER BY LogID DESC");
            Db.Execute("UPDATE MS_KOMISI_REWARD_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            skema.Items.Clear();
            periode.InnerText = "";
            Bind();
        }

    }
}

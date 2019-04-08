using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class KomisiDel : System.Web.UI.Page
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

            Fill();
        }

        private void Bind()
        {
            dari.Text = Cf.Day(DateTime.Today);
            sampai.Text = Cf.Day(DateTime.Today);

            DataTable rs;
            string strSql;

            strSql = "SELECT * FROM MS_AGENT WHERE Status = 'A' AND Project = '" + project.SelectedValue + "' ORDER BY Nama";
            rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["NoAgent"].ToString();
                string t = v + " - " + rs.Rows[i]["Nama"].ToString();
                sales.Items.Add(new ListItem(t, v));
            }
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

            string w = "";
            if (sales.SelectedIndex != 0)
                w = " AND a.NoAgent = '" + sales.SelectedValue + "'";

            string strSql = "SELECT a.*, b.Nama AS NamaAgent, b.SalesTipe, c.Nama AS NamaCust"
                + " FROM MS_KONTRAK a"
                + " INNER JOIN MS_AGENT b ON a.NoAgent = b.NoAgent"
                + " INNER JOIN MS_CUSTOMER c ON a.NoCustomer = c.NoCustomer"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,a.TglKontrak,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,a.TglKontrak,112) <= '" + Cf.Tgl112(Sampai) + "'"
                + " AND a.Status = 'A' AND a.KomisiID != ''"
                + w
                + " AND a.Project = '" + project.SelectedValue + "'"
                + " ORDER BY NoKontrak";

            DataTable rs = Db.Rs(strSql);
            del.Enabled = false;
            Rpt.NoData(list, rs, "Tidak terdapat kontrak dengan kriteria seperti tersebut diatas.");

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                DateTime TglKontrak = Convert.ToDateTime(rs.Rows[i]["TglKontrak"]);

                bool tampil = true;
                int kp = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISIP_DETAIL WHERE NoKomisi = '" + rs.Rows[i]["KomisiID"].ToString() + "'");
                if (kp != 0)
                {
                    tampil = false;
                }

                if (tampil)
                {
                    TableRow r = new TableRow();
                    TableCell c;

                    r.VerticalAlign = VerticalAlign.Top;

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NamaAgent"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoKontrak"].ToString();
                    c.ID = "komisiid_" + index;
                    c.Attributes["title"] = rs.Rows[i]["KomisiID"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoUnit"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NamaCust"].ToString();
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    string Skema = "", DasarHitung = "";
                    DataTable rsSkema = Db.Rs("SELECT * FROM REF_SKOM WHERE " + Cf.Tgl112(TglKontrak) + " >= CONVERT(VARCHAR,Dari,112) AND " + Cf.Tgl112(TglKontrak) + " <= CONVERT(VARCHAR,Sampai,112) AND SalesTipe = " + Convert.ToInt32(rs.Rows[i]["SalesTipe"]) + " AND Inaktif = 0");
                    if (rsSkema.Rows.Count > 0)
                    {
                        Skema = rsSkema.Rows[0]["Nama"].ToString();
                        DasarHitung = rsSkema.Rows[0]["DasarHitung"].ToString();
                    }

                    c = new TableCell();
                    c.Text = Skema;
                    c.HorizontalAlign = HorizontalAlign.Left;
                    r.Cells.Add(c);

                    decimal Nilai = 0;
                    if (DasarHitung != "")
                    {
                        Nilai = DasarHitung == "DPP" ? Convert.ToDecimal(rs.Rows[i]["NilaiDPP"]) : Convert.ToDecimal(rs.Rows[i]["NilaiKontrak"]);
                    }

                    c = new TableCell();
                    c.Text = Cf.Num(Nilai);
                    c.CssClass = "right";
                    r.Cells.Add(c);

                    Rpt.Border(r);
                    list.Controls.Add(r);

                    index++;
                    del.Enabled = true;
                }
            }
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            Fill();
        }
        protected void del_Click(object sender, EventArgs e)
        {
            int index = 0;
            foreach (Control tr in list.Controls)
            {
                TableCell komisiid = (TableCell)list.FindControl("komisiid_" + index);

                int kp = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISIP_DETAIL WHERE NoKomisi = '" + komisiid.Attributes["title"] + "'");
                if (kp == 0)
                {
                    DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI WHERE NoKomisi = '" + komisiid.Attributes["title"] + "'");
                    if (rs.Rows.Count == 0)
                        Response.Redirect("/CustomError/Deleted.html");
                    else
                    {
                        string Project = Db.SingleString("SELECT Project FROM MS_KOMISI WHERE NoKomisi = '" + rs.Rows[0]["NoKomisi"].ToString() + "'");
                        string Ket = "***Alasan Delete :<br>" + Cf.Str(alasan.Text)
                            + "<br><br>***Data Sebelum Delete :<br>"
                            + Cf.LogCapture(rs);

                        Db.Execute("EXEC spKomisiDel '" + rs.Rows[0]["NoKomisi"].ToString() + "'");

                        int c = Db.SingleInteger(
                            "SELECT COUNT(*) FROM MS_KOMISI WHERE NoKomisi = '" + rs.Rows[0]["NoKomisi"].ToString() + "'");

                        if (c == 0)
                        {
                            //Log
                            Db.Execute("EXEC spLogKomisi "
                                + " 'DELETE'"
                                + ",'" + Act.UserID + "'"
                                + ",'" + Act.IP + "'"
                                + ",'" + Ket + "'"
                                + ",'" + rs.Rows[0]["NoKomisi"].ToString() + "'"
                                );

                            decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_LOG ORDER BY LogID DESC");
                            Db.Execute("UPDATE MS_KOMISI_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                        }
                    }
                }

                index++;
            }

            Response.Redirect("KomisiDel.aspx");
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
            sales.Items.Clear();
            sales.Items.Add(new ListItem("Sales : "));
            Bind();
        }
    }
}

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace ISC064.KOMISI
{
    public partial class KomisiRDel : System.Web.UI.Page
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

            string strSql = "SELECT a.NoKomisiR, a.SN, a.SN_KomisiTermin, a.Nilai, c.NoKontrak, c.NoUnit, c.NamaAgent, c.NamaCust, a.NoKomisi, b.NoKomisiP"
                + " FROM MS_KOMISIR_DETAIL a"
                + " INNER JOIN MS_KOMISIR b ON a.NoKomisiR = b.NoKomisiR"
                + " INNER JOIN MS_KOMISI c ON a.NoKomisi = c.NoKomisi"
                //+ " INNER JOIN MS_KOMISI_TERM d ON a.NoKomisi = d.NoKomisi"
                + " WHERE 1=1 "
                + " AND CONVERT(varchar,b.Tgl,112) >= '" + Cf.Tgl112(Dari) + "'"
                + " AND CONVERT(varchar,b.Tgl,112) <= '" + Cf.Tgl112(Sampai) + "'"
                //+ " AND (SELECT COUNT(*) FROM MS_KOMISIR_DETAIL WHERE NoKomisi = a.NoKomisi AND SN_KomisiTermin = a.SN_KomisiTermin) = 0"
                + " AND b.Project = '" + project.SelectedValue + "'"
                + " ORDER BY a.NoKomisiR";

            DataTable rs = Db.Rs(strSql);
            Rpt.NoData(list, rs, "Tidak terdapat pengajuan dengan kriteria seperti tersebut diatas.");
            del.Enabled = false;

            int index = 0;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                HtmlTableRow r = new HtmlTableRow();
                HtmlTableCell c;
                CheckBox cb;

                //cb = new CheckBox();
                //cb.ID = "cb_" + index;
                //cb.Attributes["title"] = rs.Rows[i]["NoKomisi"] + ";" + rs.Rows[i]["SN_KomisiTermin"];

                //c = new HtmlTableCell();
                //c.Controls.Add(cb);
                //r.Cells.Add(c);

                cb = new CheckBox();
                cb.ID = "cb_" + index;
                cb.Attributes["title"] = rs.Rows[i]["NoKomisiR"].ToString();

                c = new HtmlTableCell();
                c.Controls.Add(cb);
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoKomisi"].ToString();
                r.Cells.Add(c);

                string NamaAgent = Db.SingleString("SELECT ISNULL(NamaAgent,'') FROM MS_KOMISI_TERM WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"].ToString() + "' AND SN = " + Convert.ToInt32(rs.Rows[i]["SN_KomisiTermin"]));
                string Termin = Db.SingleString("SELECT ISNULL(Nama,'') FROM MS_KOMISI_TERM WHERE NoKomisi = '" + rs.Rows[i]["NoKomisi"].ToString() + "' AND SN = " + Convert.ToInt32(rs.Rows[i]["SN_KomisiTermin"]));

                c = new HtmlTableCell();
                c.InnerHtml = NamaAgent;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NamaCust"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoKontrak"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = rs.Rows[i]["NoUnit"].ToString();
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Termin;
                r.Cells.Add(c);

                c = new HtmlTableCell();
                c.InnerHtml = Cf.Num(Convert.ToDecimal(rs.Rows[i]["Nilai"]));
                c.Attributes["class"] = "right";
                r.Cells.Add(c);

                list.Controls.Add(r);

                index++;
                del.Enabled = true;
            }
        }

        protected void display_Click(object sender, System.EventArgs e)
        {
            //Fill();
        }

        protected void delbtn_Click(object sender, EventArgs e)
        {
            int index = 0;
            foreach (Control tr in list.Controls)
            {
                CheckBox cb = (CheckBox)list.FindControl("cb_" + index);
                if (cb.Checked)
                {
                    int cfp = Db.SingleInteger("SELECT COUNT(*) FROM MS_KOMISIR_DETAIL WHERE NoKomisiR = '" + cb.Attributes["title"] + "'");
                    if (cfp > 0)
                    {
                        DataTable rs = Db.Rs("SELECT * FROM MS_KOMISIR WHERE NoKomisiR = '" + cb.Attributes["title"] + "'");
                        if (rs.Rows.Count == 0)
                            Response.Redirect("/CustomError/Deleted.html");
                        else
                        {
                            string Ket = "***Alasan Delete :<br>" + Cf.Str(alasan.Text)
                                + "<br><br>***Data Sebelum Delete :<br>"
                                + Cf.LogCapture(rs);

                            Db.Execute("EXEC spKomisiRDel '" + rs.Rows[0]["NoKomisiR"].ToString() + "'");

                            int c = Db.SingleInteger(
                            "SELECT COUNT(*) FROM MS_KOMISIR WHERE NoKomisiR = '" + rs.Rows[0]["NoKomisiR"].ToString() + "'");

                            if (c > 0)
                            {
                                //Log
                                Db.Execute("EXEC spLogKomisiR"
                                + " 'DELETE'"
                                + ",'" + Act.UserID + "'"
                                + ",'" + Act.IP + "'"
                                + ",'" + Ket + "'"
                                + ",'" + rs.Rows[0]["NoKomisiR"].ToString() + "'"
                                );

                                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISIR_LOG ORDER BY LogID DESC");
                                string Project = Db.SingleString("SELECT Project FROM MS_KOMISIR_LOG WHERE NoKomisiP = " + rs.Rows[0]["NoKomisiR"].ToString());
                                Db.Execute("UPDATE MS_KOMISIR_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                            }
                        }
                    }
                }

                index++;
            }
            Response.Redirect("KomisiRDel.aspx");
        }

        private string NoKomisiR
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKomisiR"]);
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

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill();
        }
    }
}
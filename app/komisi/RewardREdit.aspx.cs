using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ISC064.KOMISI
{
    public partial class RewardREdit : System.Web.UI.Page
    {
        protected DataTable rs;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                FillHeader();
            }

            if (!Page.IsPostBack)
            {
                FillTable();
            }
        }
        private void FillHeader()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_KOMISI_REWARD_R_LOG&Pk=" + Nomor.PadLeft(5, '0') + "'";

            DataTable rsHeader = Db.Rs("SELECT * FROM MS_KOMISI_Reward_R WHERE NoRR = '" + Nomor + "'");
            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                ket.Text = rsHeader.Rows[0]["Ket"].ToString();
                tgl.Text = Cf.Day(rsHeader.Rows[0]["Tgl"]);
                project.Text = rsHeader.Rows[0]["Project"].ToString();
            }
        }

        private void FillTable()
        {
            DataTable rs = Db.Rs("SELECT a.Reward, a.NoReward, b.* FROM MS_KOMISI_REWARD_R_DETAIL a"
                + " INNER JOIN MS_KOMISI_REWARD b ON a.NoReward = b.NoReward"
                + " WHERE a.NoRR = '" + Nomor + "'"
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

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NamaAgent"].ToString();
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["NoSkema"] + " (" + rs.Rows[i]["NamaSkema"] + ")";
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Day(Convert.ToDateTime(rs.Rows[i]["PeriodeDari"])) + " s/d " + Cf.Day(Convert.ToDateTime(rs.Rows[i]["PeriodeSampai"]));
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = rs.Rows[i]["Reward"].ToString();
                    tr.Cells.Add(c);

                    tb.Rows.Add(tr);
                }
            }
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            if (!Cf.isTgl(tgl.Text))
            {
                x = false;
                if (s == "") s = tgl.ID;
                tglc.Text = "Tanggal";
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );
            }

            return x;
        }
        private bool Save()
        {
            if (valid())
            {
                DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_REWARD_R WHERE NoRR = '" + Nomor + "'");
                if (rs.Rows.Count > 0)
                {
                    DataTable rsBef = Db.Rs("SELECT [Tgl] as [Tgl. Realisasi] "
                                          + " ,[Ket] as [Keterangan]"
                                          + " FROM MS_KOMISI_REWARD_R where NoRR = '" + Nomor + "'");

                    DateTime Tgl = Convert.ToDateTime(tgl.Text);

                    Db.Execute("EXEC spKomisiRewardREdit"
                        + " '" + Nomor + "'"
                        + ",'" + Tgl + "'"
                        + ",'" + ket.Text + "'"
                        );

                    DataTable rsAft = Db.Rs("SELECT [Tgl] as [Tgl. Realisasi] "
                                          + " ,[Ket] as [Keterangan]"
                                          + " FROM MS_KOMISI_REWARD_R WHERE NoRR = '" + Nomor + "'");

                    string Ket = "Kode Realisasi : " + Nomor + "<br>"
                        + Cf.LogCompare(rsBef, rsAft);

                    Db.Execute("EXEC spLogKomisiRewardR"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + Nomor + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KOMISI_REWARD_R_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KOMISI_REWARD_R WHERE NoRP = " + Nomor);
                    Db.Execute("UPDATE MS_KOMISI_REWARD_R_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    Js.Close(this);

                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.Close(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (Save()) Response.Redirect("RewardREdit.aspx?Nomor=" + Nomor + "&done=1");
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace ISC064.KOMISI
{
    public partial class CFPEdit : System.Web.UI.Page
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
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_KOMISI_CFP_LOG&Pk=" + Nomor.PadLeft(5, '0') + "'";
            btndel.Attributes["onclick"] = "location.href='CFPDel.aspx?NoCFP=" + Nomor + "'";

            DataTable rsHeader = Db.Rs("SELECT * FROM MS_KOMISI_CFP WHERE NoCFP = '" + Nomor + "'");
            if (rsHeader.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                ket.Text = rsHeader.Rows[0]["Ket"].ToString();
                tgl.Text = Cf.Day(rsHeader.Rows[0]["Tgl"]);
                project.Text = rsHeader.Rows[0]["Project"].ToString();
                tipe.Text = Db.SingleString("select Tipe from REF_AGENT_TIPE where ID = '" + rsHeader.Rows[0]["SalesTipe"] + "'");
            }
        }

        private void FillTable()
        {
            DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_CFP_DETAIL WHERE NoCFP = '" + Nomor + "'");
            if (rs.Rows.Count > 0)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string Cust = "", NoKontrak = "", NoUnit = "";
                    string Sales = Db.SingleString("SELECT ISNULL(NamaAgent, '') FROM MS_KOMISI_CF_DETAIL WHERE NoCF = '" + rs.Rows[i]["NoCF"] + "' AND SN = " + Convert.ToInt32(rs.Rows[i]["SN_NoCF"]));
                    DataTable dd = Db.Rs("SELECT * FROM MS_KOMISI_CF WHERE NoCF = '" + rs.Rows[i]["NoCF"].ToString() + "'");
                    if (dd.Rows.Count > 0)
                    {
                        Cust = dd.Rows[0]["NamaCust"].ToString();
                        NoKontrak = dd.Rows[0]["NoKontrak"].ToString();
                        NoUnit = dd.Rows[0]["NoUnit"].ToString();
                    }

                    TableCell c;
                    TableRow tr;

                    tr = new TableRow();

                    c = new TableCell();
                    c.Text = (i + 1).ToString() + ".";
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Sales;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cust;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = NoKontrak;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = NoUnit;
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Db.SingleString("select NamaSkema from MS_KOMISI_CF where NoCF = '" + rs.Rows[i]["NoCF"] + "'");
                    tr.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["Nilai"]);
                    c.CssClass = "right";
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
                DataTable rs = Db.Rs("SELECT * FROM MS_KOMISI_CFP WHERE NoCFP = '" + Nomor + "'");
                if(rs.Rows.Count > 0)
                {
                    DataTable rsBef = Db.Rs("SELECT [Tgl] as [Tgl. Pengajuan] "
                                          + " ,[Ket] as [Keterangan]"
                                          + " FROM MS_KOMISI_CFP where NoCFP = '" + Nomor + "'");

                    DateTime Tgl = Convert.ToDateTime(tgl.Text);

                    Db.Execute("EXEC spKomisiCFPEdit"
                        + " '" + Nomor + "'"
                        + ",'" + Tgl + "'"
                        + ",'" + ket.Text + "'"
                        );

                    DataTable rsAft = Db.Rs("SELECT [Tgl] as [Tgl. Pengajuan] "
                                          + " ,[Ket] as [Keterangan]"
                                          + " FROM MS_KOMISI_CFP WHERE NoCFP = '" + Nomor + "'");

                    string Ket = "Kode Pengajuan : " + Nomor + "<br>"
                        + Cf.LogCompare(rsBef, rsAft);

                    Db.Execute("EXEC spLogKomisiCFP"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + Nomor + "'"
                        );

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
            if (Save()) Response.Redirect("CFPEdit.aspx?Nomor=" + Nomor + "&done=1");
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

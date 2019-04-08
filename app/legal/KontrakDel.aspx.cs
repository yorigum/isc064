using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LEGAL
{
    public partial class KontrakDel : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                if (Func.CekAkunting(NoKontrak))
                {
                    warning.Text = "Transaksi sudah pernah diposting ke Akunting";
                    delbtn.Enabled = false;
                }
                else
                    warning.Text = "";

                if (Func.CekTTS(NoKontrak) || Func.CekMemo(NoKontrak))
                {
                    warning.Text = "Sebagian tagihan dari kontrak ini sudah dibayar";
                    delbtn.Enabled = false;
                }
                else
                    warning.Text = "";

                Js.Focus(this, ket);
                Js.Confirm(this,
                    "Apakah anda ingin menghapus kontrak : " + NoKontrak + " ?\\n"
                    + "Perhatian bahwa data akan dihapus secara PERMANEN."
                    );
            }
        }

        protected void delbtn_Click(object sender, System.EventArgs e)
        {
            DataTable rs = Db.Rs(
                "SELECT * FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                    + "<br><br>***Data Sebelum Delete :<br>";
                //+ Cf.LogCapture(rs);
                string Peta = Db.SingleString("SELECT Peta "
                    + " FROM MS_UNIT INNER JOIN MS_KONTRAK ON MS_UNIT.NoStock = MS_KONTRAK.NoStock "
                    + " WHERE NoKontrak = '" + NoKontrak + "'");

                Db.Execute("EXEC spKontrakDel '" + NoKontrak + "'");

                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");

                if (c == 0)
                {
                    //redraw floor plan
                    Func.GenerateFP(Peta);

                    //Log
                    Db.Execute("EXEC spLogKontrak "
                        + " 'DELETE'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + NoKontrak + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_KONTRAK_LOG ORDER BY LogID DESC");
                    string Project = Db.SingleString("SELECT Project FROM MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                    Db.Execute("UPDATE MS_KONTRAK_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                    Js.Close(this);
                }
                else
                {
                    //Tidak bisa dihapus
                    frm.Visible = false;
                    nodel.Visible = true;
                }
            }
        }

        private string NoKontrak
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoKontrak"]);
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

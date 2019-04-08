//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
//// in file 'App_Code\Migrated\Stub_Acc_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'Acc.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
    public partial class Acc : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                FillTable();
                Js.Focus(this, acc);
                Js.NumberFormat(saldoawal);
            }

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditAcc('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
            }
        }

        private void FillTable()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE Project IN (" + Act.ProjectListSql + ")");
            Rpt.NoData(sb, rs, "<font style='font:8pt'>Daftar Account belum di-setup.</font>");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                sb.Append("<li>"
                    + "<a href=\"javascript:popEditAcc('" + rs.Rows[i]["Acc"].ToString() + ";" + rs.Rows[i]["SubID"].ToString() + "')\">"
                    + rs.Rows[i]["Acc"] + " " + rs.Rows[i]["Rekening"] + " " + rs.Rows[i]["Bank"]
                    + "</a>"
                    + "</li>"
                    );
            }

            list.InnerHtml = sb.ToString();
        }

        private bool unik()
        {
            bool x = true;

            string Acc = Cf.Pk(acc.Text);
            int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE"
                + " Acc = '" + Acc + "'"
                + " AND SubID='" + subid.Text + "'"
                );

            if (c != 0)
                x = false;

            return x;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            //rekening
            acc.Text = Cf.Pk(acc.Text);

            x = Cf.ValidMandatory(this, "Rekening",project.SelectedValue);

            decimal jum = Db.SingleDecimal("SELECT COUNT(*) FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE ACC='" + acc.Text + "' AND SubID='" + subid.Text + "'");

            if (jum != 0)
            {
                x = false;
            }

            decimal Bank = Db.SingleDecimal("SELECT ISNULL(COUNT(*),0) FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE BANK='" + bank.Text + "' AND Project = '"+project.SelectedValue+"'");
            if (Bank > 0)
            {
                x = false;
                bankc.Text = "Nama Bank sudah digunakan";
            }
            else
            {
                bankc.Text = "";
            }

            if (!x)
            {
                if (!x)
                    Js.Alert(
                        this
                        , "Input Tidak Valid.\\n\\n"
                        + "Aturan Proses :\\n"
                        + "1. No. Account tidak boleh kosong dan tidak boleh duplikat.\\n"
                        + "2. Saldo Awal harus berupa angka.\\n"
                        + "3. Sudah terdapat rekening yang memiliki subid serta account yang sama.\\n"
                        + "4. Nama bank tersebut sudah digunakan.\\n"
                        , "document.getElementById('" + s + "').focus();"
                        + "document.getElementById('" + s + "').select();"
                        );
            }

            return x;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                string Acc = Cf.Pk(acc.Text);
                string Rekening = Cf.Str(rekening.Text);
                string Bank = Cf.Str(bank.Text);
                string Cabang = Cf.Str(cabang1.Text);
                string AtasNama = Cf.Str(atasnama.Text);
                decimal SaldoAwal = 0;
                if(saldoawal.Text != "") SaldoAwal = Convert.ToDecimal(saldoawal.Text);
                string Project = Cf.Pk(project.SelectedValue);

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spAccBaru"
                    + " '" + Acc + "'"
                    + ",'" + Rekening + "'"
                    + ",'" + Bank + "'"
                    + ",'" + Cabang + "'"
                    + ",'" + AtasNama + "'"
                    + ", " + SaldoAwal
                    + ",'" + subid.Text + "'"
                    + ",'" + Project + "'"
                    );

                DataTable rs = Db.Rs("SELECT "
                    + " Acc AS [No. Account]"
                    + ",Rekening"
                    + ",SubID"
                    + ",Project"
                    + ",Bank"
                    + ",AtasNama AS [Atas Nama]"
                    + ",SaldoAwal AS [Saldo Awal]"
                    + " FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC "
                    + " WHERE Acc  = '" + Acc + "'");

                string KetLog = Cf.LogCapture(rs);

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogAcc"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + Acc + ";" + subid.Text + "'"
                    );

                Response.Redirect("Acc.aspx?done=" + Acc + ";" + subid.Text);

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

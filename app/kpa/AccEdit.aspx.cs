using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA
{
    public partial class AccEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Fill();
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
                        + "Edit Berhasil...";
            }
        }

        private void Fill()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=REF_BANKKPA_LOG&Pk=" + KodeBank + "&project=" + Project + "'";
            btndel.Attributes["onclick"] = "location.href='AccDel.aspx?Kode=" + KodeBank + "&project=" + Project + "'";

            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BANKKPA WHERE KodeBank = '" + KodeBank + "' AND Project = '" + Project + "'");
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                kodebank.Text = rs.Rows[0]["KodeBank"].ToString();
                bank.Text = rs.Rows[0]["Bank"].ToString();
                project.SelectedValue = rs.Rows[0]["Project"].ToString();
            }
        }

        private bool unik()
        {
            bool x = true;

            int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BANKKPA WHERE"
                + " KodeBank <> '" + KodeBank + "'"
                + " AND KodeBank = '" + Cf.Pk(kodebank.Text) + "'"
                );

            if (c != 0)
                x = false;

            return x;
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            //acc
            if (Cf.isEmpty(kodebank))
            {
                x = false;
                if (s == "") s = kodebank.ID;
                kodebankc.Text = "Kosong";
            }
            else
            {
                if (!unik())
                {
                    x = false;
                    if (s == "") s = kodebankc.ID;
                    kodebankc.Text = "Duplikat";
                }
                else
                    kodebankc.Text = "";
            }

            if (!x)
            {
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Kode Bank tidak boleh kosong dan tidak boleh duplikat.\\n"
                    + "2. Nama Bank tidak boleh kosong.\\n"
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
                string KodeBankBaru = Cf.Pk(kodebank.Text);
                string Bank = Cf.Str(bank.Text);

                DataTable rsBef = Db.Rs("SELECT "
                    + " KodeBank AS [Kode Bank]"
                    + ",Bank"
                    + ",Project"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BANKKPA "
                    + " WHERE KodeBank = '" + KodeBank + "' AND Project = '" + Project + "'");

                Db.Execute("EXEC spBankKPAEdit"
                    + " '" + KodeBank + "'"
                    + ",'" + KodeBankBaru + "'"
                    + ",'" + Bank + "'"
                    + ",'" + Project + "'"
                    );

                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_BANKKPA SET Project = '" + project.SelectedValue + "' WHERE KodeBank = '" + KodeBankBaru + "' AND Project = '" + Project + "'");

                DataTable rsAft = Db.Rs("SELECT "
                    + " KodeBank AS [Kode Bank]"
                    + ",Bank"
                    + ",Project"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BANKKPA "
                    + " WHERE KodeBank = '" + KodeBankBaru + "' AND Project = '" + project.SelectedValue + "'");

                /*UPDATE Referensi Bank KPA di MS_KONTRAK*/
                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK SET BankKPR='" + KodeBankBaru + "' WHERE BankKPR='" + KodeBank + "'");

                string KetLog = Cf.LogCompare(rsBef, rsAft);

                if (KodeBank != KodeBankBaru)
                {
                    Db.Execute("EXEC spLogBankKPA"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + KodeBankBaru + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_BANKKPA_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE REF_BANKKPA_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                }
                else
                {
                    Db.Execute("EXEC spLogBankKPA"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + KodeBank + "'"
                        );

                    decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_BANKKPA_LOG ORDER BY LogID DESC");
                    Db.Execute("UPDATE REF_BANKKPA_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);

                }

                return true;
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
            string KodeBankBaru = Cf.Pk(kodebank.Text);
            if (Save()) Response.Redirect("AccEdit.aspx?Kode=" + KodeBankBaru + "&done=1&project=" + project.SelectedValue);
        }

        private string KodeBank
        {
            get
            {
                return Cf.Pk(Request.QueryString["Kode"]);
            }
        }

        private string Project
        {
            get
            {
                return Cf.Pk(Request.QueryString["project"]);
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

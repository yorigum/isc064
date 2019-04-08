using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.KPA
{
    public partial class Acc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                FillTable();
                Js.Focus(this, kodebank);
            }
            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                {

                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditAcc('" + Request.QueryString["done"] + "','" + Request.QueryString["project"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
                    project.SelectedValue = Request.QueryString["project"];
                    FillTable();
                }
            }
        }

        private void FillTable()
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BANKKPA WHERE Project = '" + project.SelectedValue + "'");
            Rpt.NoData(sb, rs, "<font style='font:8pt'>Daftar Account belum di-setup.</font>");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                sb.Append("<li>"
                    + "<a href=\"javascript:popEditBankKPA('" + rs.Rows[i]["KodeBank"].ToString() + "','" + rs.Rows[i]["Project"].ToString() + "')\">"
                    + rs.Rows[i]["KodeBank"] + " - " + rs.Rows[i]["Bank"]
                    + "</a>"
                    + "</li>"
                    );
            }

            list.InnerHtml = sb.ToString();

        }

        private bool unik()
        {
            bool x = true;

            string Kode = Cf.Pk(kodebank.Text);
            int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BANKKPA WHERE"
                + " KodeBank = '" + Kode + "'"
                + " AND Project = '" + project.SelectedValue + "'"
                );

            if (c != 0)
                x = false;

            return x;
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            kodebank.Text = Cf.Pk(kodebank.Text);

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
                    if (s == "") s = kodebank.ID;
                    kodebankc.Text = "Duplikat";
                }
                else
                    kodebankc.Text = "";
            }

            if (Cf.isEmpty(bank))
            {
                x = false;
                if (s == "") s = bank.ID;
                bankc.Text = "Kosong";
            }

            if (!x)
            {
                if (!x)
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

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                string KodeBank = Cf.Pk(kodebank.Text);
                string Bank = Cf.Str(bank.Text);

                Db.Execute("EXEC spBankKPABaru"
                    + " '" + KodeBank + "'"
                    + ",'" + Bank + "'"
                    + ",'" + project.SelectedValue + "'"
                    );

                DataTable rs = Db.Rs("SELECT "
                    + " KodeBank AS [Kode Bank]"
                    + ",Bank"
                    + ",Project"
                    + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_BANKKPA "
                    + " WHERE KodeBank  = '" + KodeBank + "' AND Project = '" + project.SelectedValue + "'");

                string KetLog = Cf.LogCapture(rs);

                Db.Execute("EXEC spLogBankKPA"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + KodeBank + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_BANKKPA_LOG ORDER BY LogID DESC");
                Db.Execute("UPDATE REF_BANKKPA_LOG SET Project = '" + project.SelectedValue + "' WHERE LogID  = " + LogID);


                Response.Redirect("Acc.aspx?done=" + KodeBank + "&project=" + project.SelectedValue);
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTable();
        }
    }
}

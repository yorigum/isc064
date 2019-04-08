using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class TransferAnonimRegistrasi : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Js.Focus(this, tgl);
                tgl.Text = Cf.Day(DateTime.Today);

                fillAcc();
                Act.ProjectList(project);
                nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                nilai.Attributes["onblur"] = "CalcBlur(this);";
            }

            FeedBack();
        }

        private void fillAcc()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_ACC WHERE Project ='" + project.SelectedValue + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["Acc"].ToString();
                string t = v + " : " + rs.Rows[i]["Bank"] + " " + rs.Rows[i]["Rekening"];
                ddlAcc.Items.Add(new ListItem(t, v));
            }

        }
        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditAnonim('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
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

            if (ddlAcc.SelectedValue == "")
            {
                x = false;
                if (s == "") s = ddlAcc.ID;
                bankc.Text = "Kosong";
            }
            else
                bankc.Text = "";

            if (!Cf.isMoney(nilai))
            {
                x = false;
                if (s == "") s = nilai.ID;
                nilaic.Text = "Angka";
            }
            else
                nilaic.Text = "";

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Format Tanggal : Bulan / Tanggal / Tahun.\\n"
                    + "2. Bank tidak boleh kosong.\\n"
                    + "3. Nilai harus berupa angka.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                DateTime Tgl = Convert.ToDateTime(tgl.Text);
                string Bank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + ddlAcc.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");
                string Rekening = Db.SingleString("SELECT Rekening FROM REF_ACC WHERE Acc = '" + ddlAcc.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");
                decimal Nilai = Convert.ToDecimal(nilai.Text);
                string Ket = Cf.Str(ket.Text);

                Db.Execute("EXEC spTransferAnonim"
                    + " '" + Tgl + "'"
                    + ",'" + Bank + "'"
                    + ", " + Nilai
                    + ",'" + Ket + "'"
                    );

                //get nomor terbaru
                int NoAnonim = Db.SingleInteger(
                    "SELECT TOP 1 NoAnonim FROM MS_ANONIM ORDER BY NoAnonim DESC");

                //update project & namaproject
                string NamaProject = Db.SingleString("SELECT Nama FROM " + Mi.DbPrefix + "SECURITY..REF_PROJECT WHERE Project = '" + project.SelectedValue + "'");
                Db.Execute("UPDATE MS_ANONIM SET Project = '" + project.SelectedValue + "', NamaProject='" + NamaProject + "',AccountID = '" + ddlAcc.SelectedValue + "'"
                    + ",Rekening = '" + Rekening + "' WHERE NoAnonim ='" + NoAnonim + "'");

                DataTable rs = Db.Rs("SELECT "
                    + " NoAnonim AS [No.]"
                    + ",CONVERT(varchar,Tgl,106) AS Tgl"
                    + ",Bank"
                    + ",Nilai"
                    + ",Ket"
                    + " FROM MS_ANONIM"
                    + " WHERE NoAnonim = " + NoAnonim
                    );

                Db.Execute("EXEC spLogAnonim"
                    + " 'REGIS'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Cf.LogCapture(rs) + "'"
                    + ",'" + NoAnonim.ToString().PadLeft(5, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_ANONIM_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_ANONIM WHERE NoAnonim = '" + NoAnonim + "'");
                Db.Execute("UPDATE MS_ANONIM_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

                Response.Redirect("TransferAnonimRegistrasi.aspx?done=" + NoAnonim);

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
            ddlAcc.Items.Clear();
            fillAcc();
        }
    }
}
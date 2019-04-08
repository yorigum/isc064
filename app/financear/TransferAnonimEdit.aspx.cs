using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
{
    public partial class TransferAnonimEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            Act.CekInt("NoAnonim");

            if (!Act.Sec("ED:" + Request.PhysicalPath))
            {
                ok.Enabled = false;
                save.Enabled = false;
            }

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                Js.Focus(this, nokontrak);
                Bind();
                Fill();

                nilai.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                nilai.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                nilai.Attributes["onblur"] = "CalcBlur(this);";
            }

            FeedBack();
        }

        private void Bind()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_ACC WHERE Project = '" + Project + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                bank.Items.Add(new ListItem(rs.Rows[i]["Acc"].ToString() + " " + rs.Rows[i]["Bank"].ToString() + " - " + rs.Rows[i]["Rekening"].ToString(), rs.Rows[i]["Acc"].ToString()));
            }
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
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=MS_ANONIM_LOG&Pk=" + NoAnonim.ToString().PadLeft(5, '0') + "'";
            btndel.Attributes["onclick"] = "location.href='TransferAnonimDel.aspx?NoAnonim=" + NoAnonim + "'";

            string strSql = "SELECT * "
                + " FROM MS_ANONIM WHERE NoAnonim = " + NoAnonim;
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                no.InnerHtml = NoAnonim.ToString().PadLeft(5, '0');

                tgl.Text = Cf.Day(rs.Rows[0]["Tgl"]);
                bank.SelectedValue = rs.Rows[0]["AccountID"].ToString();
                nilai.Text = Cf.Num(rs.Rows[0]["Nilai"]);

                nokontrak.Text = rs.Rows[0]["NoKontrak"].ToString();
                customer.Text = rs.Rows[0]["Customer"].ToString();
                unit.Text = rs.Rows[0]["Unit"].ToString();
                ket.Text = rs.Rows[0]["Ket"].ToString();
                project.SelectedValue = rs.Rows[0]["Project"].ToString();
                if (rs.Rows[0]["Status"].ToString() == "ID")
                {
                    trunit.Visible = trcs.Visible = true;
                }
                else
                    trunit.Visible = trcs.Visible = true;



                //kalo udah solve(udah dipake anonimnya) ga bisa ganti customer
                if (rs.Rows[0]["Status"].ToString() == "S")
                {
                    nokontrak.Enabled = false;
                    customer.Enabled = false;
                    btnpop.Disabled = true;
                }
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

        private bool Save()
        {
            if (valid())
            {
                DateTime Tgl = Convert.ToDateTime(tgl.Text);
                string Bank = Db.SingleString("SELECT Bank FROM REF_ACC WHERE Acc = '" + bank.SelectedValue + "'");
                string Rekening = Db.SingleString("SELECT Rekening FROM REF_ACC WHERE Acc = '" + bank.SelectedValue + "'");
                decimal Nilai = Convert.ToDecimal(nilai.Text);
                string Ket = Cf.Str(ket.Text);
                string NoKontrak = Cf.Str(nokontrak.Text);

                string Unit = Db.SingleString("SELECT NoUnit FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "'");
                string Customer = Db.SingleString("SELECT Nama FROM ISC064_MARKETINGJUAL..MS_CUSTOMER WHERE NoCustomer = (SELECT NoCustomer FROM ISC064_MARKETINGJUAL..MS_KONTRAK WHERE NoKontrak = '" + NoKontrak + "')");

                DataTable rs = Db.Rs("SELECT "
                    + " NoAnonim AS [No.]"
                    + " FROM MS_ANONIM"
                    + " WHERE NoAnonim = " + NoAnonim
                    );

                DataTable rsBef = Db.Rs("SELECT "
                    + " NoAnonim AS [No.]"
                    + ",CONVERT(varchar,Tgl,106) AS Tgl"
                    + ",AccountID"
                    + ",Bank"
                    + ",Rekening"
                    + ",Nilai"
                    + ",Unit"
                    + ",Customer"
                    + ",Ket AS [Keterangan Pembayaran]"
                    + ",Status"
                    + ",Project"
                    + " FROM MS_ANONIM"
                    + " WHERE NoAnonim = " + NoAnonim
                    );

                Db.Execute("UPDATE MS_ANONIM SET"
                    + " Tgl = '" + Tgl + "'"
                    + ",Nilai = " + Nilai
                    + ",AccountID = '" + bank.SelectedValue + "'"
                    + ",Bank = '" + Bank + "'"
                    + ",Rekening = '" + Rekening + "'"
                    + ",Unit = '" + Unit + "'"
                    + ",Customer = '" + Customer + "'"
                    + ",NoKontrak = '" + NoKontrak + "'"
                    + ",Ket = '" + Ket + "'"
                    + ",Project = '" + project.SelectedValue + "'"
                    + " WHERE NoAnonim = " + NoAnonim
                    );

                if (Unit != "" || Customer != "" || Ket != "")
                {
                    Db.Execute("UPDATE MS_ANONIM SET"
                        + " Status = 'ID'"
                        + " WHERE NoAnonim = " + NoAnonim
                        + " AND Status = 'BARU'"
                        );
                }

                DataTable rsAft = Db.Rs("SELECT "
                    + " NoAnonim AS [No.]"
                    + ",CONVERT(varchar,Tgl,106) AS Tgl"
                    + ",AccountID"
                    + ",Bank"
                    + ",Rekening"
                    + ",Nilai"
                    + ",NoKontrak"
                    + ",Unit"
                    + ",Customer"
                    + ",Ket AS [Keterangan Pembayaran]"
                    + ",Status"
                    + ",Project"
                    + " FROM MS_ANONIM"
                    + " WHERE NoAnonim = " + NoAnonim
                    );

                //Logfile
                string ketlog = Cf.LogCapture(rs)
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogAnonim"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + ketlog + "'"
                    + ",'" + NoAnonim.ToString().PadLeft(5, '0') + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM MS_ANONIM_LOG ORDER BY LogID DESC");
                string Project = Db.SingleString("SELECT Project FROM MS_ANONIM WHERE NoAnonim = '" + NoAnonim + "'");
                Db.Execute("UPDATE MS_ANONIM_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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
            if (Save()) Response.Redirect("TransferAnonimEdit.aspx?done=1&NoAnonim=" + NoAnonim);
        }

        private string NoAnonim
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoAnonim"]);
            }
        }

        private string Project
        {
            get
            {
                return Db.SingleString("SELECT Project FROM MS_ANONIM WHERE NoAnonim = '" + NoAnonim + "'");
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

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.FINANCEAR
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

                saldoawal.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
                saldoawal.Attributes["onkeyup"] = "CalcType(this,tempnum);";
                saldoawal.Attributes["onblur"] = "CalcBlur(this);";
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
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=" + Mi.DbPrefix + "FINANCEAR..REF_ACC_LOG&Pk=" + Acc + "'";
            btndel.Attributes["onclick"] = "location.href='AccDel.aspx?Acc=" + Acc + "'";

            string[] x = Cf.SplitByString(Acc, ";");

            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE Acc = '" + x[0] + "' AND SubID='" + x[1] + "'");
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else if (!Act.AksesProject(rs.Rows[0]["Project"].ToString()))
                Response.Redirect("/CustomError/SecLevel.html");
            else
            {
                acc.Text = rs.Rows[0]["Acc"].ToString();
                rekening.Text = rs.Rows[0]["Rekening"].ToString();
                subid.Text = rs.Rows[0]["SubID"].ToString();
                bank.Text = rs.Rows[0]["Bank"].ToString();
                cabang1.Text = rs.Rows[0]["Cabang"].ToString();
                atasnama.Text = rs.Rows[0]["AtasNama"].ToString();
                saldoawal.Text = Cf.Num(rs.Rows[0]["SaldoAwal"]);
                Cf.SelectedValue(project, rs.Rows[0]["Project"].ToString());
            }
        }

        private bool unik()
        {
            bool x = true;

            string[] y = Cf.SplitByString(Acc, ";");

            int c = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE"
                + " Acc <> '" + y[0] + "'"
                + " AND SubID <> '" + y[1] + "'"
                + " AND Acc = '" + Cf.Pk(acc.Text) + "'"
                + " AND SubID = '" + subid.Text + "'"
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
            Cf.ValidMandatory(this, "Rekening", project.SelectedValue);
            if (!unik())
            {
                x = false;
                //if (s == "") s = acc.ID;
                accc.Text = "Duplikat";
                subidc.Text = "Duplikat";
            }
            else
            {
                accc.Text = "";
                subidc.Text = "";
            }   


            if (!Cf.isMoney(saldoawal))
            {
                x = false;
                if (s == "") s = saldoawal.ID;
                saldoawalc.Text = "Angka";
            }
            else if (Cf.isEmpty(subid))
            {
                x = false;
                if (s == "") s = subid.ID;
                subidc.Text = "Kosong";
            }
            else
                saldoawalc.Text = "";

            string[] y = Cf.SplitByString(Acc, ";");
            string AccBaru = Cf.Pk(acc.Text);
            string SubID = Cf.Str(subid.Text);

            if (y[0] != AccBaru || y[1] != SubID)
            {
                decimal jum = Db.SingleDecimal("SELECT COUNT(*) FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE ACC='" + AccBaru + "' AND SubID='" + SubID + "'");

                if (jum != 0)
                {
                    x = false;
                }
            }
            decimal Bank = Db.SingleDecimal("SELECT ISNULL(COUNT(*),0) FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE BANK='" + bank.Text + "'");
            string BankBef = Db.SingleString("SELECT Bank FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC WHERE Acc='" + y[0] + "' and SUBID='" + y[1] + "'");
            if (Bank > 0 && BankBef != bank.Text)
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

        private bool Save()
        {
            if (valid())
            {
                string[] x = Cf.SplitByString(Acc, ";");
                string AccBaru = Cf.Pk(acc.Text);
                string Rekening = Cf.Str(rekening.Text);
                string SubID = Cf.Str(subid.Text);
                string Bank = Cf.Str(bank.Text);
                string Cabang = Cf.Str(cabang1.Text);
                string AtasNama = Cf.Str(atasnama.Text);
                decimal SaldoAwal = Convert.ToDecimal(saldoawal.Text);
                string Project = Cf.Pk(project.SelectedValue);

                DataTable rsBef = Db.Rs("SELECT "
                    + " Acc AS [No. Account]"
                    + ",Rekening"
                    + ",SubID"
                    + ",Project"
                    + ",Bank"
                    + ",Cabang"
                    + ",AtasNama AS [Atas Nama]"
                    + ",SaldoAwal AS [Saldo Awal]"
                    + " FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC "
                    + " WHERE Acc = '" + x[0] + "' AND SubID='" + x[1] + "'");

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spAccEdit"
                    + " '" + x[0] + "'"
                    + ",'" + AccBaru + "'"
                    + ",'" + Rekening + "'"
                    + ",'" + Bank + "'"
                    + ",'" + Cabang + "'"
                    + ",'" + AtasNama + "'"
                    + ", " + SaldoAwal
                    + ",'" + SubID + "'"
                    + ",'" + x[1] + "'"
                    + ",'" + Project + "'"
                    );

                DataTable rsAft = Db.Rs("SELECT "
                    + " Acc AS [No. Account]"
                    + ",Rekening"
                    + ",SubID"
                    + ",Project"
                    + ",Bank"
                    + ",Cabang"
                    + ",AtasNama AS [Atas Nama]"
                    + ",SaldoAwal AS [Saldo Awal]"
                    + " FROM " + Mi.DbPrefix + "FINANCEAR..REF_ACC "
                    + " WHERE Acc = '" + AccBaru + "' AND SubID='" + SubID + "'");
                string KetLog = Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC " + Mi.DbPrefix + "FINANCEAR..spLogAcc"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + AccBaru+";"+SubID + "'"
                    );

                decimal LogID = Db.SingleDecimal("SELECT TOP 1 LogID FROM REF_ACC_LOG ORDER BY LogID DESC");                
                Db.Execute("UPDATE REF_ACC_LOG SET Project = '" + Project + "' WHERE LogID  = " + LogID);

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
            string AccBaru = Cf.Pk(acc.Text);
            if (Save()) Response.Redirect("AccEdit.aspx?Acc=" + AccBaru + ";" + subid.Text.ToString() + "&done=1");
        }

        private string Acc
        {
            get
            {
                return Cf.Pk(Request.QueryString["Acc"]);
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

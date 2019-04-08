using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{


    public partial class LevelSalesEdit : System.Web.UI.Page
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
                BindTipe();
                Fill();
            }

            FeedBack();
            Js.Confirm(this, "Lanjutkan proses edit data Tipe unit properti?\\n"
                + "");
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

        private void BindTipe()
        {
            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE WHERE Project = '" + project.SelectedValue + "'";
            DataTable rs = Db.Rs(strSql);
            tipe.Items.Add(new ListItem { Text = "Tipe :", Value = "0" });

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string v = rs.Rows[i]["ID"].ToString();
                string t = rs.Rows[i]["Tipe"].ToString();
                tipe.Items.Add(new ListItem(t, v));                
            }

            string strSql2 = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL WHERE Project = '" + project.SelectedValue + "'";
            DataTable rs2 = Db.Rs(strSql2);
            parent.Items.Add(new ListItem { Text = "Parent :", Value = "0" });

            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                string v = rs2.Rows[i]["LevelID"].ToString();
                string t = rs2.Rows[i]["Nama"].ToString();
                parent.Items.Add(new ListItem(t, v));
            }
        }

        private void Fill()
        {
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?tb=ISC064_MARKETINGJUAL..REF_AGENT_LEVEL_LOG&Pk=" + NoLevel + "'";
            btndel.Attributes["onclick"] = "location.href='LevelSalesDel.aspx?NoLevel=" + NoLevel + "'";

            string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL WHERE LevelID = '" + NoLevel + "'";
            DataTable rs = Db.Rs(strSql);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else if (!Act.AksesProject(rs.Rows[0]["Project"].ToString()))
                Response.Redirect("/CustomError/SecLevel.html");
            else
            {
                nolevel.Text = rs.Rows[0]["LevelID"].ToString();
                nama.Text = rs.Rows[0]["Nama"].ToString();
                Cf.SelectedValue(project, rs.Rows[0]["Project"].ToString());
            }

            //string Tipe = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_TIPE WHERE ID = '" + rs.Rows[0]["Tipe"].ToString() + "'";
            //DataTable rs2 = Db.Rs(Tipe);
            //Cf.SelectedValue(tipe, rs2.Rows[0]["Tipe"].ToString());
            //Cf.SelectedValue(parent, rs.Rows[0]["Nama"].ToString());
        }

        private bool valid()
        {
            string s = "";
            bool x = true;

            if (Cf.isEmpty(nama))
            {
                x = false;
                if (s == "") s = nama.ID;
                namac.Text = "Kosong";
            }
            else
                namac.Text = "";

            if (tipe.SelectedIndex < 0)
            {
                x = false;
                tipec.Text = "harus pilih";
            }

            if (!x)
                Js.Alert(
                    this
                    , "Input Tidak Valid.\\n\\n"
                    + "Aturan Proses :\\n"
                    + "1. Tipe Sales tidak boleh kosong.\\n"
                    , "document.getElementById('" + s + "').focus();"
                    + "document.getElementById('" + s + "').select();"
                    );

            return x;
        }

        protected void Save(bool close)
        {
            int ada = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL WHERE Nama='" + nama.Text + "' AND Tipe='" + tipe.SelectedValue + "' AND LevelID != " + NoLevel);
            if (ada > 0)
            {
                string s = "";

                Js.Alert(
                   this
                   , "Input Tidak Valid.\\n\\n"
                   + "Aturan Proses :\\n"
                   + "1. Nama tidak boleh kosong.\\n"
                   + "2. Tipe Sales tidak boleh kosong.\\n"
                   + "3. Nama Duplikat.\\n"
                   + "4. Tipe Sales Duplikat.\\n"
                   , "document.getElementById('" + s + "').focus();"
                   + "document.getElementById('" + s + "').select();"
                   );
            }
            else
            {
                string strSql = "SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL WHERE LevelID = '" + NoLevel + "'";
                DataTable rs = Db.Rs(strSql);
                DataTable rsBef = Db.Rs("SELECT "
                            + " Nama"
                            + ",Project"
                            + ",Tipe"
                            + ",LevelID"
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL "
                            + " WHERE LevelID = '" + NoLevel + "'");

                Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL SET Nama='" + nama.Text + "',Tipe='" + tipe.SelectedValue + "',Project='" + project.SelectedValue + "' WHERE LevelID = '" + NoLevel + "'");

                DataTable rsAft = Db.Rs("SELECT "
                            + " Nama"
                            + ",Project"
                            + ",Tipe"
                            + ",LevelID"
                            + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_AGENT_LEVEL "
                            + " WHERE LevelID = '" + NoLevel + "'");

                //Logfile
                string Ket = "Tipe: " + tipe.Text + "<br>"
                    + Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC " + Mi.DbPrefix + "MARKETINGJUAL..spLogLevelSales"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + Ket + "'"
                    + ",'" + NoLevel + "'"
                    );

                if (close)
                    Js.Close(this);
                else
                    Response.Redirect("LevelSalesEdit.aspx?done=1&NoLevel=" + NoLevel);
            }
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Save(true);
            }
        }

        protected void save_Click(object sender, System.EventArgs e)
        {
            if (valid())
            {
                Save(false);
            }
        }

        private string NoLevel
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoLevel"]);
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
            BindTipe();
        }
    }
}
using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SETTINGS
{
    public partial class AlamatEmailEdit : System.Web.UI.Page
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
            btnlog.Attributes["onclick"] = "location.href='LogPk.aspx?Tb=REF_EMAIL_LOG&Pk=" + ID + "'";
            btndel.Attributes["onclick"] = "location.href='AlamatEmailDel.aspx?ID=" + ID + "'";

            string[] x = Cf.SplitByString(ID, ";");

            DataTable rs = Db.Rs("SELECT * FROM REF_EMAIL WHERE ID = '" + x[0] + "'");
            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else if (!Act.AksesProject(rs.Rows[0]["Project"].ToString()))
                Response.Redirect("/CustomError/SecLevel.html");
            else
            {
                id.Text = rs.Rows[0]["ID"].ToString();
                email.Text = rs.Rows[0]["Email"].ToString();
                Cf.SelectedValue(project, rs.Rows[0]["Project"].ToString());
            }
        }

        private bool valid()
        {
            bool x = true;
            string s = "";

            if (email.Text == "")
            {
                x = false;
                //if (s == "") s = acc.ID;
                emailc.Text = "Harus Diisi";
            }
            else
            {
                emailc.Text = "";
            }

            return x;
        }

        private bool Save()
        {
            if (valid())
            {
                string ID = Cf.Str(id.Text);
                string Email = Cf.Str(email.Text);
                string Project = Cf.Pk(project.SelectedValue);

                DataTable rsBef = Db.Rs("SELECT "
                    + " ID"
                    + ",Email"
                    + ",Project"
                    + " FROM REF_EMAIL "
                    + " WHERE ID = '" + ID + "'");

                Db.Execute("UPDATE REF_EMAIL SET Email = '" + Email + "',Project ='" + Project + "' WHERE ID = " + ID);

                DataTable rsAft = Db.Rs("SELECT "
                    + " ID"
                    + ",Email"
                    + ",Project"
                    + " FROM REF_EMAIL "
                    + " WHERE ID = '" + ID + "'");

                string KetLog = Cf.LogCompare(rsBef, rsAft);

                Db.Execute("EXEC spLogEmail"
                    + " 'EDIT'"
                    + ",'" + Act.UserID + "'"
                    + ",'" + Act.IP + "'"
                    + ",'" + KetLog + "'"
                    + ",'" + ID + "'"
                    );

                return true;
            }
            else
                return false;
        }

        protected void ok_Click(object sender, System.EventArgs e)
        {
            if (Save()) Js.CloseAndReload(this);
        }

        protected void save_Click(object sender, System.EventArgs e)
        {            
            if (Save()) Response.Redirect("AlamatEmailEdit.aspx?ID=" + ID + "&done=1");
        }

        private string ID
        {
            get
            {
                return Cf.Pk(Request.QueryString["ID"]);
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

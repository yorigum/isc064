using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{

    public partial class SatuTitik : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!Page.IsPostBack)
            {
                //filter();
                Act.ProjectList(project);
                fill();
            }
            Feedback();
            //CekSMS();
        }
        protected void Feedback()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                {
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                                + "Edit Berhasil...";
                    project.SelectedValue = Request.QueryString["project"];
                    fill();
                }
            }
        }

        //protected void CekSMS()
        //{
        //    if (Auth.SMS(dept.SelectedValue) != "ST")
        //        App.ErrAuthorize(this, this.Request.Url.ToString());
        //}
        //protected void filter()
        //{
        //    LibSec.SecProject(dept, Auth.ListAksesDept(Auth.UserID));
        //}
        protected void fill()
        {
            DataTable rsHeader = Db.Rs("SELECT * FROM SmsSatuTitik WHERE Project = '" + project.SelectedValue + "'");
            if (rsHeader.Rows.Count == 0)
            {
                username.Text = "";
                pass.Text = "";
                masking.Text = "";
                divisi.Text = "";
            }
            else
            {
                username.Text = rsHeader.Rows[0]["Username"].ToString();
                pass.Text = rsHeader.Rows[0]["Pass"].ToString();
                masking.Text = rsHeader.Rows[0]["Masking"].ToString();
                divisi.Text = rsHeader.Rows[0]["Divisi"].ToString();
                project.Text = rsHeader.Rows[0]["Project"].ToString();
            }
        }
        protected void save_Click(object sender, EventArgs e)
        {
            Save();
        }
        private bool valid()
        {
            string s = "";
            bool x = true;

            x = !Cf.isEmpty(username) ? x : false;
            x = !Cf.isEmpty(pass) ? x : false;
            x = !Cf.isEmpty(masking) ? x : false;

            if (!x)
            {
                this.RegisterStartupScript(
                    "focusScript"
                    , "<script language='javascript'>"
                    + " document.getElementById('" + s + "').focus();"
                    + " document.getElementById('" + s + "').select();"
                    + "</script>"
                    );
            }
            else
            {
            }

            return x;
        }
        protected void Save()
        {
            if (valid())
            {
                DataTable rs = Db.Rs("SELECT * FROM SmsSatuTitik WHERE Project = '" + project.SelectedValue + "'");
                if (rs.Rows.Count == 0)
                {
                    string Project = Cf.Str(project.SelectedValue);
                    string Username = Cf.Str(username.Text);
                    string Pass = Cf.Str(pass.Text);
                    string Masking = Cf.Str(masking.Text);
                    string Divisi = Cf.Str(divisi.Text);

                    Db.Execute("EXEC spSmsSatuTitikDaftar"
                        + " '" + Project + "'"
                        + ",'" + Username + "'"
                        + ",'" + Pass + "'"
                        + ",'" + Masking + "'"
                        + ",'" + Divisi + "'"
                        );

                    DataTable rsHeader = Db.Rs("SELECT "
                    + " Project"
                    + ",Username"
                    + ",Pass"
                    + ",Masking"
                    + ",Divisi"
                    + " FROM " + Mi.DbPrefix + "SECURITY..SmsSatuTitik "
                    + " WHERE Project = '" + project.SelectedValue + "'");

                    string Ket = Cf.LogCapture(rsHeader);

                    Db.Execute("EXEC spLogSmsSatuTitik"
                        + " 'DAFTAR'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + project.SelectedValue + "'"
                        );
                }
                else
                {
                    string Project = Cf.Str(project.SelectedValue);
                    string Username = Cf.Str(username.Text);
                    string Pass = Cf.Str(pass.Text);
                    string Masking = Cf.Str(masking.Text);
                    string Divisi = Cf.Str(divisi.Text);

                    Db.Execute("EXEC spSmsSatuTitikEdit"
                        + " '" + Project + "'"
                        + ",'" + Username + "'"
                        + ",'" + Pass + "'"
                        + ",'" + Masking + "'"
                        + ",'" + Divisi + "'"
                        );

                    DataTable rsHeader = Db.Rs("SELECT "
                     + " Project"
                     + ",Username"
                     + ",Pass"
                     + ",Masking"
                     + ",Divisi"
                     //+ ",CONVERT(varchar,Tgl,106) AS [Tgl. Pengajuan]"
                     //+ ",Ket AS [Keterangan]"
                     + " FROM " + Mi.DbPrefix + "SECURITY..SmsSatuTitik "
                     + " WHERE Project = '" + project.SelectedValue + "'");

                    string Ket = Cf.LogCapture(rsHeader);

                    Db.Execute("EXEC spLogSmsSatuTitik"
                        + " 'EDIT'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + Ket + "'"
                        + ",'" + project.SelectedValue + "'"
                        );
                }
                Response.Redirect("SatuTitik.aspx?done=1&project=" + project.SelectedValue);
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
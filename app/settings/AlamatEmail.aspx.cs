using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{
    public partial class AlamatEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);

            }
            FeedBack();
            fill();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                {
                    if (Request.QueryString["done"] == "1")
                    {
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                            + "Edit Berhasil...";
                    }
                    else
                    {
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                            + "<a href=\"javascript:popEditEmail('" + Request.QueryString["done"] + "')\">"
                            + "Pendaftaran Berhasil..."
                            + "</a>";
                    }
                }
                project.SelectedValue = Request.QueryString["project"];
            }
        }

        private void fill()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            DataTable rs = Db.Rs("SELECT * FROM REF_EMAIL WHERE Project = '" + project.SelectedValue + "'");
            Rpt.NoData(sb, rs, "<font style='font:8pt'>Daftar Email belum di-setup.</font>");

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                sb.Append("<li>"
                    + "<a href=\"javascript:popEditEmail('" + rs.Rows[i]["ID"].ToString() + "')\">"
                    + rs.Rows[i]["Email"]
                    + "</a>"
                    + "</li>"
                    );
            }

            list.InnerHtml = sb.ToString();

        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
            email.Text = string.Empty;
            emailcc.Text = string.Empty;
        }

        private bool valid()
        {
            bool x = true;
            if (email.Text == "")
            {
                x = false;
                emailcc.Text = "Harus Diisi";
            }
            else
            {
                emailcc.Text = "";
            }
            //int c = Db.SingleInteger(
            //    "SELECT COUNT(*) FROM REF_EMAIL WHERE Email = '" + email.Text + "' AND Project = '" + project.SelectedValue + "'");

            //if (c == 0)
            //    x = false;

            return x;
        }
        protected void ok_Click(object sender, EventArgs e)
        {
            if (valid())
            {
                int c = Db.SingleInteger("SELECT COUNT(Email) FROM REF_EMAIL WHERE Email = '" + email.Text + "' AND Project = '" + project.SelectedValue + "'");
                if (c == 1)
                {
                    emailcc.Text = "Duplikat";
                }
                else
                {


                    string Project = project.SelectedValue;
                    string Email = email.Text;
                    int jumlah = Db.SingleInteger("SELECT COUNT(ID) FROM REF_EMAIL");
                    //int ID = jumlah + 1;
                    if (jumlah > 0)
                    {
                        int ID = Db.SingleInteger("SELECT TOP 1 ID FROM REF_EMAIL ORDER BY ID DESC") + 1;
                        Db.Execute("INSERT INTO REF_EMAIL VALUES (" + ID + ",'" + Email + "','" + Project + "')");
                    }
                    else
                    {
                        Db.Execute("INSERT INTO REF_EMAIL VALUES (" + (jumlah + 1) + ",'" + Email + "','" + Project + "')");
                    }


                    int IDbaru = Db.SingleInteger("SELECT TOP 1 ID FROM REF_EMAIL ORDER BY ID DESC");

                    DataTable rs = Db.Rs("SELECT "
                            + "ID"
                            + ",Project"
                            + ",Email"
                            + " FROM REF_EMAIL "
                            + " WHERE ID  = '" + IDbaru + "'");

                    string KetLog = Cf.LogCapture(rs);

                    Db.Execute("EXEC spLogEmail"
                        + " 'REGIS'"
                        + ",'" + Act.UserID + "'"
                        + ",'" + Act.IP + "'"
                        + ",'" + KetLog + "'"
                        + ",'" + IDbaru + "'"
                        );

                    Response.Redirect("AlamatEmail.aspx?done=" + IDbaru + "&project=" + project.SelectedValue);
                }
            }
        }
    }
}
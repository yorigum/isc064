using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.NUP
{

    public partial class SetupDaftarBayar : System.Web.UI.Page
    {
        private string ParamID { get { return "FormatDaftarBayarNUP" + project.SelectedValue; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Act.ProjectList(project);
                init();
                //fill();
            }
            FeedBack();
        }

        protected void fill()
        {
            Js.NumberFormat(bayar);
            string value = Db.SingleString("SELECT ISNULL(VALUE, '0') FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'");

            if (!String.IsNullOrEmpty(value))
            {
                bayar.Text = Cf.Num(value);
            }
            else
            {
                bayar.Text = "0";
            }
        }

        protected void init()
        {
            //Js.NumberFormat(batas);
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "Edit berhasil..."
                        ;
                project.SelectedValue = Request.QueryString["project"];
                fill();
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {

            if (valid)
            {
                int ada = Db.SingleInteger("SELECT COUNT(*) FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'");
                if (ada == 0) //kalau ga ada
                {
                    Db.Execute("INSERT INTO " + Mi.DbPrefix + "SECURITY..REF_PARAM(ParamID,Keterangan,Value) VALUES('" + ParamID + "', 'Minimum Persen Pelunasan AJB.', '" + Convert.ToDecimal(bayar.Text) + "')");
                }
                else
                {
                    Db.Execute("UPDATE " + Mi.DbPrefix + "SECURITY..REF_PARAM SET Value = '" + Convert.ToDecimal(bayar.Text) + "' WHERE ParamID = '" + ParamID + "'");
                }
                Response.Redirect("SetupDaftarBayar.aspx?done=1&project=" + project.SelectedValue);
            }
        }

        protected bool valid
        {
            get
            {
                string s = "";
                bool x = true;

                x = !Cf.isEmpty(bayar) ? x : false;

                if (!x)
                    Js.Alert(
                        this
                        , "Format Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Harus Diisi.\\n"
                        , "document.getElementById('" + s + "').focus();"
                        + "document.getElementById('" + s + "').select();"
                        );
                return x;
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
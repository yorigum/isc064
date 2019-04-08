using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{

    public partial class FormatUnit : System.Web.UI.Page
    {
        private string ParamID { get { return "FormatLantai" + project.SelectedValue; } }
        private string ParamID2 { get { return "FormatUnit" + project.SelectedValue; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                init();
                fill();
            }
            FeedBack();
        }

        protected void fill()
        {
            batas.Text = Db.SingleString("SELECT ISNULL(VALUE, '') FROM REF_PARAM WHERE ParamID = '" + ParamID + "'");
            batas2.Text = Db.SingleString("SELECT ISNULL(VALUE, '') FROM REF_PARAM WHERE ParamID = '" + ParamID2 + "'");
            int cekParam = Db.SingleInteger("SELECT COUNT(*) FROM REF_PARAM WHERE ParamID = '" + ParamID + "'");
            int cekUnit = Db.SingleInteger("SELECT COUNT(*) FROM "+ Mi.DbPrefix +"MARKETINGJUAL..MS_UNIT WHERE Project = '"+project.SelectedValue+"'");
            if (cekParam > 0 && cekUnit > 0)
            {
                ok.Enabled = false;
                ok.ToolTip = "Sudah ada unit terdaftar.";   
            }
            else
            {
                
                ok.Enabled = true;
            }
        }

        protected void init()
        {
            Act.ProjectList(project);
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
                int ada = Db.SingleInteger("SELECT COUNT(*) FROM REF_PARAM WHERE ParamID = '" + ParamID + "'");
                if (ada == 0) //kalau ga ada
                    Db.Execute("INSERT INTO REF_PARAM(ParamID,Keterangan,Value) VALUES('" + ParamID + "', 'Penomoran Format Lantai Baru.', '" + Convert.ToString(batas.Text) + "')");
                else
                    Db.Execute("UPDATE REF_PARAM SET Value = '" + Convert.ToString(batas.Text) + "' WHERE ParamID = '" + ParamID + "'");

                int ada2 = Db.SingleInteger("SELECT COUNT(*) FROM REF_PARAM WHERE ParamID = '" + ParamID2 + "'");
                if (ada2 == 0)
                    Db.Execute("INSERT INTO REF_PARAM(ParamID,Keterangan,Value) VALUES('" + ParamID2 + "', 'Penomoran Format Unit Baru.', '" + Convert.ToString(batas2.Text) + "')");
                else
                    Db.Execute("UPDATE REF_PARAM SET Value = '" + Convert.ToString(batas2.Text) + "' WHERE ParamID = '" + ParamID2 + "'");

                Response.Redirect("FormatUnit.aspx?done=1&project=" + project.SelectedValue);
            }
        }

        protected bool valid
        {
            get
            {
                bool x = true;

                //x = !Cf.isEmpty(batas) ? x : false;

                //if (!x)
                //    Js.Alert(this, "", "Pembatas harus diisi.");

                return x;
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{

    public partial class TargetBAST : System.Web.UI.Page
    {
        private string ParamID { get { return "TargetBAST" + project.SelectedValue + lokasi.SelectedValue; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {                
                Act.ProjectList(project);
                init();
                fill();
            }
            FeedBack();
        }

        protected void fill()
        {
            string value = Db.SingleString("SELECT ISNULL(VALUE, ' ') FROM REF_PARAM WHERE ParamID = '" + ParamID + "'");
            
            if (!String.IsNullOrEmpty(value))
            {
                tgltarget.Text = Cf.Day(value);
            }
            else
            {
                tgltarget.Text = "";
            }

            update.Checked = false;

            int count = Db.SingleInteger("SELECT COUNT(*) FROM REF_PARAM WHERE ParamID = '" + ParamID + "'");
            if(count > 0)
            {
                int def = Db.SingleInteger("SELECT Def FROM REF_PARAM WHERE ParamID = '" + ParamID + "'");
                if(def == 1)
                {
                    update.Checked = true;
                }
            }
        }

        protected void init()
        {
            lokasi.Items.Clear();
            //Js.NumberFormat(batas);
            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_LOKASI WHERE Project ='" + project.SelectedValue + "'");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string Nama = rs.Rows[i]["Lokasi"].ToString() + " - " + rs.Rows[i]["Nama"].ToString();
                string Value = rs.Rows[i]["Lokasi"].ToString();
                lokasi.Items.Add(new ListItem(Nama, Value));
            }
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
                init();
                lokasi.SelectedValue = Request.QueryString["lokasi"];                
                fill();
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {            
            if (valid)
            {
                int ada = Db.SingleInteger("SELECT COUNT(*) FROM REF_PARAM WHERE ParamID = '" + ParamID + "'");
                int Default = 0;
                if (update.Checked)
                {
                    Default = 1;
                }

                if (ada == 0) //kalau ga ada
                {
                    Db.Execute("INSERT INTO REF_PARAM VALUES ('" + ParamID + "','Tgl Target BAST.','" + Convert.ToDateTime(tgltarget.Text) + "','" + Default + "')");

                    //Db.Execute("INSERT INTO REF_PARAM VALUES('" + ParamID + "', 'Minimum Persen Pelunasan PPJB.', '" + Convert.ToDecimal(persenlunas.Text) + "')");
                    //if (tgltarget.Text != "")
                    //{
                    //}
                }
                else
                {
                    Db.Execute("UPDATE REF_PARAM SET Value = '" + Convert.ToDateTime(tgltarget.Text) + "',Def = '" + Default + "' WHERE ParamID = '" + ParamID + "'");

                    string strSql = "SELECT * FROM " + Mi.DbPrefix + "SECURITY..REF_PARAM WHERE ParamID = '" + ParamID + "'";
                    DataTable rs = Db.Rs(strSql);
                    if (Default == 1)
                    {
                        Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_KONTRAK SET TargetST='" + Convert.ToDateTime(tgltarget.Text) + "' WHERE Lokasi ='" + lokasi.SelectedValue + "' AND Project = '" + project.SelectedValue + "'");
                        //Db.Execute("UPDATE " + Mi.DbPrefix + "MARKETINGJUAL..MS_BAST SET TargetST='" + Convert.ToDateTime(tgltarget.Text) + "' WHERE TargetST='" + rs.Rows[0]["TargetST"] + "'");
                    }
                }
                Response.Redirect("TargetBAST.aspx?done=1&project=" + project.SelectedValue + "&lokasi=" + lokasi.SelectedValue);
            }
        }

        protected bool valid
        {
            get
            {
                string s = "";
                bool x = true;

                x = !Cf.isEmpty(tgltarget) ? x : false;

                if (!x)
                {
                    Js.Alert(
                        this
                        , "Format Tidak Valid.\\n\\n"
                        + "Kemungkinan Sebab :\\n"
                        + "1. Harus Diisi.\\n"
                        , "document.getElementById('" + s + "').focus();"
                        + "document.getElementById('" + s + "').select();"
                        );
                }
                return x;
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            lokasi.Items.Clear();
            init();
            fill();
        }

        protected void lokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
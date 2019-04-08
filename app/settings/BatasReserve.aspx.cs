using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.SETTINGS
{
    public partial class BatasReserve : System.Web.UI.Page
    {
        private string ParamID { get { return "BatasReservasi" + project.SelectedValue; } }
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
            string value = Db.SingleString("SELECT ISNULL(VALUE, '0;1') FROM REF_PARAM WHERE ParamID = '" + ParamID + "'");

            if (!String.IsNullOrEmpty(value))
            {
                string[] hari = value.Split(';');

                batas.Text = Cf.Num(hari[0]);
                tipe.SelectedValue = hari[1];
            }
            else
            {
                batas.Text = "0";
                tipe.SelectedValue = "1";
            }
        }

        protected void init()
        {
            Act.ProjectList(project);
            Js.NumberFormat(batas);
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
                {
                    Db.Execute("INSERT INTO REF_PARAM(ParamID,Keterangan,Value) VALUES('" + ParamID + "', 'Batas waktu reservasi.', '" + Convert.ToDecimal(batas.Text) + ";" + tipe.SelectedValue + "')");
                }
                else
                {
                    Db.Execute("UPDATE REF_PARAM SET Value = '" + Convert.ToDecimal(batas.Text) + ";" + tipe.SelectedValue + "' WHERE ParamID = '" + ParamID + "'");
                }
                Response.Redirect("BatasReserve.aspx?done=1&project="+project.SelectedValue);
            }
        }

        protected bool valid
        {
            get
            {
                bool x = true;

                x = Cf.isMoney(batas) ? x : false;

                if (!x)
                    Js.Alert(this, "", "Hari harus dalam angka.");

                return x;
            }
        }

        protected void project_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill();
        }
    }
}
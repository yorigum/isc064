using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace ISC064.SECURITY
{
    public partial class CounterLaunching : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!IsPostBack)
            {
                var rs = from DataRow r in Db.Rs("Select * from Username where Status='A'").Rows
                         select new ListItem {
                             Value = r["UserID"].ToString(),
                             Text = r["Nama"].ToString() 
                         };
                username.Items.AddRange(rs.ToArray());
            }


            var d = from DataRow r in Db.Rs("Select * from REF_ADMIN_LAUNCHING").Rows
                    select new ListItem
                    {
                        Value = r["ID"].ToString(),
                        Text = r["Nama"].ToString()
                    };
            baru.Items.AddRange(d.ToArray());

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                    feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                        + "<a href=\"javascript:popEditCounterLaunching('" + Request.QueryString["done"] + "')\">"
                        + "Pendaftaran Berhasil..."
                        + "</a>";
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if(Valid)
            {
                Db.Execute("spRefAdminLaunching '"+ Cf.Pk(nama.Text) +"','"+ Cf.Pk(username.SelectedValue) +"'," + nomor.Text);
                int id = Db.SingleInteger("select Max(ID) from REF_ADMIN_LAUNCHING");
                Response.Redirect("CounterLaunching.aspx?done=" + id);
            }
        }
        bool Valid
        {
            get
            {
                bool x = true;
                if(nama.Text.Length < 1)
                {
                    x = false;
                    namac.Text = "harus isi";
                }else namac.Text = "";
                if (!Cf.isInt(nomor.Text))
                {
                    x = false;
                    nomorc.Text = "angka";
                }else nomorc.Text = "";

                return x;
            }
        }
    }
}

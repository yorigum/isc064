using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISC064.ADMINJUAL
{
    public partial class Gimmick : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                init();
            }
        }
        protected void init()
        {
            Act.UserProjectList(project);

            DataTable rs = Db.Rs("SELECT * FROM " + Mi.DbPrefix + "MARKETINGJUAL..REF_TIPE_GIMMICK");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string a = rs.Rows[i]["ID"].ToString();
                string b = rs.Rows[i]["Nama"].ToString();
                tipe.Items.Add(new ListItem(b, a));
            }
        }

        //protected bool Valid()
        //{
        //    bool x = true;

        //    if (project.SelectedIndex == 0)
        //    {
        //        x = false;
        //    }

        //    if (!x)
        //        Js.Alert(this,
        //            "Proses Tidak Valid.\\n\\n"
        //            + "Aturan Proses :\\n"
        //            + "1. Project harus di pilih.\\n"
        //            , "");

        //    return x;
        //}

        protected void display_Click(object sender, System.EventArgs e)
        {
            //if (Valid())
            //{
                Cf.SetGrid(tb);
                Fill();
            //}
        }

        protected void Fill()
        {
            string Status = "";
            if (status.SelectedIndex != 0)
            {
                Status = " AND Status = '" + status.SelectedValue + "'";
            }

            string Tipe = "";
            if (tipe.SelectedIndex != 0)
                Tipe = " AND Tipe = '" + tipe.SelectedValue + "'";

            //string NoKontrak = "'<a href=\"javascript:call(''' + MS_KONTRAK.NoKontrak + ''')\" ' + CASE MS_KONTRAK.Status WHEN 'B' THEN 'style = ''text-decoration:line-through''' ELSE '' END + '>' + MS_KONTRAK.NoKontrak + '</a><br>";
            string ItemID = "'<a href=\"javascript:call(''' + ItemID + ''')\">' + Nama + '</a>' AS Nama";
            //"'<a href=\"javascript:call(''' + ItemID + ''')\"'></a> AS ID";

            string strSql = "SELECT "
                + ItemID
                + ",(select Nama from REF_TIPE_GIMMICK where REF_TIPE_GIMMICK.ID = MS_GIMMICK.Tipe) AS Tipe"
                + ",CONVERT(int, Stock) AS Stock"
                + ",Satuan AS Satuan"
                + " FROM " + Mi.DbPrefix + "MARKETINGJUAL..MS_GIMMICK"
                + " WHERE 1=1"
                + " AND Project = '" + project.SelectedValue + "'"
                + Status
                + Tipe
                + " ORDER BY ItemID";

            DataTable rs = Db.Rs(strSql);

            tb.DataSource = rs;
            tb.DataBind();
        }
        protected void tb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tb.PageIndex = e.NewPageIndex;
            Fill();
        }
    }
}
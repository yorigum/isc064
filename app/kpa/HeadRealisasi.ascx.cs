using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;

namespace ISC064.KPA
{
    public partial class HeadRealisasi : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["p"] != null)
                    this.Page.RegisterStartupScript(
                        "focusScript"
                        , "<script type='text/javascript'>"
                        + " document.getElementById('" + this.ID + "_" + prev.ID + "').focus();"
                        + "</script>"
                        );
                else if (Request.QueryString["n"] != null)
                    this.Page.RegisterStartupScript(
                        "focusScript"
                        , "<script type='text/javascript'>"
                        + " document.getElementById('" + this.ID + "_" + next.ID + "').focus();"
                        + "</script>"
                        );

                string p = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 NoReal FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA a "
                                                  + " INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL c on c.NoPengajuan = a.NoPengajuan"
                                                  + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_Kontrak d on d.NoKontrak = c.NoKontrak"
                                                  + "  WHERE a.NoReal < '" + NoReal + "'"
                                                  + " AND d.Project IN (" + Act.ProjectListSql + ")"
                                                  + " ORDER BY a.NoReal DESC),'')").ToString();
                string n = Db.SingleInteger("SELECT ISNULL((SELECT TOP 1 NoReal FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA a"
                                                  + " INNER JOIN " + Mi.DbPrefix + "FINANCEAR..MS_PENGAJUAN_KPA_DETIL c on c.NoPengajuan = a.NoPengajuan"
                                                  + " INNER JOIN " + Mi.DbPrefix + "MARKETINGJUAL..MS_Kontrak d on d.NoKontrak = c.NoKontrak"
                                                  + "  WHERE a.NoReal > '" + NoReal + "'"
                                                  + " AND d.Project IN (" + Act.ProjectListSql + ")"
                                                  + " ORDER BY a.NoReal DESC),'')").ToString();
                if (p != "0") prev.HRef = "?p=1&id=" + p; else prev.InnerHtml = "<i class='fa fa-long-arrow-left btn-disabled'></i> <b class='btn-disabled'>Prev</b>";
                if (n != "0") next.HRef = "?n=1&id=" + n; else next.InnerHtml = "<b class='btn-disabled'>Next</b> <i class='fa fa-long-arrow-right btn-disabled'></i>";

                string strSql = "SELECT * FROM " + Mi.DbPrefix + "FINANCEAR..MS_REAL_KPA a"
                              + " INNER JOIN " + Mi.DbPrefix + "SECURITY..USERNAME b on a.UserID = b.UserID"
                              + " WHERE NoReal=" + NoReal;
                DataTable rs = Db.Rs(strSql);

                if (rs.Rows.Count == 0)
                    Response.Redirect("/CustomError/Deleted.html");
                else
                {
                    norealisasi.Text = rs.Rows[0]["NoReal"].ToString().PadLeft(7, '0');
                    userid.Text = rs.Rows[0]["Nama"].ToString();
                    tgl.Text = Cf.Day(rs.Rows[0]["TglInput"]);

                }
            }
        }

        private string NoReal
        {
            get
            {
                return Cf.Pk(Request.QueryString["id"]);
            }
        }
    }
}

using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.LAUNCHING
{
    public partial class TTNUPRegistrasi : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();

            Js.Focus(this, keyword);
            Js.ConfirmKeyword(this, keyword);

            FeedBack();
        }

        private void FeedBack()
        {
            feed.Text = "";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["done"] != null)
                {
                    if (Request.QueryString["from"] != null && Request.QueryString["to"] != null)
                    {
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                            + "<a href=\"javascript:popPrintTTSMulti('" + Request.QueryString["from"] + "', '" + Request.QueryString["to"] + "');\">"
                            + "Registrasi Berhasil.."
                            + "</a>"
                            ;
                    }
                    else
                    {
                        feed.Text = "<img src='/Media/db.gif' align=absmiddle> "
                            + "<a href=\"javascript:popEditTTS('" + Request.QueryString["done"] + "')\">"
                            + "Registrasi Berhasil.."
                            + "</a>"
                            ;
                    }
                }
            }
        }

        protected void search_Click(object sender, System.EventArgs e)
        {
            //string[] x = Sc.MktCatalog();
            //for (int i = 0; i <= x.GetUpperBound(0); i++)
            //{
            //    string[] xdetil = x[i].Split(';');
            Fill("ISC064_MARKETINGJUAL", "NUP");
            //}
        }

        private void Fill(string Tb, string Ket)
        {
            string strSql = "";

            strSql = "SELECT * FROM MS_NUP WHERE NoNUP NOT IN (SELECT NoNUP FROM MS_NUP_PELUNASAN)";
            //strSql = "SELECT "
            //    + " NoKontrak AS Ref"
            //    + ",NoUnit"

            //    + " FROM " + Tb + "..MS_KONTRAK AS MS_KONTRAK INNER JOIN " + Tb + "..MS_CUSTOMER AS MS_CUSTOMER ON MS_KONTRAK.NoCustomer = MS_CUSTOMER.NoCustomer "
            //    + " WHERE NoKontrak + NoUnit + Nama"
            //    + " LIKE '%" + Cf.Str(keyword.Text) + "%'"
            //    + " AND MS_KONTRAK.Status = 'A'"
            //    + " ORDER BY NoKontrak";


            DataTable rs = Db.Rs(strSql);
            for (int i = 0; i < rs.Rows.Count; i++)
            {

                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                string strMultiple = "";

                //				if(Ket != "TENANT")
                //					strMultiple = " / <a href=\"javascript:location.href='TTSRegistrasiMulti.aspx?Ref=" + rs.Rows[i]["Ref"].ToString() + "&Tipe=" + Ket + "'\">multiple</a>";

                c.Text = rs.Rows[i]["NoNUP"].ToString()
                    + "<br /><a href=\"javascript:call('" + rs.Rows[i]["NoNUP"] + "','NUP')\">Next</a>"
                    + strMultiple
                    ;
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = rs.Rows[i]["Status"].ToString();
                r.Cells.Add(c);



                Rpt.Border(r);
                rpt.Rows.Add(r);

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

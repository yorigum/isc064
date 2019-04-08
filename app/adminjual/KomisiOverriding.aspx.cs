using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ISC064.ADMINJUAL
{
    public partial class KomisiOverriding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Bind();
            }

        }
        protected void Bind()
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_KOMISI_OVER");

            if (rs != null)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    TableCell c;
                    TableRow r = new TableRow();

                    c = new TableCell();
                    c.Text = "<b>" + rs.Rows[i]["Jabatan"].ToString() + "</b>";
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["Project"]);
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = Cf.Num(rs.Rows[i]["CrossSelling"]);
                    r.Cells.Add(c);

                    c = new TableCell();
                    c.Text = "<a show-modal='#ModalPopUp' modal-title='Komisi Edit' modal-url='KomisiOverEdit.aspx?SN='" + rs.Rows[i]["SN"].ToString() + "'>Edit</a>";
                    r.Cells.Add(c);

                    rpt.Rows.Add(r);
                }
            }
        }
    }
}

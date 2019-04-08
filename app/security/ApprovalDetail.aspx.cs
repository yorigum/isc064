using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ISC064.SECURITY
{
    public partial class ApprovalDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();
            if (!IsPostBack)
                fill();
        }
        protected void fill()
        {

            tipeapproval.InnerText = "Approval for " + LibControls.Bind.TipeApproval(TipeApproval);
            DataTable rs = Db.Rs("SELECT Distinct(Lvl) FROM REF_APPROVAL WHERE Tipe='" + TipeApproval + "'");
            int Count = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM REF_APPROVAL WHERE Tipe='" + TipeApproval + "'");
            if (rs.Rows.Count == 0)
            {
                anew.Visible = true;
                anew.HRef = "ApprovalDetailEdit.aspx?lvl=1&tipe=" + TipeApproval;

            }
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "<h2>" + rs.Rows[i]["lvl"].ToString() + "</h2>";
                c.ColumnSpan = 2;
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);

                fill(Convert.ToByte(rs.Rows[i]["lvl"]));

            }
        }
        protected void fill(byte Lvl)
        {
            DataTable rs = Db.Rs("SELECT * FROM REF_APPROVAL WHERE Lvl=" + Lvl + " AND Tipe=" + TipeApproval);

            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TableRow r = new TableRow();
                TableCell c;

                c = new TableCell();
                c.Text = "&nbsp;";
                r.Cells.Add(c);

                c = new TableCell();
                c.Text = Db.SingleString("SELECT Nama FROM Username WHERE UserID='" + rs.Rows[i]["UserID"].ToString() + "'") + " (" + rs.Rows[i]["UserID"].ToString() + ")";
                r.Cells.Add(c);

                Rpt.Border(r);
                rpt.Rows.Add(r);
                if (i == rs.Rows.Count - 1)
                    fillNext(Lvl);
            }
        }
        protected void fillNext(byte Lvl)
        {
            TableRow r = new TableRow();
            TableCell c;

            c = new TableCell();
            c.Text = "&nbsp;";
            r.Cells.Add(c);

            c = new TableCell();
            c.Text = "<a href='ApprovalDetailEdit.aspx?lvl=" + Lvl + "&tipe=" + TipeApproval + "'><p style='font-size:14pt;font-weight:bold;'>Edit OR Delete</p></a>";

            r.Cells.Add(c);

            Rpt.Border(r);
            rpt.Rows.Add(r);

        }
        private byte TipeApproval
        {
            get
            {
                return Convert.ToByte(Request.QueryString["tipe"]);
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

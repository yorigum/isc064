using System;
using System.Data;

namespace ISC064.ADMINJUAL
{
    public partial class ComplainDel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Act.Pass();
            Act.NoCache();

            Act.CekInt("NoComplain");

            if (!Page.IsPostBack)
            {
                Js.Focus(this, ket);
                Js.Confirm(this,
                    "Apakah anda ingin menghapus complain : " + NoComplain + " ?\\n"
                    + "Perhatian bahwa data akan dihapus secara PERMANEN."
                    );
            }
        }

        protected void delbtn_Click(object sender, System.EventArgs e)
        {
            DataTable rs = Db.Rs(
                "SELECT * FROM MS_COMPLAIN WHERE NoComplain = " + NoComplain);

            if (rs.Rows.Count == 0)
                Response.Redirect("/CustomError/Deleted.html");
            else
            {
                string Ket = "***Alasan Delete :<br>" + Cf.Str(ket.Text)
                    + "<br><br>***Data Sebelum Delete :<br>"
                    + Cf.LogCapture(rs);

                Db.Execute("EXEC spComplaintDel " + NoComplain);

                int c = Db.SingleInteger(
                    "SELECT COUNT(*) FROM MS_COMPLAIN WHERE NoComplain = " + NoComplain);

                if (c == 0)
                {
                    //Log
                    //Db.Execute("EXEC spLogAgent "
                    //    + " 'DELETE'"
                    //    + ",'" + Act.UserID + "'"
                    //    + ",'" + Act.IP + "'"
                    //    + ",'" + Ket + "'"
                    //    + ",'" + NoComplain.PadLeft(5, '0') + "'"
                    //    );

                    Js.Close(this);
                }
                else
                {
                    //Tidak bisa dihapus
                    frm.Visible = false;
                    nodel.Visible = true;
                }
            }
        }

        private string NoComplain
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoComplain"]);
            }
        }
    }
}
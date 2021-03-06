using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

namespace ISC064.SETTINGS
{
    public partial class ApprovalDetailEdit : System.Web.UI.Page
    {
        DataTable rs;
        protected int Baris
        {
            get { return Convert.ToInt32(Session["baris"]); }
            set { Session["baris"] = value; }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!IsPostBack)
            {
                int Count = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM REF_APPROVAL WHERE Tipe=" + TipeApproval + " AND Lvl=" + Level + " AND Project='" + Project + "'");

                Baris = Count + 1;
            }
            fillbaris();
            fill();
        }
        //Place Holder Control
        protected void add_Click(object sender, EventArgs e)
        {
            Add(1);
        }
        protected void add3_Click(object sender, EventArgs e)
        {
            Add(3);
        }
        protected void Add(int c)
        {
            for (short i = 1; i <= c; i++)
            {
                Baris++;
                tambah(Baris, list);
            }
        }

        protected void tambah(int index, Control x)
        {
            TextBox tb;
            TextBox tb2;
            Label l;
            HtmlGenericControl gen;
            HtmlButton btn;
            Button btn2;
            HtmlTableRow tr;
            HtmlTableCell c;

            tr = new HtmlTableRow();
            tr.VAlign = "top";
            x.Controls.Add(tr);

            c = new HtmlTableCell();
            tb = new TextBox();
            tb.ID = "userid_" + index;
            tb2 = new TextBox();
            tb2.ID = "username_" + index;
            c.Controls.Add(tb);
            c.Controls.Add(tb2);
            tr.Cells.Add(c);
            //<button type="button" value="..." class="btn btn-orange" show-modal="#ModalPopUp" modal-url="DaftarUserBlokir.aspx" modal-title="Daftar User terblokir" id="btnpop" runat="server"
            //                name="btnpop">
            //                <i class="fa fa-search"></i>
            //            </button>
            c = new HtmlTableCell();
            btn = new HtmlButton();
            btn.ID = "btn_edit_" + index;
            //btn.Attributes.Add("show-modal", "#ModalPopUp");
            btn.Attributes["onclick"] = "popDaftarAktif('"+tb.ID+ "','" + tb2.ID + "');";
            //btn.Attributes.Add("modal-url", "/Security/DaftarUserAktif2.aspx?ctrl=" + tb.ID + "&ctrl2=" + tb2.ID);
            //btn.Attributes.Add("modal-title", "Daftar User");
            btn.Attributes.Add("class", "btn btn-orange");

            gen = new HtmlGenericControl("i");
            gen.Attributes.Add("class", "fa fa-search");
            btn.Controls.Add(gen);
            c.Controls.Add(btn);
            tr.Cells.Add(c);

            c = new HtmlTableCell();
            l = new Label();
            l.ID = "btn_delete_" + index;
            l.Attributes.Add("class", "ibtn ibtn-remove btn btn-red btn-ico");
            l.Attributes.Add("onclick", "hapusbaris('" + index + "', '" + TipeApproval + "', '" + Level + "','" + Project + "');");
            l.Text = "Delete";
            c.Controls.Add(l);
            tr.Cells.Add(c);
        }
        protected void fillbaris()
        {
            for (int i = 1; i <= Baris; i++)
            {
                tambah(i, list);
            }
        }
        protected void fill()
        {
            keterangan.InnerText = "Approval for " + LibControls.Bind.TipeApproval(TipeApproval) + " Project " + Project + " Level " + Level;
            rs = Db.Rs("SELECT * FROM REF_APPROVAL WHERE Tipe=" + TipeApproval + "AND Lvl=" + Level + " AND Project= '" + Project + "'");
            int index = 1;
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (!Response.IsClientConnected) break;

                TextBox tb = (TextBox)list.FindControl("userid_" + index);
                tb.Text = rs.Rows[i]["UserID"].ToString();
                TextBox tb2 = (TextBox)list.FindControl("username_" + index);
                tb2.Text = Db.SingleString("SELECT Nama FROM Username WHERE UserID='" + rs.Rows[i]["UserID"].ToString() + "'");
                Label l = (Label)list.FindControl("btn_delete_" + index);
                //HtmlButton l = (HtmlButton)list.FindControl("btn_delete_" + index);
                l.Attributes["onclick"] = "hapusbaris('" + index + "'," + TipeApproval + ",'" + Level + "','" + Project + "')";

                index++;

            }

        }

        private byte TipeApproval
        {
            get
            {
                return Convert.ToByte(Request.QueryString["tipe"]);
            }
        }
        private string Project
        {
            get
            {
                return Convert.ToString(Request.QueryString["project"]);
            }
        }
        private byte Level
        {
            get
            {
                return Convert.ToByte(Request.QueryString["lvl"]);
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
        protected bool validref1
        {
            get
            {
                bool x = true;
                StringBuilder err = new StringBuilder();

                for (int i = 1; i <= Baris; i++)
                {
                    if (!Response.IsClientConnected) break;

                    TextBox tb = (TextBox)list.FindControl("userid_" + i);
                    TextBox tb2 = (TextBox)list.FindControl("username_" + i);
                    if (tb.Text != "")
                    {
                        int user = Db.SingleInteger("SELECT ISNULL(COUNT(*),0) FROM Username WHERE UserID='" + tb.Text + "' AND Status='A'");

                        if (user == 0)
                        {
                            x = false;
                            Cf.MarkError(tb);
                            err.Append("Baris ke-" + i + "\\n");
                        }
                    }
                }


                if (!x)
                {
                    StringBuilder t = new StringBuilder();
                    t.Append("Proses Gagal\\n");
                    t.Append("Sebab : UserID tidak terdaftar\\n\\n");

                    Js.Alert(Page, "Account", t.ToString());
                }


                return x;
            }
        }
        protected void finish_Click(object sender, EventArgs e)
        {
            if (validref1)
            {
                Save();
                Response.Redirect("ApprovalDetail.aspx?tipe=" + TipeApproval + "&project=" + Project);
            }
        }
        protected void Save()
        {
            for (int i = 1; i <= Baris; i++)
            {
                TextBox tb = (TextBox)list.FindControl("userid_" + i);
                TextBox tb2 = (TextBox)list.FindControl("username_" + i);
                if (!Response.IsClientConnected) break;

                //Delete ALL
                Db.Execute("DELETE FROM REF_APPROVAL WHERE UserID='" + tb.Text + "' AND Tipe=" + TipeApproval + " AND Lvl=" + Level + " AND Project='" + Project + "'");
                if (i == Baris)
                {
                    if (tb.Text != "")
                        Db.Execute("EXEC spApprovalDaftar"
                                + " N'" + tb.Text + "'"
                                + "," + Level
                                + "," + TipeApproval
                                + "," + Project
                                );
                }
                else
                {
                    Db.Execute("EXEC spApprovalDaftar"
                               + " N'" + tb.Text + "'"
                               + "," + Level
                               + "," + TipeApproval
                               + "," + Project
                               );
                }
            }

        }
        protected void next_Click(object sender, EventArgs e)
        {
            if (validref1)
            {
                decimal Level2 = Convert.ToDecimal(Level);
                Save();
                Response.Redirect("ApprovalDetailEdit.aspx?tipe=" + TipeApproval + "&lvl=" + (Level2 + 1) + "&project=" + Project);
            }
        }
    }
}

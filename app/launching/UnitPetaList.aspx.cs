using System;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI;

namespace ISC064.LAUNCHING
{
    public partial class UnitPetaList : System.Web.UI.Page
    {

        protected string ParentID
        {
            get
            {
                if (Request.QueryString["ParentID"] == null)
                {
                    return "0";
                }


                if (Cf.isInt(Request.QueryString["ParentID"]))
                {
                    return Request.QueryString["ParentID"];
                }
                return "0";
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Act.Pass();
            Act.NoCache();

            if (!Page.IsPostBack)
            {
                Fill();

                if (Request.QueryString["can"] != null)
                    Js.Alert(this, "Unit sedang di pilih Customer lain.", "");
            }
        }
        
        private void Fill()
        {

            var rs = Db.Rs("Select * from ms_siteplan where parentid=0");

            var ul = new HtmlGenericControl("ul");
            ul.Attributes.Add("class", "listitem");
            HtmlGenericControl li;
            foreach (DataRow r in rs.Rows)
            {
                string ad = "";        

                li = new HtmlGenericControl("li");
                if (r["Nama"].ToString() == ad)
                {
                    //li.InnerHtml = "<a href='javascript:Edit(" + r["id"] + ");' class='link'>" + r["Nama"] + "</a>";
                    li.InnerHtml = r["Nama"].ToString();
                    ul.Controls.Add(li);

                    list.Controls.Add(ul);
                    Loop(r["ID"], ul);
                }
            }
        }

        void Loop(object ParentID, Control Ctrl)
        {
            var ul = new HtmlGenericControl("ul");
            ul.Attributes.Add("class", "listitem");
            HtmlGenericControl li;

            string ad = "";
            if (Tipe == "RUSUNAMI")
            {
                ad = "GRIYA MAHATA";
            }
            else
                ad = "PRASADA MAHATA";
            
            var rs = Db.Rs("Select * from ms_siteplan where parentid='" + ParentID + "'");
            foreach (DataRow r in rs.Rows)
            {
                //if (Db.SingleString("SELECT Nama FROM ms_siteplan WHERE ParentID = " + ParentID) == ad)
                //{
                    li = new HtmlGenericControl("li");
                    li.InnerHtml = "<a url='UnitPetaDetil.aspx?ID=" + r["id"] + "&NoNUP=" + NoNUP + "&Tipe=" + Tipe + "'>" + r["Nama"] + "</a>";
                    ul.Controls.Add(li);
                    Ctrl.Controls.Add(ul);
                //}
                //Loop(r["ID"], ul);
            }
        }

        private string NoNUP
        {
            get
            {
                return Cf.Pk(Request.QueryString["NoNUP"]);
            }
        }
        private string Tipe
        {
            get
            {
                return Cf.Pk(Request.QueryString["Tipe"]);
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

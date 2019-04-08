using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        thn.Text = DateTime.Now.Year.ToString();
        gambar.Src = "..//" + ISC064.Act.Foto(ISC064.Act.UserID);
    }
}
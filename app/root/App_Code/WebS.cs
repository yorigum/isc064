using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ISC064;
using System.Drawing;

/// <summary>
/// Summary description for WebS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebS : System.Web.Services.WebService
{

    public WebS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public string WarnaUnit(string NoStock)
    {
        Color Warna = TableStock.WarnaUnit(NoStock);
        return ColorTranslator.ToHtml(Warna);
    }

    [WebMethod(EnableSession = true)]
    public string HrefUnit(string NoStock)
    {
        return TableStock.Href(NoStock);
    }

}

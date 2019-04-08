using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class tes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string file_name = "D:\\bayar2.txt";
        string textLine = "";

        StreamReader objReader = new StreamReader(file_name);

        while (objReader.Peek() != -1)
        {
            textLine = objReader.ReadLine() + "\r\n";

            string[] hasil = Regex.Split(textLine, @"\s{1,}");
            Response.Write(hasil[0] + "<br/>");
            Response.Write(hasil[1] + "<br/>");
            Response.Write(hasil[2] + "<br/>");
            Response.Write(hasil[3] + "<br/>");
            Response.Write(hasil[4] + "<br/>");
            Response.Write(hasil[5] + "<br/>");
            Response.Write(hasil[1].Substring(0, 4) + "<br/>");
            Response.Write(hasil[1].Substring(4, 2) + "<br/>");
            Response.Write(hasil[1].Substring(6, 2) + "<br/><br/>");
        }

        objReader.Close();
    }
}

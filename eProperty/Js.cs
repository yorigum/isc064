using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
namespace ISC064
{
    /// <summary>
    /// Generate Javascript functions
    /// </summary>
    public class Js
    {
        #region public static void Focus(Page p, TextBox tb)
        public static void Focus(Page p, TextBox tb)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"focusScript"
                , "<script type='text/javascript'>"
                + " document.getElementById('" + tb.ID + "').focus();"
                + " document.getElementById('" + tb.ID + "').select();"
                + "</script>"
                );
        }
        #endregion
        #region public static void Focus(Page p, DropDownList ddl)
        public static void Focus(Page p, DropDownList ddl)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"focusScript"
                , "<script language='javascript'>"
                + " document.getElementById('" + ddl.ID + "').focus();"
                + "</script>"
                );
        }
        #endregion
        #region public static void Focus(Page p, Button button)
        public static void Focus(Page p, Button button)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"focusScript"
                , "<script language='javascript'>"
                + " document.getElementById('" + button.ID + "').focus();"
                + "</script>"
                );
        }
        #endregion
        #region public static void Focus(Page p, CheckBox cb)
        public static void Focus(Page p, CheckBox cb)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"focusScript"
                , "<script language='javascript'>"
                + " document.getElementById('" + cb.ID + "').focus();"
                + "</script>"
                );
        }
        #endregion
        #region public static void Focus(Page p, HtmlInputButton button)
        public static void Focus(Page p, HtmlInputButton button)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"focusScript"
                , "<script language='javascript'>"
                + " document.getElementById('" + button.ID + "').focus();"
                + "</script>"
                );
        }
        #endregion
        #region public static void Focus(Page p, LinkButton link)
        public static void Focus(Page p, LinkButton link)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"focusScript"
                , "<script language='javascript'>"
                + " document.getElementById('" + link.ID + "').focus();"
                + "</script>"
                );
        }
        #endregion
        #region public static void Focus(Page p, HtmlAnchor a)
        public static void Focus(Page p, HtmlAnchor a)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"focusScript"
                , "<script language='javascript'>"
                + " document.getElementById('" + a.ID + "').focus();"
                + "</script>"
                );
        }
        #endregion
        #region public static void Focus(Page p, RadioButtonList cb)
        public static void Focus(Page p, RadioButtonList cb)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"focusScript"
                , "<script language='javascript'>"
                + " document.getElementById('" + cb.ID + "_0').focus();"
                + "</script>"
                );
        }
        #endregion
        #region public static void Focus(Page p, CheckBoxList cb)
        public static void Focus(Page p, CheckBoxList cb)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"focusScript"
                , "<script language='javascript'>"
                + " document.getElementById('" + cb.ID + "_0').focus();"
                + "</script>"
                );
        }
        #endregion

        #region public static void Alert(Page p, string alert, string after)
        public static void Alert(Page p, string alert, string after)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"alertScript"
                , "<script language='javascript'>"
                + " alert('" + alert + "');"
                + after
                + "</script>"
                );
        }
        #endregion
        #region public static void Confirm(Page p, string txt)
        public static void Confirm(Page p, string txt)
        {
            p.ClientScript.RegisterOnSubmitStatement(
                p.GetType()
                ,"confirmScript"
                , "return (confirm('" + txt + "'));");
        }
        #endregion
        #region public static void ConfirmKeyword(Page p, TextBox keyword)
        public static void ConfirmKeyword(Page p, TextBox keyword)
        {
            p.ClientScript.RegisterOnSubmitStatement(
                p.GetType(),
                "confirmScript"
                , "if(document.getElementById('" + keyword.ID + "').value=='') {"
                + "document.getElementById('" + keyword.ID + "').focus();"
                + "return(confirm('Lanjutkan proses menampilkan seluruh data?\\n"
                + "Perhatian bahwa proses ini bisa memakan waktu.'))"
                + "}");
        }
        #endregion
        #region public static void Close(Page p)
        public static void Close(Page p)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"closeScript"
                , "<script language='javascript'>"
                + " window.close()"
                + "</script>"
                );
        }
        #endregion
        #region public static void CloseAndReload(Page p)
        public static void CloseAndReload(Page p)
        {
            //Redirect + reload parent
            p.ClientScript.RegisterStartupScript(
                p.GetType(),
                "closewindow",
                "<script language='javascript'>" +
                "   if (window.opener) {" +
                "           var url= window.opener.location.href;" +
                "           if(url.indexOf('?') > -1) {" +
                "               url += '&done=1'" +
                "           } else {" +
                "               url += '?done=1'" +
                "           }" +
                "           window.opener.location.href = url;" +
                "           window.close();"+
                "   }" +
                " else {" +
                "   var url= window.location.href;" +
                "   if(url.indexOf('?') > -1) {" +
                "       url += '&done=1'" +
                "   } else {" +
                "       url += '?done=1'" +
                "   }" +
                "   window.location.href = url;" +
                "}" +
                "</script>"
                );
        }
        #endregion
        #region public static void AutoPrint(Page p)
        public static void AutoPrint(Page p)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"printauto"
                , "<SCRIPT language='javascript'>"
                + "function window.onload()"
                + "{window.focus();window.print()};"
                + "</SCRIPT>"
                );
        }
        #endregion

        #region public static void PopAuto(Page p, string href, int width, int height)
        public static void PopAuto(Page p, string href, int width, int height)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"popupScript"
                , "<script language='javascript'>"
                + " openPopUp('" + href + "','" + width + "','" + height + "');"
                + "</script>"
                );
        }
        #endregion
        #region public static void PopConfirm(Page p, string txt, string href, int width, int height, string after)
        public static void PopConfirm(Page p, string txt, string href, int width, int height, string after)
        {
            p.ClientScript.RegisterStartupScript(
                p.GetType()
                ,"popupScript"
                , "<script language='javascript'>"
                + " if(confirm('" + txt + "')) {openPopUp('" + href + "','" + width + "','" + height + "');}"
                + " else {" + after + "}"
                + "</script>"
                );
        }
        #endregion

        #region public static void Confirm(Button b, string txt)
        public static void Confirm(Button b, string txt)
        {
            System.Text.StringBuilder js = new System.Text.StringBuilder();
            js.Append("if (typeof(Page_ClientValidate) == 'function') {");
            js.Append("		var oldPage_IsValid = Page_IsValid;");
            js.Append("		var oldPage_BlockSubmit = Page_BlockSubmit;");
            js.Append("		if (Page_ClientValidate('" + b.ValidationGroup + "') == false) {");
            js.Append("			Page_IsValid = oldPage_IsValid;");
            js.Append("			Page_BlockSubmit = oldPage_BlockSubmit;");
            js.Append("			return false;");
            js.Append("		}");
            js.Append(" }");
            js.Append("return confirm('" + txt + "');");

            b.Attributes.Add("onclick", js.ToString());
        }
        #endregion
        #region public static void NumberFormat(TextBox tb)
        public static void NumberFormat(TextBox tb)
        {
            tb.Attributes["onfocus"] = "tempnum=CalcFocus(this);";
            tb.Attributes["onkeyup"] = "CalcType(this,tempnum);";
            tb.Attributes["onblur"] = "CalcBlur(this);";
        }
        #endregion
		#region public static void NumberFormat2(TextBox tb)
		public static void NumberFormat2(TextBox tb)
		{
			tb.Attributes["onblur"] = "CalcBlur(this);";
		}
        #endregion
        // Added By Whintz 2018 07 23
        public static void WriteJSON(object data)
        {
            var js = new JavaScriptSerializer();
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            System.Web.HttpContext.Current.Response.Write(js.Serialize(data));
            System.Web.HttpContext.Current.Response.End();
        }
    }
}

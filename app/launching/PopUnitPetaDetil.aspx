<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.PopUnitPetaDetil" CodeFile="PopUnitPetaDetil.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Peta Floor Plan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta http-equiv="Refresh" content="10">
    <meta name="sec" content="(Pop-Up) - Unit Launching">
    <meta http-equiv="pragma" content="no-cache">
</head>
<style type="text/css">
    .sm TD
    {
        font-weight: normal;
        font-size: 8pt;
        line-height: normal;
        font-style: normal;
        font-variant: normal;
    }
    .nav, .navsub
    {
        border: 0px;
        background-color: #EEEEEE;
        font: 8pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 190;
        height: 18px;
    }
    .nav2
    {
        border: 0px;
        background-color: #EEEEEE;
        font: 14pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 200;
        height: 30px;
    }
</style>
<body onkeyup="if(event.keyCode==27) history.back(-1)">
    <form id="Form1" method="post" runat="server">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div style="z-index: 1; left: 0px; width: 100%; position: absolute; top: 80px;">
        <table width="100%" height="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="100%" height="100%" align="center" valign="middle">
                    <asp:Image ID="dasar" runat="server"></asp:Image>
                </td>
            </tr>
        </table>
    </div>
    <div style="z-index: 3; left: 0px; width: 100%; position: absolute; top: 80px;">
        <table width="100%" height="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="100%" height="100%" align="center" valign="middle">
                    <asp:Image ID="koordinat" runat="server"></asp:Image>
                    <asp:Label ID="coord" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="z-index: 2; left: 0px; width: 100%; position: absolute; top: 80px;">
        <table width="100%" height="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="100%" height="100%" align="center" valign="middle">
                    <asp:Image ID="trans" runat="server"></asp:Image>
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%;">
            <tr>
                <td colspan="4" align="left" valign="top">
                    <b>DESCRIPTION :</b>
                </td>
            </tr>
            <tr>
                <td width="4%" style="border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                    border-bottom: gray 1px solid; background-color: cyan;">
                    &nbsp;
                </td>
                <td style="width: 96%">
                    Available
                </td>
            </tr>
            <tr>
                <td width="4%" style="border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                    border-bottom: gray 1px solid; background-color: red;">
                    &nbsp;
                </td>
                <td>
                    Sold
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
    function pilih(x, x2) {
	    window.returnValue = x + ";" + x2;
	    window.close();
    }
</script>


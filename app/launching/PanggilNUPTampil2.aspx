<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.PanggilNUPTampil2" CodeFile="PanggilNUPTampil2.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP Panggil - Updater</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Panggil NUP (Hal. 1 dari 2)">
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
<body style="height: auto !important;">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <br />
    <br />
    <br />
    <br />
    <br />
    <table style="margin: 0 auto;" cellspacing="5" width="100%">
        <tr>
            <td width="50%" style="font-size: large; font-weight: bold;">
                NUP NOW
            </td>
            <td width="50%" style="font-size: large; font-weight: bold;">
                NUP PENDING
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:Table ID="nupberjalan" runat="server" CssClass="tb" CellSpacing="3">
                </asp:Table>
            </td>
            <td style="vertical-align: top;">
                <asp:Table ID="nuppending" runat="server" CssClass="tb" CellSpacing="3">
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;" colspan="2" align="center">
                <asp:Button ID="save" runat="server" CssClass="btn" Text="Save" OnClick="save_Click"
                    Width="75px"></asp:Button>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

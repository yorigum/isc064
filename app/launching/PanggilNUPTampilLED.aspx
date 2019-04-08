<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.PanggilNUPTampilLED" CodeFile="PanggilNUPTampilLED.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP Panggil - Secondary</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <%--<link href="/Media/Style.css" type="text/css" rel="stylesheet">--%>
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Panggil NUP (Hal. 1 dari 2)">
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
        img
        {
            opacity: 0.8;
            filter: alpha(opacity=40); /* For IE8 and earlier */
        }
    </style>
</head>
<body style="height: auto !important;">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div style="width: 100%; height: 250px; text-align: center; border-top: 20px solid Green;
        border-bottom: 20px solid Green;">
        <table style="width: 100%">
            <tr>
                <td style="width: 30%; text-align: center">
                    <img src="Image/AKR-02.png" style="width: 150px;" />
                </td>
                <td style="width: 40%">
                    <img src="Image/headPP.png" style="width: 1000px" />
                </td>
                <td style="width: 30%; text-align: center">
                    <img src="Image/GEMCity.png" style="width: 300px;" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div style="float: left; width: 30%;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src="Image/logo.jpg" style="width: 400px; margin-left: 100px;" />
    </div>
    <div style="float: left; width: 40%;">
        <div style="width: 100%; border: 15px inset black; border-radius: 15px;">
            <%--<table style="margin: 0 auto;" cellspacing="5" width="100%">
                <tr>
                    <td width="50%" style="font-size: 30pt; font-weight: bold; text-align: center;">
                        NOW
                    </td>
                    <td width="50%" style="font-size: 30pt; font-weight: bold; text-align: center;">
                        PENDING
                    </td>
                </tr>
            </table>--%>
            <table style="margin: 0 auto;" cellspacing="5" width="100%">
                <tr>
                    <td style="vertical-align: top;">
                        <asp:Table ID="nupberjalan" runat="server" CssClass="tb" HorizontalAlign="Center"
                            CellSpacing="25">
                        </asp:Table>
                    </td>
                    <td style="vertical-align: top;">
                        <asp:Table ID="nuppending" runat="server" CssClass="tb" HorizontalAlign="Center"
                            CellSpacing="25">
                        </asp:Table>
                    </td>
                </tr>
            </table>
        </div>
        <%--<br /><br />
        <div align="center">
            <table>
            <tr>
                <td style="vertical-align:top; font-size:15pt; width:50%"><br /><br /><br />Product by : </td>
                <td>
                    <img src="Image/AKR.png" style="width: 200px;" />
                </td>
            </tr>
        </table>
        </div>--%>
    </div>
    <div style="float: right; width: 20%; margin-right: 50px;" align="right">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src="Image/viola2.png" style="width: 280px; margin-right: 55px;" /></div>
    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.ClosingDone" CodeFile="ClosingDone.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Closing Langsung</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Pendaftaran Closing Langsung (Hal. 3)">
</head>
<style type="text/css">
    .sm TD {
        font-weight: normal;
        font-size: 8pt;
        line-height: normal;
        font-style: normal;
        font-variant: normal;
    }

    .nav, .navsub {
        border: 0px;
        background-color: #EEEEEE;
        font: 8pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 190;
        height: 18px;
    }

    .nav2 {
        border: 0px;
        background-color: #EEEEEE;
        font: 14pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 200;
        height: 30px;
    }
</style>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div style="float: left; width: 40%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20px">
                        <a href="ClosingNUP.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_prev_c.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: right; width: 10%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20px">
                        <a href="/Gateway.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_gateway2.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px">
                        <a href="/SignOut.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_out.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <h1>Closing Langsung</h1>
        <p>
            Halaman 3 dari 3
        </p>
        <br />
        <br />
        <h2 style="color: Brown; border: 1 solid silver; padding: 10">Closing Langsung Selesai
        </h2>
        <br />
        <table cellspacing="5">
            <tr>
                <td>No. Kontrak
                </td>
                <td>:
                </td>
                <td>
                    <asp:Label ID="nokontrakl" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Unit
                </td>
                <td>:
                </td>
                <td>
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Customer
                </td>
                <td>:
                </td>
                <td>
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Sales
                </td>
                <td>:
                </td>
                <td>
                    <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <br>
        <h1>
            <a id="asp" runat="server">Print SPU</a></h1>
        <br>
        <h1>
            <a id="ajp" runat="server">Print Jadwal Pembayaran</a></h1>
        <br>
        <h1 style="display: none">
            <a id="atts" runat="server">Print TTS</a></h1>
        <br>
        <br>
        <h1>
            <a id="backclosing" runat="server">Back To Closing NUP</a></h1>
    </form>
</body>
</html>

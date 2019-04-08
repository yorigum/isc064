<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.RegisReception3" CodeFile="RegisReception3.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP - Aktivasi NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Aktivasi NUP (Hal. 3 dari 3))">
</head>
<style>
    .nav2 {
        border: 0px;
        background-color: #EEEEEE;
        font: 14pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 200;
        height: 30px;
    }

        .nav2:hover {
            border: 1px;
            background-color: #666;
        }
</style>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div style="float: left; width: 40%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20px">
                        <a href="Index.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
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
        <br />
        <br />
        <div style="width: 100%; margin: 0 auto;">
            <h1>Aktivasi NUP</h1>
            <p style="font-size: 8pt; color: #666;">
                Halaman 3 dari 3
            </p>
            <br>
            <br />
            <h2 style="color: Brown; border: 1 solid silver; padding: 10">Aktivasi NPU Berhasil
            </h2>
            <br />
            <table cellspacing="5">
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Label ID="nonup" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

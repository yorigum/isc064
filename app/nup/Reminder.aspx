<%@ Page Language="c#" Inherits="ISC064.NUP.Reminder" CodeFile="Reminder.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Reminder</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder</h1>
        <br>
        <asp:DropDownList runat="server" ID="project" AutoPostBack="true"></asp:DropDownList>
        <table class="blue-list-skin">
             <tr>
                <td class="remind-td-num">
                    <a href=""  runat="server" id="nup2">
                        <asp:Label ID="countBelumBayar" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href=""  runat="server" id="nup">NUP yang belum melakukan pembayaran</a>
                    <p class="remind-span">
                        NUP yang belum melakukan pembayaran.
                    </p>
                </td>
            </tr>
             <tr>
                <td class="remind-td-num">
                    <a href="" runat="server" id="tts2">
                        <asp:Label ID="countTTSBelumPrint" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" runat="server" id="tts">TTS yang belum di print</a>
                    <p class="remind-span">
                        TTS yang belum di print.
                    </p>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

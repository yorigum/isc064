<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.PanggilNUPInput2" CodeFile="PanggilNUPInput2.aspx.cs" %>

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
<body>
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <h1>
        Halaman Panggil No.NUP untuk Back Proses (Update Status)</h1>
    <p style="font-size: 8pt; color: #666;">
        Halaman 1 dari 2</p>
    <br>
    <table style="border: 1px solid #DCDCDC" cellspacing="5">
        <tr>
            <td>
                Jumlah NUP untuk ditampilkan:
            </td>            
            <td>
                <asp:TextBox ID="input" runat="server" Width="20px" MaxLength="2" ReadOnly>4</asp:TextBox>
                <asp:Label ID="inputc" runat="server" CssClass="err"></asp:Label>
            </td>
            <td>
                <asp:Button ID="display" runat="server" CssClass="btn" Text="Display" OnClick="display_Click">
                </asp:Button>
            </td>
        </tr>
    </table>
    </script>
    </form>
</body>
</html>

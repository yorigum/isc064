<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.LogPk" CodeFile="LogPk.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html >
<html>
<head>
    <title>Log File</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Log File Detil per Objek Data">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)history.back(-1)">
    <form id="Form1" method="post" runat="server" class="cnt">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <h1 class="title title-line" style="padding: 3px">Log File</h1>
        <asp:RadioButtonList ID="tb" runat="server" Visible="False">
            <asp:ListItem Value="MS_TTS_LOG">TANDA TERIMA SEMENTARA</asp:ListItem>
            <asp:ListItem Value="MS_MEMO_LOG">MEMO</asp:ListItem>
            <asp:ListItem Value="MS_ANONIM_LOG">TRANSFER ANONIM</asp:ListItem>
            <asp:ListItem Value="REF_ACC_LOG">ACCOUNT</asp:ListItem>
            <asp:ListItem Value="MS_KASMASUK_LOG">KAS MASUK</asp:ListItem>
            <asp:ListItem Value="MS_KASKELUAR_LOG">KAS KELUAR</asp:ListItem>
            <asp:ListItem Value="REF_VA_LOG">Virtual Account</asp:ListItem>
        </asp:RadioButtonList>
        <br>
        <asp:Table ID="rpt" runat="server" CssClass="blue-skin" CellSpacing="0">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell Width="65">Tgl</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="45">Jam</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="200" ColumnSpan="2">User</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="50">Aktivitas</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="120">Referensi</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="120">Approval</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <table style="height: 50px">
            <tr>
                <td>
                    <a onclick="history.back(-1)" class="btn btn-blue" style="width: 75px">
                        <i class="fa fa-share"></i>
                        OK
                    </a>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

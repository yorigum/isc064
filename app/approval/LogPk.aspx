<%@ Page Language="c#" Inherits="ISC064.APPROVAL.LogPk" CodeFile="LogPk.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Log File</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Log File Detil per Objek Data">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)history.back(-1)">
    <form id="Form1" method="post" runat="server" class="cnt">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <h1 class="title title-line">Log File</h1>
        <asp:RadioButtonList ID="tb" runat="server" Visible="False">
            <asp:ListItem Value="MS_CUSTOMER_LOG">CUSTOMER</asp:ListItem>
            <asp:ListItem Value="MS_RESERVASI_LOG">RESERVASI</asp:ListItem>
            <asp:ListItem Value="MS_KONTRAK_LOG">KONTRAK</asp:ListItem>
            <asp:ListItem Value="MS_TTR_LOG">TTR</asp:ListItem>
        </asp:RadioButtonList>
        <br>
        <asp:Table ID="rpt" runat="server" CssClass="blue-skin tb" CellSpacing="1">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell Width="65">Tgl</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="45">Jam</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="200" ColumnSpan="2">User</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="50">Aktivitas</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="120">Referensi</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="120">Approval</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <table height="50">
            <tr>
                <td>
                    <input type="button" class="btn btn-blue" value="OK" id="cancel" onclick="history.back(-1)">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

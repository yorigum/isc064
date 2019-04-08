<%@ Page Language="c#" Inherits="ISC064.SECURITY.LogPk" CodeFile="LogPk.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
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
        <h1 class="title title-line">Log File</h1>
        <asp:RadioButtonList ID="tb" runat="server" Visible="False">
            <asp:ListItem Value="USERNAME_LOG">USERNAME</asp:ListItem>
            <asp:ListItem Value="SECLEVEL_LOG">SEC.LEVEL</asp:ListItem>
        </asp:RadioButtonList>
        <br>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell Width="65px">Tgl</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="45px">Jam</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="200px" ColumnSpan="2">User</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="50px">Aktivitas</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="120px">Referensi</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="120px">Approval</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <table style="height: 50px">
            <tr>
                <td>
                    <a id="cancel" runat="server" class="btn btn-blue" onclick="history.back(-1)" style="width:75px;"><i class="fa fa-share"></i>
                        OK
                    </a>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

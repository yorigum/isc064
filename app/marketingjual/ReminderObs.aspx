<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.ReminderObs" CodeFile="ReminderObs.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Reminder Reservasi Obsolete</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder - Reservasi Obsolete">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder Reservasi Obsolote</h1>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <br />
        <asp:Table ID="rpt" runat="server" CellSpacing="1" CssClass="tb blue-skin">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell Width="50">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="100">Unit / NUP / No. Urut</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="180">Customer / Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="100">Tgl / Tgl Expire</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="150">Skema / Netto</asp:TableHeaderCell>
                <asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <table height="50">
            <tr>
                <td>
                    <a href = 'Reminder.aspx'" type="button" class="btn btn-blue t-white" value="OK" style="width: 75px"><i class="fa fa-share"></i> OK </a>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

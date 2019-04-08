<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.ReminderPLPending" CodeFile="ReminderPLPending.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Reminder Price List Pending</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder - Price List Pending">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder Price List Pending</h1>
        <br />
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Stock" DataField="Stock" />
                <asp:BoundField HeaderText="Unit" DataField="Unit" />
                <asp:BoundField HeaderText="Keterangan" DataField="Ket" />
                <asp:BoundField HeaderText="Luas" DataField="Luas" />
                <asp:BoundField HeaderText="Luas Tanah" DataField="Tanah" />
                <asp:BoundField HeaderText="Luas Bangunan" DataField="Bangunan" />
                <asp:BoundField HeaderText="Price List Minimum" DataField="Minimum" />
                <asp:BoundField HeaderText="Price List Default" DataField="PLDefault" />
            </Columns>
        </asp:GridView>
        <label runat="server" id="kosong"></label>
        <table style="height: 50px;">
            <tr>
                <td>
                    <a href = "" runat="server" id="ok" type="button" class="btn btn-blue t-white" style="width: 75px"> <i class="fa fa-share"></i> OK </a>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

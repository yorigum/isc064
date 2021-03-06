<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.ReminderAnonimBaru" CodeFile="ReminderAnonimBaru.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Reminder Transfer Anonim Baru</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder - Transfer Anonim Baru">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder Transfer Anonim Baru</h1>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <br />
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="Tgl." DataField="Tgl" />
                <asp:BoundField HeaderText="Bank" DataField="Bank" />
                <asp:BoundField HeaderText="Nilai" DataField="Nilai" ItemStyle-HorizontalAlign="Right" />
            </Columns>
        </asp:GridView>
        <label runat="server" id="kosong"></label>
        <table height="50px">
            <tr>
                <td>
                    <a href = "" runat="server" id="ok" type="button" class="btn btn-blue t-white" style="width: 75px"><i class="fa fa-share"></i> OK </a>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

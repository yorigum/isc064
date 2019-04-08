<%@ Page language="c#" Inherits="ISC064.SECURITY.ReminderPass" CodeFile="ReminderPass.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Reminder Ganti Password</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder - Ganti Password">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder Ganti Password</h1>
        <br />
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="Nama" DataField="Nama" />
                <asp:BoundField HeaderText="Kode" DataField="Kode" />
                <asp:BoundField HeaderText="Sec. Level" DataField="SecLevel" />
                <asp:BoundField HeaderText="Terakhir Ganti" DataField="Ganti" />
                <asp:BoundField HeaderText="Rotasi" DataField="Rotasi" />
            </Columns>
        </asp:GridView>
        <label runat="server" id="kosong"></label>
       <table height="50">
            <tr>
                <td>
                    <a href='Reminder.aspx' class="btn btn-blue t-white" style="width: 75">
                        <i class="fa fa-share"></i>OK
                    </a>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

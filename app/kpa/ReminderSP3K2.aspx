﻿<%@ Page language="c#" Inherits="ISC064.KPA.ReminderSP3K2" CodeFile="ReminderSP3K2.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Reminder SP3K Sudah Dijadwalkan Belum Diajukan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder - SP3K Sudah Dijadwalkan Belum Diajukan">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder SP3K Sudah Dijadwalkan Belum Diajukan</h1>
        <br />
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Kontrak" DataField="Kontrak" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="Tgl. Kontrak" DataField="Tgl" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="Unit" DataField="Unit" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="Customer" DataField="Cs" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="Agent" DataField="Agent" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="Bank KPR" DataField="Bank" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="Target SP3K" DataField="Target" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="Tgl. Wawancara" DataField="Wawancara" ItemStyle-VerticalAlign="Top" />
            </Columns>
        </asp:GridView>
        <label runat="server" id="kosong"></label>
        <table height="50">
            <tr>
                <td>
                    <a id="cancel" href="" runat="server" style="width: 75px" class="btn btn-blue"><i class="fa fa-share"></i>OK</a>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function call(nomor) {
                popEditKontrak(nomor);
            }
        </script>
    </form>
</body>
</html>

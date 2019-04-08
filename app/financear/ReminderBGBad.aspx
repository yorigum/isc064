<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.ReminderBGBad" CodeFile="ReminderBGBad.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Reminder Cek Giro Bermasalah</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder - Cek Giro Bermasalah">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder Cek Giro Bermasalah</h1>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <br />
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="BG" DataField="BG" />
                <asp:BoundField HeaderText="TTS" DataField="TTS" />
                <asp:BoundField HeaderText="Customer" DataField="Cs" />
                <asp:BoundField HeaderText="Keterangan" DataField="Ket" />
                <asp:BoundField HeaderText="Nilai" DataField="Nilai" ItemStyle-HorizontalAlign="Right" />
            </Columns>
        </asp:GridView>
        <label runat="server" id="kosong"></label>
        <table style="height:50px">
            <tr>
                <td>
                    <a href = "" runat="server" id="ok" type="button" class="btn btn-blue t-white" style="width: 75px"><i class="fa fa-share"></i> OK </a>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

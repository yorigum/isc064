<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintTTSBelumPrint" CodeFile="PrintTTSBelumPrint.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>TTS Reservasi Belum Diprint</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="TTS Reservasi Belum Diprint">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">TTS Belum Di Print</h1>
        <table style="border: 1px solid #DCDCDC" cellspacing="10">
            <tr>
                <td>Project</td>
                <td><asp:DropDownList runat="server" ID="project" AutoPostBack="true"></asp:DropDownList> </td>
            </tr>
        </table>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Reservasi" DataField="Reservasi" />
                <asp:BoundField HeaderText="Customer " DataField="Customer" />
                <asp:BoundField HeaderText="Sales " DataField="Sales" />
                <asp:BoundField HeaderText="No. Unit " DataField="Unit" />
                <asp:BoundField HeaderText="No. TTS " DataField="TTS" />
                <asp:BoundField HeaderText="Nilai " DataField="Nilai" ItemStyle-HorizontalAlign="Right" />                
            </Columns>
        </asp:GridView>
        <Label runat="server" ID="kosong"></Label>
        <table height="50">
            <tr style="display: none;">
                <td>
                    <input id="cancel" onclick="location.href = 'Reminder.aspx'" type="button" class="btn"
                        value="OK" style="width: 75px">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<script language="javascript">
    function call(nomor) {
        popEditReservasi(nomor);
    }
</script>


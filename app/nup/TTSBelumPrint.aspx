<%@ Page Language="c#" Inherits="ISC064.NUP.TTSBelumPrint" CodeFile="TTSBelumPrint.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>TTS NUP Belum Diprint</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="TTS NUP Belum Diprint">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">TTS Belum Di Print</h1>
        <asp:DropDownList runat="server" ID="project" AutoPostBack="true"></asp:DropDownList>        
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. NUP" DataField="NUP" />
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
    function call(nomor, Jenis,project) {
        popNUP(nomor, Jenis,project);
    }
</script>


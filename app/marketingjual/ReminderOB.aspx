<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.ReminderOB" CodeFile="ReminderOB.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Reminder Selisih Kontrak Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder - Selisih Kontrak Tagihan">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder Selisih Kontrak Tagihan</h1>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <br />
        <p style="font: 8pt">
            Total Tagihan = 0 berarti proses pendaftaran tagihan masih 
				pending.
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No.Kontrak" DataField="Kontrak" />
                <asp:BoundField HeaderText="Unit" DataField="Unit" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Nilai Kontrak" DataField="Nilai" />
                <asp:BoundField HeaderText="Total Tagihan" DataField="Tagihan" />
                <asp:BoundField HeaderText="Selisih" DataField="Selisih" />
            </Columns>
        </asp:GridView>
        <label runat="server" id="kosong"></label>
        <table height="50">
            <tr>
                <td>
                    <a href="" runat="server" id="ok" type="button" class="btn btn-blue t-white"
                        value="OK" style="width: 75px"><i class="fa fa-share"></i>OK</a>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function call(nomor) {
                popJadwalTagihan(nomor);
            }
        </script>
    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.ReminderPJTBaru" CodeFile="ReminderPJTBaru.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Reminder Pemberitahuan Jatuh Tempo Baru</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder - P. Jatuh Tempo Baru">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder Pemberitahuan Jatuh Tempo Baru</h1>
        <br />
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. PJT" DataField="PJT" />
                <asp:BoundField HeaderText="Tgl." DataField="Tgl" />
                <asp:BoundField HeaderText="Customer" DataField="Cs" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan"/>
                <asp:BoundField HeaderText="Nilai" DataField="Nilai" ItemStyle-HorizontalAlign="Right" />                
            </Columns>
        </asp:GridView>
        <label runat="server" id="kosong"></label>
        <table height="50">
            <tr>
                <td>
						<a href="" runat="server" id="ok" class="btn btn-blue t-white" style="width:75px">
							<i class="fa fa-share"></i> OK
						</a>
                </td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
            function call(nomor) {
                popEditPJT(nomor);
            }
    </script>
</body>
</html>

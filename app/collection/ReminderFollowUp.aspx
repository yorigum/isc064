<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReminderFollowUp.aspx.cs" Inherits="ISC064.COLLECTION.ReminderFollowUp" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Reminder Pemberitahuan Jatuh Tempo Follow Up 7 Hari Kedepan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder - P. Jatuh Tempo 7 Follow Up Hari Kedepan">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder Pemberitahuan Jatuh Tempo (Follow Up)</h1>
        <br />
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Kontrak" DataField="Kontrak" />                
                <asp:BoundField HeaderText="Customer" DataField="Cs" />
                <asp:BoundField HeaderText="No. HP" DataField="Hp" />
                <asp:BoundField HeaderText="No. Unit" DataField="Unit" />
                <asp:BoundField HeaderText="Nama Tagihan" DataField="Nama"/>
                <asp:BoundField HeaderText="Tgl. Jatuh Tempo" DataField="Tgl" />
                <asp:BoundField HeaderText="Nilai Tagihan" DataField="Nilai" ItemStyle-HorizontalAlign="Right" />                
            </Columns>
        </asp:GridView>
        <label runat="server" id="kosong"></label>
        <table height="50">
            <tr>
                <td>
						<a href="" runat="server" id="ok" class="btn btn-blue t-white" style="width:75px">
							<i class="fa fa-share"></i> OK
						</a>                </td>
            </tr>
        </table>
        <script language="javascript">
            function call(ref, tipe) {
                popCIF(ref, tipe)
            }
        </script>
    </form>
</body>
</html>

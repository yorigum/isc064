<%@ Page Language="c#" Inherits="ISC064.COLLECTION.ReminderPJT7" CodeFile="ReminderPJT7.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>Reminder Pemberitahuan Jatuh Tempo 7 Hari Kedepan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder - P. Jatuh Tempo 7 Hari Kedepan">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder Pemberitahuan Jatuh Tempo 7 Hari Kedepan</h1>
        <br />
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Kontrak" DataField="Kontrak" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="Customer / Marketing / No. Unit" DataField="Cs" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="No. HP" DataField="HP"  ItemStyle-VerticalAlign="Top"/>
                <asp:BoundField HeaderText="Nama Tagihan" DataField="Tagihan" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="Tgl. Jatuh Tempo" DataField="JT" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="" DataField="Lbl" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="" DataField="Lbl2" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField HeaderText="Nilai" DataField="Nilai" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" />
            </Columns>
        </asp:GridView>
        <label runat="server" id="kosong"></label>
        <table height="50">
            <tr>
                <td>
                    <a href="" runat="server" id="ok" class="btn btn-blue t-white" style="width: 75px">
                        <i class="fa fa-share"></i>OK
                    </a>
                </td>
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

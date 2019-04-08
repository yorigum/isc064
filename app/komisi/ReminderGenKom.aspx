<%@ Page Language="c#" Inherits="ISC064.KOMISI.ReminderGenKom" CodeFile="ReminderGenKom.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Reminder Belum Generate Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder - Belum Generate Komisi">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder Belum Generate Komisi</h1>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <br />
        <%--<asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No.Kontrak" DataField="Kontrak" />
                <asp:BoundField HeaderText="Unit" DataField="Unit" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Sales" DataField="Sales" />
                <asp:BoundField HeaderText="Nilai Kontrak" DataField="Nilai" />                
            </Columns>
        </asp:GridView>--%>
        <asp:Table ID="rpt" runat="server" CellSpacing="1" Width="80%" CssClass="tb blue-skin">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell>No.Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell>Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell>Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell>Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell>Nilai</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>

        <label runat="server" id="kosong"></label>
        <table height="50">
            <tr>
                <td>
                    <a href="" runat="server" id="ok" type="button" class="btn btn-blue t-white" style="width: 75px">
                        <i class="fa fa-share"></i>OK
                    </a>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function call(nomor) {
                popEditTTS(nomor);
            }
        </script>
    </form>
</body>
</html>

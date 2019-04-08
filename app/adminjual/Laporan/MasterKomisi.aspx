<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Laporan.MasterKomisi" CodeFile="MasterKomisi.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Master Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Komisi">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Laporan Master Komisi</h1>
        <br />
        <br />
        Periode Komisi :<br />
        <asp:ListBox ID="periodekomisi" runat="server" CssClass="ddl" Width="200" Rows="10">
            <asp:ListItem>SEMUA</asp:ListItem>
        </asp:ListBox>
        <br />
        <br />
        <%--Status : A = Aktif / B = Batal--%>
        <table style="border-bottom: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-right: #dcdcdc 1px solid"
            cellspacing="5">
            <tr>
                <td>Nomor Sales / Nama Sales
						<asp:TextBox ID="key" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display"
                        OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <asp:Table ID="rpt" runat="server" CssClass="blue-skin tb" CellSpacing="1" Visible="false">
            <asp:TableRow HorizontalAlign="Left" VerticalAlign="Bottom">
                <asp:TableHeaderCell Width="500">Sales</asp:TableHeaderCell>
                <%--				<asp:tableheadercell width="100">Unit</asp:tableheadercell>
					<asp:tableheadercell width="75">Tgl.</asp:tableheadercell>--%>
                <asp:TableHeaderCell>Keterangan</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>

        <script type="text/javascript">
            function call(nomor) {
                popEditAgent(nomor);
            }
        </script>
    </form>
</body>
</html>

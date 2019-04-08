<%@ Page Language="c#" Inherits="ISC064.COLLECTION.JurnalPJT" CodeFile="JurnalPJT.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadPJT" Src="HeadPJT.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavPJT" Src="NavPJT.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Jurnal Pemberitahuan Jatuh Tempo</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Jurnal Pemberitahuan Jatuh Tempo">
</head>
<body onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavPJT ID="NavPJT1" runat="server" Aktif="2"></uc1:NavPJT>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadPJT ID="HeadPJT1" runat="server"></uc1:HeadPJT>
                <input type="text" style="display: none">
                <table cellspacing="5">
                    <tr>
                        <td colspan="3">
                            <asp:RadioButtonList ID="akt" runat="server" RepeatColumns="3" RepeatDirection="Vertical">
                                <asp:ListItem Selected="True">TELEPON CUSTOMER</asp:ListItem>
                                <asp:ListItem>KIRIM SURAT</asp:ListItem>
                                <asp:ListItem>LAINNYA</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>Keterangan Tambahan</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="baru" runat="server" CssClass="input-text" Width="500"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>File Hasil Scan</td>
                        <td>:</td>
                        <td>
                            <input type="file" id="file" runat="server" class="input-text" style="width: 568" name="file">
                        </td>
                    </tr>
                </table>
                <table height="50">
                    <tr>
                        <td>
                            <asp:Button ID="save" runat="server" CssClass="btn btn-blue" Text="OK" Width="75" OnClick="save_Click"></asp:Button>
                        </td>
                        <td>
                            <input id="cancel" type="button" class="btn btn-red" value="Cancel" style="width: 75" name="cancel"
                                onclick="window.close()">
                        </td>
                        <td style="padding-left: 10">
                            <p class="feed">
                                <asp:Label ID="feed" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
                <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                    <asp:TableRow HorizontalAlign="Left">
                        <asp:TableHeaderCell Width="65">Tgl</asp:TableHeaderCell>
                        <asp:TableHeaderCell Width="45">Jam</asp:TableHeaderCell>
                        <asp:TableHeaderCell Width="200" ColumnSpan="2">User</asp:TableHeaderCell>
                        <asp:TableHeaderCell Width="340">Keterangan</asp:TableHeaderCell>
                        <asp:TableHeaderCell>File</asp:TableHeaderCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
        <script type="text/javascript">
            function popGambar(foo) {
                openPopUp(foo, 700, 500);
            }
        </script>
    </form>
</body>
</html>

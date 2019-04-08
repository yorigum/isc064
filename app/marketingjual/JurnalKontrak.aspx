<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.JurnalKontrak" CodeFile="JurnalKontrak.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Jurnal Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Jurnal Kontrak">
</head>
<body onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavKontrak ID="NavKontrak1" runat="server" Aktif="2"></uc1:NavKontrak>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadKontrak ID="HeadKontrak1" runat="server"></uc1:HeadKontrak>
                <input type="text" style="display: none">
                <table cellspacing="5">
                    <tr>
                        <td colspan="3">
                            <asp:RadioButtonList ID="akt" runat="server" RepeatColumns="3"
                                RepeatDirection="Vertical" AutoPostBack="true">
                                <asp:ListItem Selected="True">FOLLOW UP DOKUMEN</asp:ListItem>
                                <asp:ListItem>FOLLOW UP TAGIHAN</asp:ListItem>
                                <asp:ListItem>URUSAN LEGAL</asp:ListItem>
                                <asp:ListItem>MEETING CUSTOMER</asp:ListItem>
                                <asp:ListItem>AGREEMENT LISAN</asp:ListItem>
                                <asp:ListItem>AKTIVITAS TELEPON</asp:ListItem>
                                <asp:ListItem>INFO TERAKHIR</asp:ListItem>
                                <asp:ListItem>COMPLAIN</asp:ListItem>
                                <asp:ListItem>LAINNYA</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">Keterangan Tambahan</td>
                        <td valign="top">:</td>
                        <td>

                            <asp:TextBox ID="baru" runat="server" CssClass="txt" Width="500" Height="70" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>File Hasil Scan</td>
                        <td>:</td>
                        <td>
                            <input type="file" id="file" runat="server" class="txt" style="width: 568px" name="file">
                        </td>
                    </tr>
                </table>
                <table height="50">
                    <tr>
                        <td>
                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK
                            </asp:LinkButton>
                        </td>
                        <td>
                            <input id="cancel" type="button" class="btn btn-red" value="Cancel" style="width: 75px" name="cancel"
                                onclick="window.close()">
                        </td>
                        <td style="padding-left: 10">
                            <p class="feed">
                                <asp:Label ID="feed" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
                <asp:Table ID="rpt" runat="server" CssClass="blue-skin tb" CellSpacing="1">
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

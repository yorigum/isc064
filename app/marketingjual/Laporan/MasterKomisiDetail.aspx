<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="C#" CodeFile="MasterKomisiDetail.aspx.cs" Inherits="ISC064.MARKETINGJUAL.Laporan.MasterKomisiDetail" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Master Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Komisi Per-kontrak">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Master Komisi Per-kontrak
                    </h1>
                    <p>
                        <b>No Kontrak - Customer :</b>
                        <br>
                        <asp:ListBox ID="nokontrak" runat="server" Width="40%" CssClass="ddl" Rows="10"></asp:ListBox>
                    </p>
                    <p class="pparam" style="display: none;">
                        <b>Status Kontrak :</b>
                        <asp:RadioButton ID="statusS" runat="server" GroupName="status" Font-Size="10" Text="SEMUA"></asp:RadioButton>
                        <asp:RadioButton ID="statusA" runat="server" GroupName="status" Font-Size="10" Text="AKTIF" Checked="True"></asp:RadioButton>
                        <asp:RadioButton ID="statusB" runat="server" GroupName="status" Font-Size="10" Text="BATAL"></asp:RadioButton>
                    </p>
                    <table>
                        <tr>
                            <td><b>Pilih Termin :</b>
                                <br>
                                <asp:ListBox ID="listtermin" runat="server" Width="120%" CssClass="ddl" Rows="4">
                                    <asp:ListItem Value="0" Selected>Semua Termin</asp:ListItem>
                                    <asp:ListItem Value="1">Termin 1</asp:ListItem>
                                    <asp:ListItem Value="2">Termin 2</asp:ListItem>
                                    <asp:ListItem Value="3">Termin 3</asp:ListItem>
                                </asp:ListBox>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" OnClick="scr_Click">
											<i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                    <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>

                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="middle" BorderColor="Black" BorderWidth="1">
            </asp:TableRow>
        </asp:Table>

        <script type="text/javascript">
            function popJadwalKomisi(NoKontrak) {
                openModal('../KontrakEdit.aspx?NoKontrak=' + NoKontrak, '800', '600');
            }
        </script>
    </form>
</body>
</html>

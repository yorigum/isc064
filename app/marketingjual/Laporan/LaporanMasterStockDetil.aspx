<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanMasterStockDetil"
    CodeFile="LaporanMasterStockDetil.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Laporan Master Stock Per Tipe Property (Detail)</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Stock Per Tipe Property (Detail)">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
    <div style="display: none">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
    </div>
    <table id="param" runat="server" width="100%" cellspacing="3">
        <tr>
            <td>
                <p class="comp" id="comp" runat="server">
                </p>
                <h1 id="judul" runat="server">
                    Laporan Master Stock Per Tipe Property (Detail)</h1>
                <table cellspacing="0" cellpadding="0">
                    <tr valign="top">
                        <td>
                        </td>
                        <td>
                            <p class="pparam">
                                <table>
                                    <tr>
                                        <td colspan="5">
                                            <b>Tipe Property :</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:DropDownList ID="tipe" runat="server">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                                <asp:ListItem>Apartment</asp:ListItem>
                                                <asp:ListItem>Service Apartment</asp:ListItem>
                                                <asp:ListItem>Medical Clinic</asp:ListItem>
                                                <asp:ListItem>Office</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <b>Status Titip Jual :</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:DropDownList ID="titipjual" runat="server" Width="200" CssClass="ddl">
                                                <asp:ListItem Selected="True" Value="">SEMUA</asp:ListItem>
                                                <asp:ListItem Value="1">Titip Jual</asp:ListItem>
                                                <asp:ListItem Value="0">Non Titip Jual</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <b>Status Paket Investasi :</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:DropDownList ID="papen" runat="server" Width="200" CssClass="ddl">
                                                <asp:ListItem Selected="True" Value="">SEMUA</asp:ListItem>
                                                <asp:ListItem Value="1">Paket Investasi</asp:ListItem>
                                                <asp:ListItem Value="0">Non Paket Investasi</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </p>
                        </td>
                    </tr>
                </table>
                <br />
                <div class="ins">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="scr" AccessKey="s" runat="server" Text="Screen Preview" Width="100"
                                    CssClass="btn" OnClick="scr_Click"></asp:Button>
                                <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" Width="100"
                                    CssClass="btn" OnClick="xls_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:Table ID="rpt" runat="server" CssClass="datatb" CellSpacing="0" >
        <asp:TableRow>
            <asp:TableCell ColumnSpan="14">
                <h1>
                    Laporan Penjualan Master Stock (Detail)</h1>
                <asp:Label ID="filter" runat="server"></asp:Label>
                <br />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow VerticalAlign="Bottom" BorderWidth="1" Style="border-collapse: collapse;"
            BorderColor="Black">
            <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Tipe</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Stock</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Sold</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black" RowSpan="3"
                VerticalAlign="Middle">Deviasi STU Sold</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Available</asp:TableHeaderCell>
        </asp:TableRow>
        <asp:TableRow BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">
            <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Unit</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Total Luas</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Nilai (excl. ppn)</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Unit</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Total Luas</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Nilai (excl. ppn)</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Unit</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Total Luas</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Nilai (excl. ppn)</asp:TableHeaderCell>
        </asp:TableRow>
        <asp:TableRow BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Net</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">SGA</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Net</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">SGA</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">Net</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="Gray" ForeColor="White"
                BorderWidth="1" Style="border-collapse: collapse;" BorderColor="Black">SGA</asp:TableHeaderCell>
        </asp:TableRow>
    </asp:Table>
    </form>
</body>
</html>

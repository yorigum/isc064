<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.NUP.Laporan.LaporanKasirNUP" CodeFile="LaporanKasirNUP.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Laporan Kasir NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Kasir">
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
                <h1 id="judul" class="title title-line" runat="server">
                    Laporan Kasir NUP
                </h1>
                <table cellspacing="0" cellpadding="0">
                    <tr valign="top">
                        <td>
                            <p class="pparam">
                                <asp:RadioButton ID="tglinput" runat="server" Text="Tanggal Input" Font-Bold="True"
                                    Font-Size="10" GroupName="tgl" Checked="True"></asp:RadioButton>
                                :
                            </p>
                            <table>
                                <tr>
                                    <td>
                                        dari
                                    </td>
                                    <td>
                                        <asp:TextBox ID="dari" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                                        <input class="btn" onclick="openCalendar('dari');" type="button" value="...">
                                    </td>
                                    <td rowspan="2">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        sampai
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sampai" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                                        <input class="btn" onclick="openCalendar('sampai');" type="button" value="...">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <p class="pparam">
                                <b>Kasir :</b>
                                <br>
                                <asp:ListBox ID="kasir" runat="server" CssClass="ddl" Width="300" Rows="12">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:ListBox>
                            </p>
                        </td>
                    </tr>
                </table>
                <br>
                <div class="ins">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="scr" AccessKey="s" CssClass="btn btn-blue" runat="server" Text="Screen Preview" Width="120" OnClick="scr_Click"></asp:Button>
                                <asp:Button ID="xls" AccessKey="e" CssClass="btn btn-green" runat="server" Text="Download Excel" Width="120" OnClick="xls_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="15" Font-Size="8pt">
                <asp:Label ID="lblHeader" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
                <br />
                <asp:Label ID="lblSubHeader" runat="server" Font-Size="9pt" Font-Bold="True"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="15" Font-Size="8pt">
                Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer Bank
                / BG = Cek Giro / UJ = Uang Jaminan / DN = Diskon.<br>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow VerticalAlign="Middle">
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="gray" ForeColor="white"
                RowSpan="2">NO. URUT</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="gray" ForeColor="white"
                RowSpan="2">TANGGAL TTS</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="gray" ForeColor="white"
                RowSpan="2">NO. NUP</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="gray" ForeColor="white"
                RowSpan="2">NO. FORM TTS</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="gray" ForeColor="white"
                RowSpan="2">NO. UNIT</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="gray" ForeColor="white"
                RowSpan="2">NAMA PEMESAN</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="gray" ForeColor="white"
                RowSpan="2">JUMLAH</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="gray" ForeColor="white"
                RowSpan="2">CARA PEMBAYARAN</asp:TableHeaderCell>
            <asp:TableHeaderCell BackColor="gray" ForeColor="white" ColumnSpan="4">PEMBAYARAN</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="gray" ForeColor="white"
                RowSpan="2">BANK</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="gray" ForeColor="white"
                RowSpan="2">PETUGAS KASIR</asp:TableHeaderCell>
            <asp:TableHeaderCell HorizontalAlign="Center" BackColor="gray" ForeColor="white"
                RowSpan="2">KETERANGAN</asp:TableHeaderCell>
        </asp:TableRow>
        <asp:TableRow VerticalAlign="Middle">
            <asp:TableHeaderCell BackColor="gray" ForeColor="white">CASH</asp:TableHeaderCell>
            <asp:TableHeaderCell BackColor="gray" ForeColor="white">DEBIT</asp:TableHeaderCell>
            <asp:TableHeaderCell BackColor="gray" ForeColor="white">TRANSFER</asp:TableHeaderCell>
            <asp:TableHeaderCell BackColor="gray" ForeColor="white">CC</asp:TableHeaderCell>
        </asp:TableRow>
    </asp:Table>
    </form>
</body>
</html>

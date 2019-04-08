<%@ Page Language="c#" Inherits="ISC064.NUP.Laporan.HasilLaunching" CodeFile="HasilLaunching.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="~/Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Hasil Launching NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Hasil Launching NUP">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <div style="display: none">
                <uc1:Head ID="Head1" runat="server"></uc1:Head>
            </div>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" class="title title-line" runat="server">Laporan Hasil Launching
                    </h1>
                    <p class="pparam">

                        <b>Aktivasi</b>
                        <br />
                        <asp:DropDownList ID="aktivasi" runat="server" Width="280" CssClass="ddl">
                            <asp:ListItem Value="0">SEMUA</asp:ListItem>
                            <asp:ListItem Value="1">Aktivasi</asp:ListItem>
                            <asp:ListItem Value="2">Tidak Aktivasi</asp:ListItem>
                        </asp:DropDownList><br />
                        <br />

                        <b>Status</b>
                        <br />
                        <asp:DropDownList ID="status" runat="server" Width="280" CssClass="ddl">
                            <asp:ListItem Value="0">SEMUA</asp:ListItem>
                            <asp:ListItem Value="PilihUnit">Pilih Unit</asp:ListItem>
                            <asp:ListItem Value="SudahClosing">Sudah Closing</asp:ListItem>
                            <asp:ListItem Value="SudahBayar">Sudah Bayar</asp:ListItem>
                        </asp:DropDownList><br />
                        <br />

                        <b>Tipe Properti</b>
                        <br />
                        <asp:DropDownList ID="tipepro" runat="server" Width="280" CssClass="ddl">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <b>Tgl. Pelaksanaan Launching</b>
                        <br />
                        Dari :&nbsp;<asp:TextBox ID="tglawal" runat="server" CssClass="tgl" Width="85"></asp:TextBox><input class="btn" onclick="openCalendar('tglawal');" type="button" value="...">
                        <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                        Sampai :&nbsp;<asp:TextBox ID="tglakhir" runat="server" CssClass="tgl" Width="85"></asp:TextBox><input class="btn" onclick="openCalendar('tglakhir');" type="button" value="...">
                        <asp:Label ID="Label1" runat="server" CssClass="err"></asp:Label>
                        <br />
                        <br />
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" OnClick="scr_Click">
											<i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Table ID="rptTotal" runat="server" CssClass="tb blue-skin" CellSpacing="3">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="18">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
                    <br />
                    <asp:Label ID="lblSubHeader" runat="server" Font-Size="9pt" Font-Bold="True"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Nama Project</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">NUP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Belum Aktivasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Aktivasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Tidak Memilih</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Memilih</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Pelunasan</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="3">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">No. NUP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Tipe Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Tgl. Aktivasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Unit Dipilih</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Cara Bayar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Pricelist</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Pelunasan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="White">Status</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

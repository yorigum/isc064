<%@ Page Language="c#" Inherits="ISC064.NUP.Laporan.SummaryNUP" CodeFile="SummaryNUP.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="LaporanSummary NUP">
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
                    <h1 id="judul" class="title title-line" runat="server">Laporan NUP
                    </h1>
                    <p class="pparam">
                        <b>Admin</b>
                        <br />
                        <asp:DropDownList ID="admin" runat="server" Width="280" CssClass="ddl">
                            <asp:ListItem Value="0">Semua Admin</asp:ListItem>
                        </asp:DropDownList><br />
                        Dari :&nbsp;<asp:TextBox ID="tglawal" runat="server" CssClass="txt_center" Width="85"></asp:TextBox><input class="btn" onclick="openCalendar('tglawal');" type="button" value="...">
                        <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                        sampai :&nbsp;<asp:TextBox ID="tglakhir" runat="server" CssClass="txt_center" Width="85"></asp:TextBox><input class="btn" onclick="openCalendar('tglakhir');" type="button" value="...">
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
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="3">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="18">
                    <asp:Label ID="lblHeader" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
                    <br />
                    <asp:Label ID="lblSubHeader" runat="server" Font-Size="9pt" Font-Bold="True"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Tgl NUP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Nomor NUP & Revisi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Nama Pemesan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Alamat Pemesan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">No Telp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Nama Sales/Agent</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">HP Sales/Agent</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Type</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Nama & NoRek Refund</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Admin</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Pembayaran</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

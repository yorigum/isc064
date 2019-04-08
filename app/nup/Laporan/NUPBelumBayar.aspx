<%@ Page Language="c#" Inherits="ISC064.NUP.Laporan.NUPBelumBayar" CodeFile="NUPBelumBayar.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Laporan NUP Belum Bayar</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Summary NUP">
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
                    <h1 id="judul" class="title title-line" runat="server">Laporan NUP Belum Bayar
                    </h1>
                    <p class="pparam">
                        <b>Admin</b>
                        <br />
                        <asp:DropDownList ID="admin" runat="server" Width="280" CssClass="ddl">
                            <asp:ListItem Value="0">Semua Admin</asp:ListItem>
                        </asp:DropDownList><br />
                        <b>As of :</b>
                        <br>
                        <asp:TextBox ID="tgl" runat="server" CssClass="txt_center" Width="85"></asp:TextBox><input class="btn" onclick="openCalendar('tgl');" type="button" value="...">
                        <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="scr" AccessKey="s" runat="server" Text="Screen Preview" Width="120" CssClass="btn btn-blue" OnClick="scr_Click"></asp:Button>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" Width="120" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
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
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Nama Pemesan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Alamat Pemesan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">No Telp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Nama Sales/Agent</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">HP Sales/Agent</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Type</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="gray" ForeColor="white">Nama & NoRek Refund</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

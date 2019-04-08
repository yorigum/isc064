<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.NUP.Laporan.TerpilihNUP" CodeFile="TerpilihNUP.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Unit Terpilih NUP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Terpilih NUP">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" cellspacing="3" width="100%" runat="server">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" class="title title-line" runat="server">Unit Terpilih NUP
                    </h1>
                    <table cellspacing="0" cellpadding="0">
                        <tr valign="top">
                            <td>
                                <p class="pparam">
                                    <b>Lokasi :</b>
                                    <br>
                                    <asp:ListBox ID="lokasi" runat="server" Rows="10" CssClass="ddl" Width="200">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
                            </td>
                            <td width="20"></td>
                            <td>
                                <p class="pparam">
                                    <b>Status :</b>
                                    <asp:RadioButton ID="statusS" runat="server" Checked="true" Text="Terpilih NUP" Font-Size="14" GroupName="status"></asp:RadioButton>
                                </p>
                                <br />
                                <p class="pparam">
                                    <asp:CheckBox ID="jenisCheck" runat="server" Text="<b>Jenis :</b>" Checked="True" AutoPostBack="True" OnCheckedChanged="jenisCheck_CheckedChanged"/>
                                    <asp:Label ID="jenisc" runat="server" CssClass="err"></asp:Label>
                                </p>
                                <asp:CheckBoxList ID="jenis" runat="server"></asp:CheckBoxList></td>
                        </tr>
                    </table>
                    <br>
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" Width="120" Text="Screen Preview" OnClick="scr_Click"></asp:Button>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" CssClass="btn btn-green" Width="120" Text="Download Excel" OnClick="xls_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="7" Font-Size="8pt">
						Status : P = Prioritas (hanya untuk satu NUP).
						<br>
						Unit yang ditampilkan merupakan unit yang sudah dipilih, namun belum terjadi kontrak.
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left">No. Urut</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Tgl. Input</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Lokasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">NUP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Pembayaran NUP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Agent</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

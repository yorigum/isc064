<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Laporan.PriceList" CodeFile="PriceList.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Price List</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Price List">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" width="100%" runat="server" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Price List
                    </h1>
                    <table cellspacing="0" cellpadding="0">
                        <tr valign="top">
                            <td>
                                <p class="pparam">
                                    <b>Periode :</b>
                                    <br>
                                    <asp:ListBox ID="periode" runat="server" Rows="10" CssClass="ddl" Width="200">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
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
                                    <asp:RadioButton ID="statusS" runat="server" Text="SEMUA" CssClass="igroup-radio" GroupName="status"></asp:RadioButton>
                                    <asp:RadioButton ID="statusA" runat="server" Text="AKTIF" CssClass="igroup-radio" GroupName="status" Checked="True"></asp:RadioButton>
                                    <asp:RadioButton ID="statusB" runat="server" Text="BLOKIR" CssClass="igroup-radio" GroupName="status"></asp:RadioButton>
                                    <asp:RadioButton ID="statusC" runat="server" Text="HOLD" CssClass="igroup-radio" GroupName="status"></asp:RadioButton>
                                </p>
                                <br />
                                <p class="pparam">
                                    <asp:CheckBox ID="jenisCheck" runat="server" Text="<b>Jenis :</b>" Checked="True" AutoPostBack="True" OnCheckedChanged="jenisCheck_CheckedChanged"></asp:CheckBox>
                                    <asp:Label ID="jenisc" runat="server" CssClass="err"></asp:Label>
                                    <br />
                                    <asp:CheckBoxList ID="jenis" runat="server"></asp:CheckBoxList>
                                </p>

                            </td>
                        </tr>
                    </table>
                    <br>
                </td>
            </tr>
            <tr>
                <td>

                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" runat="server" CssClass="btn btn-blue" AccessKey="s" OnClick="scr_Click">
											<i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="xls" runat="server" CssClass="btn btn-green" AccessKey="e" OnClick="xls_Click">
											Download Excel
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Table ID="rpt" runat="server" CssClass="blue-skin tb" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="7" Font-Size="8pt">
						Status : A = Aktif / B = Blokir.
						<br>
						Luas dalam meter persegi. Harga price list adalah dalam rupiah.
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left">No. Urut</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Tgl. Input</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Lokasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Luas Nett</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Status</asp:TableHeaderCell>
                <%--<asp:TableHeaderCell HorizontalAlign="Left">Status</asp:TableHeaderCell>--%>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Espt.aspx.cs" Inherits="ISC064.FINANCEAR.Espt" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Espt</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Generate SMS Blast">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none" />
        <h1 class="title title-line">Generate Template CSV</h1>
        <br />
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>Dari
                            </td>
                            <td>
                                <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                                    <asp:TextBox ID="dari" runat="server" type="text" CssClass="form-control" Style="width: 70%"></asp:TextBox>
                                    <span class="input-group-btn" style="height: 34px; display: block">
                                        <label for="dari" class="btn-a btn-cal default"><i class="fa fa-calendar"></i></label>
                                    </span>
                                </div>
                            </td>
                            <td>Sampai
                            </td>
                            <td>
                                <div class="item">
                                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                                        <asp:TextBox ID="sampai" runat="server" type="text" CssClass="form-control" Style="width: 70%"></asp:TextBox>
                                        <span class="input-group-btn" style="height: 34px; display: block">
                                            <label for="sampai" class="btn-a btn-cal default"><i class="fa fa-calendar"></i></label>
                                        </span>
                                    </div>
                                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div class="form-inline col pparam sub">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="scr" AccessKey="s" runat="server" Text="Screen Preview"
                                        CssClass="btn btn-blue" OnClick="scr_Click"></asp:Button>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download"
                                        CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Table ID="rpt" runat="server" CellSpacing="1" CssClass="tb blue-skin">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Kode Pajak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Kode Transaksi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Kode Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Kode Dokumen</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Flag VAT</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">NPWP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Nama Lawan Transaksi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Nomor Faktur / Dokumen</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Jenis Dokumen</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">No Faktur Pengganti / Retur</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Jenis Faktur Pengganti / Retur</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Tgl. Faktur / Dokumen</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">TglSSP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Masa Pajak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Tahun Pajak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Pembetulan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false">DPP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false">PPN</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false">PPnBM</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

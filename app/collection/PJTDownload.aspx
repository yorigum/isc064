<%@ Page Language="c#" Inherits="ISC064.COLLECTION.PJTDownload" CodeFile="PJTDownload.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tagihan - Export Data">
    <style>
        .cnt {
            padding: 10;
            height: 100%;
        }
    </style>
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none" />
        <h1 class="title title-line">Export Data Tagihan</h1>
        <table id="param" runat="server" width="100%" cellspacing="3">
                        <tr>
                            <td style="padding-left:10px;">
                                <b>Dari</b>
                            </td>
                            <td>
                                <asp:TextBox ID="dari" runat="server" type="text" CssClass=""></asp:TextBox>
                                <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                            </td>
                            <td style="padding-left:10px;">
                                <b>Sampai</b>
                            </td>
                            <td>
                                <asp:TextBox ID="sampai" runat="server" type="text" CssClass=""></asp:TextBox>
                                <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" Width="110"
                                        CssClass="btn btn-blue" OnClick="scr_Click"><i class="fa fa-search"></i> Preview</asp:LinkButton>
                                </td>
                                <td>
                                    <br />
                                </td>
                                <td>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download" Width="100"
                                        CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">No. HP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Pesan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Jadwal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Nama</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">No VA</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Tgl Jatuh Tempo</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Nama Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Angsuran</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Outstanding</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">Total</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

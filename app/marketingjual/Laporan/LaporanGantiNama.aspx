<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanGantiNama" CodeFile="LaporanGantiNama.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Pengalihan Hak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Pengalihan Hak">
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
                    <h1 id="judul" class="title title-line" runat="server">Laporan Pengalihan Hak
                    </h1>
                    <table cellspacing="0" cellpadding="0">
                        <tr valign="top">
                            <td></td>
                            <td>
                                <p class="pparam">
                                    <strong>Tgl. Pengalihan Hak:</strong>
                                    <table style="margin-top: 10px">
                                        <tr>
                                            <td>dari</td>
                                            <td>
                                                <asp:TextBox ID="dari" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                                                <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            </td>
                                            <td rowspan="2">&nbsp;&nbsp;</td>
                                            <td>sampai</td>
                                            <td>
                                                <asp:TextBox ID="sampai" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                                                <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                                            <td colspan="3">
                                                <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label></td>
                                        </tr>
                                    </table>
                                </p>
                            </td>
                            <tr>
                                <td></td>
                                <td>
                                    <table>
                            <tr>
                                <td>Project</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="project" runat="server" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Perusahaan</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="pers" runat="server">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
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
        <div id="headReport" runat="server">
        </div>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl. Pengalihan Hak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Customer Lama</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Customer Baru</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                <asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Project</asp:tableheadercell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

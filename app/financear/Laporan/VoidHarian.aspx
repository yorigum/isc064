<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.Laporan.VoidHarian" CodeFile="VoidHarian.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Void Harian</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Void Harian">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" class="title title-line" runat="server">Laporan Void Harian
                    </h1>
                    <table cellspacing="0" cellpadding="0">
                        <tr valign="top">
                            <td>
                                <table>
                                    <tr>
                                        <td colspan="6"><strong style="font-size: 14px">Tanggal Void</strong></td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 14px">Dari</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                                                <asp:TextBox ID="dari" runat="server" type="text" CssClass="form-control" Style="width: 55%; height: 20px; font-size: 12px"></asp:TextBox>
                                                <span class="input-group-btn" style="height: 34px; display: block">
                                                    <label for="dari" class="btn-a btn-cal"><i class="fa fa-calendar"></i></label>
                                                </span>
                                            </div>
                                        </td>
                                        <td rowspan="2">&nbsp;&nbsp;</td>
                                        <td style="font-size: 14px">Sampai</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                                                <asp:TextBox ID="sampai" runat="server" type="text" CssClass="form-control" Style="width: 55%; height: 20px; font-size: 12px"></asp:TextBox>
                                                <span class="input-group-btn" style="height: 34px; display: block">
                                                    <label for="sampai" class="btn-a btn-cal"><i class="fa fa-calendar"></i></label>
                                                </span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label></td>
                                    </tr>
                                </table>
                                <br />
                                <div>
                                    <p class="pparam">
                                        <b>Perusahaan :</b>
                                        <br />
                                        <asp:DropDownList ID="pers" runat="server" Width="175" AutoPostBack="true" OnSelectedIndexChanged="pers_SelectedIndexChanged">
                                            <asp:ListItem>SEMUA</asp:ListItem>
                                        </asp:DropDownList>
                                    </p>
                                    <p class="pparam">
                                        <b>Project :</b>
                                        <br />
                                        <asp:DropDownList ID="project" runat="server" Width="175" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                            <asp:ListItem>SEMUA</asp:ListItem>
                                        </asp:DropDownList>
                                    </p>
                                    <p class="pparam">
                                        <b>Kasir :</b>
                                        <br />
                                        <asp:ListBox ID="kasir" runat="server" CssClass="ddl" Width="300" Rows="12">
                                            <asp:ListItem>SEMUA</asp:ListItem>
                                        </asp:ListBox>
                                    </p>
                                </div>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <br>
                    <div class="form-inline col pparam sub">
                        <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" Text="Screen Preview" CssClass="btn btn-blue" OnClick="scr_Click"><i class="fa fa-search"></i> Screen Preview</asp:LinkButton>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                    <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom" CssClass="tb blue-skin">
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. TTS</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tgl. Void</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tanggal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Kasir</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Cara Bayar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Keterangan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. BG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tgl. BG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. KWT</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tgl. KWT</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Rekening</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Pelunasan Piutang</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Pembulatan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Nilai Pembayaran</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

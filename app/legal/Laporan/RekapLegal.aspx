<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.LEGAL.Laporan.RekapLegal" CodeFile="RekapLegal.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Rekap Legal</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Rekap Legal">
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
                    <h1 id="judul" runat="server" class="title title-line">Rekap Legal
                    </h1>
                    <table cellspacing="0" cellpadding="0">
                        <tr valign="top">
                            <td width="20"></td>
                            <td>
                                <p>Tanggal</p>
                                <table>
                                    <tr>
                                        <td>dari</td>
                                        <td>
                                            <asp:TextBox ID="dari" runat="server" CssClass="tgl" Width="85" style="text-align:center"></asp:TextBox>
                                            <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        </td>
                                        <td rowspan="2">&nbsp;&nbsp;</td>
                                        <td>sampai</td>
                                        <td>
                                            <asp:TextBox ID="sampai" runat="server" CssClass="tgl" Width="85" style="text-align:center"></asp:TextBox>
                                            <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Project </td>
                                        <td>
                                            <asp:DropDownList ID="project" runat="server" Width="175" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Perusahaan </td>
                                        <td>
                                            <asp:DropDownList ID="pers" runat="server" Width="175">
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
                                    <asp:LinkButton ID="scr" runat="server" CssClass="btn btn-blue" AccessKey="s" OnClick="scr_Click">
											    <i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="xls" runat="server" CssClass="btn btn-green" AccessKey="e" OnClick="xls_Click">
											    Download Excel
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="pdf" runat="server" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click">
											    Download PDF
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
        </div>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellPadding="10" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom" BackColor="#5c9bd1" ForeColor="white">
                <asp:TableHeaderCell HorizontalAlign="Left" VerticalAlign="Middle" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" VerticalAlign="Middle" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" VerticalAlign="Middle" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" VerticalAlign="Middle" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="White">PPJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="5" BackColor="#1E90FF" ForeColor="White">AJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="White">BAST</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="White">IMB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="White">Sertifikat</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow BackColor="#5c9bd1" ForeColor="white">
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Kelengkapan Berkas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Target PPJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">PPJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No PPJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Tanda Tangan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Target AJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">AJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No AJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Tanda Tangan AJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Target BAST</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">BAST</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No BAST</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Tanda Tangan BAST</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Target IMB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Proses IMB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">IMB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No IMB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Target Sertifikat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Proses Sertifikat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Sertifikat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No Sertifikat</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

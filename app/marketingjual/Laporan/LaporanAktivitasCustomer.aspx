<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanAktivitasCustomer" CodeFile="LaporanAktivitasCustomer.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Aktivitas Customer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Aktivitas Customer">
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
                    <h1 id="judul" class="title title-line" runat="server">Laporan Aktivitas Customer
                    </h1>
                    <table cellspacing="0" cellpadding="0">
                        <tr valign="top">
                            <td></td>
                            <td>
                                <p class="pparam">
                                    <strong>Tgl. Aktivitas:</strong>
                                    <table style="margin-top: 10px">
                                        <tr>
                                            <td>dari</td>
                                            <td style="min-width: 200px;">
                                                <asp:TextBox ID="dari" runat="server" CssClass="txt_center"></asp:TextBox>
                                                <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            </td>
                                            <td rowspan="2">&nbsp;&nbsp;</td>
                                            <td>sampai</td>
                                            <td>
                                                <asp:TextBox ID="sampai" runat="server" CssClass="txt_center"></asp:TextBox>
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
                        </tr>
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
                                    <asp:Button ID="xls" AccessKey="e" runat="server" CssClass="btn btn-green" Text="Download Excel" OnClick="xls_Click"></asp:Button>

                                    <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>


                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
            <asp:Label ID="headJudul" runat="server"></asp:Label>
        </div>
        <br />
        <asp:Label ID="lblA" runat="server"></asp:Label>
        <asp:Table ID="rptA" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom" BackColor="LightGray" HorizontalAlign="Center">
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Tgl. Reservasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No. Urut Reservasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Batas Waktu</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No. Urut Prioritas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Nilai Pengikatan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">PIC</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Label ID="lblB" runat="server"></asp:Label>
        <asp:Table ID="rptB" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom" BackColor="LightGray" HorizontalAlign="Center">
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Tgl. Batal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Alasan Pembatalan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Biaya Administrasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">PIC</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Label ID="lblC" runat="server"></asp:Label>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom" BackColor="LightGray" HorizontalAlign="Center">
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Tgl. Pindah Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Unit Sebelumnya</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Unit Sekarang</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Biaya Administrasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">PIC</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Label ID="lblD" runat="server"></asp:Label>
        <asp:Table ID="rptD" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom" BackColor="LightGray" HorizontalAlign="Center">
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Tgl. Pengalihan Hak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Nama Sebelumnya</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Nama Sekarang</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Biaya Administrasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">PIC</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:PlaceHolder ID="rp" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>

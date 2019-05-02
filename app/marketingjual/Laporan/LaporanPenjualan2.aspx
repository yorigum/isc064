<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanPenjualan2" CodeFile="LaporanPenjualan2.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Penjualan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Penjualan">

    <script type="text/javascript">
        function switchDetail(open, hide) {
            open.style.display = "";
            hide.style.display = "none";
        }
    </script>

</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" cellspacing="3" width="100%" runat="server">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server">
                    </p>
                    <h1 id="judul" class="title title-line" runat="server">Laporan Penjualan
                    </h1>
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <p class="pparam">
                                    <strong>Project :</strong>
                                    <br>
                                    <asp:DropDownList ID="project" runat="server" Width="200" CssClass="ddl" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:DropDownList>
                                </p>
                                <p class="pparam">
                                    <table>
                                        <tr>
                                            <td colspan="5">
                                                <strong>Tanggal:</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>dari
                                            </td>
                                            <td>
                                                <asp:TextBox ID="dari" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                                                <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            </td>
                                            <td rowspan="2">&nbsp;&nbsp;
                                            </td>
                                            <td>sampai
                                            </td>
                                            <td>
                                                <asp:TextBox ID="sampai" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                                                <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </p>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <p class="pparam">
                                    <b>Lokasi :</b>
                                    <br>
                                    <asp:ListBox ID="lokasi" runat="server" Rows="10" CssClass="ddl" Width="200">
                                        <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
                                <p class="pparam" style="display: none">
                                    <b>Status :</b>
                                    <asp:RadioButton ID="statusS" runat="server" GroupName="status" Font-Size="14" Text="SEMUA"></asp:RadioButton><asp:RadioButton ID="statusA" runat="server" GroupName="status"
                                        Font-Size="14" Text="AKTIF" Checked="True"></asp:RadioButton><asp:RadioButton ID="statusB"
                                            runat="server" GroupName="status" Font-Size="14" Text="BATAL"></asp:RadioButton>
                                </p>
                            </td>
                            <td width="20"></td>
                        </tr>
                        <tr>
                            <td>
                                <p class="pparam">
                                    <strong>Tipe Sales:</strong>
                                    <br>
                                    <asp:DropDownList ID="tipesales" runat="server" Width="200" CssClass="ddl" AutoPostBack="true" OnSelectedIndexChanged="tipesales_SelectedIndexChanged">
                                        <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                    </asp:DropDownList>
                                </p>
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <p class="pparam">
                                    <strong>Status Titip Jual:</strong>
                                    <br>
                                    <asp:DropDownList ID="titipjual" runat="server" Width="200" CssClass="ddl">
                                        <asp:ListItem Value="">SEMUA</asp:ListItem>
                                        <asp:ListItem Value="1">Titip Jual</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">Non Titip Jual</asp:ListItem>
                                    </asp:DropDownList>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p class="pparam">
                                    <strong>Sales:</strong>
                                    <br>
                                    <asp:DropDownList ID="ddlAgent" runat="server" Width="200" CssClass="ddl">
                                        <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                    </asp:DropDownList>
                                </p>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>
                                <p class="pparam">
                                    <strong>Perusahaan :</strong>
                                    <br>
                                    <asp:DropDownList ID="pers" runat="server" Width="200" CssClass="ddl">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 100%">
                                    <div style="display: none; float: left">
                                        <p class="pparam">
                                            <asp:CheckBox ID="cbPrincipal" runat="server" Text="<strong>Principal:</strong>"
                                                Checked="True" AutoPostBack="True" OnCheckedChanged="cbPrincipal_CheckedChanged"></asp:CheckBox><asp:Label ID="lblPrincipal" runat="server" CssClass="err"></asp:Label><asp:CheckBoxList
                                                    ID="cblPrincipal" runat="server">
                                                </asp:CheckBoxList>
                                        </p>
                                    </div>
                                    <div style="float: left">
                                        <p class="pparam">
                                            <asp:CheckBox ID="cbTipe" runat="server" Text="<strong>Tipe:</strong>" Checked="True"
                                                AutoPostBack="True" OnCheckedChanged="cbTipe_CheckedChanged"></asp:CheckBox>
                                            <asp:Label
                                                    ID="lblTipe" runat="server" CssClass="err"></asp:Label><asp:CheckBoxList ID="cblTipe"
                                                        runat="server">
                                                    </asp:CheckBoxList>
                                        </p>
                                        <p class="pparam">
                                            <strong>Tipe Properti :</strong>
                                            <br />
                                            <asp:DropDownList ID="tipepro" runat="server">
                                                <asp:ListItem>Tipe Properti :</asp:ListItem>
<%--                                                <asp:ListItem>Apartment</asp:ListItem>
                                                <asp:ListItem>Service Apartment</asp:ListItem>
                                                <asp:ListItem>Medical Clinic</asp:ListItem>
                                                <asp:ListItem>Office</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </p>
                                    </div>
                                </div>
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
                                    <asp:Button ID="xls" AccessKey="e"
                                        runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                    <%--<asp:LinkButton ID="pdf" runat="server" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click">
											Download PDF
                                    </asp:LinkButton>--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div>
            <asp:Label ID="headJudul" runat="server"></asp:Label>
        </div>
        <br>
        <asp:Table ID="graph" runat="server">
        </asp:Table>
        <br>
        <asp:Table ID="summary" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <img src='/Media/g2.jpg' height='15px' width='15px' />
                    Total Penjualan
                </asp:TableCell>
                <asp:TableCell>
                    :
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="sumall" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <img src='/Media/g1.jpg' height='15px' width='15px' />
                    Batal
                </asp:TableCell>
                <asp:TableCell>
                    :
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="sumbatal" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <img src='/Media/g3.jpg' height='15px' width='15px' />
                    Netto
                </asp:TableCell>
                <asp:TableCell>
                    :
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="sumnetto" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Label ID="lblA" Style="float: left" onclick="switchDetail(rptA, sA)" runat="server"></asp:Label><asp:Label
            ID="sA" Style="display: none" runat="server"></asp:Label><asp:Table ID="rptA" Style="clear: both"
                runat="server" CssClass="tb blue-skin" CellSpacing="1">
                <asp:TableRow VerticalAlign="Middle" onclick="switchDetail(sA, rptA)">
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tgl. Kontrak</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tipe Property</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Luas Tanah</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Luas Bangunan</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Arah hadap</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Price List</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Diskon</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Diskon Tambahan</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tambahan Harga Gimmick</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tambahan Harga Lain - Lain</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Bunga</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak (Include PPN)</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="3" BackColor="#1E90FF" ForeColor="White">Perincian Kontrak</asp:TableHeaderCell>
                     <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">NUP</asp:TableHeaderCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak (Exclude PPN)</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">PPN</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Skema Cara Bayar</asp:TableHeaderCell>
                    <%--<asp:TableCell Font-Size="8pt"></asp:TableCell>--%>
                </asp:TableRow>
            </asp:Table>
        <br>
        <asp:Label ID="lblB" Style="float: left" onclick="switchDetail(rptB, sumB)" runat="server"></asp:Label><asp:Label
            ID="sumB" Style="display: none" runat="server"></asp:Label><asp:Table ID="rptB" Style="clear: both"
                runat="server" CssClass="tb blue-skin" CellSpacing="1">
                <asp:TableRow VerticalAlign="Middle" onclick="switchDetail(sumB, rptB)">
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tgl. Kontrak</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No. Unit</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tipe Property</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Luas Tanah</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Luas Bangunan</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tanggal Batal</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Diskon</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Diskon Tambahan</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tambahan Harga Gimmick</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tambahan Harga Lain - Lain</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Bunga</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Biaya Administrasi</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Pembayaran</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Nilai Pengembalian</asp:TableHeaderCell>
                    <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">NUP</asp:TableHeaderCell>
                </asp:TableRow>
                <asp:TableRow>
                </asp:TableRow>
            </asp:Table>
        <br />
        <asp:Label ID="lblC" Style="float: left" onclick="switchDetail(rptC, sumC)" runat="server"></asp:Label><asp:Label
            ID="sumC" Style="display: none" onclick="switchDetail(rptC, sumC)" runat="server"></asp:Label>
        <asp:Table ID="rptC" runat="server" Style="clear: both" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle" onclick="switchDetail(sumC, rptC)">
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tipe Property</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Luas Tanah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Luas Bangunan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Cara Bayar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Price List</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Diskon</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Diskon Tambahan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tambahan Harga Gimmick</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tambahan Harga Lain - Lain</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Bunga</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Perincian Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">NUP</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">DPP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">PPN</asp:TableHeaderCell>
                <%--<asp:TableCell Font-Size="8pt"></asp:TableCell>--%>
            </asp:TableRow>
        </asp:Table>
        <br />
        <asp:Label ID="lblD" Style="float: left" onclick="switchDetail(rptD, sumD)" runat="server"></asp:Label><asp:Label
            ID="sumD" Style="display: none" onclick="switchDetail(rptD, sumD)" runat="server"></asp:Label>
        <asp:Table ID="rptD" runat="server" Style="clear: both" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle" onclick="switchDetail(sA, rptA)">
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tipe Property</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Luas Tanah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Luas Bangunan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Price List</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Diskon</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Diskon Tambahan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tambahan Harga Gimmick</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tambahan Harga Lain - Lain</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Bunga</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak (Include PPN)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Perincian Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">NUP</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak (Exclude PPN)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">PPN</asp:TableHeaderCell>
                <%--<asp:TableCell Font-Size="8pt"></asp:TableCell>--%>
            </asp:TableRow>
        </asp:Table>
        <br />
        <asp:PlaceHolder ID="rpt" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>

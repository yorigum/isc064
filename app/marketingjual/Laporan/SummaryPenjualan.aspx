<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.SummaryPenjualan" CodeFile="SummaryPenjualan.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Rekapitulasi Kontrak dan Pembayaran</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Summary Penjualan">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Rekapitulasi Kontrak dan Pembayaran
                    </h1>
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
                                <p class="pparam">
                                    <b>Sales :</b>
                                    <br>
                                    <asp:ListBox ID="agent" runat="server" Rows="10" CssClass="ddl" Width="200">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
                            </td>
                            <td width="20"></td>
                            <td>
                                <p class="pparam">
                                    <b>Status</b><b style="margin-left: 30px">:</b>
                                    <asp:RadioButton ID="statusS" runat="server" Text="SEMUA" GroupName="status" Style="padding-right: 20px"></asp:RadioButton>
                                    <asp:RadioButton ID="statusA" runat="server" Text="AKTIF" GroupName="status" Style="padding-right: 20px" Checked="True"></asp:RadioButton>
                                    <asp:RadioButton ID="statusB" runat="server" Text="BATAL" GroupName="status" Style="padding-right: 20px"></asp:RadioButton>
                                </p>
                                <div>
                                    <p class="pparam">
                                        <asp:RadioButton ID="tglkontrak" runat="server" Text="Tanggal Kontrak" Font-Size="10" GroupName="tgl"
                                            Checked="True" Font-Bold="True"></asp:RadioButton>:
                                    </p>
                                </div>
                                <div>
                                    <table>
                                        <tr>
                                            <td>dari</td>
                                            <td>
                                                <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                                <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            </td>
                                            <td rowspan="2">&nbsp;&nbsp;</td>
                                            <td>sampai</td>
                                            <td>
                                                <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
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
                                </div>
                                <p class="pparam">
                                    <asp:CheckBox ID="jenisCheck" runat="server" Text="<b>Jenis</b><b style='margin-left:20px'>:</b>" Checked="True" AutoPostBack="True" OnCheckedChanged="jenisCheck_CheckedChanged"></asp:CheckBox><asp:Label ID="jenisc" runat="server" CssClass="err"></asp:Label>
                                </p>
                                <br />
                                <asp:CheckBoxList ID="jenis" runat="server"></asp:CheckBoxList>
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
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tgl Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" BackColor="#1E90FF" ForeColor="White">No Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" BackColor="#1E90FF" ForeColor="White">View</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Biaya Admin</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Total</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="5" BackColor="#1E90FF" ForeColor="White">Pembayaran</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="5" BackColor="#1E90FF" ForeColor="White">Outstanding</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanPenjualan3" CodeFile="LaporanPenjualan3.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Penjualan Tahunan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Penjualan Tahunan">
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
                    <h1 id="judul" class="title title-line" runat="server">Laporan Penjualan Tahunan
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
                                <p class="pparam">
                                    <b>Principal :</b>
                                    <br>
                                    <asp:ListBox ID="agent" runat="server" Rows="10" CssClass="ddl" Width="200">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
                            </td>
                            <td width="20"></td>
                            <td>
                                <table>
                                    <tr>
                                        <td>Project</td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
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
                                <br />
                                <br />
                                Periode : 
								    <asp:DropDownList ID="thn1" runat="server"></asp:DropDownList>
                                Sampai : 
								    <asp:DropDownList ID="thn2" runat="server"></asp:DropDownList>
                                <br />
                                <br />

                                <br />
                                <p class="pparam">
                                    <asp:CheckBox ID="jenisCheck" runat="server" Text="<b>Jenis</b><b style='margin-left:17px'>:</b>" Checked="True" AutoPostBack="True" OnCheckedChanged="jenisCheck_CheckedChanged"></asp:CheckBox><asp:Label ID="jenisc" runat="server" CssClass="err"></asp:Label>
                                </p>
                                <asp:CheckBoxList ID="jenis" runat="server"></asp:CheckBoxList>
                                <p class="pparam" style="display: none;">
                                    <asp:CheckBox ID="cbcarabayar" runat="server" Text="<b>Cara Bayar :</b>" Checked="True" AutoPostBack="True" OnCheckedChanged="cbcarabayar_CheckedChanged"></asp:CheckBox><asp:Label ID="errcarabayar" runat="server" CssClass="err"></asp:Label><asp:CheckBoxList ID="cblcarabayar" runat="server"></asp:CheckBoxList>
                                </p>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" Text="Screen Preview" OnClick="scr_Click">
										<i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" CssClass="btn btn-green" Text="Download Excel" OnClick="xls_Click"></asp:Button>
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
        <asp:Label ID="headJudul" runat="server"></asp:Label>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <%--            <asp:TableRow>
                <asp:TableCell ColumnSpan="23" Font-Size="8pt">
						Status : A = Aktif / B = Batal.
                </asp:TableCell>
            </asp:TableRow>--%>
            <asp:TableRow VerticalAlign="Bottom" BackColor="LightGray">
                <asp:TableHeaderCell ColumnSpan="5" BackColor="#1E90FF" ForeColor="White">Total Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell BackColor="#1E90FF" ForeColor="White"></asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="4" BackColor="#1E90FF" ForeColor="White">Total Canceled</asp:TableHeaderCell>
                <asp:TableHeaderCell BackColor="#1E90FF" ForeColor="White"></asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="4" BackColor="#1E90FF" ForeColor="White">Total Net Sales</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Bottom" BackColor="LightGray">
                <asp:TableHeaderCell RowSpan="2" BackColor="#1E90FF" ForeColor="White">Tahun</asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Total Luas</asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="2" BackColor="#1E90FF" ForeColor="White">Nilai<br />( incl ppn )</asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="2" BackColor="#1E90FF" ForeColor="White"></asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Total Luas</asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="2" BackColor="#1E90FF" ForeColor="White">Nilai<br />( incl ppn )</asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="2" BackColor="#1E90FF" ForeColor="White"></asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Total Luas</asp:TableHeaderCell>
                <asp:TableHeaderCell RowSpan="2" BackColor="#1E90FF" ForeColor="White">Nilai<br />( incl ppn )</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow BackColor="LightGray">
                <asp:TableHeaderCell BackColor="#1E90FF" ForeColor="White">Net</asp:TableHeaderCell>
                <asp:TableHeaderCell BackColor="#1E90FF" ForeColor="White">SGA</asp:TableHeaderCell>
                <asp:TableHeaderCell BackColor="#1E90FF" ForeColor="White">Net</asp:TableHeaderCell>
                <asp:TableHeaderCell BackColor="#1E90FF" ForeColor="White">SGA</asp:TableHeaderCell>
                <asp:TableHeaderCell BackColor="#1E90FF" ForeColor="White">Net</asp:TableHeaderCell>
                <asp:TableHeaderCell BackColor="#1E90FF" ForeColor="White">SGA</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:PlaceHolder ID="rp" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>

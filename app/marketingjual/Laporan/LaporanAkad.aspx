<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LaporanAkad.aspx.cs" Inherits="ISC064.MARKETINGJUAL.Laporan.Laporan_LaporanAkad" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Akad</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Akad">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" style="width:100%">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" class="title title-line" runat="server">Laporan Akad
                    </h1>
                    <p class="pparam">
                        <asp:ListBox ID="lokasi" runat="server" Width="200" CssClass="ddl" Rows="10">
                            <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                        </asp:ListBox>
                    </p>
                    <table cellspacing="0" cellpadding="0">
                        <tr valign="top">
                            <td>
                                <p class="pparam">
                                    <asp:RadioButton ID="tbTgl" runat="server" Text="Tanggal Akad" Font-Bold="True" Font-Size="10" GroupName="tgl"
                                        Checked="True"></asp:RadioButton>
                                    <b>:</b>
                                </p>
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
                                <table style="margin-left:5px">
                                    <tr>
                                        <td style="width:108px"><strong>Perusahaan</strong></td>
                                        <td><strong>:</strong></td>
                                        <td>
                                            <asp:DropDownList ID="pers" runat="server" Style="margin-left: 5px">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:108px"><strong>Project</strong></td>
                                        <td><strong>:</strong></td>
                                        <td>
                                            <asp:DropDownList ID="project" runat="server" Style="margin-left: 5px">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <p class="pparam">
                                    <b>Rekening Bank :</b>
                                    <br>
                                    <asp:ListBox ID="rekening" runat="server" Width="250" CssClass="ddl" Rows="10">
                                        <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                        <%--<asp:listitem>MANDIRI</asp:listitem>
										    <asp:listitem>MANDIRI SYA</asp:listitem>
										    <asp:listitem>NISP</asp:listitem>
										    <asp:listitem>BCA</asp:listitem>
										    <asp:listitem>BCA SYA</asp:listitem>
										    <asp:listitem>BRI</asp:listitem>
										    <asp:listitem>BRI SYA</asp:listitem>
										    <asp:listitem>NIAGA</asp:listitem>
										    <asp:listitem>BII</asp:listitem>
										    <asp:listitem>BNI</asp:listitem>
										    <asp:listitem>BNI SYA</asp:listitem>
										    <asp:listitem>BAG</asp:listitem>
										    <asp:listitem>PERMATA</asp:listitem>
										    <asp:listitem>BUMI PUTERA</asp:listitem>
										    <asp:listitem>TUNAI</asp:listitem>
										    <asp:listitem>MEGA</asp:listitem>
										    <asp:listitem>MEGA SYA</asp:listitem>
										    <asp:listitem>BTN</asp:listitem>
										    <asp:listitem>BTN SYA</asp:listitem>--%>
											
                                    </asp:ListBox>
                                </p>
                                <%--	<p class="pparam"><strong>PT :</strong>
										<br>
										<asp:dropdownlist id="pt" runat="server" cssclass="ddl" width="250" autopostback="True" onselectedindexchanged="pt_SelectedIndexChanged">
											<asp:listitem selected="True">SEMUA</asp:listitem>
										</asp:dropdownlist></p>
									<p class="pparam" id="prj" runat="server"><strong>Project :</strong>
										<br>
										<asp:dropdownlist id="proj" runat="server" cssclass="ddl" width="250" autopostback="True" onselectedindexchanged="proj_SelectedIndexChanged">
											<asp:listitem selected="True">SEMUA</asp:listitem>
										</asp:dropdownlist></p>
									<p class="pparam" id="clsr" runat="server"><strong>Cluster :</strong>
										<br>
										<asp:dropdownlist id="cluster" runat="server" cssclass="ddl" width="250">
											<asp:listitem selected="True">SEMUA</asp:listitem>
										</asp:dropdownlist></p>--%>
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
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">#</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Akad</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl. Akad</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">NPWP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Alamat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White" ID="tower" runat="server">Tower</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Bank KPR</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Harga Jual Awal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Diskon Harga Jual</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Potensi KPR</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="left" BackColor="#1E90FF" ForeColor="White">Realisasi Akad</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

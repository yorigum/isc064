<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.MasterKontrak" CodeFile="MasterKontrak.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Master Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Kontrak">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head" runat="server" />
        <table id="param" cellspacing="3" width="100%" runat="server">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" class="title title-search" runat="server">Laporan Master Kontrak
                    </h1>
                    <table cellspacing="0" cellpadding="0">
                        <tr valign="top">
                            <td style="width: 300px">
                                <p class="pparam">
                                    <b>Lokasi :</b>
                                    <br>
                                    <asp:ListBox ID="lokasi" runat="server" Rows="10" CssClass="ddl" Width="200">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
                            </td>
                            <%--                            <td width="20"></td>--%>
                            <td>
                                <table>
                                    <tr>
                                        <td class="pparam"><b>Project</b><b style="padding-left: 75px">:</b></td>
                                        <%--<td>:</td>--%>
                                        <td>
                                            <asp:DropDownList ID="project" runat="server" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="pparam"><b>Perusahaan</b><b style="padding-left: 48px">:</b></td>
                                        <%--<td>:</td>--%>
                                        <td>
                                            <asp:DropDownList ID="pers" runat="server">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <p class="pparam">
                                                <b>Status</b><b style="padding-left: 80px">:</b>
                                                <asp:RadioButton ID="statusS" runat="server" Text="SEMUA" GroupName="status" Style="padding-right: 10px"></asp:RadioButton>
                                                <asp:RadioButton ID="statusA" runat="server" Text="AKTIF" GroupName="status" Style="padding-right: 10px" Checked="True"></asp:RadioButton>
                                                <asp:RadioButton ID="statusB" runat="server" Text="BATAL" GroupName="status" Style="padding-right: 10px"></asp:RadioButton>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <p class="pparam">
                                                <b>TTS</b><b style="padding-left: 95px">:</b>
                                                <asp:RadioButton ID="bfS" runat="server" Text="SEMUA" GroupName="bf" Checked="True" Style="padding-right: 10px"></asp:RadioButton>
                                                <asp:RadioButton ID="bf1" runat="server" Text="< 10 Juta" GroupName="bf" Style="padding-right: 10px"></asp:RadioButton>
                                                <asp:RadioButton ID="bf2" runat="server" Text="> 10 Juta" GroupName="bf" Style="padding-right: 10px"></asp:RadioButton>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <p class="pparam">
                                                <b>Pelunasan</b><b style="padding-left: 57px">:</b>
                                                <asp:RadioButton ID="semua" runat="server" Text="SEMUA" GroupName="LN" Checked="True" Style="padding-right: 10px"></asp:RadioButton>
                                                <asp:RadioButton ID="statusL0" runat="server" Text="Lunas 0%" GroupName="LN" Style="padding-right: 10px"></asp:RadioButton>
                                                <asp:RadioButton ID="statusL" runat="server" Text="Lunas >0%" GroupName="LN" Style="padding-right: 10px"></asp:RadioButton>
                                                <asp:RadioButton ID="statusL1" runat="server" Text="Lunas >20%" GroupName="LN" Style="padding-right: 10px"></asp:RadioButton>
                                                <asp:RadioButton ID="statusL2" runat="server" Text="Lunas" GroupName="LN" Style="padding-right: 10px"></asp:RadioButton>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:150px">
                                            <p class="pparam">
                                                <asp:RadioButton ID="tglkontrak" runat="server" Text="Tanggal Kontrak" GroupName="tgl"
                                                    Checked="True" Font-Bold="True"></asp:RadioButton><b> :</b>
                                            </p>
                                        </td>
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
                                        <td colspan="3">
                                            <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div>
                                                <p class="pparam">
                                                    <asp:CheckBox ID="jenisCheck" runat="server" Text="<b>Jenis</b><b style='padding-left:70px'>:</b>" Checked="True" AutoPostBack="True" OnCheckedChanged="jenisCheck_CheckedChanged"></asp:CheckBox>
                                                    <asp:Label ID="jenisc" runat="server" CssClass="err"></asp:Label>
                                                </p>
                                                <br />
                                                <asp:CheckBoxList ID="jenis" runat="server"></asp:CheckBoxList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <p class="pparam">
                                                <asp:CheckBox ID="cbcarabayar" runat="server" Text="<b>Cara Bayar</b><b style='padding-left:33px'>:</b>" Checked="True" AutoPostBack="True" OnCheckedChanged="cbcarabayar_CheckedChanged"></asp:CheckBox>
                                                <asp:Label ID="errcarabayar" runat="server" CssClass="err"></asp:Label>
                                                <asp:CheckBoxList ID="cblcarabayar" runat="server"></asp:CheckBoxList>
                                            </p>
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
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Urut</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl. Input</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Lokasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">NUP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Luas SG (M2)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Skema Cara Bayar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Cara Bayar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. VA</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Price List</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Diskon</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Bunga</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Diskon Tambahan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Biaya Administrasi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Fitting Out</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Nominal Bayar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Sisa Belum Bayar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Persentase Lunas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl. Pelunasan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Principal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl. PPJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. PPJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl. BAST</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. BAST</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl. AJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. AJB</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

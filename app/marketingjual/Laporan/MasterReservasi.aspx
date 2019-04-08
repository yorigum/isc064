<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.MasterReservasi" CodeFile="MasterReservasi.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Laporan Master Reservasi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Reservasi">
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
                    <h1 id="judul" runat="server" class="title">Laporan Master Reservasi</h1>
                    <br />
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
                                    <asp:ListBox ID="lokasi" runat="server" Width="200" CssClass="ddl" Rows="10">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
                                <p class="pparam">
                                    <b>Sales :</b>
                                    <br>
                                    <asp:ListBox ID="agent" runat="server" Width="200" CssClass="ddl" Rows="10">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
                            </td>
                            <td width="20"></td>
                            <td>
                                <p class="pparam">
                                    <b>Status :</b>
                                    <asp:RadioButton ID="statusS" runat="server" GroupName="status" Font-Size="14" Text="SEMUA"></asp:RadioButton>
                                    <asp:RadioButton ID="statusA" runat="server" GroupName="status" Font-Size="14" Text="AKTIF" Checked="True"></asp:RadioButton>
                                    <asp:RadioButton ID="statusE" runat="server" GroupName="status" Font-Size="14" Text="EXPIRED"></asp:RadioButton>
                                </p>
                                <p class="pparam">
                                    <asp:RadioButton ID="tglreservasi" runat="server" Text="Tanggal Reservasi" Font-Bold="True" Font-Size="10"
                                        GroupName="tgl" Checked="True"></asp:RadioButton>
                                    :
										<br>
                                    <asp:RadioButton ID="tglbatas" runat="server" Text="Tanggal Batas Waktu" Font-Bold="True" Font-Size="10"
                                        GroupName="tgl"></asp:RadioButton>
                                    :
                                </p>
                                <table>
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
                                <p class="pparam">
                                    <asp:CheckBox ID="jenisCheck" runat="server" Text="<b>Jenis :</b>" Checked="True" AutoPostBack="True" OnCheckedChanged="jenisCheck_CheckedChanged"></asp:CheckBox>
                                    <asp:Label ID="jenisc" runat="server" CssClass="err"></asp:Label>
                                </p>
                                <asp:CheckBoxList ID="jenis" runat="server"></asp:CheckBoxList>
                                <br>
                                <asp:CheckBox ID="toponly" runat="server" Text="Tampilkan reservasi urutan pertama saja"></asp:CheckBox>
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
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Tgl</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Principal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">No. Urut</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Batas Waktu</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">NUP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left">Skema</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right">Nilai</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

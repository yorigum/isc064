<%@ Page language="c#" Inherits="ISC064.COLLECTION.Laporan.DaftarTagihan" CodeFile="DaftarTagihan.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
		<title>Laporan Daftar Piutang</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
        <link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Daftar Piutang">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" width="100%" runat="server">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Daftar Piutang
                    </h1>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:Label ID="Label2" runat="server"><b>Perusahaan</b><b style="padding-left:11px;font-size:12px">:</b></asp:Label>
                            </div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:DropDownList ID="pers" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="pers_SelectedIndexChanged">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:Label ID="Label1" runat="server"><b>Project</b><b style="padding-left:48px;font-size:12px">:</b></asp:Label>
                            </div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Lokasi</p>
                                <table>
                                    <tr>
                                        <td>
                                            <b style="padding-right: 10px; vertical-align: top">:</b>
                                            <asp:ListBox ID="lokasi" runat="server" Width="200" CssClass="ddl" Rows="10">
                                                <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                            </asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Biaya Admin</p>
                                <table>
                                    <tr>                                        
                                        <td>
                                            <b style="padding-right: 10px; vertical-align: top">:</b>
                                            <asp:RadioButton ID="exclude" runat="server" GroupName="status" Text="EXCLUDE" Style="padding-right: 30px;"></asp:RadioButton>
                                            <asp:RadioButton ID="include" runat="server" GroupName="status" Text="INCLUDE" Checked="True"></asp:RadioButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">As Of</p>                                
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <b style="padding-right: 10px; vertical-align: top">:</b>
                                                <asp:TextBox ID="tgl" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                                                <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                                <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <br>
                        <br />
                        <div class="ins">
                            <table>
                                <tr>
                                    <td style="min-width: auto; padding-right: 10px">
                                        <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" OnClick="scr_Click"><i class="fa fa-search"></i> Screen Preview</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>
                                    </td>
								</tr>
							</table>
						</div>
                        </div>
					</td>
				</tr>
			</table>
        <div id="headReport" runat="server">
        </div>

			<asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
				<asp:tablerow verticalalign="Middle">
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Nama</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">No. Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Harga Kesepakatan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">PPN</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Total Harga</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Pembayaran</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Piutang</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2" ID="tradmin" runat="server" Visible="false">Biaya Admin</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white" columnspan="7">KELOMPOK UMUR TAGIHAN</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Jangka Pendek</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Center" backcolor="#1E90FF" forecolor="white" rowspan="2">Jangka Panjang</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Middle">
				    <asp:tableheadercell backcolor="#1E90FF" forecolor="white">Belum Jatuh Tempo</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white">0 - 30 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white">31 - 60 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white">61 - 90 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white">91 - 120 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white">121 - 180 hari</asp:tableheadercell>
					<asp:tableheadercell backcolor="#1E90FF" forecolor="white">>181 hari</asp:tableheadercell>
				</asp:tablerow>
				
			</asp:table>
		</form>
	</body>
</HTML>

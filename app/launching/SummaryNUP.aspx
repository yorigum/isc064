<%@ Page language="c#" Inherits="ISC064.LAUNCHING.Laporan.SummaryNUP" CodeFile="SummaryNUP.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Laporan NUP</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Summary NUP">
	</HEAD>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">
							Laporan NUP
						</h1>
						<p class="pparam">
						    <b>Admin</b>
						    <br />
						    <asp:DropDownList ID="admin" runat="server" Width="280" CssClass="ddl">
                                <asp:ListItem Value="0">Semua Admin</asp:ListItem>
                            </asp:DropDownList><br />
                            <b>Tipe</b>
						    <br />
						    <asp:DropDownList ID="ntipe" runat="server" CssClass="ddl">
                                <asp:ListItem Value="0">Semua</asp:ListItem>
                            </asp:DropDownList><br />
                            <br />
							Dari :&nbsp;<asp:textbox id="tglawal" runat="server" cssclass="txt_center" width="85"></asp:textbox><input class="btn" onclick="openCalendar('tglawal');" type="button" value="...">
							<asp:label id="tglc" runat="server" cssclass="err"></asp:label>
							sampai :&nbsp;<asp:textbox id="tglakhir" runat="server" cssclass="txt_center" width="85"></asp:textbox><input class="btn" onclick="openCalendar('tglakhir');" type="button" value="...">
							<asp:label id="Label1" runat="server" cssclass="err"></asp:label>
							<br /><br />
						</p>
						<P></P>
						<div class="ins">
							<table>
								<tr>
									<td>
										<asp:button id="scr" accesskey="s" runat="server" text="Screen Preview" width="100" cssclass="btn" onclick="scr_Click"></asp:button>
										<asp:button id="xls" accesskey="e" runat="server" text="Download Excel" width="100" cssclass="btn" onclick="xls_Click"></asp:button>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow>
					<asp:tablecell columnspan="18">
						<asp:Label ID="lblHeader" Runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
						<br />
						<asp:Label ID="lblSubHeader" Runat="server" Font-Size="9pt" Font-Bold="True"></asp:Label>
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Tgl NUP</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Nomor NUP & Revisi</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Nama Pemesan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Alamat Pemesan</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">No Telp</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Nama Sales/Agent</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">HP Sales/Agent</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Type</asp:tableheadercell>
				    <asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Nama & NoRek Refund</asp:tableheadercell>
				    <asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Admin</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Pembayaran</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</HTML>

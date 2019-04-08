<%@ Page Language="C#" CodeFile="LapRevisiNUP.aspx.cs" Inherits="ISC064.NUP.Laporan.LapRevisiNUP" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
    <head>
		<title>Laporan Revisi NUP</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Management Report">
	</head>
    <body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" class="title title-line" runat="server">
							Laporan Revisi NUP
						</h1>
						<p class="pparam">
							Dari :&nbsp;<asp:textbox id="tglawal" runat="server" cssclass="txt_center" width="85"></asp:textbox><input class="btn" onclick="openCalendar('tglawal');" type="button" value="...">
							<asp:label id="tglc" runat="server" cssclass="err"></asp:label>
							sampai :&nbsp;<asp:textbox id="tglakhir" runat="server" cssclass="txt_center" width="85"></asp:textbox><input class="btn" onclick="openCalendar('tglakhir');" type="button" value="...">
							<asp:label id="Label1" runat="server" cssclass="err"></asp:label>
							<br /><br />
						</p>
					</td>
				</tr>
                <tr>
                    <td>
                        <div class="ins">
							<table>
								<tr>
									<td>
										<asp:button id="scr" accesskey="s" runat="server" text="Screen Preview" width="120" CssClass="btn btn-blue" onclick="scr_Click"></asp:button>
										<asp:button id="xls" accesskey="e" runat="server" text="Download Excel" width="120" CssClass="btn btn-green" onclick="xls_Click"></asp:button>
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
						<asp:Label ID="lblPT" Runat="server" Font-Size="8pt" Font-Bold="True"></asp:Label><br />
						<asp:Label ID="lblHeader" runat="server" Font-Size="12pt" Font-Bold="true" />
						<br />
						<asp:Label ID="lblSubHeader" Runat="server" Font-Size="9pt" Font-Bold="True"></asp:Label>
					</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Tanggal<br/>TTS</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Nomor NUP</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Tanggal<br/>Ganti Nama</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Nama Pemesan - Lama</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="center" BackColor="gray" ForeColor="white">Nama Pemesan - Baru</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>

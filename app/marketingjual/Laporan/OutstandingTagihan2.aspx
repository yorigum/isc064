<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.OutstandingTagihan2" CodeFile="OutstandingTagihan2.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Laporan Sisa Tagihan Customer Berdasarkan Hari Keterlambatan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Sisa Tagihan Konsumen Berdasarkan Hari Keterlambatan">
	</head>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="DISPLAY: none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" cellspacing="3" width="100%" runat="server">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" runat="server">Laporan Sisa Tagihan Konsumen Berdasarkan Hari 
							Keterlambatan
						</h1>
						<table cellspacing="0" cellpadding="0">
							<tr valign="top">
								<td colspan="3">
									<p class="pparam"><b>As of:</b>
									</p>
									<asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
									<label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
									<asp:label id="tglc" runat="server" cssclass="err"></asp:label>
								</td>
							</tr>
							<tr valign="top">
								<td>
									<p class="pparam"><strong>Kelompok Hari Keterlambatan:</strong>
									</p>
									<asp:listbox id="hari" runat="server" width="200" cssclass="ddl" rows="10">
										<asp:listitem selected="True" value="3">> 3 hari</asp:listitem>
										<asp:listitem value="7">> 7 hari</asp:listitem>
										<asp:listitem value="15">> 15 hari</asp:listitem>
										<asp:listitem value="30">> 30 hari</asp:listitem>
										<asp:listitem value="60">> 60 hari</asp:listitem>
										<asp:listitem value="60">> 90 hari</asp:listitem>
									</asp:listbox>
									<p class="pparam"><strong>Lokasi:</strong></p>
									<asp:listbox id="lokasi" runat="server" width="200" cssclass="ddl" rows="10">
										<asp:listitem>SEMUA</asp:listitem>
									</asp:listbox>
								</td>
								<td width="20"></td>
								<td>
									<p class="pparam"><strong>Sales:</strong></p>
									<asp:listbox id="agent" runat="server" width="200" cssclass="ddl" rows="10">
										<asp:listitem>SEMUA</asp:listitem>
									</asp:listbox>
								</td>
							</tr>
						</table>
						<br>
						<div class="ins">
							<table>
								<tr>
									<td><asp:button id="scr" accesskey="s" runat="server" cssclass="btn" width="100" text="Screen Preview" onclick="scr_Click"></asp:button><asp:button id="xls" accesskey="e" runat="server" cssclass="btn" width="100" text="Download Excel" onclick="xls_Click"></asp:button></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow>
					<asp:tablecell columnspan="8"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow verticalalign="Bottom">
					<asp:tableheadercell horizontalalign="Left">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">No. Kontrak</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Tgl. Kontrak</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Customer</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Sales</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Agent</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Right">Nilai Kontrak</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>

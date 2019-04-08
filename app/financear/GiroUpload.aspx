<%@ Page language="c#" Inherits="ISC064.FINANCEAR.GiroUpload" CodeFile="GiroUpload.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Upload Cek Giro</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Upload Cek Giro">
		<style type="text/css">
		.sm { FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal }
		</style>
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<input type="text" style="DISPLAY:none">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1>Upload Cek Giro</h1>
			<br>
			<h2>Standard Pengisian</h2>
			<asp:table id="rule" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell>No.</asp:tableheadercell>
					<asp:tableheadercell width="200">Kolom</asp:tableheadercell>
					<asp:tableheadercell width="75">Format</asp:tableheadercell>
					<asp:tableheadercell>Panjang</asp:tableheadercell>
					<asp:tableheadercell width="300">Keterangan</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>1.</asp:tablecell>
					<asp:tablecell>No. BG</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>20</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>2.</asp:tablecell>
					<asp:tablecell>Tgl. Kwitansi</asp:tablecell>
					<asp:tablecell>TANGGAL</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
			</asp:table>
			<p style="PADDING-BOTTOM:10px; BORDER-BOTTOM:gray 1px dashed">
				<a href="Template\Giro.xls">Download Template...</a></p>
			<br>
			<table cellspacing="5">
				<tr>
					<td>File Excel</td>
					<td>:</td>
					<td>
						<input type="file" id="file" runat="server" class="txt" style="WIDTH:600px" name="file">
					</td>
				</tr>
			</table>
			<table height="50">
				<tr>
					<td>
						<asp:button id="upload" runat="server" cssclass="btn" width="75" text="OK" onclick="upload_Click"></asp:button>
					</td>
					<td style="PADDING-LEFT:10px">
						<p class="feed">
							<asp:label id="feed" runat="server"></asp:label>
						</p>
					</td>
				</tr>
			</table>
			<br>
			<h2 style="BORDER-TOP:gray 1px dashed; PADDING-TOP:10px">Gagal Upload :</h2>
			<asp:table id="gagal" runat="server"></asp:table>
		</form>
	</body>
</html>

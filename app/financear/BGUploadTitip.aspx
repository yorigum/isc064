<%@ Page language="c#" Inherits="ISC064.FINANCEAR.BGUploadTitip" CodeFile="BGUploadTitip.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Upload Pengelola Cek Giro</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Tanda Terima Sementara - Upload Pengelola Cek Giro">
		<style type="text/css">
		.sm  {font:8pt}
		</style>
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<input type="text" style="display:none">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1>Upload Pengelola Cek Giro</h1>
			<br>
			<h2>Standard Pengisian</h2>
			<asp:table id="rule" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell>No.</asp:tableheadercell>
					<asp:tableheadercell width="150">Kolom</asp:tableheadercell>
					<asp:tableheadercell width="75">Format</asp:tableheadercell>
					<asp:tableheadercell>Panjang</asp:tableheadercell>
					<asp:tableheadercell width="350">Keterangan</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>1.</asp:tablecell>
					<asp:tablecell>No. BG</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>20</asp:tablecell>
					<asp:tablecell cssclass="sm">Nomor cek giro yang tidak terdaftar akan diabaikan</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>2.</asp:tablecell>
					<asp:tablecell>Pengelola Pencairan</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>200</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
			</asp:table>
			<p style="border-bottom:1px solid gray;padding-bottom:10px">
				<a href="Template\Titip.xls">Download Template...</a></p>
			<br>
			<table cellspacing="5">
				<tr>
					<td>File Excel</td>
					<td>:</td>
					<td>
						<input type="file" id="file" runat="server" class="txt" style="width:600px" name="file">
					</td>
				</tr>
			</table>
			<table style="height:50px">
				<tr>
					<td>
						<asp:button id="upload" runat="server" cssclass="btn" width="75" text="OK" onclick="upload_Click"></asp:button>
					</td>
					<td style="padding-left:10px">
						<p class="feed">
							<asp:label id="feed" runat="server"></asp:label>
						</p>
					</td>
				</tr>
			</table>
			<br>
			<h2 style="border-top:1px solid gray;padding-top:10px">Gagal Upload :</h2>
			<asp:table id="gagal" runat="server"></asp:table>
		</form>
	</body>
</html>

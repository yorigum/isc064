<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.UploadJadwal" CodeFile="UploadJadwal.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Upload Jadwal Tagihan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Migrate - Upload Jadwal Tagihan">
		<style type="text/css">
		.sm  {font:8pt}
		</style>
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<input type="text" style="display:none">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Upload Jadwal Tagihan</h1>
			<br>
			<h2>Standard Pengisian</h2>
			<asp:table id="rule" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell>No.</asp:tableheadercell>
					<asp:tableheadercell width="200">Kolom</asp:tableheadercell>
					<asp:tableheadercell width="75">Format</asp:tableheadercell>
					<asp:tableheadercell>Panjang</asp:tableheadercell>
					<asp:tableheadercell width="300">Keterangan</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>1.</asp:tablecell>
					<asp:tablecell>No. Kontrak</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>2.</asp:tablecell>
					<asp:tablecell>Nama Tagihan</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>3.</asp:tablecell>
					<asp:tablecell>Tipe Tagihan</asp:tablecell>
					<asp:tablecell>RANGE</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm">BF/DP/ANG/ADM</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>4.</asp:tablecell>
					<asp:tablecell>Tgl. JT</asp:tablecell>
					<asp:tablecell>TANGGAL</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm">Tanggal Jatuh Tempo Tagihan</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>5.</asp:tablecell>
					<asp:tablecell>Nilai Tagihan</asp:tablecell>
					<asp:tablecell>ANGKA</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>6.</asp:tablecell>
					<asp:tablecell>Denda</asp:tablecell>
					<asp:tablecell>ANGKA</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>7.</asp:tablecell>
					<asp:tablecell>KPR</asp:tablecell>
					<asp:tablecell>RANGE</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm">YA/TIDAK</asp:tablecell>
				</asp:tablerow>
			</asp:table>
			<p style="border-bottom:1px dashed gray;padding-bottom:10">
				<a href="Template\MigrateJadwalTagihan.xls">Download Template...</a></p>
			<br>
			<table cellspacing="5">
				<tr>
					<td>File Excel</td>
					<td>:</td>
					<td>
						<input type="file" id="file" runat="server" class="txt" style="width:600" name="file">
					</td>
				</tr>
			</table>
			<table height="50">
				<tr>
					<td>
						<asp:LinkButton id="upload" runat="server" cssclass="btn btn-blue" width="75" onclick="upload_Click">
							<i class="fa fa-share"></i> OK
						</asp:LinkButton>
					</td>
					<td>
						<asp:checkbox id="overwrite" runat="server" text="Overwrite" ></asp:checkbox>
					</td>
					<td style="padding-left:10">
						<p class="feed">
							<asp:label id="feed" runat="server"></asp:label>
						</p>
					</td>
				</tr>
			</table>
			<br>
			<h2 style="border-top:1px dashed gray;padding-top:10">Gagal Upload :</h2>
			<asp:table id="gagal" runat="server"></asp:table>
		</form>
	</body>
</html>

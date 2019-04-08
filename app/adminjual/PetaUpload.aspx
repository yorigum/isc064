<%@ Page language="c#" Inherits="ISC064.ADMINJUAL.PetaUpload" CodeFile="PetaUpload.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Upload Peta Dasar</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Setup Peta Floor Plan - Upload Peta Dasar">
	</head>
	<body class="default-content">
		<form id="Form1" method="post" runat="server">
			<input type="text" style="display:none" />
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1>Upload Peta Dasar</h1>
			<br>
			<table cellspacing="5">
				<tr>
					<td>Nama Peta</td>
					<td>:</td>
					<td>
						<asp:textbox id="namapeta" runat="server" cssclass="txt" maxlength="100" width="300"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td colspan="3" style="font:8pt;color:gray">
						Field ini akan menjadi <u>nama peta floor plan</u> yang akan muncul di dalam 
						program
						<br>
						<br>
					</td>
				</tr>
				<tr>
					<td>File Peta Dasar</td>
					<td>:</td>
					<td>
						<input type="file" id="file1" runat="server" class="txt" style="width:600" name="file" />
					</td>
				</tr>
				<tr>
					<td colspan="3" style="font:8pt;color:gray">
						File gambar yang akan menjadi peta dasar.
						<br />
						Formatnya adalah <u>JPG dengan mode warna RGB</u>.
						<br />
						Dimensi yang dianjurkan adalah : 700 x 400
						<br />
						<br />
					</td>
				</tr>
				<%--<tr>
					<td>File Peta Nomor</td>
					<td>:</td>
					<td>
						<input type="file" id="file2" runat="server" class="txt" style="width:600" name="file"/>
					</td>
				</tr>
				<tr>
					<td colspan="3" style="font:8pt;color:gray">
						File gambar yang akan menjadi peta nomor.
						<br />
						Formatnya adalah <u>PNG dengan dasar transparan</u>.
						<br />
						Dimensi yang dianjurkan adalah : 700 x 400
						<br />
						<br />
					</td>
				</tr>--%>
			</table>
			<table height="50">
				<tr>
					<td>
						<asp:button id="upload" runat="server" cssclass="btn" width="75" text="OK" onclick="upload_Click"></asp:button>
					</td>
					<td style="padding-left:10">
						<p class="feed">
							<asp:label id="feed" runat="server"></asp:label>
						</p>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

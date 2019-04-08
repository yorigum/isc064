<%@ Page language="c#" Inherits="ISC064.ADMINJUAL.AgentUpload" CodeFile="AgentUpload.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Upload Marketing</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Sales - Upload Sales">
		<style type="text/css">
		.sm  {font:8pt}
		</style>
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<input type="text" style="display:none">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Upload Marketing</h1>
			<br>
            <asp:dropdownlist runat="server" ID="project" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
            </asp:dropdownlist>
            <br />
			<h2>Standard Pengisian</h2>
			<asp:table id="rule" runat="server" cssclass="blue-skin tb" cellspacing="0">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell>No.</asp:tableheadercell>
					<asp:tableheadercell width="150">Kolom</asp:tableheadercell>
					<asp:tableheadercell width="75">Format</asp:tableheadercell>
					<asp:tableheadercell>Panjang</asp:tableheadercell>
					<asp:tableheadercell width="350">Keterangan</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>1.</asp:tablecell>
					<asp:tablecell>Kode Sales</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>2.</asp:tablecell>
					<asp:tablecell>Nama</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>100</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
<%--				<asp:tablerow>
					<asp:tablecell>2.</asp:tablecell>
					<asp:tablecell>Principal</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>100</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>--%>
				<asp:tablerow>
					<asp:tablecell>3.</asp:tablecell>
					<asp:tablecell>Alamat</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>200</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
                <asp:tablerow>
					<asp:tablecell>4.</asp:tablecell>
					<asp:tablecell>Tipe</asp:tablecell>
					<asp:tablecell>RANGE</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
                <asp:tablerow>
					<asp:tablecell>5.</asp:tablecell>
					<asp:tablecell>Level</asp:tablecell>
					<asp:tablecell>RANGE</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
                <asp:tablerow>
					<asp:tablecell>6.</asp:tablecell>
					<asp:tablecell>Atasan</asp:tablecell>
					<asp:tablecell>RANGE</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>7.</asp:tablecell>
					<asp:tablecell>Email</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>8.</asp:tablecell>
					<asp:tablecell>Telepon</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>100</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
<%--				<asp:tablerow>
					<asp:tablecell>6.</asp:tablecell>
					<asp:tablecell>Skema Komisi Default</asp:tablecell>
					<asp:tablecell>RANGE</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>--%>
				<%--<asp:tablerow>
					<asp:tablecell>5.</asp:tablecell>
					<asp:tablecell>Tipe</asp:tablecell>
					<asp:tablecell>RANGE</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm">
					    <asp:label id="kettipe" runat="server"></asp:label>
					</asp:tablecell>
				</asp:tablerow>--%>
                <asp:tablerow>
					<asp:tablecell>9.</asp:tablecell>
					<asp:tablecell>Handphone</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
                <asp:tablerow>
					<asp:tablecell>10.</asp:tablecell>
					<asp:tablecell>Whatsapp</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
                <asp:tablerow>
					<asp:tablecell>11.</asp:tablecell>
					<asp:tablecell>NPWP</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
                <asp:tablerow>
					<asp:tablecell>12.</asp:tablecell>
					<asp:tablecell>Rekening</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
                <asp:tablerow>
					<asp:tablecell>13.</asp:tablecell>
					<asp:tablecell>Bank</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>20</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
                <asp:tablerow>
					<asp:tablecell>14.</asp:tablecell>
					<asp:tablecell>Atas Nama Rekening</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>100</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>              
<%--                <asp:tablerow>
					<asp:tablecell>15.</asp:tablecell>
					<asp:tablecell>Project</asp:tablecell>
					<asp:tablecell>RANGE</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>--%>
			</asp:table>
			<p style="border-bottom:1px solid gray;padding-bottom:10px">
				<a href="Template\Agent.xls">Download Template...</a></p>
			<br>
			<table cellspacing="5">
				<tr>
					<td>File Excel</td>
					<td>:</td>
					<td>
						<input type="file" id="file" runat="server" class="btn" style="width:600px; text-align:left;" name="file">
					</td>
				</tr>
			</table>
			<table height="50">
				<tr>
					<td>
                        <asp:LinkButton ID="upload" runat="server" CssClass="btn btn-blue" Width="75" OnClick="upload_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
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

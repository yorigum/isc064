<%@ Reference Page="~/SecLevel.aspx" %>
<%@ Page language="c#" Inherits="ISC064.SECURITY.MasterData" CodeFile="MasterData.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Master Data</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Master Data">
	</head>
	<body class="default-content">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Master Data </h1>
			<br>
			<table cellspacing="0" cellspacing="0">
				<tr valign="top">
					<td>
						<table cellspacing="5">
							<tr valign="top">
								<td class="igroup-label">Nama Perusahaan</td>
								
								<td>
									<b><asp:Label ID="namapt" runat="server"></asp:Label></b>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">NPWP</td>
								
								<td>
									<asp:TextBox ID="npwp" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Nama NPWP</td>
								
								<td>
									<asp:TextBox ID="npwpnama" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Alamat NPWP</td>
								
								<td>
									<asp:TextBox ID="npwpalamat" runat="server" cssclass="igroup" TextMode="MultiLine" Width="300" Height="70"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">No. Telp</td>
								
								<td>
									<asp:TextBox ID="notelp" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">No. Fax</td>
								
								<td>
									<asp:TextBox ID="nofax" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Alamat Perusahaan</td>
								
								<td>
									<asp:TextBox ID="alamat1" runat="server" cssclass="igroup" TextMode="MultiLine" Width="300" Height="70"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Alamat Project</td>
								
								<td>
									<asp:TextBox ID="alamat2" runat="server" cssclass="igroup" TextMode="MultiLine" Width="300" Height="70"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td colspan="3">
								    <br />
								    <u class="igroup-label">Data Rekening</u>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Nama Bank</td>
								
								<td>
									<asp:TextBox ID="namabank" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Cabang</td>
								
								<td>
									<asp:TextBox ID="cabang" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">No. Rekening</td>
								
								<td>
									<asp:TextBox ID="norek" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Atas Nama</td>
								
								<td>
									<asp:TextBox ID="an" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Alamat</td>
								
								<td>
									<asp:TextBox ID="alamatbank" runat="server" cssclass="igroup" TextMode="MultiLine" Width="300" Height="70"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td colspan="3">
								    <br />
								    <u class="igroup-label">Data NPWP</u>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Blok NPWP</td>
								
								<td>
									<asp:TextBox ID="BlokNPWP" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Nomor NPWP</td>
								
								<td>
									<asp:TextBox ID="NomorNPWP" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">RT NPWP</td>
								
								<td>
									<asp:TextBox ID="RTNPWP" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">RW NPWP</td>
								
								<td>
									<asp:TextBox ID="RWNPWP" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Kecamatan NPWP</td>
								
								<td>
									<asp:TextBox ID="KecamatanNPWP" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Kabupaten NPWP</td>
								
								<td>
									<asp:TextBox ID="KabupatenNPWP" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Propinsi NPWP</td>
								
								<td>
									<asp:TextBox ID="PropinsiNPWP" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
							<tr valign="top">
								<td class="igroup-label">Kode Pos NPWP</td>
								
								<td>
									<asp:TextBox ID="KodePosNPWP" runat="server" cssclass="igroup" MaxLength="50" Width="300"></asp:TextBox>
								</td>
							</tr>
						</table>
						<table height="50">
							<tr>
								<td>
									<asp:LinkButton id="save" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton> 
									<asp:label id="feed" runat="server"></asp:label>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

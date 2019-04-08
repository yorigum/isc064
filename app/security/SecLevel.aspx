<%@ Page language="c#" Inherits="ISC064.SECURITY.SecLevel" CodeFile="SecLevel.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Setup Security Level</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Setup Security Level">
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Setup Security Level</h1>
			<br>
			<table cellpadding="0" cellspacing="0">
				<tr valign="top">
					<td width="250">
						<ul id="aktif" runat="server" class="plike">
						</ul>
					</td>
					<td style="padding:5px 10px 0px 15px"><img src="/Media/line_vert.gif"></td>
					<td>
						<h2 style="padding-left:5px;padding-bottom:5px">Pendaftaran Security Level Baru</h2>
						<table cellspacing="5">
							<tr>
								<td class="igroup-label">Kode</td>
								
								<td>
									<asp:textbox id="kode" runat="server"  maxlength="10" cssclass="input-txt igroup"></asp:textbox>
									<asp:label id="kodec" runat="server" cssclass="err"></asp:label>
								</td>
							</tr>
							<tr>
								<td class="igroup-label">Nama</td>
								
								<td>
									<asp:textbox id="nama" runat="server" cssclass="input-txt igroup" maxlength="50"></asp:textbox>
									<asp:label id="namac" runat="server" cssclass="err"></asp:label>
								</td>
							</tr>
							<tr>
								<td colspan="3">
									<br>
									Konfigurasi security diturunkan / di-copy dari :
									<br>
									<asp:dropdownlist id="copyconfig" runat="server" cssclass="ddl form-control" width="268">
										<asp:listitem></asp:listitem>
									</asp:dropdownlist>
								</td>
							</tr>
						</table>
						<style type="text/css">
							.input-txt{
								width: 180px;
								border:1px solid #cccfd5;
								height: 25px;
							}
						</style>
						<table height="50">
							<tr>
								<td>
									<asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
								</td>
								<td style="padding-left:10px">
									<p class="feed">
										<asp:label id="feed" runat="server"></asp:label>
									</p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

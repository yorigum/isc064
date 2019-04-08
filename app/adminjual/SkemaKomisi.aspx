<%@ Page language="c#" Inherits="ISC064.ADMINJUAL.SkemaKomisi" CodeFile="SkemaKomisi.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Setup Skema Komisi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Setup Skema Komisi">
	</head>
	<body class="default-content">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1>Setup Skema Komisi</h1>
			<br>
			<table cellpadding="0" cellspacing="0" width="100%">
				<tr valign="top">
					<td width="49%">
					    
						<h2 style="padding-left:5;padding-bottom:5">Skema Komisi Agent Internal</h2>
						<p style="font:bold 10pt">&nbsp;</p>
                        <p style="font:bold 10pt">
                            <asp:LinkButton ID="RegSkemaIn" runat="server" onclick="RegSkemaIn_Click">Daftar Skema Baru</asp:LinkButton>
                        </p>
                        <p style="font:bold 10pt">&nbsp;</p>
                        <p style="font:bold 10pt">Skema Aktif</p>
						<ul id="aktifIntern" runat="server" class="plike">
						</ul>
						<br>
						<p style="font:bold 10pt">Skema Inaktif</p>
						<ul id="inaktifIntern" runat="server" class="plike">
						</ul>
					</td>
					<td style="padding:5 10 0 15;display:none"><img src="/Media/line_vert.gif"></td>
					<td width="49%" style="display: none;">						
						<h2 style="padding-left:5;padding-bottom:5">Skema Komisi Agent External</h2>
						<p style="font:bold 10pt">&nbsp;</p>
                        <p style="font:bold 10pt">
                            <asp:LinkButton ID="RegSkemaEx" runat="server" onclick="RegSkemaEx_Click">Daftar Skema Baru</asp:LinkButton>
                        </p>
                        <p style="font:bold 10pt">&nbsp;</p>
						<p style="font:bold 10pt">Skema Aktif</p>
						<ul id="aktifExtern" runat="server" class="plike">
						</ul>
						<br>
						<p style="font:bold 10pt">Skema Inaktif</p>
						<ul id="inaktifExtern" runat="server" class="plike">
						</ul>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

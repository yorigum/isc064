<%@ Reference Control="~/Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.SECURITY.MKA" CodeFile="MKA.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Matrikulasi Kode Akses</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Matrikulasi Kode Akses">
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Matrikulasi Kode Akses</h1>
            <br />
			<p class="feed">
				<asp:label id="feed" runat="server"></asp:label>
			</p>
			<table class="tb blue-skin" cellspacing="1">
				<tr align="left" id="head" runat="server">
					<th onmouseover="this.style.color='blue'" onmouseout="this.style.color=''" onclick="location.href='?'">
						Kode</th>
					<th style="width:250px">
						Nama</th>
				</tr>
				<asp:placeholder id="list" runat="server" enableviewstate="True"></asp:placeholder>
			</table>
			<table style="height:50px;">
				<tr>
					<td>
						<asp:LinkButton id="save" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
					</td>
				</tr>
			</table>
		</form>
        <script type="text/javascript">
            function mka(nomor) {
                popEditUser(nomor);
            }
        </script>
	</body>
</html>

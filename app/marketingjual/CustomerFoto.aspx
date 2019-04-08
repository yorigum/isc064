<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.CustomerFoto" CodeFile="CustomerFoto.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadCustomer" Src="HeadCustomer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavCustomer" Src="NavCustomer.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Foto Customer</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Customer - Foto Customer">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
			<div class="content-header">
				<uc1:navcustomer id="NavCustomer1" runat="server" aktif="3"></uc1:navcustomer>
			</div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headcustomer id="HeadCustomer1" runat="server"></uc1:headcustomer>
					<table cellspacing="5">
						<tr>
							<td>File Foto</td>
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
							<td style="padding-left:10">
								<p class="feed">
									<asp:label id="feed" runat="server"></asp:label>
								</p>
							</td>
						</tr>
					</table>
					<br>
					<table cellspacing="5">
						<tr>
							<td>
								<asp:image id="foto" runat="server"></asp:image>
							</td>
						</tr>
					</table>
				</div>
			</div>
		</form>
	</body>
</html>

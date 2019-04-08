<%@ Page language="c#" Inherits="ISC064.SECURITY.Reminder" CodeFile="Reminder.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Reminder</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Reminder">
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Reminder</h1>
			<br>
			<table class="blue-list-skin">
				<tr>
					<td style="padding-right:15px; font-weight:bold; font-size:24pt; line-height:normal; font-style:normal; font-variant:normal">
						<a href="ReminderKosong.aspx" style="font-size:24pt"><asp:label id="countKosong" runat="server"></asp:label></a>
					</td>
					<td style=" font-size:10pt; line-height:normal; font-style:normal; font-variant:normal">
						<a href="ReminderKosong.aspx">Mapping Kosong</a>
						<p style="font-weight:normal; font-size:8pt; line-height:normal; font-style:normal; font-variant:normal">
							Daftar mapping program tanpa konfigurasi security level. Umumnya adalah program 
							baru.
						</p>
					</td>
				</tr>
				<tr>
					<td  style="padding-right:15px; font-weight:bold; font-size:24pt; line-height:normal; font-style:normal; font-variant:normal">
						<a href="ReminderPass.aspx" style="font-size:24pt"><asp:label id="countPass" runat="server"></asp:label></a>
					</td>
					<td style="font-size:10pt; line-height:normal; font-style:normal; font-variant:normal">
						<a href="ReminderPass.aspx">Ganti Password</a>
						<p style="font-weight:normal; font-size:8pt; line-height:normal; font-style:normal; font-variant:normal">
							Username aktif yang sudah harus ganti password.
						</p>
					</td>
				</tr>
				<tr>
					<td  style="padding-right:15px; font-weight:bold; font-size:24pt; line-height:normal; font-style:normal; font-variant:normal">
						<a href="ReminderIdle.aspx" style="font-size:24pt"><asp:label id="countIdle" runat="server"></asp:label></a>
					</td>
					<td style="font-size:10pt; line-height:normal; font-style:normal; font-variant:normal">
						<a href="ReminderIdle.aspx">Username Idle</a>
						<p style="font-weight:normal; font-size:8pt; line-height:normal; font-style:normal; font-variant:normal">
							Username aktif yang tidak pernah login lagi ke dalam sistem selama satu tahun.
						</p>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

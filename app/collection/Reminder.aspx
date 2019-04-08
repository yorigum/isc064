<%@ Page language="c#" Inherits="ISC064.COLLECTION.Reminder" CodeFile="Reminder.aspx.cs" %>
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
		<meta name="ctrl" content="1">
		<meta name="sec" content="Reminder">
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Reminder</h1>
			<br />
            <asp:DropDownList runat="server" ID="project" AutoPostBack="true"></asp:DropDownList>
            <br />
			<table cellspacing="5" class="blue-list-skin">
				<tr>
					<td style="padding-right: 15px; font-weight: bold; font-size: 24pt; line-height: normal; font-style: normal; font-variant: normal">
						<a href="" id="kurang" runat="server"><asp:label id="countKurang" runat="server"></asp:label></a>
					</td>
					<td style="font-size: 10pt; line-height: normal; font-style: normal; font-variant: normal">
						<a href="" id="kurang2" runat="server">Tagihan Kurang Bayar</a>
						<p style="font:8pt">
							Tagihan yang sudah dibayar sebagian tetapi belum lunas.
						</p>
					</td>
				</tr>
				<tr>
					<td style="padding-right: 15px; font-weight: bold; font-size: 24pt; line-height: normal; font-style: normal; font-variant: normal">
						<a href="" id="pjt" runat="server"><asp:label id="countPJT" runat="server"></asp:label></a>
					</td>
					<td style="font-size: 10pt; line-height: normal; font-style: normal; font-variant: normal">
						<a href="" id="pjt2" runat="server">Surat Pemberitahuan Jatuh Tempo Baru</a>
						<p style="font:8pt">
							Surat Pemberitahuan Jatuh Tempo baru yang belum dicetak ke printer.
						</p>
					</td>
				</tr>
				<tr>
					<td style="padding-right: 15px; font-weight: bold; font-size: 24pt; line-height: normal; font-style: normal; font-variant: normal">
						<a href="" id="tunggakan" runat="server"><asp:label id="countTunggakan" runat="server"></asp:label></a>
					</td>
					<td style="font-size: 10pt; line-height: normal; font-style: normal; font-variant: normal" style="font:bold 10pt">
						<a href="" id="tunggakan2" runat="server">Surat Peringatan Baru</a>
						<p style="font:8pt">
							Surat Peringatan baru yang belum dicetak ke printer.
						</p>
					</td>
				</tr>
				<tr>
					<td style="padding-right: 15px; font-weight: bold; font-size: 24pt; line-height: normal; font-style: normal; font-variant: normal">
						<a href="" id="settle" runat="server"><asp:label id="countBelumSettle" runat="server"></asp:label></a>
					</td>
					<td style="font-size: 10pt; line-height: normal; font-style: normal; font-variant: normal">
						<a href="" id="settle2" runat="server">Surat Peringatan Belum Settle</a>
						<p style="font:8pt">
							Peringatan yang kasusnya belum di-settle oleh customer.
						</p>
					</td>
				</tr>
				<tr>
					<td style="padding-right: 15px; font-weight: bold; font-size: 24pt; line-height: normal; font-style: normal; font-variant: normal">
						<a href="" id="pjt7" runat="server"><asp:label id="coutPJT7" runat="server"></asp:label></a>
					</td>
					<td style="font-size: 10pt; line-height: normal; font-style: normal; font-variant: normal">
						<a href="" id="pjt72" runat="server">Pemberitahuan Jatuh Tempo</a>
						<p style="font:8pt">
							Tagihan yang akan jatuh tempo 7 hari kedepan.
						</p>
					</td>
				</tr>
				<tr>
					<td style="padding-right: 15px; font-weight: bold; font-size: 24pt; line-height: normal; font-style: normal; font-variant: normal">
						<a href="" id="fu" runat="server"><asp:label id="countFU" runat="server"></asp:label></a>
					</td>
					<td style="font-size: 10pt; line-height: normal; font-style: normal; font-variant: normal">
						<a href="" id="fu2" runat="server">Follow Up Pemberitahuan Jatuh Tempo</a>
						<p style="font:8pt">
							Tagihan yang akan jatuh tempo 7 hari kedepan dan belum di Follow Up.
						</p>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

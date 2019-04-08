<%@ Reference Control="~/PrintKMTemplate.ascx" %>
<%@ Page language="c#" Inherits="ISC064.FINANCEAR.PrintKMBatch" CodeFile="PrintKMBatch.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Print Voucher Kas Masuk</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Print.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Print Voucher Kas Masuk (Batch)">
	</head>
	<body onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup layar print ini?')) window.close();">
		<form id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<div id="reprint" runat="server">
				<h1 style="border-bottom:1px solid silver">Print Voucher Kas Masuk</h1>
				<p style="padding:5px"><b>Tanggal :</b></p>
				<table>
					<tr>
						<td>
							dari</td>
						<td>
							<asp:textbox id="dari" runat="server" cssclass="txt_center" width="85"></asp:textbox>
							<label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						</td>
						<td>&nbsp;</td>
						<td>sampai</td>
						<td>
							<asp:textbox id="sampai" runat="server" cssclass="txt_center" width="85"></asp:textbox>
							<label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						</td>
					</tr>
					<tr>
						<td colspan="3"><asp:label id="daric" runat="server" cssclass="err"></asp:label></td>
						<td colspan="3"><asp:label id="sampaic" runat="server" cssclass="err"></asp:label></td>
					</tr>
				</table>
				<br>
				<div class="ins">
					<table>
						<tr>
							<td>
								<asp:button id="print" runat="server" cssclass="btn" text="Print" width="75" accesskey="p" onclick="print_Click"></asp:button>
							</td>
						</tr>
					</table>
				</div>
			</div>
			<div id="print">
				<asp:placeholder id="list" runat="server"></asp:placeholder>
			</div>
		</form>
	</body>
</html>

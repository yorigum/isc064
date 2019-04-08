<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SkemaBunga.aspx.cs" Inherits="ISC064.SETTINGS.SkemaBunga" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
		<title>Set Skema Bunga</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="/Js/NumberFormat.js"></script>
		<meta name="ctrl" content="1">
		<meta name="sec" content="(Pop-Up) Set Skema Bunga">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
	</head>
	<body class="pop" onkeyup="if(event.keyCode==27){window.close()}">
		<form id="Form1" method="post" runat="server">
			<table>
				<tr>
					<td><asp:textbox id="ket1" runat="server" cssclass="txt" width="300"></asp:textbox></td>
					<td><asp:textbox id="persen1" runat="server" cssclass="txt_center" width="70"></asp:textbox>%</td>
				</tr>
				<tr>
					<td><asp:textbox id="ket2" runat="server" cssclass="txt" width="300"></asp:textbox></td>
					<td><asp:textbox id="persen2" runat="server" cssclass="txt_center" width="70"></asp:textbox>%</td>
				</tr>
				<tr>
					<td><asp:textbox id="ket3" runat="server" cssclass="txt" width="300"></asp:textbox></td>
					<td><asp:textbox id="persen3" runat="server" cssclass="txt_center" width="70"></asp:textbox>%</td>
				</tr>
				<tr>
					<td><asp:textbox id="ket4" runat="server" cssclass="txt" width="300"></asp:textbox></td>
					<td><asp:textbox id="persen4" runat="server" cssclass="txt_center" width="70"></asp:textbox>%</td>
				</tr>
				<tr>
					<td><asp:textbox id="ket5" runat="server" cssclass="txt" width="300"></asp:textbox></td>
					<td><asp:textbox id="persen5" runat="server" cssclass="txt_center" width="70"></asp:textbox>%</td>
				</tr>
				<tr>
					<td><asp:textbox id="ket6" runat="server" cssclass="txt" width="300"></asp:textbox></td>
					<td><asp:textbox id="persen6" runat="server" cssclass="txt_center" width="70"></asp:textbox>%</td>
				</tr>
				<tr>
					<td><asp:textbox id="ket7" runat="server" cssclass="txt" width="300"></asp:textbox></td>
					<td><asp:textbox id="persen7" runat="server" cssclass="txt_center" width="70"></asp:textbox>%</td>
				</tr>
				<tr>
					<td><asp:textbox id="ket8" runat="server" cssclass="txt" width="300"></asp:textbox></td>
					<td><asp:textbox id="persen8" runat="server" cssclass="txt_center" width="70"></asp:textbox>%</td>
				</tr>
				<tr>
					<td><asp:textbox id="ket9" runat="server" cssclass="txt" width="300"></asp:textbox></td>
					<td><asp:textbox id="persen9" runat="server" cssclass="txt_center" width="70"></asp:textbox>%</td>
				</tr>
				<tr style="display:none">
					<td><asp:textbox id="ket10" runat="server" cssclass="txt" width="300"></asp:textbox></td>
					<td><asp:textbox id="persen10" runat="server" cssclass="txt_center" width="70"></asp:textbox>%</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:LinkButton id="ok" runat="server" text=" OK " cssclass="btn btn-blue" width="75" onclick="ok_Click"><i class="fa fa-share"></i> 
                                    OK</asp:LinkButton>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

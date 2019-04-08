<%@ Page language="c#" Inherits="ISC064.ADMINJUAL.AgentSSK" CodeFile="AgentSSK.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadAgent" Src="HeadAgent.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavAgent" Src="NavAgent.ascx" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Set Skema Komisi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Sales - Set Skema Komisi">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close()">
		
		<form id="Form1" method="post" runat="server">
			<uc1:navagent id="NavAgent1" runat="server" aktif="2"></uc1:navagent>
			<div class="tabdata">
				<div class="pad">
					<uc1:headagent id="HeadAgent1" runat="server"></uc1:headagent>
					<table cellspacing="5">
						<tr align="left">
							<th colspan="2">
								Target Marketing</th>
							<th>
								Skema Komisi</th>
						</tr>
						<tr>
							<td colspan="2">
								Standard
							</td>
							<td>
								<asp:dropdownlist id="skema0" runat="server" cssclass="ddl" width="300">
									<asp:listitem value="0">SKEMA :</asp:listitem>
								</asp:dropdownlist>
							</td>
						</tr>
						<tr>
							<td style="font:bold 15pt">
								1
							</td>
							<td>
								<asp:textbox id="target1" runat="server" cssclass="txt_num" width="150"></asp:textbox>
							</td>
							<td>
								<asp:dropdownlist id="skema1" runat="server" cssclass="ddl" width="300">
									<asp:listitem value="0">SKEMA :</asp:listitem>
								</asp:dropdownlist>
							</td>
							<td><asp:label id="target1c" runat="server" cssclass="err"></asp:label></td>
						</tr>
						<tr>
							<td style="font:bold 15pt">
								2
							</td>
							<td>
								<asp:textbox id="target2" runat="server" cssclass="txt_num" width="150"></asp:textbox>
							</td>
							<td>
								<asp:dropdownlist id="skema2" runat="server" cssclass="ddl" width="300">
									<asp:listitem value="0">SKEMA :</asp:listitem>
								</asp:dropdownlist>
							</td>
							<td><asp:label id="target2c" runat="server" cssclass="err"></asp:label></td>
						</tr>
						<tr>
							<td style="font:bold 15pt">
								3
							</td>
							<td>
								<asp:textbox id="target3" runat="server" cssclass="txt_num" width="150"></asp:textbox>
							</td>
							<td>
								<asp:dropdownlist id="skema3" runat="server" cssclass="ddl" width="300">
									<asp:listitem value="0">SKEMA :</asp:listitem>
								</asp:dropdownlist>
							</td>
							<td><asp:label id="target3c" runat="server" cssclass="err"></asp:label></td>
						</tr>
						<tr>
							<td style="font:bold 15pt">
								4
							</td>
							<td>
								<asp:textbox id="target4" runat="server" cssclass="txt_num" width="150"></asp:textbox>
							</td>
							<td>
								<asp:dropdownlist id="skema4" runat="server" cssclass="ddl" width="300">
									<asp:listitem value="0">SKEMA :</asp:listitem>
								</asp:dropdownlist>
							</td>
							<td><asp:label id="target4c" runat="server" cssclass="err"></asp:label></td>
						</tr>
						<tr>
							<td style="font:bold 15pt">
								5
							</td>
							<td>
								<asp:textbox id="target5" runat="server" cssclass="txt_num" width="150"></asp:textbox>
							</td>
							<td>
								<asp:dropdownlist id="skema5" runat="server" cssclass="ddl" width="300">
									<asp:listitem value="0">SKEMA :</asp:listitem>
								</asp:dropdownlist>
							</td>
							<td><asp:label id="target5c" runat="server" cssclass="err"></asp:label></td>
						</tr>
					</table>
					<table height="50">
						<tr>
							<td>
								<asp:button id="ok" runat="server" cssclass="btn btn-blue" text="OK" width="75" onclick="ok_Click"></asp:button>
							</td>
							<td>
								<input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel">
							</td>
							<td>
								<asp:button id="save" runat="server" cssclass="btn btn-orange" text="Apply" width="75" accesskey="a" onclick="save_Click"></asp:button>
							</td>
							<td style="padding-left:10">
								<p class="feed">
									<asp:label id="feed" runat="server"></asp:label>
								</p>
							</td>
						</tr>
					</table>
				</div>
			</div>
		</form>
	</body>
</html>

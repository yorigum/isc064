<%@ Page language="c#" Inherits="ISC064.LAUNCHING.ShowPeta4C2" CodeFile="ShowPeta4C2.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Peta Site Plan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta http-equiv="Refresh" content="15"> <%--Refresh 15 secs--%>
		<meta name="ctrl" content="1">
		<meta name="sec" content="Unit - Peta Floor Plan Detil">
		<meta http-equiv="pragma" content="no-cache">
	</head>
	<body onkeyup="if(event.keyCode==27) history.back(-1)">
		<form id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<div style="z-index:1;LEFT:0px;WIDTH:100%;POSITION:absolute;">
				<table width="100%" height="100%" cellpadding="0" cellspacing="0">
					<tr>
						<td height="50%" align="center" valign="middle">
							<asp:image id="dasar1" runat="server"></asp:image>
						</td>
						<td height="50%" align="center" valign="middle">
							<asp:image id="dasar2" runat="server"></asp:image>
						</td>
					</tr>
					<tr>
						<td height="50%" align="center" valign="middle">
							<asp:image id="dasar3" runat="server"></asp:image>
						</td>
						<td height="50%" align="center" valign="middle">
							<asp:image id="dasar4" runat="server"></asp:image>
						</td>
					</tr>
				</table>
			</div>
			<div style="z-index:3;LEFT:0px;WIDTH:100%;POSITION:absolute;">
				<table width="100%" height="100%" cellpadding="0" cellspacing="0">
					<tr>
						<td height="100%" align="center" valign="middle">
							<asp:image id="koordinat1" runat="server"></asp:image>
							<asp:label id="coord1" runat="server"></asp:label>
						</td>
						<td height="100%" align="center" valign="middle">
							<asp:image id="koordinat2" runat="server"></asp:image>
							<asp:label id="coord2" runat="server"></asp:label>
						</td>
					</tr>
					<tr>
						<td height="100%" align="center" valign="middle">
							<asp:image id="koordinat3" runat="server"></asp:image>
							<asp:label id="coord3" runat="server"></asp:label>
						</td>
						<td height="100%" align="center" valign="middle">
							<asp:image id="koordinat4" runat="server"></asp:image>
							<asp:label id="coord4" runat="server"></asp:label>
						</td>
					</tr>
				</table>
			</div>
			<div style="z-index:2;LEFT:0px;WIDTH:100%;POSITION:absolute;">
                <table width="100%" height="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="100%" align="center" valign="middle">
                            <asp:Image ID="trans1" runat="server"></asp:Image>
                        </td>
                        <td height="100%" align="center" valign="middle">
                            <asp:Image ID="trans2" runat="server"></asp:Image>
                        </td>
                    </tr>
                    <tr>
                        <td height="100%" align="center" valign="middle">
                            <asp:Image ID="trans3" runat="server"></asp:Image>
                        </td>
                        <td height="100%" align="center" valign="middle">
                            <asp:Image ID="trans4" runat="server"></asp:Image>
                        </td>
                    </tr>
                </table>
            </div>
		</form>
	</body>
</html>

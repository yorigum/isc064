<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.UnitPetaList" CodeFile="UnitPetaList.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Peta Floor Plan List</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Unit - Peta Floor Plan List">
		<meta http-equiv="pragma" content="no-cache">
         <style>
            
            .img{
                border:solid 1px #bbbbbb;
            }
        </style>
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27) history.back(-1)">
		<form id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head>
            <p valign="top" style="min-width:300px;text-decoration:none;font-size:24px;">Site Plan</p>
            <table>
                <tr>
                    <td valign="top" style="min-width:300px;">
                         <asp:Table ID="tbList" runat="server"> </asp:Table>
                    </td>
                    <td valign="top">
                        <asp:PlaceHolder ID="imgpanel" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
                
            </table>
            
		</form>
	</body>
</html>

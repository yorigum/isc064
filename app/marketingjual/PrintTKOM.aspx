<%@ Reference Control="~/PrintTKOMTemplate.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.PrintTKOM" CodeFile="PrintTKOM.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Print TKOM</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<%--<LINK href="/Media/Print.css" type="text/css" rel="stylesheet">--%>
		<meta name="ctrl" content="3">
		<meta name="sec" content="Print Tanda Terima Komisi">
        <style type="text/css">
            #print td {
                font-size: 5pt;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }
            #print div {
                font-size: 5pt;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }
        </style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:PlaceHolder ID="list" Runat="server"></asp:PlaceHolder>
		</form>
	</body>
</HTML>

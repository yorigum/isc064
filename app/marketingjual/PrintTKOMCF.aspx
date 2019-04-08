<%@ Reference Control="~/PrintTKOMTemplateCF.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.PrintTKOMCF" CodeFile="PrintTKOMCF.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Print TKOM Closing Fee</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/Media/Print.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="3">
		<meta name="sec" content="Print Tanda Terima Komisi">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:PlaceHolder ID="list" Runat="server"></asp:PlaceHolder>
		</form>
	</body>
</HTML>

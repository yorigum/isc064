<%@ Page language="c#" Inherits="ISC064.SETTINGS.Peta" CodeFile="Peta.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Peta Floor Plan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Setup Peta Floor Plan">
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1>Peta Floor Plan</h1>
			<p class="feed">
				<asp:label id="feed" runat="server"></asp:label>
			</p>
			<asp:table id="rpt" runat="server" cssclass="blue-skin tb" cellspacing="0">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell width="300">File Floor Plan</asp:tableheadercell>
					<asp:tableheadercell></asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<script type="text/javascript">
			function call(f)
			{
				popPeta(f);
			}
			function hapus(f)
			{
				if(confirm('Hapus Floor Plan ini dan file gambarnya dari server?\nPerhatian bahwa proses ini TIDAK bisa dibalik.'))
					location.href='PetaDel.aspx?f='+f;
			}
			</script>
		</form>
	</body>
</html>

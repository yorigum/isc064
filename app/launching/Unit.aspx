<%@ Page language="c#" Inherits="ISC064.LAUNCHING.Unit" CodeFile="Unit.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Master Unit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Unit">
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1>Master Unit</h1>
			<br>
			<table style="border:1px solid #DCDCDC" cellspacing="5">
				<tr>
					<td>
						<input type="button" class="btn" value="Search" onclick="popDaftarUnit();" id="search"
							runat="server" name="search" accesskey="s">
					</td>
					<td>
						<asp:dropdownlist id="jenis" runat="server" cssclass="ddl" width="230">
							<asp:listitem value="">Jenis :</asp:listitem>
						</asp:dropdownlist>
					</td>
					<td>
						<asp:dropdownlist id="lokasi" runat="server" cssclass="ddl" width="170">
							<asp:listitem value="">Lokasi :</asp:listitem>
						</asp:dropdownlist>
					</td>
					<td>
						<asp:dropdownlist id="status" runat="server" cssclass="ddl">
							<asp:listitem>Status :</asp:listitem>
							<asp:listitem>Unit Aktif</asp:listitem>
							<asp:listitem>Unit Blokir</asp:listitem>
							<asp:listitem>Unit Hold</asp:listitem>
						</asp:dropdownlist>
					</td>
					<td>
						<asp:button id="display" runat="server" cssclass="btn" text="Display" onclick="display_Click"></asp:button>
					</td>
				</tr>
			</table>
			<br>
			Status : A = Aktif / B = Blokir.
			<br>
			Luas dalam meter persegi. Price list dalam rupiah per meter persegi per bulan.
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow horizontalalign="Left" verticalalign="Bottom">
					<asp:tableheadercell width="75">No. Stock</asp:tableheadercell>
					<asp:tableheadercell>Status</asp:tableheadercell>
					<asp:tableheadercell width="100">Unit</asp:tableheadercell>
					<asp:tableheadercell width="200">Keterangan</asp:tableheadercell>
					<%--<asp:tableheadercell width="100" horizontalalign="Right">Luas</asp:tableheadercell>--%>
					<asp:tableheadercell width="100" horizontalalign="Right">Luas Tanah</asp:tableheadercell>
					<asp:tableheadercell width="100" horizontalalign="Right">Luas Bangunan</asp:tableheadercell>
					<asp:tableheadercell width="100" horizontalalign="Right">Price List</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<script language="javascript">
			function call(nomor)
			{
				popUnit(nomor);
			}
			</script>
		</form>
	</body>
</html>

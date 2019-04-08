<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PemutihanDenda2.aspx.cs" Inherits="ISC064.COLLECTION.PemutihanDenda2" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Pemutihan Denda 2</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Pemutihan Denda">
	</HEAD>
	<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}"
		ms_positioning="GridLayout">
		<script language="javascript" src="/Js/Pop.js"></script>
		<script language="javascript" src="/Js/NumberFormat.js"></script>
		<form class="cnt" id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head><input style="DISPLAY: none">
			<div id="frm" runat="server">
				<h1 class="title title-line">Pemutihan Denda</h1>
				<p>Halaman 2 dari 2</p>
				<br>
				<table cellspacing="5">
					<tr>
						<td>No. Kontrak</td>
						<td>:</td>
						<td><asp:label id="nokontrakl" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Unit</td>
						<td>:</td>
						<td><asp:label id="unit" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Customer</td>
						<td>:</td>
						<td><asp:label id="customer" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Sales</td>
						<td>:</td>
						<td><asp:label id="agent" runat="server" font-bold="True"></asp:label></td>
					</tr>
				</table>
				<br>
				<table cellspacing="1" class="tb blue-skin">
					<tr align="left">
						<th>
							No.</th>
						<th>
							Nama Tagihan</th>
						<th>
							Tipe</th>
						<th>
							Jatuh Tempo</th>
						<th>
							Nilai</th>
						<th>
							Denda</th>
						<th>
							Sisa</th>
						<th>
							Action</th>
					</tr>
					<asp:placeholder id="list" runat="server"></asp:placeholder>
				</table>				
				
			</div>
			<asp:label id="warning" runat="server" cssclass="err" font-bold="True" font-size="12pt"></asp:label>
			<script language="javascript" type="text/javascript">
			    function call(nokontrak) {
			        document.getElementById('nokontrak').value = nokontrak;
			        document.getElementById('next').click();
			    }
			</script>
		</form>
	</body>
</HTML>

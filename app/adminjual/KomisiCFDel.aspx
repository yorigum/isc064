<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KomisiCFDel.aspx.cs" Inherits="ISC064.ADMINJUAL.KomisiCFDel" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Delete Komisi Closing Fee</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Komisi - Delete Komisi Overriding">
	</head>
	<body onkeyup="if(event.keyCode==27) history.back(-1)">
		<form id="Form1" method="post" runat="server">
			<div id="frm" runat="server">
				<input type="text" style="display:none">
				<h1>Delete Komisi Closing Fee</h1>
				<br/>
				Keterangan :
				<asp:textbox id="ket" runat="server" cssclass="txt" width="400"></asp:textbox>
				<asp:button id="delbtn" runat="server" cssclass="btn btn-red" text="Delete"></asp:button>
			</div>
			<asp:label id="nodel" runat="server" visible="false">
				<h1>
					Sales Tidak Dapat Dihapus
				</h1>
				<br>
				<div class="plike">
					<h2>Kemungkinan Terjadi Karena:</h2>
					<ul>
						<li>
							Sales tersebut sudah pernah melakukan reservasi
						</li>
						<li>
							Sales tersebut sudah pernah melakukan penjualan
						</li>
					</ul>
				</div>
			</asp:label>
		</form>
	</body>
</html>

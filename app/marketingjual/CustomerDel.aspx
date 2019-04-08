<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.CustomerDel" CodeFile="CustomerDel.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Delete Customer</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Customer - Delete Customer">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27) history.back(-1)">
		<form id="Form1" method="post" runat="server">
			<div id="frm" runat="server">
				<input type="text" style="display:none">
				<h1 class="title title-line">Delete Customer</h1>
				<br>
				Keterangan :
				<asp:textbox id="ket" runat="server" cssclass="txt" width="400"></asp:textbox>
				<asp:button id="delbtn" runat="server" cssclass="btn btn-red" text="Delete" onclick="delbtn_Click"></asp:button>
			</div>
            <br />
            <asp:label id="warning" runat="server" cssclass="err" font-bold="True" font-size="12pt"></asp:label>
			<asp:label id="nodel" runat="server" visible="false">
				<h1>
					Customer Tidak Dapat Dihapus
				</h1>
				<br>
				<div class="plike">
					<h2>Kemungkinan Terjadi Karena:</h2>
					<ul>
						<li>
							Customer tersebut sudah pernah melakukan reservasi
						</li>
						<li>
							Customer tersebut sudah pernah melakukan kontrak
						</li>
					</ul>
				</div>
			</asp:label>
		</form>
	</body>
</html>

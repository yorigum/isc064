<%@ Page language="c#" Inherits="ISC064.COLLECTION.TunggakanSettle" CodeFile="TunggakanSettle.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Settlement Surat Peringatan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Surat Peringatan - Settlement Surat Peringatan">
	</head>
	<body onkeyup="if(event.keyCode==27) history.back(-1)">
		<form id="Form1" method="post" runat="server">
			<asp:label id="nodel" runat="server" visible="false">
				<h1>
					Proses Settlement Tidak Dapat Dilanjutkan
				</h1>
				<br>
				<div class="plike">
					<h2>Kemungkinan Terjadi Karena:</h2>
					<ul>
						<li>
							Surat Peringatan sudah di-UPGRADE secara otomatis oleh server</li>
						<li>
							Surat Peringatan sudah pernah menjalani prosedur SETTLEMENT sebelumnya</li>
					</ul>
				</div>
			</asp:label>
		</form>
	</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.TTSVoid" CodeFile="TTSVoid.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Void Tanda Terima Sementara</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tanda Terima Sementara - Void TTS">
</head>
<body onkeyup="if(event.keyCode==27) history.back(-1)">
    <form id="Form1" method="post" runat="server">
        <asp:Label ID="nodel" runat="server" Visible="false">
				<h1>
					Proses Void Tidak Dapat Dilanjutkan
				</h1>
				<br>
				<div class="plike">
					<h2>Kemungkinan Terjadi Karena:</h2>
					<ul>
						<li>
							Tanda Terima Sementara sudah di-POSTING</li>
						<li>
							Tanda Terima Sementara sudah pernah menjalani prosedur VOID sebelumnya</li>
					</ul>
				</div>
        </asp:Label>
    </form>
</body>
</html>

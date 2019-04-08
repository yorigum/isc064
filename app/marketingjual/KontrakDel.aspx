<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakDel" CodeFile="KontrakDel.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Delete Kontrak</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Delete Kontrak">
        <script src="/Js/Jquery.min.js"></script>
        <script src="/Js/jquery.signalR-2.2.3.min.js"></script>
        <script src="signalr/hubs" type="text/javascript"></script>
        <script src="/Js/iwc-all.min.js"></script>
        <script src="/Js/signalr-patch.js"></script>
        <script src="/Js/iwc-signalr.js"></script>
        <script type="text/javascript">

            var echoHub = SJ.iwc.SignalR.getHubProxy('unitHub', {
                client: {
                    broadcastStatus: function (NoStock) {
                    }
                }
            });

            SJ.iwc.SignalR.start().done(function () {
                echoHub.server.invokeStatus('<%=Request.QueryString["NoStock"]%>').done(function () {
                    console.log('sent');
                }).fail(function (jqXHR, textStatus) {
                    console.log('failed ' + textStatus);
                });
            });

        </script>
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27) history.back(-1)">
		<form id="Form1" method="post" runat="server">
			<div id="frm" runat="server">
				<input type="text" style="DISPLAY:none">
				<h1 class="title title-line">Delete Kontrak</h1>
				<br>
				Keterangan :
				<asp:textbox id="ket" runat="server" cssclass="txt" width="400"></asp:textbox>
				<asp:button id="delbtn" runat="server" cssclass="btn btn-red" text="Delete" onclick="delbtn_Click"></asp:button>
			</div>
			<br>
			<asp:label id="warning" runat="server" cssclass="err" font-bold="True" font-size="12pt"></asp:label>
			<asp:label id="nodel" runat="server" visible="false">
				<h1>
					Kontrak Tidak Dapat Dihapus
				</h1>
				<br>
				<div class="plike">
					<h2>Kemungkinan Terjadi Karena:</h2>
					<ul>
					</ul>
				</div>
			</asp:label>
		</form>
	</body>
</html>

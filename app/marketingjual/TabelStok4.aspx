<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.TabelStok4" CodeFile="TabelStok4.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Pendaftaran Closing Langsung</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Closing Langsung - Pendaftaran Closing Langsung (Hal. 3)">
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Closing Langsung</h1>
			<p>Halaman 3 dari 3</p>
			<br/>
			<br/>
			<h2 style="color:Brown;border:1 solid silver;padding:10">
				Closing Langsung Selesai
			</h2>
			<br/>
			<table cellspacing="5">
				<tr>
					<td>No. Kontrak</td>
					<td>:</td>
					<td>
						<asp:label id="nokontrakl" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Unit</td>
					<td>:</td>
					<td>
						<asp:label id="unit" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Customer</td>
					<td>:</td>
					<td>
						<asp:label id="customer" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
				<tr>
					<td>Sales</td>
					<td>:</td>
					<td>
						<asp:label id="agent" runat="server" font-bold="True"></asp:label>
					</td>
				</tr>
			</table>
			<br>
			<h1><a id="asp" runat="server">Print SP</a></h1>
			<br>
			<h1 style="display:none"><a id="atts" runat="server">Print TTS</a></h1>
			<br>
			<%--<p style="padding:5px"><b>Prosedur 
					Lanjutan :</b></p>
			<ul>
			    <li>
					<a style="FONT-WEIGHT:bold; FONT-SIZE:10pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal" id="aDiskon" runat="server">Diskon</a>
					<br/>
					    Memberikan diskon tambahan diluar diskon skema cara bayar.
					<br/>
					<br/>
				</li>
                <div id="lanjutan" runat="server">
			    <li>
					<a style="FONT-WEIGHT:bold; FONT-SIZE:10pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal" id="aReset" runat="server">Reset Tagihan</a>
					<br/>
                        Ganti jadwal tagihan secara keseluruhan.
					<br/>
					<br/>
				</li>
			    <li>
					<a style="FONT-WEIGHT:bold; FONT-SIZE:10pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal" id="aCustom" runat="server">Customize Tagihan</a>
					<br/>
                        Membuat jadwal tagihan diluar skema cara bayar yang berlaku. Prosedur tidak 
    					tersedia untuk surat pesanan yang sudah memiliki jadwal tagihan.
					<br/>
					<br/>
				</li>
                </div>

				<%--<li>
					<a style="FONT-WEIGHT:bold; FONT-SIZE:10pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal"
						id="aTagihan" runat="server">Customize Tagihan</a>
					<br>
					Membuat jadwal tagihan diluar skema cara bayar yang berlaku. Prosedur tidak 
					tersedia untuk surat pesanan yang sudah memiliki jadwal tagihan.
				</li>--%>
        </ul>--%>
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
                echoHub.server.invokeStatus('<%=ISC064.Db.SingleString("SELECT ISNULL(NoStock, '') FROM MS_KONTRAK WHERE NoKontrak = '"+Request.QueryString["NoKontrak"]+"' AND Status = 'A'")%>').done(function () {
                    console.log('sent');
                }).fail(function (jqXHR, textStatus) {
                    console.log('failed ' + textStatus);
                });
            });

        </script>
    </form>
</body>
</html>

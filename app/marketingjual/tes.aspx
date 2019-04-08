<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tes.aspx.cs" Inherits="ISC064.MARKETINGJUAL.tes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input NoKontrak Manual</title>
    <script src="/Js/Jquery.min.js"></script>
    <script src="/Js/jquery.signalR-2.2.3.min.js"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
    <script src="/Js/iwc-all.min.js"></script>
    <script src="/Js/signalr-patch.js"></script>
    <script src="/Js/iwc-signalr.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="username" runat="server">W</asp:TextBox>
        </div>
        <script type="text/javascript">

            //stat = 'B';
            //stock = '0000184';

            //var echoHub = SJ.iwc.SignalR.getHubProxy('unitHub', {
            //    client: {
            //        broadcastStatus: function (NoStock) {
            //        }
            //    }
            //});

            //SJ.iwc.SignalR.start().done(function () {
            //    echoHub.server.invokeStatus(stock).done(function () {
            //        console.log('sent');
            //    }).fail(function (jqXHR, textStatus) {
            //        console.log('failed ' + textStatus);
            //    });

            //    console.log('akhir');
            //});
        </script>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Testing.aspx.cs" Inherits="Testing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
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
                echoHub.server.invokeStatus('0002136').done(function () {
                    console.log('sent');
                }).fail(function (jqXHR, textStatus) {
                    console.log('failed ' + jqXHR);
                });

                console.log('akhir');
            });
        </script>
    </form>
</body>
</html>

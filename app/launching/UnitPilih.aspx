<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnitPilih.aspx.cs" Inherits="ISC064.LAUNCHING.UnitPilih" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <script src="/Js/jquery.signalR-2.2.3.min.js"></script>
        <script src="signalr/hubs" type="text/javascript"></script>
        <script type="text/javascript">
            ////ini untuk booking saat closing
            $(function () {
                var userid = "<%=ISC064.Act.UserID %>";
                var nostock = $("#nostock2").val();
                var test = $.connection.closingHub;
                $.connection.hub.qs = "UserID=" + userid + "&NoStock=" + nostock;

                test.client.broadcastMsg = function (user, nostock) {
                    console.log(user);
                    console.log(nostock);
                };

                $.connection.hub.start().done(function () {
                    test.server.hello(userid, nostock);
                });
            });
        </script>
    </form>
</body>
</html>

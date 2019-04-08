<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="ISC064.SETTINGS.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Js/jQuery.min.js"></script>
    <script src="/Js/spectrum.js"></script>
    <link rel="stylesheet" type="text/css" href="/Media/spectrum.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type='text' id="custom" />
        </div>
        <script>
            $("#custom").spectrum({
                color: "#f00"
            });
        </script>
    </form>
</body>
</html>

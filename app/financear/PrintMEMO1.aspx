<%@ Reference Control="~/PrintMEMOTemplate.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.PrintMEMO" CodeFile="PrintMEMO1.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Print Memorial Pelunasan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Print.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="3">
    <meta name="sec" content="Print Memorial Pelunasan">
    <style type="text/css">
        div {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        td {
            font-size: 8pt;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
    </style>
</head>
<body onkeyup="if(event.keyCode==27&&document.getElementById('cancel')){document.getElementById('cancel').click()}else if(event.keyCode==27){document.getElementById('cancel2').click()}">
    <script language="JavaScript" src="/Js/MD5.js"></script>
    <form id="Form1" method="post" runat="server">
        <div id="p">
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintTTS1.aspx.cs" Inherits="ISC064.NUP.PrintTTS1" %>

<%@ Reference Control="~/PrintTTSTemplate2.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Print Surat Pesanan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Print.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="3">
    <meta name="sec" content="Print Surat Pesanan">
    <style type="text/css">
        #print TD {
            FONT: 6.5pt arial;
        }

        #print P {
            FONT: 6.5pt arial;
        }

        #print LI {
            FONT: 6.5pt arial;
        }

        #print DIV {
            FONT: 6.5pt arial;
        }

        #print H3 {
            FONT: bold 10pt arial;
            PADDING-TOP: 20px;
        }

        #print H2 {
            BORDER-RIGHT: 0px;
            BORDER-TOP: 0px;
            FONT: bold 14pt arial;
            BORDER-LEFT: 0px;
            BORDER-BOTTOM: 0px;
        }

        #print TH {
            FONT: bold 10pt arial;
        }

        #print hitam {
            background-color: black;
            color: white;
            width: 100%;
        }
    </style>
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;document.getElementById('cancel')){document.getElementById('cancel').click()}else if(event.keyCode==27){document.getElementById('cancel2').click()}">
    <script type="text/JavaScript" src="/Js/MD5.js"></script>
    <form id="Form1" method="post" runat="server">
        <div id="print">
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>

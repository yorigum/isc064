<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.UnitPetaList" CodeFile="UnitPetaList.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Peta Site Plan List</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <script src="/Js/JQuery.js" type="text/javascript"></script>
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Peta Floor Plan List">
    <meta http-equiv="pragma" content="no-cache">
    <style>
        .img {
            border: solid 1px #bbbbbb;
        }

        .dropdown {
            position: relative;
            display: inline-block;
            z-index: 2;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #f9f9f9;
            min-width: 180px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            /*padding: 12px 16px;*/
            z-index: 1;
        }

        ul {
            list-style: none;
            margin: 0px;
            padding: 0px;
        }

        li {
            margin: 0px;
            padding-left: 0px;
            padding: 10px;
        }

            li:hover {
                background-color: rgb(56, 196, 242,0.5);
            }

        .dropdown:hover .dropdown-content {
            display: block;
        }
    </style>
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27) history.back(-1)">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div class="dropdown">
            <span style="font-size:30pt;font-weight:bold;">List Site Plan</span>

            <div class="dropdown-content">
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <br style="clear: both;" />
        <div id="map" style="width:100%">
        </div>

        <script>
            //manggil Halaman
            $('[url]').click(function () {
                var url = $(this).attr('url');
                setInterval(AuthReload(url), 10000); //1000 = 1 Sec
            });

            function AuthReload(url) {
                $.ajax({
                    url: url,
                    type: "GET",
                    success: function (result) {
                        $('#map').html(result);
                    }
                });
            }
        </script>
    </form>
</body>
</html>

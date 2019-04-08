<%@ Page Language="c#" Inherits="ISC064.NUP.TabelStokViewB" CodeFile="TabelStokViewB.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>TABLE STOCK</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">    
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <head>
        <title>Tower</title>
        <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
        <meta name="CODE_LANGUAGE" content="C#">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">        
        <link href="/Media/Style.css" type="text/css" rel="stylesheet">
        <style type="text/css">
            A {
                COLOR: black;
            }

                A:visited {
                    COLOR: black;
                }

                A:hover {
                    COLOR: black;
                }

            #tb {
                BORDER-COLLAPSE: collapse;
                COLOR: white;
            }

            #tb_kios {
                BORDER-COLLAPSE: collapse;
            }

            #tb_food {
                BORDER-COLLAPSE: collapse;
            }

            #tb TD {
                FONT: 8pt lucida;
                WIDTH: 23px;
                TEXT-ALIGN: center;
            }

            #tb_kios TD {
                FONT: 8pt lucida;
                WIDTH: 23px;
                TEXT-ALIGN: center;
            }

            #tb_food TD {
                FONT: 8pt lucida;
                WIDTH: 23px;
                TEXT-ALIGN: center;
            }

            #tb .h {
                COLOR: white;
                BACKGROUND-COLOR: gray;
            }

            #tb_kios .h {
                COLOR: white;
                BACKGROUND-COLOR: gray;
            }

            #tb .lt {
                COLOR: black;
            }

            #tb .ket {
                COLOR: black;
                font-size: 12pt;
                font-weight: bold;
                width: auto;
            }

            #tb .ket2 {
                COLOR: black;
                font-size: 11pt;
            }
        </style>
    </head>
    <body class="body-padding">
        <form id="Form1" method="post" runat="server">
            <div align="center">
                <img src="/Media/logo.jpg" width="250px" height="50px" />
            </div>
            <div align="center">
                <span class="title" id="spanlokasi" runat="server"></span>
                <br />
                <asp:Literal runat="server" ID="legend" Visible="true" />
                <br />
                <h3 style="font-size: 10pt; margin: 5px"></h3>
                <asp:table id="tb" runat="server" gridlines="Both" cellpadding="1"></asp:table>
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
                            var href = "";
                            var warna = "";
                            $.ajax({
                                type: "POST",
                                async: false,
                                url: "/WebS.asmx/WarnaUnit",
                                data: "{ NoStock: '" + NoStock + "'}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                            }).done(function (data) {
                                warna = data.d;
                            }).fail(function (data) {
                                console.log(data);
                            });
                            $.ajax({
                                type: "POST",
                                async: false,
                                url: "/WebS.asmx/HrefUnit",
                                data: "{ NoStock: '" + NoStock + "'}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                            }).done(function (data) {
                                href = data.d;
                                console.log(data.d);
                            }).fail(function (data) {
                                console.log(data);
                            });

                            updateUnit(NoStock, warna, href);
                        }
                    }
                });

                SJ.iwc.SignalR.start().done(function () {
                    console.log('mulai');

                    echoHub.server.refreshStatusUnit();
                }).fail(function (data) {
                    console.log(data);
                });

                function updateUnit(Nostock, Warna, Href) {
                    console.log(Nostock);
                    console.log(Warna);
                    var unit = $("td#" + Nostock);
                    $("td#" + Nostock).css("background-color", Warna);
                    $("td#" + Nostock).find("a").attr("href", Href);
                }
            </script>
        </form>
    </body>
</html>

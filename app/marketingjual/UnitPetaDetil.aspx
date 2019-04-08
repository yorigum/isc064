<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.UnitPetaDetil" CodeFile="UnitPetaDetil.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Peta Floor Plan</title>
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Peta Floor Plan Detil">
    <meta http-equiv="pragma" content="no-cache">
</head>
<body onkeyup="if(event.keyCode==27) history.back(-1)">
    <form id="Form1" method="post" runat="server" style="width: 100%;">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div style="z-index: 1; left: 50px; width: 100%; position: absolute; top: 5px;">
            <asp:Literal runat="server" ID="legend" Visible="true" />
        </div>
        <%--<div style="top: 50px; left: 50px;">
        </div>--%>
        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        <div id="tooltip" style="z-index: 5">
        </div>
    </form>
    <script src="/Js/Jquery.min.js"></script>
    <script src="/Js/JQueryUi.js"></script>
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
        });

        function updateUnit(Nostock, Warna, Href) {
            console.log(Nostock);
            console.log(Warna);
            var unit = $("td#" + Nostock);
            var Color = hexToRgbA(Warna);
            $("[data-stock=" + Nostock + "]").attr('style', "fill: " + Color + ";stroke:purple;stroke-width:0;")//.css("background-color", Warna);
            $("[data-stock=" + Nostock + "]").find("a").attr("href", Href);
        }
        function hexToRgbA(hex) {
            var c;
            if (/^#([A-Fa-f0-9]{3}){1,2}$/.test(hex)) {
                c = hex.substring(1).split('');
                if (c.length == 3) {
                    c = [c[0], c[0], c[1], c[1], c[2], c[2]];
                }
                c = '0x' + c.join('');
                return 'rgba(' + [(c >> 16) & 255, (c >> 8) & 255, c & 255].join(',') + ',0.5)';
            }
            throw new Error('Bad Hex');
        }

        //hexToRgbA('#fbafff')

        // Tooltip
        $(document).click(function () {
            $('#tooltip').hide();
        });
        $('[tooltip-url]').mouseout(function () {
            $('#tooltip').hide();
        });
        $('[tooltip-url]').mouseenter(function () {
            var top = $(this).offset().top;
            var left = $(this).offset().left;
            var oleft = $(window).scrollLeft();

            if (left > (498 + oleft)) {
                $('#tooltip').css({
                    top: (top + $(this).height() + -130),
                    left: (left + $(this).height() + -225),
                    position: 'absolute'
                });
            }
            else {
                $('#tooltip').css({
                    top: (top + $(this).height() + -130),
                    left: (left + $(this).height() + -125),
                    position: 'absolute'
                });
            }

            var url = $(this).attr('tooltip-url');

            $.ajax({
                url: url,
                type: 'GET',
                success: function (result) {
                    tooltip = result;
                    $('#tooltip').html(result);
                }
            });
            $('#tooltip').show();
        });

    </script>
</body>
</html>
